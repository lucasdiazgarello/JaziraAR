using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Para manejar los botones

public class ColocarPieza : NetworkBehaviour
{
    //public static ColocarPieza Instance { get; private set; }
    public GameObject prefabCaminoA;
    public GameObject prefabBaseA; // Cambiado Casa por Base
    public GameObject prefabPuebloA; // Nuevo prefab para el pueblo
    public GameObject prefabCaminoR;
    public GameObject prefabBaseR; // Cambiado Casa por Base
    public GameObject prefabPuebloR; // Nuevo prefab para el pueblo
    private GameObject currentPrefabBase;
    private GameObject currentPrefabCamino;
    private GameObject currentPrefabPueblo;
    private GameObject currentBase;
    private GameObject currentCamino;
    private GameObject currentPueblo;

    private bool _isTouching = false;
    private bool canPlace = false;

    public ComprobarObjeto comprobarObjeto;
    // Declaraci�n de una m�scara de capa.
    public LayerMask myLayerMask;
    //private ARCursor arCursor;
    //private bool _isTouching;

    // Botones para las acciones de colocar camino y base
    public Button buttonCamino;
    public Button buttonBase;
    public Button buttonPueblo;
    public Button confirmBaseButton; // Aseg�rate de asignar este bot�n en el inspector de Unity
    public Button confirmCaminoButton;
    public Button confirmPuebloButton;
    // Agrega esta propiedad para almacenar el identificador de la parcela
    //public string identificadorParcela;

    // Referencia al script ComprarPieza
    //public ComprarPieza comprarPieza;
    //public IdentificadorParcela identificadorParcela;

    // Variable booleana para indicar si tiene una base colocada
    //public bool tieneBase;
    //public bool tienePueblo;
    public TipoObjeto tipoActual;

