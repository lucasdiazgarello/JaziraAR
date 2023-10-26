using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Netcode;
using Unity.Services.Qos;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Para manejar los botones

public class PlacePiece : NetworkBehaviour
{
    //public static PlacePiece Instance { get; private set; }
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
    public LayerMask myLayerMask;
    public Button buttonCamino;
    public Button buttonBase;
    public Button buttonPueblo;
    private string tipoActual;
    Dictionary<int, GameObject> gameObjectsByID = new Dictionary<int, GameObject>();

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
        tipoActual = "Ninguno";
        // Agregar los listeners a los botones
        buttonCamino.onClick.AddListener(() => {
            tipoActual = "Camino";
            canPlace = true;
        });
        buttonBase.onClick.AddListener(() => {
            tipoActual = "Base";
            canPlace = true;
        });
        buttonPueblo.onClick.AddListener(() => {
            tipoActual = "Pueblo";
            canPlace = true;
        });

        int id = PlayerPrefs.GetInt("jugadorId");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        // Al principio, solo permitir la colocación de dos caminos y bases.
        buttonCamino.interactable = jugador.cantidadCaminos > 0;
        buttonBase.interactable = jugador.cantidadBases > 0;
        buttonPueblo.interactable = false;
    }

    void Update()
    {
        int id = PlayerPrefs.GetInt("jugadorId");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        //primero desactivo botones si no es mi turno
        if (PlayerNetwork.Instance.IsMyTurn(PlayerPrefs.GetInt("jugadorId")))
        {
            if (jugador.cantidadCaminos > 0)
            {
                buttonCamino.interactable = true;
            }
            else
            {
                buttonCamino.interactable = false;
            }
            if (jugador.cantidadBases > 0)
            {
                buttonBase.interactable = true;
            }
            else
            {
                buttonBase.interactable = false;
            }
            if (jugador.cantidadPueblos > 0)
            {
                buttonPueblo.interactable = true;
            }
            else
            {
                buttonPueblo.interactable = false;
            }
        }
        else
        {
            buttonCamino.interactable = false;
            buttonBase.interactable = false;
            buttonPueblo.interactable = false;
        }
        //luego el update de siempre
        if (NetworkManager.Singleton.IsServer)
        {
            if (!jugador.primerasPiezas && jugador.cantidadCaminos == 0 && jugador.cantidadBases == 0)
            {
                //primerasPiezas = true;
                int indexJugador = -1;
                bool jugadorEncontrado = false;
                // Búsqueda del jugador en la lista playerData
                for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
                {
                    //Debug.Log("La lista Ids es " + playerData[i].jugadorId);
                    if (PlayerNetwork.Instance.playerData[i].jugadorId == id)
                    {
                        jugadorEncontrado = true;
                        indexJugador = i;
                        break;
                    }
                }
                if (!jugadorEncontrado)
                {
                    Debug.Log("Jugador no encontrado en la lista playerData");
                    return;
                }
                // Crear una copia del jugador, modificarla y luego reemplazar el elemento original
                PlayerNetwork.DatosJugador jugadorcopia = PlayerNetwork.Instance.playerData[indexJugador];
                // Aquí es donde actualizarías los recursos del jugador en tu juego.
                jugadorcopia.primerasPiezas = true;
                PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
            }
        }
        else
        {
            int idcliente = PlayerPrefs.GetInt("jugadorId");
            PlayerNetwork.Instance.PrimerasPiezasServerRpc(idcliente);
        }
        
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
                //PruebaServerRpc();
                var color =PlayerPrefs.GetString("colorJugador");

                if (hit.collider.gameObject.CompareTag("Arista") && tipoActual == "Camino")
                {
                    ColocarCamino(color);
                }
                else if (hit.collider.gameObject.CompareTag("Esquina") && tipoActual == "Base")
                {
                    ColocarBase(color);
                }
                // Nuevo if para manejar el caso de TipoObjeto.Pueblo
                else if (hit.collider.gameObject.CompareTag("Esquina") && tipoActual == "Pueblo")
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
            int id = PlayerPrefs.GetInt("jugadorId");
            //PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
            int indexJugador = -1;
            bool jugadorEncontrado = false;

            // Búsqueda del jugador en la lista playerData
            for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
            {
                //Debug.Log("La lista Ids es " + playerData[i].jugadorId);
                if (PlayerNetwork.Instance.playerData[i].jugadorId == id)
                {
                    jugadorEncontrado = true;
                    indexJugador = i;
                    break;
                }
            }
            if (!jugadorEncontrado)
            {
                Debug.Log("Jugador no encontrado en la lista playerData");
                return;
            }
            // Crear una copia del jugador, modificarla y luego reemplazar el elemento original
            PlayerNetwork.DatosJugador jugadorcopia = PlayerNetwork.Instance.playerData[indexJugador];
            var currPrefCamino = currentPrefabCamino.name;
            Debug.Log("el prefab camino se llama " + currPrefCamino);
            if (NetworkManager.Singleton.IsServer)
            {
                EjecutarColocarCamino(hit, color, currPrefCamino);
                // Luego de colocar un camino, disminuyes el contador y verificas si desactivar el botón.
                jugadorcopia.cantidadCaminos = jugadorcopia.cantidadCaminos - 1;
                PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
                //jugador.cantidadCaminos--;
                Debug.Log("Caminos restantes POST: " + PlayerNetwork.Instance.playerData[indexJugador].cantidadCaminos);
            }
            else // si es un cliente
            {
                string colliderName = hit.collider.gameObject.name;
                Debug.Log("colliderName: " + colliderName);
                var position = hit.collider.gameObject.transform.position;
                var rotation = hit.collider.gameObject.transform.rotation;
                PlayerNetwork.Instance.ColocarCaminoServerRpc(color, currPrefCamino, colliderName, position, rotation);
            }
            if (PlayerNetwork.Instance.playerData[indexJugador].cantidadCaminos <= 0)
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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
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
            int id = PlayerPrefs.GetInt("jugadorId");
            //PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
            int indexJugador = -1;
            bool jugadorEncontrado = false;

            // Búsqueda del jugador en la lista playerData
            for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
            {
                //Debug.Log("La lista Ids es " + playerData[i].jugadorId);
                if (PlayerNetwork.Instance.playerData[i].jugadorId == id)
                {
                    jugadorEncontrado = true;
                    indexJugador = i;
                    break;
                }
            }
            if (!jugadorEncontrado)
            {
                Debug.Log("Jugador no encontrado en la lista playerData");
                return;
            }
            // Crear una copia del jugador, modificarla y luego reemplazar el elemento original
            PlayerNetwork.DatosJugador jugadorcopia = PlayerNetwork.Instance.playerData[indexJugador];
            var currPrefBase = currentPrefabBase.name;
            Debug.Log("el prefab base se llama " + currPrefBase);
            var nombrecollider = hit.collider.gameObject.name;
            var nombreSinClone = CollidersList.Instance.RemoverCloneDeNombre(nombrecollider);
            Debug.Log("El nombre de la casa de la que buscare los caminos es:  " + nombreSinClone);
            if (NetworkManager.Singleton.IsServer && CollidersListCaminos.Instance.VerificarHayCaminoPorNombre(nombreSinClone, color))
            {

                EjecutarColocarBase(hit, color, currPrefBase);
                // Luego de colocar una base, disminuyes el contador y verificas si desactivar el botón.
                jugadorcopia.cantidadBases = jugadorcopia.cantidadBases - 1;
                jugadorcopia.puntaje = jugadorcopia.puntaje + 1;
                PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
                //jugador.cantidadBases--;
                Debug.Log("Bases restantes POST: " + PlayerNetwork.Instance.playerData[indexJugador].cantidadBases);
                //Actualizar textos puntajes
                int idJugador = PlayerNetwork.Instance.GetPlayerId(PlayerNetwork.Instance.playerData[indexJugador]);
                // Itera sobre los elementos de playerData para encontrar los datos del jugador
                PlayerNetwork.DatosJugador juga1 = new PlayerNetwork.DatosJugador();
                PlayerNetwork.DatosJugador juga2 = new PlayerNetwork.DatosJugador();
                PlayerNetwork.DatosJugador juga3 = new PlayerNetwork.DatosJugador();
                PlayerNetwork.DatosJugador juga4 = new PlayerNetwork.DatosJugador();
                var posicion = -1;
                for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            juga1 = PlayerNetwork.Instance.playerData[i];
                            Debug.Log("el puntaje del jugador1 es " + juga1.puntaje);
                            BoardManager.Instance.Puntaje1.text = juga1.puntaje.ToString();
                            BoardManager.Instance.Nombre1.text = juga1.nomJugador.ToString();
                            break;
                        case 1:
                            juga2 = PlayerNetwork.Instance.playerData[i];
                            Debug.Log("el puntaje del jugador2 es " + juga2.puntaje);
                            BoardManager.Instance.Puntaje2.text = juga2.puntaje.ToString();
                            BoardManager.Instance.Nombre2.text = juga2.nomJugador.ToString();
                            break;
                        case 2:
                            juga3 = PlayerNetwork.Instance.playerData[i];
                            if(juga3.puntaje == 0)
                            {
                                BoardManager.Instance.Puntaje3.text = "";
                                BoardManager.Instance.Nombre3.text = juga3.nomJugador.ToString();
                            }
                            else
                            {
                                BoardManager.Instance.Puntaje1.text = juga3.puntaje.ToString();
                                BoardManager.Instance.Nombre1.text = juga3.nomJugador.ToString();
                            }
                            break;
                        case 3:
                            juga4 = PlayerNetwork.Instance.playerData[i];
                            if (juga4.puntaje == 0)
                            {
                                BoardManager.Instance.Puntaje4.text = "";
                                BoardManager.Instance.Nombre4.text = juga3.nomJugador.ToString();
                            }
                            else
                            {
                                BoardManager.Instance.Puntaje4.text = juga3.puntaje.ToString();
                                BoardManager.Instance.Nombre4.text = juga3.nomJugador.ToString();
                            }

                            break;
                    }

                }

                if (juga3.puntaje == 0)
                {
                    BoardManager.Instance.Puntaje3.text = "";
                }
                if (juga4.puntaje == 0)
                {
                    BoardManager.Instance.Puntaje4.text = "";
                }
                PlayerNetwork.Instance.UpdatePuntajeTextClientRpc(juga1, juga2, juga3, juga4);
                //Debug.Log("el puntaje del jugador1 es " + juga1.puntaje);
                //Debug.Log("el puntaje del jugador2 es " + juga2.puntaje);
