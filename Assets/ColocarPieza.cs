using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Para manejar los botones

public class ColocarPieza : MonoBehaviour
{
    public GameObject prefabCamino;
    public GameObject prefabBase; // Cambiado Casa por Base
    private GameObject currentBase;

    // Declaración de una máscara de capa.
    public LayerMask myLayerMask;
    private ARCursor arCursor;
    private bool _isTouching;

    // Botones para las acciones de colocar camino y base
    public Button buttonCamino;
    public Button buttonBase;

    // Agrega esta propiedad para almacenar el identificador de la parcela
    //public string identificadorParcela;

    // Referencia al script ComprarPieza
    public ComprarPieza comprarPieza;

    // Variable booleana para indicar si tiene una base colocada
    private bool tieneBase; 

    public enum TipoObjeto { Ninguno, Camino, Base }
    public TipoObjeto tipoActual;

    void Start()
    {
        arCursor = GetComponentInParent<ARCursor>();
        enabled = false; // Desactivar la colocación de piezas al inicio
        tipoActual = TipoObjeto.Ninguno;

        // Agregar los listeners a los botones
        buttonCamino.onClick.AddListener(() => tipoActual = TipoObjeto.Camino);
        buttonBase.onClick.AddListener(() => tipoActual = TipoObjeto.Base);
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
                if (hit.collider.gameObject.CompareTag("Arista") && tipoActual == TipoObjeto.Camino)
                {
                    ColocarCamino(hit);
                }
                else if (hit.collider.gameObject.CompareTag("Esquina") && tipoActual == TipoObjeto.Base)
                {
                    ColocarBase(hit);
                }
            }
        }
        else if (Input.touchCount == 0)
        {
            _isTouching = false;
        }
    }

    public void ColocarCamino(RaycastHit hit)
    {
        // El objeto golpeado es una arista.
        GameObject camino = Instantiate(prefabCamino, hit.collider.gameObject.transform.position, Quaternion.identity);
        camino.transform.rotation = hit.collider.transform.rotation;

        // Obtener el identificador de la parcela
        //identificadorParcela = hit.collider.gameObject.GetComponent<ColocarPieza>().identificadorParcela;

        // Llamar al método en ComprarPieza para incrementar los recursos
        comprarPieza.IncrementarRecursos(1); // Elige el número de dado adecuado

        // Verificar si hay una base colocada en esta parcela
        VerificarBaseEnEsquina(hit.collider);

        // Resetear la opción actual a Ninguno para evitar colocaciones no deseadas
        tipoActual = TipoObjeto.Ninguno;
    }

    public void ColocarBase(RaycastHit hit)
    {
        Debug.Log("EntroColocar 2");
        // El objeto golpeado es una esquina.
        currentBase = Instantiate(prefabBase, hit.collider.gameObject.transform.position, Quaternion.identity);
        currentBase.GetComponent<NetworkObject>().Spawn();

        // Obtener el identificador de la parcela
        //identificadorParcela = hit.collider.gameObject.GetComponent<ColocarPieza>().identificadorParcela;

        // Llamar al método en ComprarPieza para incrementar los recursos
        comprarPieza.IncrementarRecursos(1); // Elige el número de dado adecuado

        // Verificar si hay una base colocada en esta parcela
        VerificarBaseEnEsquina(hit.collider);

        // Resetear la opción actual a Ninguno para evitar colocaciones no deseadas
        tipoActual = TipoObjeto.Ninguno;
    }
    public void ColocarCamino()
    {
        //Debug.Log("EntroColocar 1");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            ColocarCamino(hit);
        }
        ARCursor arCursor = FindObjectOfType<ARCursor>();
        if (arCursor != null)
        {
            arCursor.ActivatePlacementMode();
        }
    }

    public void ColocarBase()
    {
        Debug.Log("EntroColocar 1");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            ColocarBase(hit);
        }
        ARCursor arCursor = FindObjectOfType<ARCursor>();
        if (arCursor != null)
        {
            arCursor.ActivatePlacementMode();
        }
    }

    // Método para verificar si hay una base colocada en una esquina
    private void VerificarBaseEnEsquina(Collider collider)
    {
        // Verificar si el objeto tiene el componente ColocarPieza
        ColocarPieza colocarPieza = collider.gameObject.GetComponent<ColocarPieza>();
        if (colocarPieza != null)
        {
            // Obtener el valor de tieneBase del objeto en la esquina
            tieneBase = colocarPieza.tieneBase;

            // Aquí puedes implementar la lógica para verificar si hay una base colocada en la esquina
            // Utiliza la variable tieneBase para tomar las acciones correspondientes.
        }
    }
    public void ActivarColocacion(TipoObjeto tipo)
    {
        tipoActual = tipo;
    }
}


/*
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
        // Aquí puedes implementar la lógica para verificar si hay una casa colocada en la esquina
        // Puedes acceder a los componentes y propiedades necesarios del collider y del objeto en sí
        // para determinar si hay una casa presente y tomar las acciones correspondientes.
    }
}
*/
