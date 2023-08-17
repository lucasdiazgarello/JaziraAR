using System;
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
    private GameObject prefabCaminoA;
    private GameObject prefabBaseA; // Cambiado Casa por Base
    private GameObject prefabPuebloA; // Nuevo prefab para el pueblo
    private GameObject prefabCaminoR;
    private GameObject prefabBaseR; // Cambiado Casa por Base
    private GameObject prefabPuebloR; // Nuevo prefab para el pueblo
    private GameObject prefabCaminoV;
    private GameObject prefabBaseV; // Cambiado Casa por Base
    private GameObject prefabPuebloV; // Nuevo prefab para el pueblo
    private GameObject prefabCaminoN;
    private GameObject prefabBaseN; // Cambiado Casa por Base
    private GameObject prefabPuebloN; // Nuevo prefab para el pueblo
    private GameObject currentPrefabBase;
    private GameObject currentPrefabCamino;
    private GameObject currentPrefabPueblo;
    private GameObject currentBase;
    private GameObject currentCamino;
    private GameObject currentPueblo;
    private bool _isTouching = false;
    private bool canPlace = false;
    private ComprobarObjeto comprobarObjeto;
    public LayerMask myLayerMask;

    // Botones para las acciones de colocar camino y base
    public Button buttonCamino;
    public Button buttonBase;
    public Button buttonPueblo;
    public Button confirmBaseButton; // Asegúrate de asignar este botón en el inspector de Unity
    public Button confirmCaminoButton;
    public Button confirmPuebloButton;

    public TipoObjeto tipoActual;

    void Start()
    {
        prefabBaseA = Resources.Load("Base Azul") as GameObject;
        prefabCaminoA = Resources.Load("Camino Azul") as GameObject;
        prefabPuebloA = Resources.Load("Pueblo Azul") as GameObject;
        prefabBaseR = Resources.Load("Base Rojo") as GameObject;
        prefabCaminoR = Resources.Load("Camino Rojo") as GameObject;
        prefabPuebloR = Resources.Load("Pueblo Rojo") as GameObject;
        prefabBaseV = Resources.Load("Base Violeta") as GameObject;
        prefabCaminoV = Resources.Load("Camino Violeta") as GameObject;
        prefabPuebloV = Resources.Load("Pueblo Violeta") as GameObject;
        prefabBaseN = Resources.Load("Base Naranja") as GameObject;
        prefabCaminoN = Resources.Load("Camino Naranja") as GameObject;
        prefabPuebloN = Resources.Load("Pueblo Naranja") as GameObject;

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

    void Update()
    {
        //PruebaServerRpc();
        if (canPlace && Input.touchCount == 1 && !_isTouching && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _isTouching = true;
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.GetTouch(0).position;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            //PruebaServerRpc();
            if (results.Count > 0)  // Si hay algún resultado, el toque está sobre un elemento de la interfaz de usuario
            {
                return;
            }
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
            {
                //PruebaServerRpc();
                var color =PlayerPrefs.GetString("colorJugador");

                if (hit.collider.gameObject.CompareTag("Arista") && tipoActual == TipoObjeto.Camino)
                {

                    ColocarCamino(color);
                }
                else if (hit.collider.gameObject.CompareTag("Esquina") && tipoActual == TipoObjeto.Base)
                {
                    ColocarBase(color);
                    //EjecutarColocarBase(hit);
                    //canPlace = false; // Desactivar la capacidad de colocar después de que se ha colocado la base
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

    public void EjecutarColocarCamino(RaycastHit hit,string color)
    {
        switch (color)
        {
            case "Rojo":  // Aquí se hace uso del tipo enumerado TipoObjeto
                currentPrefabCamino = prefabCaminoR;
                break;
            case "Azul":  // Aquí se hace uso del tipo enumerado TipoObjeto
                currentPrefabCamino = prefabCaminoA;
                break;
            case "Violeta":  // Aquí se hace uso del tipo enumerado TipoObjeto
                currentPrefabCamino = prefabCaminoV;
                break;
            case "Naranja":  // Aquí se hace uso del tipo enumerado TipoObjeto
                currentPrefabCamino = prefabCaminoN;
                break;
        }
        Debug.Log("EjecutarColocarCamino");
        // El objeto golpeado es una esquina.
        //currentCamino = Instantiate(prefabCamino, hit.collider.gameObject.transform.position, Quaternion.identity);
        comprobarObjeto = hit.collider.gameObject.GetComponent<ComprobarObjeto>();
        var nombreCol = hit.collider.gameObject.GetComponent<ComprobarObjeto>().name;
        Debug.Log("El col se llama " + nombreCol);
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
        if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log("Soy server y voy a poner un camino");
            currentCamino = Instantiate(currentPrefabCamino, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation); //con rotacion
            currentCamino.GetComponent<NetworkObject>().Spawn();
        }
        else // es un cliente
        {
            Debug.Log("Soy cliente q va a poner camino");
            var currPrefabBase = currentPrefabBase.name;
            ColocarCaminoServerRpc(color, nombreCol, currPrefabBase);
        }
        Debug.Log("el tipo del camino es " + tipoActual);
        tipoActual = TipoObjeto.Ninguno;
        //Debug.Log("Termino EjecutarColocarBase");
    }

    [ServerRpc(RequireOwnership = false)]
    public void ColocarCaminoServerRpc(string color, string collider, string currentPrefabBase)
    {
        Debug.Log("Entre a ColocarCaminoServerRpc");
        var objetoCollider = GameObject.Find(collider);
        switch (color)
        {
            case "Rojo":  // Aquí se hace uso del tipo enumerado TipoObjeto
                currentPrefabCamino = prefabCaminoR;
                break;
            case "Azul":  // Aquí se hace uso del tipo enumerado TipoObjeto
                currentPrefabCamino = prefabCaminoA;
                break;
            case "Violeta":  // Aquí se hace uso del tipo enumerado TipoObjeto
                currentPrefabCamino = prefabCaminoV;
                break;
            case "Naranja":  // Aquí se hace uso del tipo enumerado TipoObjeto
                currentPrefabCamino = prefabCaminoN;
                break;
        }
        currentCamino = Instantiate(currentPrefabCamino, objetoCollider.transform.position, objetoCollider.transform.rotation); //con rotacion
        currentCamino.GetComponent<NetworkObject>().Spawn();
    }

    public void EjecutarColocarBase(RaycastHit hit, string color, string currentPrefabBase)
    {
        Debug.Log("EntroColocar 2");

        var objetoBase = Resources.Load(currentPrefabBase) as GameObject;
        Debug.Log("2 preafb base es " + objetoBase.name);
        // El objeto golpeado es una esquina.
        currentBase = Instantiate(objetoBase, hit.collider.gameObject.transform.position, Quaternion.identity);
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
        Debug.Log("el tipo de la base colocada es " + tipoActual);
        tipoActual = TipoObjeto.Ninguno;
    }

 /*   [ServerRpc(RequireOwnership = false)]
    public void ColocarBaseServerRpc(string color, string currentbase, Vector3 posititon)
    {
        try
        {
            Debug.Log("Entre a la BaseServerRpc");
            var objetoBase = GameObject.Find(currentbase);

            currentBase = Instantiate(objetoBase, posititon, Quaternion.identity);
            currentBase.GetComponent<NetworkObject>().Spawn();
            // Obtener el componente ComprobarObjeto del objeto golpeado
            comprobarObjeto = objetoBase.gameObject.GetComponent<ComprobarObjeto>();
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
                //Debug.LogError("El objeto " + objetoCollider.gameObject.name + " no tiene un script ComprobarObjeto.");
            }
            Debug.Log("el tipo de la base colocada es " + tipoActual);
            tipoActual = TipoObjeto.Ninguno;
        }
        catch (Exception e)
        {
            Debug.Log("Error en ColocarBaseServerRpc: " + e);
        }

    }*/
    public void EjecutarColocarPueblo(RaycastHit hit, string color)
    {
        switch (color)
        {
            case "Rojo":  // Aquí se hace uso del tipo enumerado TipoObjeto
                currentPrefabPueblo = prefabPuebloR;
                break;
            case "Azul":  // Aquí se hace uso del tipo enumerado TipoObjeto
                currentPrefabPueblo = prefabPuebloA;
                break;
            case "Violeta":  // Aquí se hace uso del tipo enumerado TipoObjeto
                currentPrefabPueblo = prefabPuebloV;
                break;
            case "Naranja":  // Aquí se hace uso del tipo enumerado TipoObjeto
                currentPrefabPueblo = prefabPuebloN;
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
        //PruebaServerRpc();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //PruebaServerRpc();
        //if (ray == null) Debug.LogError("RAY is null");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            switch (color)
            {
                case "Rojo":
                    currentPrefabBase = prefabBaseR;
                    break;
                case "Azul":
                    currentPrefabBase = prefabBaseA;
                    break;
                case "Violeta":
                    currentPrefabBase = prefabBaseV;
                    break;
                case "Naranja":
                    currentPrefabBase = prefabBaseN;
                    break;
            }
            var currPrefBase = currentPrefabBase.name;
            Debug.Log("el prefab base se llama " + currPrefBase);
            if (NetworkManager.Singleton.IsServer)
            {

                EjecutarColocarBase(hit, color, currPrefBase);
            }
            else // si es un cliente
            {
                string colliderName = hit.collider.gameObject.name;
                Debug.Log("colliderName: " + colliderName);
                //PlayerPrefs.SetString(colliderName, "collider");
                //PlayerNetwork.Instance.PruebaServerRpc();
                var position = hit.collider.gameObject.transform.position;

                PlayerNetwork.Instance.ColocarBaseServerRpc(color, currPrefBase, position);
            }

            confirmBaseButton.gameObject.SetActive(true); // Habilita el botón de confirmación
        }
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
            confirmPuebloButton.gameObject.SetActive(true); // Habilita el botón de confirmación
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
        if (currentPueblo != null && canPlace)
        {
            // Deshabilita el botón de confirmación
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
