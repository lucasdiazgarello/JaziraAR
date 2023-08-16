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
    private bool isBoardPlaced = false; // Para rastrear si el tablero ya ha sido colocado o no

    public GameObject dadoToPlace; // Prefab del dado
    public float dadoDistance = 0.3f; // Distancia de desplazamiento del dado (en metros)
    private GameObject currentDado; // Dado actualmente en proceso de colocación
    private GameObject currentDado2;
    public Button tirarDadoButton;
    public Button terminarTurnoButton;
    public bool dicesThrown = false;

    public ColocarPieza colocarPieza;
    private GameObject[] colliderPrefabs;
    //public GameObject colliderPrefab; // Agrega esto en la parte superior de tu script
    private Vector3 initialDadoPosition; // Para guardar la posición inicial del dado
    private GameObject tableromInstance;
    private int currentPlayerId;


    void Start()
    {
        Debug.Log("Empezo el start de ARCursor");
        StartCoroutine(WaitForRelay());
        if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log("SOY EL HOST de ARCUROSr");
            Debug.Log("Is Server y activo botones ");
            //placeButton.gameObject.SetActive(true);
            //confirmButton.gameObject.SetActive(true);
            // El jugador es el servidor. Puedes ejecutar lógica específica aquí.
            placeButton.onClick.AddListener(ActivatePlacementMode);
            confirmButton.onClick.AddListener(ConfirmPlacement);
            confirmButton.gameObject.SetActive(false); // Desactivar el botón de confirmación al inicio
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
            confirmButton.gameObject.SetActive(false);
        }
        //activo colocar pieza por si es esto qeu el cliente no puede colocar
        foreach (ColocarPieza colocarPieza in GetComponentsInChildren<ColocarPieza>())
        {
            Debug.Log("entro al foreach de colocar pieza");
            colocarPieza.enabled = true;
        }
        // Configurar los botones dependiendo de si el jugador es el host o un cliente
        //int currentPlayerId = ConvertirAlfaNumericoAInt(AuthenticationService.Instance.PlayerId);        
        //SetupButtonsBasedOnPlayerType();


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
    /*void SetupButtonsBasedOnPlayerType()
    {
        int num = PlayerPrefs.GetInt("jugadorId");
        Debug.Log("num es " + num + " y currentplayerid es " + currentPlayerId);
        if (currentPlayerId == num) // Si es el host
        {
            Debug.Log("Is Server y activo botones ");
            placeButton.gameObject.SetActive(true);
            confirmButton.gameObject.SetActive(true);
        }
        else // Si es un cliente
        {
            Debug.Log("NO Is Server y desactivo botones ");
            placeButton.gameObject.SetActive(false);
            confirmButton.gameObject.SetActive(false);
        }
    }*/
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
                        Debug.Log("Antes De tableroInstance");
                        // Luego, crear un nuevo tablero y guardarlo como currentObject
                        tableromInstance = Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                        Debug.Log("Despues De tableroInstance");
                        tableromInstance.GetComponent<NetworkObject>().Spawn();
                        foreach (GameObject colliderPrefab in colliderPrefabs)
                        {
                            Debug.Log("el coll es " + colliderPrefab.name);
                            GameObject childInstance = Instantiate(colliderPrefab, tableromInstance.transform);
                            Debug.Log("Despues De Instantiate");
                            // Ajusta su posición/rotación local si es necesario. Esto puede depender de cómo hayas configurado tus prefabs.
                            childInstance.GetComponent<NetworkObject>().Spawn();
                            Debug.Log("Despues De Spawn");
                        }
                        
                        // Instancia el collider desde el prefab y colócalo como hijo del tableromInstance
                        //GameObject childInstance = Instantiate(colliderPrefab, tableromInstance.transform);

                        // Ajusta su posición/rotación local si es necesario, basado en valores previamente guardados o establecidos
                        // Por ejemplo: childInstance.transform.localPosition = new Vector3(x, y, z);
                        // childInstance.transform.localRotation = Quaternion.Euler(rx, ry, rz);

                        //childInstance.GetComponent<NetworkObject>().Spawn();
                        /*// Buscar el collider específico por su nombre.
                        Transform childCollider = tableromInstance.transform.Find("Empty camino rot der (5)");
                        Debug.Log("Encontre el collider " + childCollider.name);
                        if (childCollider)
                        {
                            NetworkObject childNetworkObject = childCollider.GetComponent<NetworkObject>();
                            if (childNetworkObject)
                            {
                                Debug.Log("spawn " + childNetworkObject.name);
                                // Instanciar y spawnea el collider específico.
                                GameObject childInstance = Instantiate(childCollider.gameObject, tableromInstance.transform);
                                childInstance.GetComponent<NetworkObject>().Spawn();
                            }
                        }
                        else
                        {
                            Debug.Log("ColliderEspecifico no encontrado.");
                        }
                        */
                        /*// PONER ESTO PARA QUE SPAWNEE TODOS LOS COLLIDERS NO SOLO UNO
                        // Ahora, para cada hijo que sea un NetworkObject:
                        foreach (Transform child in tableromInstance.transform)
                        {
                            // Comprobar si el hijo es un NetworkObject.
                            NetworkObject childNetworkObject = child.GetComponent<NetworkObject>();
                            Debug.Log("el collider se llama " + childNetworkObject.name);
                            if (childNetworkObject)
                            {
                                // Instanciar y spawnea cada collider que sea un NetworkObject.
                                GameObject childInstance = Instantiate(child.gameObject, tableromInstance.transform);
                                Debug.Log("Instancio " + childInstance.name);
                                childInstance.GetComponent<NetworkObject>().Spawn();
                            }
                        }*/
                        Debug.Log("Despues De tableroInstance y de spawnear el collider específico");
                        placeButton.gameObject.SetActive(false); // Desactivar el botón de colocación después de colocar el tablero
                        confirmButton.gameObject.SetActive(true); // Activar el botón de confirmación después de colocar el tablero
                    }
                }
            }          
            //Debug.Log("llego y el id es " + PlayerPrefs.GetInt("jugadorId"));
            if (PlayerNetwork.Instance.IsMyTurn(PlayerPrefs.GetInt("jugadorId")))
            {
                //Debug.Log("Es mi TURNO");
                tirarDadoButton.interactable = true;
                //terminarTurnoButton.interactable = true;
            }
            else
            {
                tirarDadoButton.interactable = false;
                //terminarTurnoButton.interactable = false;
            }
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
    }

    private void EnableRecursos()
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
        Debug.Log("Diste click en el boton");
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
        BoardManager.Instance.ManejoParcelas(DiceNumberTextScript.Instance.randomDiceNumber);
        //tirarDadoButton.interactable = false;
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
        PlayerNetwork.Instance.EndTurn();

    }
}

