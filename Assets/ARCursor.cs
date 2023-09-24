using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.Services.Authentication;
using Unity.Netcode;
using System;

public class ARCursor : NetworkBehaviour
{
    public static ARCursor Instance { get; private set; }
    public ARPlaneManager planeManager;
    public GameObject objectToPlace; // Este es el tablero
    public ARRaycastManager raycastManager;
    public Button placeButton;
    public Button confirmButton;
    public List<GameObject> recursos; // Lista de objetos GameObject que contienen las imágenes a mostrar

    private GameObject currentObject; // Guarda una referencia al objeto colocado actualmente
    private bool isPlacementModeActive = false; // Para rastrear si el modo de colocación está activo o no
    public bool isBoardPlaced = false; // Para rastrear si el tablero ya ha sido colocado o no

    public GameObject dadoToPlace; // Prefab del dado
    public float dadoDistance = 0.3f; // Distancia de desplazamiento del dado (en metros)
    public GameObject currentDado; // Dado actualmente en proceso de colocación
    public GameObject currentDado2;
    public Button tirarDadoButton;
    public Button terminarTurnoButton;
    public bool dicesThrown = false;
    private bool botonPulsado = false;

    public ColocarPieza colocarPieza;
    private GameObject[] colliderPrefabs;
    //public GameObject colliderPrefab; // Agrega esto en la parte superior de tu script
    private Vector3 initialDadoPosition; // Para guardar la posición inicial del dado
    public GameObject tableromInstance;
    private int currentPlayerId;


