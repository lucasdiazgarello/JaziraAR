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

    // Agrega esta propiedad para almacenar el identificador de la parcela
    public string identificadorParcela;

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
                return;
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
            {
                if (hit.collider.gameObject.CompareTag("Arista"))
                {
                    // El objeto golpeado es una arista.
                    GameObject camino = Instantiate(prefabCamino, hit.collider.gameObject.transform.position, Quaternion.identity);
                    camino.transform.rotation = hit.collider.transform.rotation;

                    // Obtener el identificador de la parcela
                    identificadorParcela = hit.collider.gameObject.GetComponent<ColocarPieza>().identificadorParcela;

                    // Llamar al m�todo en ComprarPieza para verificar si hay una casa colocada en esta parcela
                    //FindObjectOfType<ComprarPieza>().VerificarCasaEnEsquina(hit.collider);
                }
                else if (hit.collider.gameObject.CompareTag("Esquina"))
                {
                    // El objeto golpeado es una esquina.
                    Instantiate(prefabCasa, hit.collider.gameObject.transform.position, Quaternion.identity);

                    // Obtener el identificador de la parcela
                    identificadorParcela = hit.collider.gameObject.GetComponent<ColocarPieza>().identificadorParcela;

                    // Llamar al m�todo en ComprarPieza para verificar si hay una casa colocada en esta parcela
                    //FindObjectOfType<ComprarPieza>().VerificarCasaEnEsquina(hit.collider);
                }
            }
        }
        else if (Input.touchCount == 0)
        {
            _isTouching = false;
        }
    }
}
