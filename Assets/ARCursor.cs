using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ARCursor : MonoBehaviour
{
    public GameObject objectToPlace; // Este es el tablero
    public GameObject placementPointsParent; // Esto debe ser el GameObject padre que contiene todos los puntos de colocación
    public ARRaycastManager raycastManager;
    public Button placeButton;
    public Button confirmButton;

    private GameObject currentObject; // Guarda una referencia al objeto colocado actualmente

    private bool isPlacementModeActive = false; // Para rastrear si el modo de colocación está activo o no

    void Start()
    {
        placeButton.onClick.AddListener(ActivatePlacementMode);
        confirmButton.onClick.AddListener(ConfirmPlacement);
        confirmButton.gameObject.SetActive(false); // Desactivar el botón de confirmación al inicio
        placementPointsParent.SetActive(false); // Desactivar los puntos de colocación al inicio
    }

    void Update()
    {
        if (isPlacementModeActive && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
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
                if (currentObject != null)
                {
                    Destroy(currentObject);
                }

                // Luego, crear un nuevo tablero y guardarlo como currentObject
                currentObject = GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);

                placeButton.gameObject.SetActive(false); // Desactivar el botón de colocación después de colocar el tablero
                confirmButton.gameObject.SetActive(true); // Activar el botón de confirmación después de colocar el tablero
            }
        }
        else if (!isPlacementModeActive && placementPointsParent.activeSelf && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hit))
            {
                if (hit.transform.parent == placementPointsParent.transform)
                {
                    GameObject newObject = GameObject.Instantiate(objectToPlace, hit.transform.position, hit.transform.rotation);
                    // Desactivar el punto de colocación después de colocar el objeto
                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }

    public void ActivatePlacementMode()
    {
        isPlacementModeActive = true;
    }

    public void ConfirmPlacement()
    {
        isPlacementModeActive = false; // Desactivar el modo de colocación
        placeButton.gameObject.SetActive(false); // Desactivar el botón de colocación después de confirmar la colocación
        placementPointsParent.SetActive(true); // Activar los puntos de colocación
    }
}
