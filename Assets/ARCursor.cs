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
    public List<GameObject> placementMarkers; // Lista de marcadores invisibles

    private GameObject currentObject; // Guarda una referencia al objeto colocado actualmente
    private GameObject currentMarker; // Marcador actualmente seleccionado

    private bool isPlacementModeActive = false; // Para rastrear si el modo de colocaci�n est� activo o no

    void Start()
    {
        placeButton.onClick.AddListener(ActivatePlacementMode);
        confirmButton.onClick.AddListener(ConfirmPlacement);
        confirmButton.gameObject.SetActive(false); // Desactivar el bot�n de confirmaci�n al inicio
    }

    void Update()
    {
        if (isPlacementModeActive && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
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
                // Buscar el marcador invisible m�s cercano al punto de toque
                float closestDistance = Mathf.Infinity;
                GameObject closestMarker = null;

                foreach (GameObject marker in placementMarkers)
                {
                    float distance = Vector3.Distance(hits[0].pose.position, marker.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestMarker = marker;
                    }
                }

                if (closestMarker != null)
                {
                    // Asignar el marcador actualmente seleccionado
                    currentMarker = closestMarker;

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
    }

    public void ActivatePlacementMode()
    {
        isPlacementModeActive = true;
    }

    public void ConfirmPlacement()
    {
        if (currentMarker != null)
        {
            MarcadorInteractivo marcadorInteractivo = currentMarker.GetComponent<MarcadorInteractivo>();

            if (marcadorInteractivo != null && marcadorInteractivo.canPlaceObject)
            {
                // Crear una instancia del objeto a colocar en la posici�n del marcador
                GameObject objetoColocado = Instantiate(marcadorInteractivo.objectToPlace, currentMarker.transform.position, currentMarker.transform.rotation);

                // Asignar el objeto colocado al marcador o hacer cualquier otra acci�n adicional necesaria

                // Desactivar el marcador seleccionado
                currentMarker.SetActive(false);
                currentMarker = null;
            }
        }

        isPlacementModeActive = false; // Desactivar el modo de colocaci�n
        confirmButton.gameObject.SetActive(false); // Desactivar el bot�n de confirmaci�n despu�s de confirmar la colocaci�n
    }

}
