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
    public GameObject prefabPueblo; // Nuevo prefab para el pueblo
    private GameObject currentBase;
    private GameObject currentPueblo;

    // Declaración de una máscara de capa.
    public LayerMask myLayerMask;
    private ARCursor arCursor;
    private bool _isTouching;

    // Botones para las acciones de colocar camino y base
    public Button buttonCamino;
    public Button buttonBase;
    public Button buttonPueblo;

    // Agrega esta propiedad para almacenar el identificador de la parcela
    //public string identificadorParcela;

    // Referencia al script ComprarPieza
    public ComprarPieza comprarPieza;

    // Variable booleana para indicar si tiene una base colocada
    private bool tieneBase;
    private bool tienePueblo;

    public enum TipoObjeto { Ninguno, Camino, Base, Pueblo }
    public TipoObjeto tipoActual;

    void Start()
    {
        arCursor = GetComponentInParent<ARCursor>();
        // para la busqueda del null reference verifico que arcursor no es null
        if (arCursor == null)
        {
            //Debug.LogError("ARCursor is null in object " + gameObject.name);
        }
        else
        {
            //Debug.Log("ARCursor is not null in object " + gameObject.name);
        }
        enabled = false; // Desactivar la colocación de piezas al inicio
        tipoActual = TipoObjeto.Ninguno;

        // Agregar los listeners a los botones
        buttonCamino.onClick.AddListener(() => tipoActual = TipoObjeto.Camino);
        buttonBase.onClick.AddListener(() => tipoActual = TipoObjeto.Base);
        buttonPueblo.onClick.AddListener(() => tipoActual = TipoObjeto.Pueblo);
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
                // Nuevo if para manejar el caso de TipoObjeto.Pueblo
                else if (hit.collider.gameObject.CompareTag("Esquina") && tipoActual == TipoObjeto.Pueblo)
                {
                    ColocarPueblo(hit);
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
        //Debug.Log("EntroColocar 2");
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
    public void ColocarPueblo(RaycastHit hit)
    {
        //Debug.Log("EntroColocar 2");
        // El objeto golpeado es una esquina.
        currentPueblo = Instantiate(prefabPueblo, hit.collider.gameObject.transform.position, Quaternion.identity);
        currentPueblo.GetComponent<NetworkObject>().Spawn();

        // Obtener el identificador de la parcela
        //identificadorParcela = hit.collider.gameObject.GetComponent<ColocarPieza>().identificadorParcela;

        // Llamar al método en ComprarPieza para incrementar los recursos
        comprarPieza.IncrementarRecursos(1); // Elige el número de dado adecuado

        // Verificar si hay una base colocada en esta parcela
        VerificarPuebloEnEsquina(hit.collider); //CHEQUEAR

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
        //Debug.Log("EntroColocar 1");
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
    public void ColocarPueblo()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            ColocarPueblo(hit);
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
    private void VerificarPuebloEnEsquina(Collider collider)
    {
        // Verificar si el objeto tiene el componente ColocarPieza
        ColocarPieza colocarPieza = collider.gameObject.GetComponent<ColocarPieza>();
        if (colocarPieza != null)
        {
            // Obtener el valor de tieneBase del objeto en la esquina
            tienePueblo = colocarPieza.tienePueblo;

            // Aquí puedes implementar la lógica para verificar si hay una base colocada en la esquina
            // Utiliza la variable tieneBase para tomar las acciones correspondientes.
        }
    }
    public void ActivarColocacion(TipoObjeto tipo)
    {
        tipoActual = tipo;
    }
}
