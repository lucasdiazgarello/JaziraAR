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
    private ComprobarObjeto comprobarObjeto2;
    public LayerMask myLayerMask;

    // Botones para las acciones de colocar camino y base
    public Button buttonCamino;
    public Button buttonBase;
    public Button buttonPueblo;
    public int caminosRestantes = 2;
    public int basesRestantes = 2;
    public int pueblosRestantes =0;
    public bool primerasPiezas = false;
    public bool yaEjecutado = false;
    //public Button confirmBaseButton; // Asegúrate de asignar este botón en el inspector de Unity
    //public Button confirmCaminoButton;
    //public Button confirmPuebloButton;

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
        //confirmBaseButton.gameObject.SetActive(false);
        //confirmCaminoButton.gameObject.SetActive(false);
        //confirmPuebloButton.gameObject.SetActive(false);
        // Al principio, solo permitir la colocación de dos caminos y bases.
        buttonCamino.interactable = caminosRestantes > 0;
        buttonBase.interactable = basesRestantes > 0;

        // Asumiendo que tienes los botones definidos, desactivarlos al inicio
        // excepto los botones para los caminos y bases iniciales.
        buttonPueblo.interactable = false;
    }

    void Update()
    {
        if (!yaEjecutado && caminosRestantes == 0 && basesRestantes == 0)
        {
            primerasPiezas = true;
            yaEjecutado = true;
        }
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


    public void ColocarCamino(string color)
    {
        AllowPlace();
        //Debug.Log("EntroColocar 1");
        Debug.Log("Caminos restantes PRE: " + caminosRestantes);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            switch (color)
            {
                case "Rojo":
                    currentPrefabCamino = prefabCaminoR;
                    break;
                case "Azul":
                    currentPrefabCamino = prefabCaminoA;
                    break;
                case "Violeta":
                    currentPrefabCamino = prefabCaminoV;
                    break;
                case "Naranja":
                    currentPrefabCamino = prefabCaminoN;
                    break;
            }
            var currPrefCamino = currentPrefabCamino.name;
            Debug.Log("el prefab camino se llama " + currPrefCamino);
            if (NetworkManager.Singleton.IsServer)
            {
                EjecutarColocarCamino(hit, color, currPrefCamino);
            }
            else // si es un cliente
            {
                string colliderName = hit.collider.gameObject.name;
                Debug.Log("colliderName: " + colliderName);
                var position = hit.collider.gameObject.transform.position;
                var rotation = hit.collider.gameObject.transform.rotation;
                PlayerNetwork.Instance.ColocarCaminoServerRpc(color, currPrefCamino, position, rotation);
            }
            // Luego de colocar un camino, disminuyes el contador y verificas si desactivar el botón.
            caminosRestantes--;
            Debug.Log("Caminos restantes POST: " + caminosRestantes);
            if (caminosRestantes <= 0)
                buttonCamino.interactable = false;
            // Inclusión de la funcionalidad de ConfirmarBase()
            if (currentCamino == null)
            {
                Debug.Log("currentCamino es null");
            }
            if (currentCamino != null)
            {
                //confirmBaseButton.gameObject.SetActive(false);  // Deshabilita el botón de confirmación
                canPlace = false;  // Desactiva la capacidad de mover la base
                currentCamino = null;  // Borra la referencia a la base actual
            }
            else
            {
                Debug.Log("El Camino no puede ser confirmada");
            }
        }
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
                var position = hit.collider.gameObject.transform.position;
                PlayerNetwork.Instance.ColocarBaseServerRpc(color, currPrefBase, colliderName, position);
            }
            basesRestantes--;
            Debug.Log("Bases restantes POST: " + basesRestantes);
            if (basesRestantes <= 0)
                buttonBase.interactable = false;
            // Inclusión de la funcionalidad de ConfirmarBase()
            if (currentBase == null)
            {
                Debug.Log("currentBase es null");
            }

            if (currentBase != null)
            {
                //confirmBaseButton.gameObject.SetActive(false);  // Deshabilita el botón de confirmación
                canPlace = false;  // Desactiva la capacidad de mover la base
                currentBase = null;  // Borra la referencia a la base actual
            }
            else
            {
                Debug.Log("La base no puede ser confirmada");
            }
        }
        if (ARCursor.Instance != null)
        {
            ARCursor.Instance.ActivatePlacementMode();
        }
    }
    public void ColocarPueblo(string color)
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
                    currentPrefabPueblo = prefabPuebloR;
                    break;
                case "Azul":
                    currentPrefabPueblo = prefabPuebloA;
                    break;
                case "Violeta":
                    currentPrefabPueblo = prefabPuebloV;
                    break;
                case "Naranja":
                    currentPrefabPueblo = prefabPuebloN;
                    break;
            }
            var currPrefPueblo = currentPrefabPueblo.name;
            Debug.Log("el prefab pueblo se llama " + currPrefPueblo);
            if (NetworkManager.Singleton.IsServer)
            {
                EjecutarColocarPueblo(hit, color, currPrefPueblo);
            }
            else // si es un cliente
            {
                string colliderName = hit.collider.gameObject.name;
                Debug.Log("colliderName: " + colliderName);
                var position = hit.collider.gameObject.transform.position;
                PlayerNetwork.Instance.ColocarPuebloServerRpc(color, currPrefPueblo, position);
            }
            pueblosRestantes--;
            Debug.Log("Pueblos restantes POST: " + pueblosRestantes);
            if (pueblosRestantes <= 0)
                buttonPueblo.interactable = false;
            // Inclusión de la funcionalidad de ConfirmarBase()
            if (currentPueblo == null)
            {
                Debug.Log("currentPueblo es null");
            }

            if (currentPueblo != null)
            {
                //confirmBaseButton.gameObject.SetActive(false);  // Deshabilita el botón de confirmación
                canPlace = false;  // Desactiva la capacidad de mover la base
                currentPueblo = null;  // Borra la referencia a la base actual
            }
            else
            {
                Debug.Log("El Pueblo no puede ser confirmado");
            }
        }
        if (ARCursor.Instance != null)
        {
            ARCursor.Instance.ActivatePlacementMode();
        }
    }

    public void EjecutarColocarCamino(RaycastHit hit, string color, string currentPrefabCamino)
    {
        Debug.Log("EntroColocar 2");

        var objetoCamino = Resources.Load(currentPrefabCamino) as GameObject;
        Debug.Log("2 preafb base es " + objetoCamino.name);
        currentCamino = Instantiate(objetoCamino, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
        currentCamino.GetComponent<NetworkObject>().Spawn();
        comprobarObjeto = hit.collider.gameObject.GetComponent<ComprobarObjeto>();
        Debug.Log("el collider del comprobarobjeto es " + hit.collider.gameObject.name);
        if (comprobarObjeto != null)
        {
            comprobarObjeto.CambiarTipoObjeto("Camino");
            //comprobarObjeto.tipoObjeto = TipoObjeto.Camino; // Puedes cambiar esto al tipo de objeto que corresponda
            Debug.Log("puse el tipo del camino a: " + comprobarObjeto.tipoObjeto);
        }
        else
        {
            Debug.LogError("El objeto " + hit.collider.gameObject.name + " no tiene un script ComprobarObjeto.");
        }
        //Debug.Log("el tipo del camino colocado es " + tipoActual);
        tipoActual = TipoObjeto.Ninguno;
    }
    public void EjecutarColocarBase(RaycastHit hit, string color, string currentPrefabBase)
    {
        Debug.Log("EntroColocar 2");
        var objetoBase = Resources.Load(currentPrefabBase) as GameObject;
        Debug.Log("2 preafb base es " + objetoBase.name);
        currentBase = Instantiate(objetoBase, hit.collider.gameObject.transform.position, Quaternion.identity);
        currentBase.GetComponent<NetworkObject>().Spawn();
        Debug.Log("CP SC Nombre de collider" + hit.collider.gameObject.GetComponent<ComprobarObjeto>().name);
        comprobarObjeto = hit.collider.gameObject.GetComponent<ComprobarObjeto>();
        
        Debug.Log("el collider del comprobarobjeto es " + hit.collider.gameObject.name);
        if (comprobarObjeto != null)
        {
            //comprobarObjeto.CambiarTipoObjeto("Base");
  
            var nombreSinClone = ListaColliders.Instance.RemoverCloneDeNombre(comprobarObjeto.name);
            Debug.Log("nombreSinClone = " + nombreSinClone);

            ListaColliders.Instance.ModificarTipoPorNombre(nombreSinClone, "Base");
            ListaColliders.Instance.ImprimirListaColliders();
            //comprobarObjeto.tipoObjeto = TipoObjeto.Base; // Puedes cambiar esto al tipo de objeto que corresponda
            //comprobarObjeto2 = hit.collider.gameObject.GetComponent<ComprobarObjeto>();
            Debug.Log("puse el tipo de la base a: " + comprobarObjeto.tipoObjeto);
            //Debug.Log("LA BASE ES BASE? : " + comprobarObjeto2.tipoObjeto);
            Debug.Log("CP Nombre de collider" + comprobarObjeto.name);
            //Debug.Log("CP SC Nombre de collider" + hit.collider.gameObject.GetComponent<ComprobarObjeto>().name); 
            Debug.Log("Asignando tipo a: " + hit.collider.gameObject.name + " - Instancia: " + hit.collider.gameObject.GetInstanceID());

        }
        else
        {
            Debug.LogError("El objeto " + hit.collider.gameObject.name + " no tiene un script ComprobarObjeto.");
        }
        //Debug.Log("el tipo de la base colocada es " + tipoActual);
        tipoActual = TipoObjeto.Ninguno;
    }
    public void SetTipoCollider (Collider collider, Collider tipo)
    {

    }
    public void EjecutarColocarPueblo(RaycastHit hit, string color, string currentPrefabPueblo)
    {
        Debug.Log("EntroColocar 2");

        var objetoPueblo = Resources.Load(currentPrefabPueblo) as GameObject;
        Debug.Log("2 preafb pueblo es " + objetoPueblo.name);
        currentPueblo = Instantiate(objetoPueblo, hit.collider.gameObject.transform.position, Quaternion.identity);
        currentPueblo.GetComponent<NetworkObject>().Spawn();
        comprobarObjeto = hit.collider.gameObject.GetComponent<ComprobarObjeto>();
        Debug.Log("el collider del comprobarobjeto es " + hit.collider.gameObject.name);
        if (comprobarObjeto != null)
        {
            comprobarObjeto.CambiarTipoObjeto("Pueblo");
            //comprobarObjeto.tipoObjeto = TipoObjeto.Pueblo; // Puedes cambiar esto al tipo de objeto que corresponda
            Debug.Log("puse el tipo del pueblo a: " + comprobarObjeto.tipoObjeto);
        }
        else
        {
            Debug.LogError("El objeto " + hit.collider.gameObject.name + " no tiene un script ComprobarObjeto.");
        }
        //Debug.Log("el tipo del pueblo colocado es " + tipoActual);
        tipoActual = TipoObjeto.Ninguno;
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
}
