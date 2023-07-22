using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Para manejar los botones

public class ColocarPieza : MonoBehaviour
{
    public GameObject prefabCamino;
    public GameObject prefabBase; // Cambiado Casa por Base
    public GameObject prefabPueblo; // Nuevo prefab para el pueblo
    private GameObject currentBase;
    private GameObject currentCamino;
    private GameObject currentPueblo;
    private bool _isTouching = false;
    private bool canPlace = false;

    public ComprobarObjeto comprobarObjeto;
    // Declaración de una máscara de capa.
    public LayerMask myLayerMask;
    private ARCursor arCursor;
    //private bool _isTouching;

    // Botones para las acciones de colocar camino y base
    public Button buttonCamino;
    public Button buttonBase;
    public Button buttonPueblo;
    public Button confirmBaseButton; // Asegúrate de asignar este botón en el inspector de Unity
    public Button confirmCaminoButton;
    public Button confirmPuebloButton;
    // Agrega esta propiedad para almacenar el identificador de la parcela
    //public string identificadorParcela;

    // Referencia al script ComprarPieza
    public ComprarPieza comprarPieza;

    // Variable booleana para indicar si tiene una base colocada
    public bool tieneBase;
    public bool tienePueblo;

    //public enum TipoObjeto { Ninguno, Camino, Base, Pueblo }
    public TipoObjeto tipoActual;


    public IdentificadorParcela identificadorParcela;


