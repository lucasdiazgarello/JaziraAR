using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.Netcode;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ARCursor : NetworkBehaviour
{
    public GameObject objectToPlace; // Este es el tablero
    public ARRaycastManager raycastManager;
    public Button placeButton;
    public Button confirmButton;
    public List<GameObject> recursos; // Lista de objetos GameObject que contienen las im�genes a mostrar
    private GameObject currentObject; // Guarda una referencia al objeto colocado actualmente

    private bool isPlacementModeActive = false; // Para rastrear si el modo de colocaci�n est� activo o no
    private bool isPlatformPlacementModeActive = false; // Para rastrear si el modo de colocaci�n de la plataforma est� activo o no

    public GameObject platformToPlace; // Prefab de la plataforma
    public Button placePlatformButton; // Bot�n para colocar la plataforma
    public Button confirmPlatformButton; // Bot�n para confirmar la ubicaci�n de la plataforma
    private GameObject currentPlatform; // Plataforma actualmente en proceso de colocaci�n
    private bool isBoardPlaced = false; // Para rastrear si el tablero ya ha sido colocado o no

    public GameObject dadoToPlace; // Prefab del dado
    public float dadoDistance = 0.05f; // Distancia de desplazamiento del dado (en metros)
    private GameObject currentDado; // Dado actualmente en proceso de colocaci�n
    public Button tirarDadoButton;
    private Vector3 initialDadoPosition; // Para guardar la posici�n inicial del dado
    public GameObject tableromInstance;

    void Start()
    {
        placeButton.onClick.AddListener(ActivatePlacementMode);
        confirmButton.onClick.AddListener(ConfirmPlacement);
        confirmButton.gameObject.SetActive(false); // Desactivar el bot�n de confirmaci�n al inicio
        DisableRecursos();
        objectToPlace = Resources.Load("TableroCC 2") as GameObject;

        confirmPlatformButton.gameObject.SetActive(false); // Desactivar el bot�n de confirmaci�n de la plataforma al inicio
                                                           // Agrega los listeners de los botones para la plataforma
        placePlatformButton.onClick.AddListener(ActivatePlatformPlacementMode);
        confirmPlatformButton.onClick.AddListener(ConfirmPlatformPlacement);
        tirarDadoButton.onClick.AddListener(OnDiceRollButtonPressed);
    }
    void Update()
    {
        //colocar tablero
        if (isPlacementModeActive && !isBoardPlaced && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //Debug.Log("Entrando en el bloque de colocaci�n del tablero.");
            // Comprobar si el toque est� sobre un elemento de la interfaz de usuario
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return; // No colocar el tablero si el toque est� sobre un elemento de la interfaz de usuario
            }

            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

            if (hits.Count > 0)
            {
                // Primero, eliminar el tablero actual si existe
                // if (currentObject != null)
                //{
                // Destroy(currentObject);
                //}
                if (tableromInstance != null)
                 {
                     var networkObject = tableromInstance.GetComponent<NetworkObject>();
                     if (networkObject != null && networkObject.IsSpawned)
                     {
                        Debug.Log("quise deaparecer el tablero");
                        networkObject.Despawn();
                     }
                 }

                // Luego, crear un nuevo tablero y guardarlo como currentObject
                //currentObject = GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                tableromInstance = Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                tableromInstance.GetComponent<NetworkObject>().Spawn();
                placeButton.gameObject.SetActive(false); // Desactivar el bot�n de colocaci�n despu�s de colocar el tablero
                confirmButton.gameObject.SetActive(true); // Activar el bot�n de confirmaci�n despu�s de colocar el tablero
            }
        }//colocar plataforma
        else if (isPlatformPlacementModeActive && isBoardPlaced && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Comprobar si el toque est� sobre un elemento de la interfaz de usuario
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return; // No colocar la plataforma si el toque est� sobre un elemento de la interfaz de usuario
            }

            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

            if (hits.Count > 0)
            {
                // Primero, eliminar la plataforma actual si existe
                //if (currentPlatform != null)
                //{
                //    Destroy(currentPlatform);
                //}
                if (currentPlatform != null)
                {
                    var networkObject = currentPlatform.GetComponent<NetworkObject>();
                    if (networkObject != null && networkObject.IsSpawned)
                    {
                        //Debug.Log("quise deaparecer la plataforma");
                        networkObject.Despawn();
                    }
                }

                // Luego, crear una nueva plataforma y guardarlo como currentPlatform
                //currentPlatform = GameObject.Instantiate(platformToPlace, hits[0].pose.position, hits[0].pose.rotation);
                currentPlatform = Instantiate(platformToPlace, hits[0].pose.position, hits[0].pose.rotation);
                currentPlatform.GetComponent<NetworkObject>().Spawn();
                /*
                // Haz que el dado aparezca por encima de la plataforma
                Vector3 dadoOffset = new Vector3(0, dadoDistance, 0);  // Ajusta este valor seg�n sea necesario

                // Si el dado ya existe, simplemente mu�velo encima de la nueva plataforma
                if (currentDado != null)
                {
                    currentDado.transform.position = currentPlatform.transform.position + dadoOffset;
                }
                */
                /*// Solo crea el dado si a�n no existe
                else
                {
                    Vector3 dadoPosition = currentPlatform.transform.position + dadoOffset;
                    // Luego, crear un nuevo dado y guardarlo como currentDado
                    currentDado = Instantiate(dadoToPlace, dadoPosition, Quaternion.identity);
                }*/

                placePlatformButton.gameObject.SetActive(false); // Desactivar el bot�n de colocaci�n despu�s de colocar la plataforma
                confirmPlatformButton.gameObject.SetActive(true); // Activar el bot�n de confirmaci�n despu�s de colocar la plataforma
            }
        }
    }
    public void ActivatePlacementMode()
    {
        //Debug.Log("Se llam� a ActivatePlacementMode()");
        isPlacementModeActive = true;
    }

    public void ConfirmPlacement()
    {
        isPlacementModeActive = false; // Desactivar el modo de colocaci�n
        confirmButton.gameObject.SetActive(false); // Desactivar el bot�n de confirmaci�n despu�s de confirmar la colocaci�n
        // Activar la colocaci�n de las piezas en los marcadores invisibles
        foreach (ColocarPieza colocarPieza in GetComponentsInChildren<ColocarPieza>())
        {
            colocarPieza.enabled = true;
        }
        // Mostrar las im�genes en el canvas
        EnableRecursos();
        isBoardPlaced = true;
        placePlatformButton.gameObject.SetActive(true); // Activar el bot�n de colocar plataforma despu�s de confirmar la colocaci�n del tablero
    }
    public void ActivatePlatformPlacementMode()
    {
        isPlatformPlacementModeActive = true;
    }
    public void ConfirmPlatformPlacement()
    {
        if (currentPlatform != null && IsServer) // Aseg�rate de que solo el host pueda confirmar la colocaci�n
        {
            // Desactivar el modo de colocaci�n de la plataforma
            isPlatformPlacementModeActive = false;

            // Desactivar el bot�n de confirmaci�n despu�s de confirmar la colocaci�n
            confirmPlatformButton.gameObject.SetActive(false);

            // Obtener y mostrar la posici�n de la plataforma
            Vector3 platformPosition = currentPlatform.transform.position;
            Debug.Log("Posici�n de la plataforma: " + platformPosition);

            // Ahora creamos y colocamos el dado cuando se confirma la plataforma
            if (currentDado == null)
            {
                // Ajusta este valor seg�n sea necesario
                initialDadoPosition = platformPosition + new Vector3(0, dadoDistance, 0);
                Debug.Log("Posici�n inicial del dado: " + initialDadoPosition);

                // Luego, crear un nuevo dado y guardarlo como currentDado
                // Usa Spawn en lugar de Instantiate
                currentDado = Instantiate(currentDado, initialDadoPosition, Quaternion.identity);
                currentDado.GetComponent<NetworkObject>().Spawn();

            }
        }
    }
    /*public void ConfirmPlatformPlacement()
    {
        if (currentPlatform != null)
        {
            // Desactivar el modo de colocaci�n de la plataforma
            isPlatformPlacementModeActive = false;

            // Desactivar el bot�n de confirmaci�n despu�s de confirmar la colocaci�n
            confirmPlatformButton.gameObject.SetActive(false);

            // Aqu� puedes agregar cualquier otra l�gica que necesites despu�s de confirmar la colocaci�n de la plataforma
            // Ahora creamos y colocamos el dado cuando se confirma la plataforma
            if (currentDado == null)
            {
                initialDadoPosition = currentPlatform.transform.position + new Vector3(0, dadoDistance, 0);  // Ajusta este valor seg�n sea necesario
                // Luego, crear un nuevo dado y guardarlo como currentDado
                currentDado = Instantiate(dadoToPlace, initialDadoPosition, Quaternion.identity);
            }
        }
    }*/
    public void OnDiceRollButtonPressed()
    {
        Debug.Log("Entro a OnDiceRollButtonPressed.");
        // Aseg�rate de que currentPlatform y currentDado no sean null
        if (currentPlatform != null && currentDado != null)
        {
            currentDado.GetComponent<DiceScript>().RollDice(initialDadoPosition);
        }
    }

    private void EnableRecursos()
    {
        foreach (GameObject recurso in recursos)
        {
            recurso.SetActive(true);
        }
    }

    private void DisableRecursos()
    {
        foreach (GameObject recurso in recursos)
        {
            recurso.SetActive(false);
        }
    }

    [ServerRpc]
    public void SpawnTableroServerRpc(Vector3 position, Quaternion rotation)
    {
        Debug.Log("Entro ServerRpc de tablero");
        tableromInstance = Instantiate(objectToPlace, position, rotation);
        tableromInstance.GetComponent<NetworkObject>().Spawn();
    }


}