/*
                BoardManager.Instance.Puntaje1.text = juga1.puntaje.ToString();
                BoardManager.Instance.Nombre1.text = juga1.nomJugador.ToString();
                BoardManager.Instance.Puntaje2.text = juga2.puntaje.ToString();
                BoardManager.Instance.Nombre2.text = juga2.nomJugador.ToString();
                BoardManager.Instance.Puntaje3.text = juga3.puntaje.ToString();
                BoardManager.Instance.Nombre3.text = juga3.nomJugador.ToString();
                BoardManager.Instance.Puntaje4.text = juga4.puntaje.ToString();
                BoardManager.Instance.Nombre4.text = juga4.nomJugador.ToString();
*/

            }
            else // si es un cliente
            {
                CollidersListCaminos.Instance.camino1si = false;
                CollidersListCaminos.Instance.camino2si = false;
                CollidersListCaminos.Instance.camino3si = false;
                CollidersList.Instance.hayCaminoGlobal = false;
                CollidersListCaminos.Instance.canttrues = 0;
                string colliderName = hit.collider.gameObject.name;
                Debug.Log("colliderName: " + colliderName);
                var position = hit.collider.gameObject.transform.position;
                //CollidersList.Instance.VerificarHayCaminoPorNombreServerRpc(nombreSinClone, color, id, currPrefBase, colliderName, position);
                PlayerNetwork.Instance.ColocarBaseServerRpc(id, color, currPrefBase, colliderName, position);


            }
            /*Inicializo las variables que se quedan fijadas entre intentos de colocacion*/
            CollidersListCaminos.Instance.camino1si = false;
            CollidersListCaminos.Instance.camino2si = false;
            CollidersListCaminos.Instance.camino3si = false;
            CollidersList.Instance.hayCaminoGlobal = false;
            CollidersListCaminos.Instance.canttrues = 0;
            if (PlayerNetwork.Instance.playerData[indexJugador].cantidadBases <= 0)
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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
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
            int id = PlayerPrefs.GetInt("jugadorId");
            //PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
            int indexJugador = -1;
            bool jugadorEncontrado = false;

            // Búsqueda del jugador en la lista playerData
            for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
            {
                //Debug.Log("La lista Ids es " + playerData[i].jugadorId);
                if (PlayerNetwork.Instance.playerData[i].jugadorId == id)
                {
                    jugadorEncontrado = true;
                    indexJugador = i;
                    break;
                }
            }
            if (!jugadorEncontrado)
            {
                Debug.Log("Jugador no encontrado en la lista playerData");
                return;
            }
            // Crear una copia del jugador, modificarla y luego reemplazar el elemento original
            PlayerNetwork.DatosJugador jugadorcopia = PlayerNetwork.Instance.playerData[indexJugador];
            var currPrefPueblo = currentPrefabPueblo.name;
            Debug.Log("el prefab pueblo se llama " + currPrefPueblo);

            if (NetworkManager.Singleton.IsServer)
            {
                var nombrecollider = hit.collider.gameObject.name;
                var nombreSinClone = CollidersList.Instance.RemoverCloneDeNombre(nombrecollider);
                var tipo = CollidersList.Instance.GetTipoPorNombre(nombreSinClone);
                Debug.Log("El tipo del collider es " + tipo);
                var colorCollider = CollidersList.Instance.GetColorPorNombre(nombreSinClone);
                if (tipo == "Base" && color == colorCollider)
                {
                    EjecutarColocarPueblo(hit, color, currPrefPueblo);
                    // Luego de colocar una base, disminuyes el contador y verificas si desactivar el botón.
                    jugadorcopia.cantidadPueblos = jugadorcopia.cantidadPueblos - 1;
                    jugadorcopia.puntaje = jugadorcopia.puntaje + 1;
                    PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
                    //jugador.cantidadPueblos--;
                    Debug.Log("Pueblos restantes POST: " + PlayerNetwork.Instance.playerData[indexJugador].cantidadPueblos);
                }
                else
                {
                    Debug.Log("El pueblo se debe colocar sobre una Base");
                }
                //Actualizar textos puntajes
                int idJugador = PlayerNetwork.Instance.GetPlayerId(PlayerNetwork.Instance.playerData[indexJugador]);
                // Itera sobre los elementos de playerData para encontrar los datos del jugador
                PlayerNetwork.DatosJugador juga1 = new PlayerNetwork.DatosJugador();
                PlayerNetwork.DatosJugador juga2 = new PlayerNetwork.DatosJugador();
                PlayerNetwork.DatosJugador juga3 = new PlayerNetwork.DatosJugador();
                PlayerNetwork.DatosJugador juga4 = new PlayerNetwork.DatosJugador();
                PlayerNetwork.DatosJugador datosJugador = default;
                var posicion = -1;
                for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            juga1 = PlayerNetwork.Instance.playerData[i];
                            Debug.Log("el puntaje del jugador1 es " + juga1.puntaje);
                            BoardManager.Instance.Puntaje1.text = juga1.puntaje.ToString();
                            BoardManager.Instance.Nombre1.text = juga1.nomJugador.ToString();
                            break;
                        case 1:
                            juga2 = PlayerNetwork.Instance.playerData[i];
                            Debug.Log("el puntaje del jugador2 es " + juga2.puntaje);
                            BoardManager.Instance.Puntaje2.text = juga2.puntaje.ToString();
                            BoardManager.Instance.Nombre2.text = juga2.nomJugador.ToString();
                            break;
                        case 2:
                            juga3 = PlayerNetwork.Instance.playerData[i];
                            if (juga3.puntaje == 0)
                            {
                                BoardManager.Instance.Puntaje3.text = "";
                                BoardManager.Instance.Nombre3.text = juga3.nomJugador.ToString();
                            }
                            else
                            {
                                BoardManager.Instance.Puntaje1.text = juga3.puntaje.ToString();
                                BoardManager.Instance.Nombre1.text = juga3.nomJugador.ToString();
                            }
                            break;
                        case 3:
                            juga4 = PlayerNetwork.Instance.playerData[i];
                            if (juga4.puntaje == 0)
                            {
                                BoardManager.Instance.Puntaje4.text = "";
                                BoardManager.Instance.Nombre4.text = juga3.nomJugador.ToString();
                            }
                            else
                            {
                                BoardManager.Instance.Puntaje4.text = juga3.puntaje.ToString();
                                BoardManager.Instance.Nombre4.text = juga3.nomJugador.ToString();
                            }

                            break;
                    }

                }

                if (juga3.puntaje == 0)
                {
                    BoardManager.Instance.Puntaje3.text = "";
                }
                if (juga4.puntaje == 0)
                {
                    BoardManager.Instance.Puntaje4.text = "";
                }
                PlayerNetwork.Instance.UpdatePuntajeTextClientRpc(juga1, juga2, juga3, juga4);
            }
            else // si es un cliente
            {
                string colliderName = hit.collider.gameObject.name;
                Debug.Log("colliderName: " + colliderName);
                var position = hit.collider.gameObject.transform.position;
                ;
                var nombresinClone = CollidersList.Instance.RemoverCloneDeNombre(colliderName);
                var colorCollider = CollidersList.Instance.GetColorPorNombre(nombresinClone);
                Debug.Log("color es : " + color + " y colorcollider es:" + colorCollider);
                if (color == colorCollider)
                {
                    PlayerNetwork.Instance.ColocarPuebloServerRpc(id, color, currPrefPueblo, colliderName, position);
                }
                else
                {
                    Debug.Log("Esa base no es tuya");
                }
            }
            if (PlayerNetwork.Instance.playerData[indexJugador].cantidadPueblos <= 0) // CHEQUEAR QUE EL CLIENTE PUEDA DESACTIVAR EL BOTON TAMBIEN
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
        var objetoCamino = Resources.Load(currentPrefabCamino) as GameObject;
        currentCamino = Instantiate(objetoCamino, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
        currentCamino.GetComponent<NetworkObject>().Spawn();
        Debug.Log("nombre collider CAMINO " + hit.collider.gameObject.name);
        var nombrecollider = hit.collider.gameObject.name;
        var nombreSinClone = CollidersList.Instance.RemoverCloneDeNombre(nombrecollider);
        Debug.Log("nombreSinClone = " + nombreSinClone);
        CollidersListCaminos.Instance.ModificarHayCaminoYColorPorNombre(nombreSinClone,color);
        CollidersList.Instance.ModificarTipoPorNombre(nombreSinClone, "Camino");
        CollidersList.Instance.ModificarColorPorNombre(nombreSinClone, color);
        CollidersList.Instance.ImprimirColliderPorNombre(nombreSinClone);
        /*comprobarObjeto = hit.collider.gameObject.GetComponent<ComprobarObjeto>();
        if (comprobarObjeto != null)
        {
            var nombreSinClone = CollidersList.Instance.RemoverCloneDeNombre(comprobarObjeto.name);
            Debug.Log("nombreSinClone = " + nombreSinClone);
            CollidersList.Instance.ModificarTipoPorNombre(nombreSinClone, "Camino");
            CollidersList.Instance.ModificarColorPorNombre(nombreSinClone, color);
            CollidersList.Instance.ImprimirColliderPorNombre(nombreSinClone);
        }
        else
        {
            Debug.LogError("El objeto " + hit.collider.gameObject.name + " no tiene un script ComprobarObjeto.");
        }*/
        tipoActual = "Ninguno";
    }
    public void EjecutarColocarBase(RaycastHit hit, string color, string currentPrefabBase)
    {
        Debug.Log("EntroColocar 2");
        int id = PlayerPrefs.GetInt("jugadorId");
        var objetoBase = Resources.Load(currentPrefabBase) as GameObject;
        currentBase = Instantiate(objetoBase, hit.collider.gameObject.transform.position, Quaternion.identity);
        gameObjectsByID.Add(currentBase.GetInstanceID(), currentBase);
        currentBase.GetComponent<NetworkObject>().Spawn();
        int idBase = currentBase.GetInstanceID();
        //Debug.Log("id base INSTANCIADA " + idBase);       
        Debug.Log("nombre collider BASE " + hit.collider.gameObject.name);
        var nombrecollider = hit.collider.gameObject.name;
        var nombreSinClone = CollidersList.Instance.RemoverCloneDeNombre(nombrecollider);
        Debug.Log("nombreSinClone = " + nombreSinClone);
        CollidersList.Instance.ModificarTipoPorNombre(nombreSinClone, "Base");
        CollidersList.Instance.ModificarColorPorNombre(nombreSinClone, color);
        CollidersList.Instance.ModificarIdPiezaPorNombre(nombreSinClone, idBase);
        CollidersList.Instance.ImprimirColliderPorNombre(nombreSinClone);
        /*if (NetworkManager.Singleton.IsServer)
        {
            PlayerNetwork.Instance.SetPuntajebyId(id, 1);
        }
        else // si es un cliente
        {
            Debug.Log("Antes de SetPuntajebyIdServerRpc");
            PlayerNetwork.Instance.SetPuntajebyIdServerRpc(id, 1);
            Debug.Log("Despues de SetPuntajebyIdServerRpc");
        }*/
        tipoActual = "Ninguno";
    }

    public void EjecutarColocarPueblo(RaycastHit hit, string color, string currentPrefabPueblo)
    {
        Debug.Log("EntroColocar 2");
        int id = PlayerPrefs.GetInt("jugadorId");
        var objetoPueblo = Resources.Load(currentPrefabPueblo) as GameObject;
        Debug.Log("2 preafb pueblo es " + objetoPueblo.name);
        currentPueblo = Instantiate(objetoPueblo, hit.collider.gameObject.transform.position, Quaternion.identity);
        currentPueblo.GetComponent<NetworkObject>().Spawn();
        Debug.Log("nombre collider Pueblo " + hit.collider.gameObject.name);
        var nombrecollider = hit.collider.gameObject.name;
        var nombreSinClone = CollidersList.Instance.RemoverCloneDeNombre(nombrecollider);
        Debug.Log("nombreSinClone = " + nombreSinClone);
        int idbase = CollidersList.Instance.GetIdPiezaPorNombre(nombreSinClone);
        var colorCollider = CollidersList.Instance.GetColorPorNombre(nombreSinClone);
        Debug.Log("color es : " + color + " y colorcollider es:" + colorCollider);
        if (gameObjectsByID.ContainsKey(idbase) && color == colorCollider)
        {
            GameObject baseToDespawn = gameObjectsByID[idbase];
            baseToDespawn.GetComponent<NetworkObject>().Despawn(); // Despawn using your networking library
            Destroy(baseToDespawn); // Destroy the object if needed
            gameObjectsByID.Remove(idbase);
        }
        CollidersList.Instance.ModificarTipoPorNombre(nombreSinClone, "Pueblo");
        CollidersList.Instance.ModificarColorPorNombre(nombreSinClone, color);
        CollidersList.Instance.ImprimirColliderPorNombre(nombreSinClone);           
        /*if (NetworkManager.Singleton.IsServer)
        {
            PlayerNetwork.Instance.SetPuntajebyId(id, 2);
        }
        else // si es un cliente
        {
            Debug.Log("Antes de SetPuntajebyIdServerRpc");
            PlayerNetwork.Instance.SetPuntajebyIdServerRpc(id, 2);
            Debug.Log("Despues de SetPuntajebyIdServerRpc");
        }*/
        tipoActual = "Ninguno";                             
    }
    public void AllowPlace() // Método que permitiría colocar una base, podría ser llamado por un botón
    {
        canPlace = true;
    }

}
