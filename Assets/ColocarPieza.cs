using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocarPieza : MonoBehaviour
{
    public GameObject prefabCamino;
    public GameObject prefabCasa;

    // Declaración de una máscara de capa.
    public LayerMask myLayerMask;
    private ARCursor arCursor;


    void Start()
    {
        arCursor = GetComponentInParent<ARCursor>();
        enabled = false; // Desactivar la colocación de piezas al inicio
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Comprobar si el toque está sobre un elemento de la interfaz de usuario
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Debug.Log("El toque está sobre un elemento de la interfaz de usuario");
                return; // No colocar la pieza si el toque está sobre un elemento de la interfaz de usuario
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
            {
                Debug.Log("Raycast ha golpeado algo.");

                if (hit.collider.gameObject.CompareTag("Arista"))
                {
                    Debug.Log("El objeto golpeado es una arista.");
                    Instantiate(prefabCamino, hit.point + new Vector3(0, 0.1f, 0), Quaternion.identity);
                }
                else if (hit.collider.gameObject.CompareTag("Esquina"))
                {
                    Debug.Log("El objeto golpeado es una esquina.");
                    Instantiate(prefabCasa, hit.point + new Vector3(0, 0.1f, 0), Quaternion.identity);
                }
            }
        }
    }
}
