using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.Netcode;

public class ARCursor : NetworkBehaviour
{
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
    public bool dicesThrown = false;

    //private ColocarPieza colocarPieza;

    private Vector3 initialDadoPosition; // Para guardar la posición inicial del dado
    private GameObject tableromInstance;


    void Start()
    {
        placeButton.onClick.AddListener(ActivatePlacementMode);
        confirmButton.onClick.AddListener(ConfirmPlacement);
        confirmButton.gameObject.SetActive(false); // Desactivar el botón de confirmación al inicio
        DisableRecursos();

        objectToPlace = Resources.Load("TableroCC 2") as GameObject;
        //tirarDadoButton.onClick.AddListener(OnDiceRollButtonPressed);
        //colocarPieza = GetComponentInChildren<ColocarPieza>();
        //playerNetwork = PlayerNetwork.Instance;
    }

    void Update()
    {
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
                // Luego, crear un nuevo tablero y guardarlo como currentObject
                tableromInstance = Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                Debug.Log("Antes De tableroInstance");
                tableromInstance.GetComponent<NetworkObject>().Spawn();
                Debug.Log("Despues De tableroInstance");
                placeButton.gameObject.SetActive(false); // Desactivar el botón de colocación después de colocar el tablero
                confirmButton.gameObject.SetActive(true); // Activar el botón de confirmación después de colocar el tablero
            }
        }
        /*if (colocarPieza != null && colocarPieza.enabled)
        {
            if (colocarPieza.tipoActual == TipoObjeto.Base)
            {
                Debug.Log("Voy a llamar a colocar base");
                colocarPieza.ColocarBase();
            }
            else if (colocarPieza.tipoActual == TipoObjeto.Camino)
            {
                colocarPieza.ColocarCamino();
            }
        }*/
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

        //Debug.Log("nombre host 1");
        //Debug.Log("nombre host 2" + playerNetwork.GetNomJugador(0));
        //Debug.Log("nombre host 3");
        /*
        if (playerNetwork != null)
        {
            Debug.Log("nombre host 2" + playerNetwork.GetNomJugador(0));
        }
        else
        {
            Debug.Log("PlayerNetwork instance is null");
        }
        */
        confirmButton.gameObject.SetActive(false); // Desactivar el botón de confirmación
        // Activar la colocación de las piezas en los marcadores invisibles
        foreach (ColocarPieza colocarPieza in GetComponentsInChildren<ColocarPieza>())
        {
            colocarPieza.enabled = true;
        }
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
        //DiceNumberTextScript.Instance.DarResultadoRandom();
        //BoardManager.Instance.ManejoParcelas(DiceNumberTextScript.Instance.randomDiceNumber);
        //tirarDadoButton.interactable = false;

        //NO BORRAR ESTO COMENTADO POR SI SURGE DENUEVO EL TEMA DE LOS DADOS
        // Si el tablero no está colocado, regresar
        if (!isBoardPlaced) return;

        // Comprobar si dadoToPlace o tableromInstance son null antes de proceder
        if (dadoToPlace == null || tableromInstance == null) return;

        // Si el dado no existe, crearlo
        if (currentDado == null)
        {
            // Crear un nuevo dado en la posición por encima del tablero
            currentDado = Instantiate(dadoToPlace, tableromInstance.transform.position + Vector3.up * dadoDistance, Quaternion.identity);
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
            currentDado2 = Instantiate(dadoToPlace, tableromInstance.transform.position + Vector3.up * dadoDistance + Vector3.right * dadoDistance, Quaternion.identity);
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
            diceScript2.RollDice(currentDado2, tableromInstance.transform.position + Vector3.up * dadoDistance + Vector3.right * dadoDistance);
        }
        // Ajustar dicesThrown a true luego de lanzar los dados
        dicesThrown = true;
        BoardManager.Instance.ManejoParcelas(DiceNumberTextScript.totalDiceNumber);
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
}

