using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.Netcode;

public class ARCursor : NetworkBehaviour
{
    public GameObject objectToPlace; // Este es el tablero
    public ARRaycastManager raycastManager;
    public Button placeButton;
    public Button confirmButton;
    public List<GameObject> recursos; // Lista de objetos GameObject que contienen las im�genes a mostrar

    private GameObject currentObject; // Guarda una referencia al objeto colocado actualmente
    private bool isPlacementModeActive = false; // Para rastrear si el modo de colocaci�n est� activo o no
    private bool isBoardPlaced = false; // Para rastrear si el tablero ya ha sido colocado o no

    public GameObject dadoToPlace; // Prefab del dado
    public float dadoDistance = 0.05f; // Distancia de desplazamiento del dado (en metros)
    private GameObject currentDado; // Dado actualmente en proceso de colocaci�n
    public Button tirarDadoButton;

    private Vector3 initialDadoPosition; // Para guardar la posici�n inicial del dado
    private GameObject tableromInstance;

    void Start()
    {
        placeButton.onClick.AddListener(ActivatePlacementMode);
        confirmButton.onClick.AddListener(ConfirmPlacement);
        confirmButton.gameObject.SetActive(false); // Desactivar el bot�n de confirmaci�n al inicio
        DisableRecursos();
        objectToPlace = Resources.Load("TableroCC 2") as GameObject;
        tirarDadoButton.onClick.AddListener(OnDiceRollButtonPressed);
    }

    void Update()
    {
        //colocar tablero
        if (isPlacementModeActive && !isBoardPlaced && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
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
                tableromInstance.GetComponent<NetworkObject>().Spawn();
                placeButton.gameObject.SetActive(false); // Desactivar el bot�n de colocaci�n despu�s de colocar el tablero
                confirmButton.gameObject.SetActive(true); // Activar el bot�n de confirmaci�n despu�s de colocar el tablero
            }
        }
    }

    public void ActivatePlacementMode()
    {
        isPlacementModeActive = true;
        if (isBoardPlaced)
        {
            // Si el tablero ya est� colocado, desactivar el modo de colocaci�n
            isPlacementModeActive = false;
        }
    }
    public void ConfirmPlacement()
    {
        // Confirmar la colocaci�n del tablero y desactivar el modo de colocaci�n
        isBoardPlaced = true;
        isPlacementModeActive = false;
        confirmButton.gameObject.SetActive(false); // Desactivar el bot�n de confirmaci�n
        EnableRecursos();
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

    private void OnDiceRollButtonPressed()
    {
        // Si el tablero no est� colocado, regresar
        if (!isBoardPlaced) return;

        // Si ya hay un dado, destruirlo
        if (currentDado != null)
        {
            Destroy(currentDado);
        }

        // Crear un nuevo dado y guardar su posici�n inicial
        currentDado = Instantiate(dadoToPlace, tableromInstance.transform.position + Vector3.up * dadoDistance, Quaternion.identity);
        initialDadoPosition = currentDado.transform.position;

        Rigidbody rb = currentDado.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    private void ResetDicePosition()
    {
        // Restablecer la posici�n del dado a la inicial
        if (currentDado != null)
        {
            currentDado.transform.position = initialDadoPosition;
        }
    }

    public void RespawnDice()
    {
        // Restablecer la posici�n del dado y reactivar su cuerpo r�gido
        ResetDicePosition();
        Rigidbody rb = currentDado.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}