    void Start()
    {
        arCursor = GetComponentInParent<ARCursor>();
        prefabBase = Resources.Load("TR Casa Azul") as GameObject;
        prefabCamino = Resources.Load("TR Camino Azul  1") as GameObject;
        prefabPueblo = Resources.Load("TR Pueblo Azul") as GameObject;
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
        //tipoActual = new TipoObjeto();
        tipoActual = TipoObjeto.Ninguno;

        // Agregar los listeners a los botones
        buttonCamino.onClick.AddListener(() => {
            tipoActual = TipoObjeto.Camino;
            canPlace = true;
        });
        buttonBase.onClick.AddListener(() => {
            tipoActual = TipoObjeto.Base;
            canPlace = true;
        });
        buttonPueblo.onClick.AddListener(() => {
            tipoActual = TipoObjeto.Pueblo;
            canPlace = true;
        });

        confirmBaseButton.onClick.AddListener(() => {
            //canPlace = true;
            ConfirmarBase();
            //canPlace = false;
        });
        confirmCaminoButton.onClick.AddListener(() => {
            //canPlace = true;
            ConfirmarCamino();
            //canPlace = false;
        });
        confirmPuebloButton.onClick.AddListener(() => {
            //canPlace = true;
            ConfirmarPueblo();
            //canPlace = false;
        });
        confirmBaseButton.gameObject.SetActive(false);
        confirmCaminoButton.gameObject.SetActive(false);
        confirmPuebloButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (canPlace && Input.touchCount == 1 && !_isTouching && Input.GetTouch(0).phase == TouchPhase.Began)
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
                    ColocarCamino();
                }
                else if (hit.collider.gameObject.CompareTag("Esquina") && tipoActual == TipoObjeto.Base)
                {
                    ColocarBase();
                    //EjecutarColocarBase(hit);
                    //canPlace = false; // Desactivar la capacidad de colocar después de que se ha colocado la base
                }
                // Nuevo if para manejar el caso de TipoObjeto.Pueblo
                else if (hit.collider.gameObject.CompareTag("Esquina") && tipoActual == TipoObjeto.Pueblo)
                {
                    ColocarPueblo();
                }
            }
        }
        else if (Input.touchCount == 0)
        {
            _isTouching = false;
        }
    }
    public void AllowPlace() // Método que permitiría colocar una base, podría ser llamado por un botón
    {
        canPlace = true;
    }
    public int DarTipo()
    {
        Debug.Log("Entre a dartipo");
        int tipo = 0; // Valor por defecto
        Debug.Log("tipo actual tiene:" + tipoActual);
        if (tipoActual == TipoObjeto.Camino)
            tipo = 1;
        else if (tipoActual == TipoObjeto.Base)
            tipo = 2;
        else if (tipoActual == TipoObjeto.Pueblo)
            tipo = 3;
        return tipo;
    }

    public void ColocarCamino(RaycastHit hit)
    {

        currentCamino = Instantiate(prefabCamino, hit.collider.gameObject.transform.position, Quaternion.identity);
        currentCamino.GetComponent<NetworkObject>().Spawn();
        tipoActual = TipoObjeto.Camino;

        // El objeto golpeado es una arista.
        //GameObject camino = Instantiate(prefabCamino, hit.collider.gameObject.transform.position, Quaternion.identity);
       // camino.transform.rotation = hit.collider.transform.rotation;

        // Obtener el identificador de la parcela
        //identificadorParcela = hit.collider.gameObject.GetComponent<ColocarPieza>().identificadorParcela;

        // Llamar al método en ComprarPieza para incrementar los recursos
        //comprarPieza.IncrementarRecursos();

        // Verificar si hay una base colocada en esta parcela
       // VerificarBaseEnEsquina(hit.collider);

        // Resetear la opción actual a Ninguno para evitar colocaciones no deseadas
        tipoActual = TipoObjeto.Ninguno;
    }

    public void EjecutarColocarBase(RaycastHit hit)
    {
        //Debug.Log("EntroColocar 2");

        // El objeto golpeado es una esquina.
        currentBase = Instantiate(prefabBase, hit.collider.gameObject.transform.position, Quaternion.identity);
        //Debug.Log("Luego de instatiate");
        currentBase.GetComponent<NetworkObject>().Spawn();

        // Obtener el componente ComprobarObjeto del objeto golpeado
        comprobarObjeto = hit.collider.gameObject.GetComponent<ComprobarObjeto>();
        //Debug.Log("el collider es : " + hit.collider.gameObject.name);
        //Debug.Log("comprobarobjeto al poner la base: " + comprobarObjeto);
        // Asegurarse de que el componente existe
        if (comprobarObjeto != null)
        {
            // Guardar una referencia a la pieza que acabamos de colocar
            //comprobarObjeto.objetoColocado = this; // esto pone ControldorColocarPieza
            //Debug.Log("EL OBJETO colocado es: " + comprobarObjeto.objetoColocado);

            // Almacenar el tipo de objeto que acabamos de colocar
            comprobarObjeto.tipoObjeto = TipoObjeto.Base; // Puedes cambiar esto al tipo de objeto que corresponda
            Debug.Log("puse el tipo de la base a: " + comprobarObjeto.tipoObjeto);
        }
        else
        {
            Debug.LogError("El objeto " + hit.collider.gameObject.name + " no tiene un script ComprobarObjeto.");
        }
        //Debug.Log("Antes de la asignación");
        //tipoActual = TipoObjeto.Base;
        //Debug.Log("Después de la asignación");
        Debug.Log("el tipo de la base colocada es " + tipoActual);
        tipoActual = TipoObjeto.Ninguno;
        Debug.Log("Termino EjecutarColocarBase");

        // Obtener el identificador de la parcela
        //identificadorParcela = hit.collider.gameObject.GetComponent<ColocarPieza>().identificadorParcela;

        // Llamar al método en ComprarPieza para incrementar los recursos
        //comprarPieza.IncrementarRecursos();

        // Verificar si hay una base colocada en esta parcela
        //VerificarBaseEnEsquina(hit.collider);

        // Resetear la opción actual a Ninguno para evitar colocaciones no deseadas

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
        //comprarPieza.IncrementarRecursos();

        // Verificar si hay una base colocada en esta parcela
        //VerificarPuebloEnEsquina(hit.collider); //CHEQUEAR

        // Resetear la opción actual a Ninguno para evitar colocaciones no deseadas
        tipoActual = TipoObjeto.Ninguno;
    }
    public void ColocarCamino()
    {
        AllowPlace();
        //Debug.Log("EntroColocar 1");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            ColocarCamino(hit);
            confirmCaminoButton.gameObject.SetActive(true);
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
        AllowPlace();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.Log("Lanzando raycast");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            //Debug.Log("Antes EjecutarColocarBase");
            EjecutarColocarBase(hit);
            //Debug.Log("Despues EjecutarColocarBase");
            confirmBaseButton.gameObject.SetActive(true); // Habilita el botón de confirmación
        }
        ARCursor arCursor = FindObjectOfType<ARCursor>();
        if (arCursor != null)
        {
            arCursor.ActivatePlacementMode();
        }
    }
    
    public void ColocarPueblo()
    {
        AllowPlace();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            ColocarPueblo(hit);
            confirmPuebloButton.gameObject.SetActive(true);
        }
        ARCursor arCursor = FindObjectOfType<ARCursor>();
        if (arCursor != null)
        {
            arCursor.ActivatePlacementMode();
        }
    }

    public void ConfirmarBase()
    {
        Debug.Log("Entro a ConfirmarBase");
        Debug.Log("canPlace es : " + canPlace);
        if (currentBase == null)
        {
            Debug.Log("currentBase es null");
        }
        // Verifica si la base actual no es nula y se puede colocar
        if (currentBase != null && canPlace)
        {
            Debug.Log("entro AL IF");
            // Aquí puedes incluir cualquier lógica adicional que necesites cuando una base es confirmada.
            // Por ejemplo, podrías actualizar el estado de la base para indicar que ha sido "confirmada".

            // Deshabilita el botón de confirmación
            confirmBaseButton.gameObject.SetActive(false);

            // Desactiva la capacidad de mover la base
            canPlace = false;

            // Puedes guardar la referencia a la base confirmada
            // confirmedBase = currentBase;

            // Borra la referencia a la base actual
            currentBase = null;
        }
        else
        {
            // Puedes mostrar algún mensaje o realizar alguna acción si la base no puede ser confirmada
            Debug.Log("La base no puede ser confirmada");
        }
    }
    public void ConfirmarCamino()
    {
        if (currentCamino != null && canPlace)
        {
            // Deshabilita el botón de confirmación
            confirmCaminoButton.gameObject.SetActive(false);
            canPlace = false;
            currentCamino = null;
        }
    }

    public void ConfirmarPueblo()
    {
        if (currentPueblo != null && canPlace)
        {
            // Deshabilita el botón de confirmación
            confirmPuebloButton.gameObject.SetActive(false);
            canPlace = false;
            currentPueblo = null;
        }
    }
   
    public void ActivarColocacion(TipoObjeto tipo)
    {
        tipoActual = tipo;
    }

}
