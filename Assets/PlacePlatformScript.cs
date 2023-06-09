using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class PlacePlatformScript : MonoBehaviour
{
    public GameObject plataformaDados; // Esta es la plataforma
    public GameObject dado; // Este es el dado
    public float plataformaDistance = 0.05f; // Distancia de desplazamiento de la plataforma (en metros)
    public float dadoDistance = 0.05f; // Distancia de desplazamiento del dado (en metros)
    public ARRaycastManager raycastManager; // Necesitamos esto para hacer raycasts
    private GameObject currentPlatform; // Guarda una referencia a la plataforma instanciada actualmente
    private GameObject currentDado; // Guarda una referencia al dado instanciado actualmente
    private Coroutine platformPlacementRoutine; // Añade una variable para la coroutine

    public void PlacePlatformAndDado()
    {
        platformPlacementRoutine = StartCoroutine(PlatformPlacementRoutine());  // Guarda la referencia a la coroutine cuando la inicias
    }

    public void StopPlatformPlacement()
    {
        if (platformPlacementRoutine != null)  // Comprueba que la coroutine está activa antes de intentar detenerla
        {
            StopCoroutine(platformPlacementRoutine);
        }
    }
    /*public void PlacePlatformAndDado()
    {
        // Cambiamos el comportamiento de este método para iniciar el modo de colocación de la plataforma
        StartCoroutine(PlatformPlacementRoutine());
    }*/

    private IEnumerator PlatformPlacementRoutine()
    {
        while (true)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // Comprobar si el toque está sobre un elemento de la interfaz de usuario
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    yield return null; // No colocar la plataforma si el toque está sobre un elemento de la interfaz de usuario
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

                    // Luego, crear una nueva plataforma y guardarla como currentPlatform
                    Vector3 plataformaPosition = hits[0].pose.position;
                    currentPlatform = Instantiate(plataformaDados, plataformaPosition, Quaternion.identity);

                    // Haz que el dado aparezca por encima de la plataforma
                    Vector3 dadoOffset = new Vector3(0, dadoDistance, 0);  // Ajusta este valor según sea necesario
                    Vector3 dadoPosition = currentPlatform.transform.position + dadoOffset;

                    // Primero, eliminar el dado actual si existe
                    if (currentDado != null)
                    {
                        Destroy(currentDado);
                    }

                    // Luego, crear un nuevo dado y guardarlo como currentDado
                    currentDado = Instantiate(dado, dadoPosition, Quaternion.identity);
                }
            }

            yield return null;
        }
    }
}
