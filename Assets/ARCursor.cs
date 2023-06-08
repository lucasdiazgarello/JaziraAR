using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ARCursor : MonoBehaviour
{
    public GameObject objectToPlace; // Este es el tablero
    public ARRaycastManager raycastManager;
    public Button placeButton;
    public Button confirmButton;
    public List<GameObject> recursos; // Lista de objetos GameObject que contienen las im�genes a mostrar
    private GameObject currentObject; // Guarda una referencia al objeto colocado actualmente

    private bool isPlacementModeActive = false; // Para rastrear si el modo de colocaci�n est� activo o no

    public Button placePlatformButton; // Nuevo bot�n para colocar la plataforma


    void Start()
    {
        placeButton.onClick.AddListener(ActivatePlacementMode);
        confirmButton.onClick.AddListener(ConfirmPlacement);
        confirmButton.gameObject.SetActive(false); // Desactivar el bot�n de confirmaci�n al inicio
        DisableRecursos();
        objectToPlace = Resources.Load("TableroCC 2") as GameObject;
        placePlatformButton.onClick.AddListener(PlacePlatform); // A�adido el Listener para el nuevo bot�n
    }
    void Update()
    {
        if (isPlacementModeActive && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
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
                if (currentObject != null)
                {
                    Destroy(currentObject);
                }

                // Luego, crear un nuevo tablero y guardarlo como currentObject
                currentObject = GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);

                placeButton.gameObject.SetActive(false); // Desactivar el bot�n de colocaci�n despu�s de colocar el tablero
                confirmButton.gameObject.SetActive(true); // Activar el bot�n de confirmaci�n despu�s de colocar el tablero
            }
        }
    }

    public void ActivatePlacementMode()
    {
        Debug.Log("Se llam� a ActivatePlacementMode()");
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

    }
    public void PlacePlatform()
    {
        // Aqu� invocamos el script que coloca la plataforma y el dado
        // Pero antes de eso, nos aseguramos de que currentObject (el tablero) no sea null
        if (currentObject != null)
        {
            // Aqu� asumimos que tu script para colocar la plataforma se llama PlacePlatformScript
            PlacePlatformScript placePlatformScript = GetComponent<PlacePlatformScript>();
            placePlatformScript.PlacePlatformAndDado(currentObject);
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
}