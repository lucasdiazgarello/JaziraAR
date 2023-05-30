using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColocarPieza : MonoBehaviour
{
    public GameObject prefabCamino;
    public GameObject prefabCasa;

    // Declaraci�n de una m�scara de capa.
    public LayerMask myLayerMask;
    private ARCursor arCursor;
    private bool _isTouching;

    void Start()
    {
        arCursor = GetComponentInParent<ARCursor>();
        enabled = false; // Desactivar la colocaci�n de piezas al inicio
    }

    void Update()
    {
        if (Input.touchCount == 1 && !_isTouching && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _isTouching = true;
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.GetTouch(0).position;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            if (results.Count > 0)  // Si hay alg�n resultado, el toque est� sobre un elemento de la interfaz de usuario
            {
                //Debug.Log("El toque est� sobre un elemento de la interfaz de usuario"); // esto sigue sin funcionar pero por ahora no afecta
                return;
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
            {
                Debug.Log("Raycast ha golpeado algo.");

                if (hit.collider.gameObject.CompareTag("Arista"))
                {
                    Debug.Log("El objeto golpeado es una arista.");
                    Instantiate(prefabCamino, hit.collider.gameObject.transform.position, Quaternion.identity);
                }
                else if (hit.collider.gameObject.CompareTag("Esquina"))
                {
                    Debug.Log("El objeto golpeado es una esquina.");
                    Instantiate(prefabCasa, hit.collider.gameObject.transform.position, Quaternion.identity);
                }
            }
        }
        else if (Input.touchCount == 0)
        {
            _isTouching = false;
        }
    }
}