    void Start()
    {
        //arCursor = GetComponentInParent<ARCursor>();
        prefabBaseA = Resources.Load("TR Casa Azul") as GameObject;
        prefabCaminoA = Resources.Load("TR Camino Azul") as GameObject;
        prefabPuebloA = Resources.Load("TR Pueblo Azul") as GameObject;
        prefabBaseR = Resources.Load("TR Casa Rojo") as GameObject;
        prefabCaminoR = Resources.Load("TR Camino Rojo") as GameObject;
        prefabPuebloR = Resources.Load("TR Pueblo Rojo") as GameObject;
        // para la busqueda del null reference verifico que arcursor no es null
        if (ARCursor.Instance == null)
        {
            //Debug.LogError("ARCursor is null in object " + gameObject.name);
        }
        else
        {
            //Debug.Log("ARCursor is not null in object " + gameObject.name);
        }
        enabled = false; // Desactivar la colocaci�n de piezas al inicio
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

        /*confirmBaseButton.onClick.AddListener(() => {
            //canPlace = true;
            ConfirmarBase();
            //canPlace = false;
        });*/
        /*confirmCaminoButton.onClick.AddListener(() => {
            //canPlace = true;
            ConfirmarCamino();
            //canPlace = false;
        });
        confirmPuebloButton.onClick.AddListener(() => {
            //canPlace = true;
            ConfirmarPueblo();
            //canPlace = false;
        });*/
        confirmBaseButton.gameObject.SetActive(false);
        confirmCaminoButton.gameObject.SetActive(false);
        confirmPuebloButton.gameObject.SetActive(false);
    }
    /*private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Esto garantiza que el objeto no se destruir� al cargar una nueva escena
        }
        else
        {
            Destroy(gameObject); // Si ya hay una instancia, destruye esta
        }
    }*/
    void Update()
    {
        if (canPlace && Input.touchCount == 1 && !_isTouching && Input.GetTouch(0).phase == TouchPhase.Began)
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
                var color =PlayerPrefs.GetString("colorJugador");

                if (hit.collider.gameObject.CompareTag("Arista") && tipoActual == TipoObjeto.Camino)
                {

                    ColocarCamino(color);
                }
                else if (hit.collider.gameObject.CompareTag("Esquina") && tipoActual == TipoObjeto.Base)
                {
                    ColocarBase(color);
                    //EjecutarColocarBase(hit);
                    //canPlace = false; // Desactivar la capacidad de colocar despu�s de que se ha colocado la base
                }
                // Nuevo if para manejar el caso de TipoObjeto.Pueblo
                else if (hit.collider.gameObject.CompareTag("Esquina") && tipoActual == TipoObjeto.Pueblo)
                {
                    ColocarPueblo(color);
                }
            }
        }
        else if (Input.touchCount == 0)
        {
            _isTouching = false;
        }
    }
    public void AllowPlace() // M�todo que permitir�a colocar una base, podr�a ser llamado por un bot�n
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

    public void EjecutarColocarCamino(RaycastHit hit,string color)
    {
        switch (color)
        {
            case "Rojo":  // Aqu� se hace uso del tipo enumerado TipoObjeto
                currentPrefabCamino = prefabCaminoR;
                break;
            case "Azul":  // Aqu� se hace uso del tipo enumerado TipoObjeto
                currentPrefabCamino = prefabCaminoA;
                break;
        }
        Debug.Log("EjecutarColocarCamino");
        // El objeto golpeado es una esquina.
        //currentCamino = Instantiate(prefabCamino, hit.collider.gameObject.transform.position, Quaternion.identity);
        currentCamino = Instantiate(currentPrefabCamino, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation); //con rotacion
        currentCamino.GetComponent<NetworkObject>().Spawn();
        comprobarObjeto = hit.collider.gameObject.GetComponent<ComprobarObjeto>();

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

    public void EjecutarColocarBase(RaycastHit hit, string color)
    {
        switch (color)
        {
            case "Rojo":  // Aqu� se hace uso del tipo enumerado TipoObjeto
                currentPrefabBase = prefabBaseR;
                break;
            case "Azul":  // Aqu� se hace uso del tipo enumerado TipoObjeto
                currentPrefabBase = prefabBaseA;
                break;
        }
        Debug.Log("EntroColocar 2");

        // El objeto golpeado es una esquina.
        currentBase = Instantiate(currentPrefabBase, hit.collider.gameObject.transform.position, Quaternion.identity);
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
        //Debug.Log("Antes de la asignaci�n");
        //tipoActual = TipoObjeto.Base;
        //Debug.Log("Despu�s de la asignaci�n");
        Debug.Log("el tipo de la base colocada es " + tipoActual);
        tipoActual = TipoObjeto.Ninguno;
    }
    public void EjecutarColocarPueblo(RaycastHit hit, string color)
    {

        switch (color)
        {
            case "Rojo":  // Aqu� se hace uso del tipo enumerado TipoObjeto
                currentPrefabPueblo = prefabPuebloR;
                break;
            case "Azul":  // Aqu� se hace uso del tipo enumerado TipoObjeto
                currentPrefabPueblo = prefabPuebloA;
                break;
        }
        //Debug.Log("EntroColocar 2");
        // El objeto golpeado es una esquina.
        currentPueblo = Instantiate(currentPrefabPueblo, hit.collider.gameObject.transform.position, Quaternion.identity);
        //currentPueblo = Instantiate(prefabPueblo, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation); //con rotacion
        currentPueblo.GetComponent<NetworkObject>().Spawn();
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
    /*public void ColocarPueblo(RaycastHit hit)
    {
        //Debug.Log("EntroColocar 2");
        // El objeto golpeado es una esquina.
        currentPueblo = Instantiate(prefabPueblo, hit.collider.gameObject.transform.position, Quaternion.identity);
        currentPueblo.GetComponent<NetworkObject>().Spawn();
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
        Debug.Log("el tipo de la base colocada es " + tipoActual);
        tipoActual = TipoObjeto.Ninguno;
        //Debug.Log("Termino EjecutarColocarBase");
    }*/
    public void ColocarCamino(string color)
    {
        AllowPlace();
        //Debug.Log("EntroColocar 1");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            EjecutarColocarCamino(hit,color);
            confirmCaminoButton.gameObject.SetActive(true);
        }
        //ARCursor arCursor = FindObjectOfType<ARCursor>();
        if (ARCursor.Instance != null)
        {
            ARCursor.Instance.ActivatePlacementMode();
        }
    }

    public void ColocarBase(string color)
    {
        Debug.Log("EntroColocar 1");
        AllowPlace();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //Debug.Log("Lanzando raycast");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            //Debug.Log("Antes EjecutarColocarBase");
            EjecutarColocarBase(hit, color);
            //Debug.Log("Despues EjecutarColocarBase");
            confirmBaseButton.gameObject.SetActive(true); // Habilita el bot�n de confirmaci�n
        }
        //ARCursor arCursor = FindObjectOfType<ARCursor>();
        if (ARCursor.Instance != null)
        {
            ARCursor.Instance.ActivatePlacementMode();
        }
    }
    
    public void ColocarPueblo(string color)
    {
        AllowPlace();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            //Debug.Log("Antes EjecutarColocarBase");
            EjecutarColocarPueblo(hit, color);
            //Debug.Log("Despues EjecutarColocarBase");
            confirmPuebloButton.gameObject.SetActive(true); // Habilita el bot�n de confirmaci�n
        }
        //ARCursor arCursor = FindObjectOfType<ARCursor>();
        if (ARCursor.Instance != null)
        {
            ARCursor.Instance.ActivatePlacementMode();
        }
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
            // Deshabilita el bot�n de confirmaci�n
            confirmBaseButton.gameObject.SetActive(false);

            // Desactiva la capacidad de mover la base
            canPlace = false;

            // Borra la referencia a la base actual
            currentBase = null;
        }
        else
        {
            // Puedes mostrar alg�n mensaje o realizar alguna acci�n si la base no puede ser confirmada
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
        if (currentPueblo != null && canPlace)
        {
            // Deshabilita el bot�n de confirmaci�n
            confirmPuebloButton.gameObject.SetActive(false);
            canPlace = false;
            currentPueblo = null;
        }
    }
   
    /*public void ActivarColocacion(TipoObjeto tipo)
    {
        tipoActual = tipo;
    }
    */

}
