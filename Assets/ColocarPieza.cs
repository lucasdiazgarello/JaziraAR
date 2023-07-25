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
    public static ColocarPieza Instance;

    public TipoObjeto tipoActual;

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
        confirmBaseButton.gameObject.SetActive(false);
        confirmCaminoButton.gameObject.SetActive(false);
        confirmPuebloButton.gameObject.SetActive(false);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Esto garantiza que el objeto no se destruirá al cargar una nueva escena
        }
        else
        {
            Destroy(gameObject); // Si ya hay una instancia, destruye esta
        }
    }

    void Update()
    {
        if (canPlace && Input.touchCount == 1 && !_isTouching && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("IF del Update");
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
                    //Debug.Log("Detecto toque en arista");
                    ColocarCamino();
                }
                else if (hit.collider.gameObject.CompareTag("Esquina") && tipoActual == TipoObjeto.Base)
                {
                    //Debug.Log("Detecto toque en Esquina BASE");
                    ColocarBase();
                }
                // Nuevo if para manejar el caso de TipoObjeto.Pueblo
                else if (hit.collider.gameObject.CompareTag("Esquina") && tipoActual == TipoObjeto.Pueblo)
                {
                    //Debug.Log("Detecto toque en Esquina Pueblo");
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

    public void EjecutarColocarCamino(RaycastHit hit)
    {
        //Debug.Log("Entro Colocar Camino 2");
        currentCamino = Instantiate(prefabCamino, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation); //con rotacion
        //Debug.Log("Luego de instatiate");
        currentCamino.GetComponent<NetworkObject>().Spawn();
        //Debug.Log("Luego del Spawn");
        comprobarObjeto = hit.collider.gameObject.GetComponent<ComprobarObjeto>();
        //Debug.Log("Luego del comprobarObjeto");
        if (comprobarObjeto != null)
        {
            // Almacenar el tipo de objeto que acabamos de colocar
            comprobarObjeto.tipoObjeto = TipoObjeto.Camino; // Puedes cambiar esto al tipo de objeto que corresponda
            Debug.Log("puse el tipo camino a: " + comprobarObjeto.tipoObjeto);
        }
        else
        {
            Debug.LogError("El objeto " + hit.collider.gameObject.name + " no tiene un script ComprobarObjeto.");
        }
        Debug.Log("el tipo del camino es " + tipoActual);
        tipoActual = TipoObjeto.Ninguno;
        //Debug.Log("Termino EjecutarColocarBase");
    }

    public void EjecutarColocarBase(RaycastHit hit)
    {
        Debug.Log("EntroColocar 2");
        // El objeto golpeado es una esquina.
        currentBase = Instantiate(prefabBase, hit.collider.gameObject.transform.position, Quaternion.identity);
        //Debug.Log("Luego de instatiate");
        currentBase.GetComponent<NetworkObject>().Spawn();
        // Obtener el componente ComprobarObjeto del objeto golpeado
        comprobarObjeto = hit.collider.gameObject.GetComponent<ComprobarObjeto>();
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
        Debug.Log("el tipo de la base colocada es " + tipoActual);
        tipoActual = TipoObjeto.Ninguno;
    }
    public void EjecutarColocarPueblo(RaycastHit hit)
    {
        //Debug.Log("EntroColocarPueblo 2");
        // El objeto golpeado es una esquina.
        currentPueblo = Instantiate(prefabPueblo, hit.collider.gameObject.transform.position, Quaternion.identity);
        //Debug.Log("Luego del Instantiate");
        //currentPueblo = Instantiate(prefabPueblo, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation); //con rotacion
        currentPueblo.GetComponent<NetworkObject>().Spawn();
        //Debug.Log("Luego del Spawn");
        comprobarObjeto = hit.collider.gameObject.GetComponent<ComprobarObjeto>();

        if (comprobarObjeto != null)
        {
            // Almacenar el tipo de objeto que acabamos de colocar
            comprobarObjeto.tipoObjeto = TipoObjeto.Pueblo; // Puedes cambiar esto al tipo de objeto que corresponda
            Debug.Log("puse el tipo del pueblo a: " + comprobarObjeto.tipoObjeto);
        }
        else
        {
            Debug.LogError("El objeto " + hit.collider.gameObject.name + " no tiene un script ComprobarObjeto.");
        }
        Debug.Log("el tipo del camino es " + tipoActual);
        tipoActual = TipoObjeto.Ninguno;
        //Debug.Log("Termino EjecutarColocarBase");
    }
    public void ColocarCamino()
    {
        Debug.Log("Entro Colocar Camino 1");
        AllowPlace();
        //Debug.Log("EntroColocar 1");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            Debug.Log("Antes EjecutarColocarCamino");
            EjecutarColocarCamino(hit);
            Debug.Log("Despues EjecutarColocarCamino");
            confirmCaminoButton.gameObject.SetActive(true);
        }
        /*ARCursor arCursor = FindObjectOfType<ARCursor>();
        if (arCursor != null)
        {
            arCursor.ActivatePlacementMode();
        }*/
    }
    public void ColocarBase()
    {
        Debug.Log("EntroColocar 1");
        AllowPlace();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //Debug.Log("Lanzando raycast");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            Debug.Log("Antes EjecutarColocarBase");
            EjecutarColocarBase(hit);
            Debug.Log("Despues EjecutarColocarBase");
            confirmBaseButton.gameObject.SetActive(true); // Habilita el botón de confirmación
        }
       /* ARCursor arCursor = FindObjectOfType<ARCursor>();
        if (arCursor != null)
        {
            arCursor.ActivatePlacementMode();
        }*/
    }
    public void ColocarPueblo()
    {
        AllowPlace();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            Debug.Log("Antes EjecutarColocarPueblo");
            EjecutarColocarPueblo(hit);
            Debug.Log("Despues EjecutarColocarPueblo");
            confirmPuebloButton.gameObject.SetActive(true); // Habilita el botón de confirmación
        }
        /*ARCursor arCursor = FindObjectOfType<ARCursor>();
        if (arCursor != null)
        {
            arCursor.ActivatePlacementMode();
        }*/
    }
    public void ConfirmarBase()
    {
        if (currentBase == null)
        {
            Debug.Log("currentBase es null");
        }
        // Verifica si la base actual no es nula y se puede colocar
        if (currentBase != null && canPlace)
        {
            // Deshabilita el botón de confirmación
            confirmBaseButton.gameObject.SetActive(false);
            // Desactiva la capacidad de mover la base
            canPlace = false;
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
        if (currentCamino == null)
        {
            Debug.Log("currentBase es null");
        }
        // Verifica si la base actual no es nula y se puede colocar
        if (currentCamino != null && canPlace)
        {
            confirmCaminoButton.gameObject.SetActive(false);
            canPlace = false;
            currentCamino = null;
        }
        else
        {
            Debug.Log("El camino no puede ser confirmado");
        }
    }
    public void ConfirmarPueblo()
    {
        if (currentPueblo == null)
        {
            Debug.Log("currentBase es null");
        }
        if (currentPueblo != null && canPlace)
        {
            // Deshabilita el botón de confirmación
            confirmPuebloButton.gameObject.SetActive(false);
            canPlace = false;
            currentPueblo = null;
        }
        else
        {
            Debug.Log("El camino no puede ser confirmado");
        }
    }
   
    /*public void ActivarColocacion(TipoObjeto tipo)
    {
        tipoActual = tipo;
    }
    */

}
