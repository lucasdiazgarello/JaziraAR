using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColocarPieza : MonoBehaviour
{
    public GameObject prefabCamino;
    public GameObject prefabCasa;

    // Declaración de una máscara de capa.
    public LayerMask myLayerMask;
    private ARCursor arCursor;
    private bool _isTouching;

    // Agrega esta propiedad para almacenar el identificador de la parcela
    public string identificadorParcela;

    // Referencia al script ComprarPieza
    public ComprarPieza comprarPieza;

    // Variable booleana para indicar si tiene una casa colocada
    public bool tieneCasa;

    void Start()
    {
        arCursor = GetComponentInParent<ARCursor>();
        enabled = false; // Desactivar la colocación de piezas al inicio
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

            if (results.Count > 0)  // Si hay algún resultado, el toque está sobre un elemento de la interfaz de usuario
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

                    // Llamar al método en ComprarPieza para incrementar los recursos
                    comprarPieza.IncrementarRecursos(1); // Elige el número de dado adecuado

                    // Verificar si hay una casa colocada en esta parcela
                    VerificarCasaEnEsquina(hit.collider);
                }
                else if (hit.collider.gameObject.CompareTag("Esquina"))
                {
                    // El objeto golpeado es una esquina.
                    Instantiate(prefabCasa, hit.collider.gameObject.transform.position, Quaternion.identity);

                    // Obtener el identificador de la parcela
                    identificadorParcela = hit.collider.gameObject.GetComponent<ColocarPieza>().identificadorParcela;

                    // Llamar al método en ComprarPieza para incrementar los recursos
                    comprarPieza.IncrementarRecursos(1); // Elige el número de dado adecuado

                    // Verificar si hay una casa colocada en esta parcela
                    VerificarCasaEnEsquina(hit.collider);
                }
            }
        }
        else if (Input.touchCount == 0)
        {
            _isTouching = false;
        }
    }

    // Método para verificar si hay una casa colocada en una esquina
    private void VerificarCasaEnEsquina(Collider collider)
    {
        // Verificar si el objeto tiene el componente ColocarPieza
        ColocarPieza colocarPieza = collider.gameObject.GetComponent<ColocarPieza>();
        if (colocarPieza != null)
        {
            // Obtener el valor de tieneCasa del objeto en la esquina
            tieneCasa = colocarPieza.tieneCasa;

            // Aquí puedes implementar la lógica para verificar si hay una casa colocada en la esquina
            // Utiliza la variable tieneCasa para tomar las acciones correspondientes.
        }
    }
}
