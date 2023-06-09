using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlacePlatformController : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject platformToPlace; // Prefab de la plataforma
    public Button placePlatformButton; // Bot�n para colocar la plataforma
    public Button confirmPlatformButton; // Bot�n para confirmar la ubicaci�n de la plataforma
    private GameObject currentPlatform; // Plataforma actualmente en proceso de colocaci�n

    private bool isPlacementModeActive = false;

    void Start()
    {
        // Inicialmente los botones est�n inactivos
        placePlatformButton.gameObject.SetActive(false);
        confirmPlatformButton.gameObject.SetActive(false);

        // Agrega los listeners de los botones
        placePlatformButton.onClick.AddListener(ActivatePlacementMode);
        confirmPlatformButton.onClick.AddListener(ConfirmPlatformPlacement);
    }

    void Update()
    {
        // Si estamos en modo de colocaci�n y el usuario toca la pantalla
        if (isPlacementModeActive && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Evitar el toque en un elemento UI
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }

            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

            if (hits.Count > 0)
            {
                // Primero, eliminar la plataforma actual si existe
                if (currentPlatform != null)
                {
                    Destroy(currentPlatform);
                }

                // Luego, crear una nueva plataforma
                currentPlatform = Instantiate(platformToPlace, hits[0].pose.position, hits[0].pose.rotation);

                placePlatformButton.gameObject.SetActive(false);
                confirmPlatformButton.gameObject.SetActive(true);
            }
        }
    }

    public void ActivatePlacementMode()
    {
        // Activar el modo de colocaci�n
        isPlacementModeActive = true;
    }

    public void ConfirmPlatformPlacement()
    {
        // Confirmar la colocaci�n de la plataforma, desactivando el modo de colocaci�n
        isPlacementModeActive = false;

        // Desactivar el bot�n de confirmaci�n despu�s de confirmar la colocaci�n
        confirmPlatformButton.gameObject.SetActive(false);
        // Desactiva este script
        this.enabled = false;
    }
}