    void Start()
    {
        Debug.Log("Empezo el start de ARCursor");
        StartCoroutine(WaitForRelay());
        if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log("SOY EL HOST de ARCUROSr");
            Debug.Log("Is Server y activo botones ");
            placeButton.onClick.AddListener(ActivatePlacementMode);
            if (placeButton == null) Debug.LogError("confirmButton HOST is null");
            confirmButton.onClick.AddListener(ConfirmPlacement);
            confirmButton.gameObject.SetActive(false); // Desactivar el botón de confirmación al inicio
            if (confirmButton == null) Debug.LogError("confirmButton HOST is null");
            //tirarDadoButton.interactable = false;
            DisableRecursos();

            objectToPlace = Resources.Load("TableroCC 2") as GameObject;
            //tirarDadoButton.onClick.AddListener(OnDiceRollButtonPressed);
            //colocarPieza = GetComponentInChildren<ColocarPieza>();
            //playerNetwork = PlayerNetwork.Instance;
            colliderPrefabs = Resources.LoadAll<GameObject>("Colliders");
        }
        else // Si es un cliente
        {
            Debug.Log("NO Is Server y desactivo botones ");
            placeButton.gameObject.SetActive(false);
            if(placeButton==null) Debug.LogError("placeButton is null");
            confirmButton.gameObject.SetActive(false);
            if (confirmButton == null) Debug.LogError("confirmButton is null");
        }
        //activo colocar pieza por si es esto qeu el cliente no puede colocar
        foreach (ColocarPieza colocarPieza in GetComponentsInChildren<ColocarPieza>())
        {
            Debug.Log("entro al foreach de colocar pieza");
            colocarPieza.enabled = true;
            Debug.Log("Termino el foreach");
        }
    }
    IEnumerator WaitForRelay()
    {
        while (!TestRelay.Instance.isRelayCreated)
        {
            yield return null;  // Wait for next frame
        }
        Debug.Log("Termine de esperar");

        // Tu código aquí...
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Esto garantiza que el objeto no se destruirá al cargar una nueva escena
        }
        else
        {
            Destroy(gameObject); // Si ya hay una instancia, destruye esta
        }
    }
    void Update()
    {
        try
        {
            // Solo el host puede colocar el tablero
            if (NetworkManager.Singleton.IsServer)
            {
                //Debug.Log("is server update");
                //colocar tablero
                if (isPlacementModeActive && !isBoardPlaced && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    // Comprobar si el toque está sobre un elemento de la interfaz de usuario
                    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    {
                        return; // No colocar el tablero si el toque está sobre un elemento de la interfaz de usuario
                    }
                    List<ARRaycastHit> hits = new List<ARRaycastHit>();
                    raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
                    if (hits.Count > 0)
                    {
                        // Primero, eliminar el tablero actual si existe
                        if (tableromInstance != null)
                        {
                            var networkObject = tableromInstance.GetComponent<NetworkObject>();
                            if (networkObject != null && networkObject.IsSpawned)
                            {
                                networkObject.Despawn();
                            }
                        }
                        //Debug.Log("Antes De tableroInstance");
                        // Luego, crear un nuevo tablero y guardarlo como currentObject
                        if (objectToPlace == null) Debug.Log("object to place is null");
                        tableromInstance = Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                        Debug.Log("Nombre de la instancia del tablero" + tableromInstance.name);
                        tableromInstance.GetComponent<NetworkObject>().Spawn();
                        foreach (GameObject colliderPrefab in colliderPrefabs)
                        {
                            //Debug.Log("el coll es " + colliderPrefab.name);
                            GameObject childInstance = Instantiate(colliderPrefab, tableromInstance.transform);
                            if (childInstance == null) Debug.Log("childInstance is null");
                            //Debug.Log("Despues De Instantiate");
                            // Ajusta su posición/rotación local si es necesario. Esto puede depender de cómo hayas configurado tus prefabs.
                            childInstance.GetComponent<NetworkObject>().Spawn();
                            //Debug.Log("Despues De Spawn");
                        }
                        Debug.Log("Se spawneo tablero y colliders");
                        placeButton.gameObject.SetActive(false); // Desactivar el botón de colocación después de colocar el tablero
                        confirmButton.gameObject.SetActive(true); // Activar el botón de confirmación después de colocar el tablero
                    }
                }
            }
            //Debug.Log("llego y el id es " + PlayerPrefs.GetInt("jugadorId"));

            if (PlayerNetwork.Instance.IsMyTurn(PlayerPrefs.GetInt("jugadorId")) && !botonPulsado)
            {
                tirarDadoButton.interactable = true;
            }


                if (PlayerNetwork.Instance.IsMyTurn(PlayerPrefs.GetInt("jugadorId")))
            {

                //Debug.Log("Es mi TURNO");
                //if (!botonPulsado)
               // {

              //  }
                terminarTurnoButton.interactable = true;
            }
            else
            {
                terminarTurnoButton.interactable = true;


                //botonPulsado = false;
            }
            PlayerNetwork.Instance.SetGano();
        }

        catch (Exception ex)
        {
            Debug.LogError("Error en Update: " + ex.Message);
            Debug.LogError(ex.StackTrace);
        }
    }

    public void ActivatePlacementMode()
    {
        isPlacementModeActive = true;
        if (isBoardPlaced)
        {
            // Si el tablero ya está colocado, desactivar el modo de colocación
            isPlacementModeActive = false;
        }
    }
    public void ConfirmPlacement()
    {
        // Confirmar la colocación del tablero y desactivar el modo de colocación
        isBoardPlaced = true;
        isPlacementModeActive = false;
        confirmButton.gameObject.SetActive(false); // Desactivar el botón de confirmación
        // Activar la colocación de las piezas en los marcadores invisibles
        /*foreach (ColocarPieza colocarPieza in GetComponentsInChildren<ColocarPieza>())
        {
            Debug.Log("entro al foreach de colocar pieza");
            colocarPieza.enabled = true;
        }*/
        EnableRecursos();
        int playerId = PlayerPrefs.GetInt("jugadorId");
        BoardManager.Instance.UpdateResources(playerId);
        // Desactivar la detección de planos al confirmar la colocación del tablero
        if (planeManager)
        {
            planeManager.enabled = false;
            // Y esto eliminará todos los planos existentes
            foreach (var plane in planeManager.trackables)
            {
                Destroy(plane.gameObject);
            }
            // Esto también funcionaría:
            // planeManager.planeDetectionMode = PlaneDetectionMode.None;
        }
        PlayerNetwork.Instance.ImprimirTodosLosJugadores();
    }

    public void EnableRecursos()
    {
        foreach (var recurso in recursos)
        {
            recurso.SetActive(true);
        }
    }

    private void DisableRecursos()
    {
        foreach (var recurso in recursos)
        {
            recurso.SetActive(false);
        }
    }
    private void OnDiceRollButtonPressed() //Boton Tirar Dados
    {

        tirarDadoButton.interactable = false;
        botonPulsado = true;
        /*
        var objetoBoton = GameObject.Find("TirarDados").GetComponent<Button>().enabled;
        Debug.Log("El boton tiene interactable en : " + objetoBoton.GetComponent<Button>().enabled);
        objetoBoton.GetComponent<Button>().enabled = false;
        Debug.Log("El boton tiene interactable en : "+objetoBoton.GetComponent<Button>().enabled);
        */

        if (NetworkManager.Singleton.IsServer)
        {
            // NO BORRAR ESTO COMENTADO POR SI SURGE DENUEVO EL TEMA DE LOS DADOS
            // Si el tablero no está colocado, regresar
            if (!isBoardPlaced) return;

            // Comprobar si dadoToPlace o tableromInstance son null antes de proceder
            if (dadoToPlace == null || tableromInstance == null) return;

            // Si el dado no existe, crearlo
            if (currentDado == null)
            {
                // Crear un nuevo dado en la posición por encima del tablero
                currentDado = Instantiate(dadoToPlace, tableromInstance.transform.position + Vector3.up * dadoDistance + Vector3.right * dadoDistance / 4, Quaternion.identity);
                currentDado.GetComponent<NetworkObject>().Spawn();
                DiceNumberTextScript.dice1 = currentDado;
                //Destroy(currentDado2, 5f);
            }
            else
            {
                // Si el dado existe, reposicionarlo para el nuevo lanzamiento
                currentDado.transform.position = tableromInstance.transform.position + Vector3.up * dadoDistance;
            }

            if (currentDado2 == null)
            {
                // Crear un segundo dado al costado del primero
                currentDado2 = Instantiate(dadoToPlace, tableromInstance.transform.position + Vector3.up * dadoDistance + Vector3.right * dadoDistance / 2, Quaternion.identity);
                currentDado2.GetComponent<NetworkObject>().Spawn();
                DiceNumberTextScript.dice2 = currentDado2;
                //Destroy(currentDado2, 5f);
            }
            else
            {
                // Si el segundo dado existe, reposicionarlo para el nuevo lanzamiento
                currentDado2.transform.position = tableromInstance.transform.position + Vector3.up * dadoDistance + Vector3.right * dadoDistance;
            }

            // Obtén el DiceScript del dado actual y lanza el dado
            DiceScript diceScript = currentDado.GetComponent<DiceScript>();
            if (diceScript != null)
            {
                diceScript.RollDice(currentDado, tableromInstance.transform.position + Vector3.up * dadoDistance);
            }

            // Obtén el DiceScript del segundo dado y lanza el dado
            DiceScript diceScript2 = currentDado2.GetComponent<DiceScript>();
            if (diceScript2 != null)
            {
                diceScript2.RollDice(currentDado2, tableromInstance.transform.position + Vector3.up * dadoDistance + Vector3.right * dadoDistance / 2);
            }
            // Ajustar dicesThrown a true luego de lanzar los dados
            dicesThrown = true;
            DiceNumberTextScript.Instance.DarResultadoRandom();
            int hostPlayerID = PlayerPrefs.GetInt("jugadorId"); // Este en este caso por ser server esta bien tomar este id asi
            Debug.Log("el id que toco TirarDados es" + hostPlayerID);
            BoardManager.Instance.ManejoParcelas(DiceNumberTextScript.Instance.randomDiceNumber);
            
            //tirarDadoButton.interactable = false;
        }
        else
        {
            Debug.Log("Tiro dados como cliente");
            //obtener id del jugador cliente qeu toco el boton 
            int idJugador = PlayerPrefs.GetInt("jugadorId");
            PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(idJugador);
            Debug.Log("Cliente va a UpdateResourceTexts con ID antes de tirar dados" + idJugador);
            PlayerNetwork.Instance.UpdateResourcesTextClientRpc(jugador);

            Debug.Log("el id que toco TirarDados es" + idJugador);
            PlayerNetwork.Instance.TirarDadosServerRpc();
        }

    }
    public int ConvertirAlfaNumericoAInt(string texto)
    {
        string soloNumeros = string.Empty;

        foreach (char c in texto)
        {
            if (Char.IsDigit(c))
            {
                soloNumeros += c;
            }
        }

        if (Int32.TryParse(soloNumeros, out int resultado))
        {
            return resultado;
        }
        else
        {
            throw new Exception("No se pudo convertir la cadena a un número entero.");
        }
    }
    private void ResetDicePosition()
    {
        // Restablecer la posición del dado a la inicial
        if (currentDado != null)
        {
            currentDado.transform.position = initialDadoPosition;
        }
    }

    public void RespawnDice()
    {
        // Restablecer la posición del dado y reactivar su cuerpo rígido
        ResetDicePosition();
        Rigidbody rb = currentDado.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
    public void BotonEndTurn()
    {
        Debug.Log("Toque boton terminar turno ");
        botonPulsado = false;
        //PlayerNetwork.Instance.EndTurn();
        //NO BORRAR, dejar comentado hasta que los turnos funcionen bien 
        int id = PlayerPrefs.GetInt("jugadorId");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        if (jugador.primerasPiezas) 
        {
            PlayerNetwork.Instance.EndTurn();
        }
        else
        {
            Debug.Log("Colocar las 4 piezas para pasar turno");
        }
    }
}

