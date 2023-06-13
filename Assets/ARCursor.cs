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
    public List<GameObject> recursos; // Lista de objetos GameObject que contienen las imágenes a mostrar

    private GameObject currentObject; // Guarda una referencia al objeto colocado actualmente
    private bool isPlacementModeActive = false; // Para rastrear si el modo de colocación está activo o no
    private bool isBoardPlaced = false; // Para rastrear si el tablero ya ha sido colocado o no

    public GameObject dadoToPlace; // Prefab del dado
    public float dadoDistance = 0.05f; // Distancia de desplazamiento del dado (en metros)
    private GameObject currentDado; // Dado actualmente en proceso de colocación
    public Button tirarDadoButton;

    private Vector3 initialDadoPosition; // Para guardar la posición inicial del dado
    private GameObject tableromInstance;

    void Start()
    {
        placeButton.onClick.AddListener(ActivatePlacementMode);
        confirmButton.onClick.AddListener(ConfirmPlacement);
        confirmButton.gameObject.SetActive(false); // Desactivar el botón de confirmación al inicio
        DisableRecursos();
        objectToPlace = Resources.Load("TableroCC 2") as GameObject;
        tirarDadoButton.onClick.AddListener(OnDiceRollButtonPressed);
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
                tableromInstance.GetComponent<NetworkObject>().Spawn();
                placeButton.gameObject.SetActive(false); // Desactivar el botón de colocación después de colocar el tablero
                confirmButton.gameObject.SetActive(true); // Activar el botón de confirmación después de colocar el tablero
            }
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
        // Si el tablero no está colocado, regresar
        if (!isBoardPlaced) return;

        // Si ya hay un dado, destruirlo
        if (currentDado != null)
        {
            Destroy(currentDado);
        }

        // Crear un nuevo dado y guardar su posición inicial
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

