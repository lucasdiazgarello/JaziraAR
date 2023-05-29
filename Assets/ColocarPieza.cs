using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocarPieza : MonoBehaviour
{
    public GameObject prefabCamino;
    public GameObject prefabCasa;

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
                return; // No colocar la pieza si el toque está sobre un elemento de la interfaz de usuario
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Marcador invisible tocado: " + gameObject.name); // Mensaje de depuración
                    // Colocar camino si el marcador es una arista
                    if (gameObject.CompareTag("Arista"))
                    {
                        Instantiate(prefabCamino, hit.point, Quaternion.identity);
                    }
                    // Colocar casa si el marcador es una esquina de parcela
                    else if (gameObject.CompareTag("Esquina"))
                    {
                        Instantiate(prefabCasa, hit.point, Quaternion.identity);
                    }
                }
            }
        }
    }
}

