using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlacePlatformController : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject platformToPlace; // Prefab de la plataforma
    public Button placePlatformButton; // Botón para colocar la plataforma
    public Button confirmPlatformButton; // Botón para confirmar la ubicación de la plataforma
    private GameObject currentPlatform; // Plataforma actualmente en proceso de colocación

    private bool isPlacementModeActive = false;

    void Start()
    {
        // Inicialmente los botones están inactivos
        placePlatformButton.gameObject.SetActive(false);
        confirmPlatformButton.gameObject.SetActive(false);

        // Agrega los listeners de los botones
        placePlatformButton.onClick.AddListener(ActivatePlacementMode);
        confirmPlatformButton.onClick.AddListener(ConfirmPlatformPlacement);
    }

    void Update()
    {
        // Si estamos en modo de colocación y el usuario toca la pantalla
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
        // Activar el modo de colocación
        isPlacementModeActive = true;
    }

    public void ConfirmPlatformPlacement()
    {
        // Confirmar la colocación de la plataforma, desactivando el modo de colocación
        isPlacementModeActive = false;

        // Desactivar el botón de confirmación después de confirmar la colocación
        confirmPlatformButton.gameObject.SetActive(false);
        // Desactiva este script
        this.enabled = false;
    }
}
