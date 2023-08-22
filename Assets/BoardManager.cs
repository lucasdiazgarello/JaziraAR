using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;
//using static PlayerNetwork;

public class BoardManager : NetworkBehaviour
{
    private GameObject parcela;
    private GameObject parcela2;
    private string recurso1;
    private string recurso2;
    public Text MaderaCountText;
    public Text LadrilloCountText;
    public Text OvejaCountText;
    public Text PiedraCountText;
    public Text TrigoCountText;

    private IdentificadorParcela identificadorParcela;
    private ComprobarObjeto comprobarObjeto;
    public static BoardManager Instance; // Instancia Singleton

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
    // Llamar a esta función cuando se tira el dado.
    void Start()
    {
        // Obtén el ID del jugador desde donde lo tengas almacenado.
        // En este ejemplo, simplemente lo he establecido como 1.
        //int currentPlayerID = PlayerPrefs.GetInt("jugadorId");
        //Instance.UpdateResourceTexts(currentPlayerID);
        // Actualiza los textos de recursos para el jugador al inicio.
        //UpdateResourceTexts(jugadorId);
    }

    public void ManejoParcelas(int diceNumber, int idJugador)
    {
        Debug.Log("Manejo Parcelas ID: " + idJugador);
        // Obtener las parcelas correspondientes al número del dado.
        string parcelName = "Parcela " + diceNumber.ToString();
        Debug.Log("parcelaName: " + parcelName);
        switch (parcelName)
        {
            case "Parcela 2":
                Debug.Log("Busque parcela 2");
                parcela = GameObject.Find("Parcela 2 Trigo");
                if (parcela == null) Debug.Log("parcela vacia");
                recurso1 = "Trigo";
                break;
            case "Parcela 3":
                Debug.Log("Busque parcela 3");
                parcela = GameObject.Find("Parcela 3 Trigo");
                if (parcela == null) Debug.Log("parcela vacia");
                parcela2 = GameObject.Find("Parcela 3 Madera");
                recurso1 = "Trigo";
                recurso2 = "Madera";
                break;
            case "Parcela 4":
                Debug.Log("Busque parcela 4");
                parcela = GameObject.Find("Parcela 4 Ladrillo");
                if (parcela == null) Debug.Log("parcela vacia");
                parcela2 = GameObject.Find("Parcela 4 Madera");
                recurso1 = "Ladrillo";
                recurso2 = "Madera";
                break;
            case "Parcela 5":
                Debug.Log("Busque parcela 5");
                parcela = GameObject.Find("Parcela 5 Ladrillo");
                if (parcela == null) Debug.Log("parcela vacia");
                parcela2 = GameObject.Find("Parcela 5 Piedra");
                recurso1 = "Ladrillo";
                recurso2 = "Piedra";
                break;
            case "Parcela 6":
                Debug.Log("Busque parcela 6");
                parcela = GameObject.Find("Parcela 6 Piedra");
                if (parcela == null) Debug.Log("parcela vacia");
                parcela2 = GameObject.Find("Parcela 6 Madera");
                recurso1 = "Piedra";
                recurso2 = "Madera";
                break;
            case "Parcela 8":
                Debug.Log("Busque parcela 8");
                parcela = GameObject.Find("Parcela 8 Trigo");
                if (parcela == null) Debug.Log("parcela vacia");
                parcela2 = GameObject.Find("Parcela 8 Ladrillo");
                recurso1 = "Trigo";
                recurso2 = "Ladrillo";
                break;
            case "Parcela 9":
                Debug.Log("Busque parcela 9");
                parcela = GameObject.Find("Parcela 9 Trigo");
                if (parcela == null) Debug.Log("parcela vacia");
                parcela2 = GameObject.Find("Parcela 9 Oveja");
                recurso1 = "Trigo";
                recurso2 = "Oveja";
                break;
            case "Parcela 10":
                Debug.Log("Busque parcela 10");
                parcela = GameObject.Find("Parcela 10 Oveja");
                if (parcela == null) Debug.Log("parcela vacia");
                parcela2 = GameObject.Find("Parcela 10 Oveja");
                recurso1 = "Oveja";
                recurso2 = "Oveja";
                break;
            case "Parcela 11":
                Debug.Log("Busque parcela 11");
                parcela = GameObject.Find("Parcela 11 Trigo");
                if (parcela == null) Debug.Log("parcela vacia");
                parcela2 = GameObject.Find("Parcela 11 Madera");
                recurso1 = "Trigo";
                recurso2 = "Madera";
                break;
            case "Parcela 12":
                Debug.Log("Busque parcela 12");
                parcela = GameObject.Find("Parcela 12 Oveja");
                if (parcela == null) Debug.Log("parcela vacia");
                recurso1 = "Oveja";
                break;

        }
        parcela = GameObject.Find("Parcela 5 Piedra");
        recurso1 = "Piedra";
        recurso2 = null;
        if (recurso2 == null)
        {
            identificadorParcela = parcela.GetComponent<IdentificadorParcela>();
            List<Collider> collidersParcela = identificadorParcela.GetCollidersParcela(parcela.name);
            if (collidersParcela.Count > 0)
            {
                comprobarObjeto = collidersParcela[0].gameObject.GetComponent<ComprobarObjeto>();
            }
            foreach (var empty in collidersParcela)
            {
                
                comprobarObjeto = empty.gameObject.GetComponent<ComprobarObjeto>();
                if (comprobarObjeto == null)
                {
                    Debug.LogError("No se pudo obtener el componente ComprobarObjeto de " + empty.gameObject.name);
                }
                if (comprobarObjeto != null)
                {
                    Debug.Log("comprobarObjeto no es null");                   
                    Debug.Log("Nombre de collider " + comprobarObjeto.name);
                    var tipoCollider = ListaColliders.Instance.GetTipoPorNombre(comprobarObjeto.name).ToString();
                    //OBTENER COLOR DE LA PIEZA COLOCADA EN ESE COLLIDER
                    var colorCollider = ListaColliders.Instance.GetColorPorNombre(comprobarObjeto.name).ToString();
                    Debug.Log("tipocollider es " + tipoCollider);
                    Debug.Log("colorcollider es " + colorCollider);
                    if (colorCollider != "Vacio")
                    {
                        PlayerNetwork.DatosJugador jugador = (PlayerNetwork.DatosJugador)PlayerNetwork.Instance.GetPlayerByColor(colorCollider);
                        int id = PlayerNetwork.Instance.GetPlayerId(jugador); //USAR ESTE ID PARA QUE AUMETNE RECURSO. 
                        switch (tipoCollider)
                        {
                            case "Ninguno":  // Aquí se hace uso del tipo enumerado TipoObjeto
                                Debug.Log("Ninguno");
                                break;
                            case "Camino":  // Aquí se hace uso del tipo enumerado TipoObjeto
                                Debug.Log("Camino");
                                break;
                            case "Base":    // Aquí se hace uso del tipo enumerado TipoObjeto
                                Debug.Log("Sumar a la Base 1 de " + recurso1);
                                Debug.Log("Id jugador que va a AumentarRecursos " + id);
                                PlayerNetwork.Instance.AumentarRecursos(id, recurso1, 1);
                                Instance.UpdateResourceTexts(id);
                                PlayerNetwork.Instance.ImprimirJugadorPorId(id);
                                Debug.Log("ya sumo recurso " + recurso1);
                                break;
                            case "Pueblo":  // Aquí se hace uso del tipo enumerado TipoObjeto
                                Debug.Log("Pueblo");
                                //PlayerNetwork.Instance.AumentarRecursos(idJugador, recurso, 2);
                                Debug.Log("sumo 2 " + recurso1);
                                break;
                        }
                    }                                                       
                }
                else
                {
                    Debug.LogError("El objeto " + empty.name + " no tiene un script de ComprobarObjeto.");
                }
            }
        }
        else
        {
            Debug.Log("Primera parte");
            identificadorParcela = parcela.GetComponent<IdentificadorParcela>();
            List<Collider> collidersParcela = identificadorParcela.GetCollidersParcela(parcela.name);
            if (collidersParcela.Count > 0)
            {
                comprobarObjeto = collidersParcela[0].gameObject.GetComponent<ComprobarObjeto>();
            }
            foreach (var empty in collidersParcela)
            {
                comprobarObjeto = empty.gameObject.GetComponent<ComprobarObjeto>();
                if (comprobarObjeto == null)
                {
                    Debug.LogError("No se pudo obtener el componente ComprobarObjeto de " + empty.gameObject.name);
                }
                if (comprobarObjeto != null)
                {
                    Debug.Log("comprobarObjeto no es null");
                    Debug.Log("Nombre de collider " + comprobarObjeto.name);

                    var tipocolider = ListaColliders.Instance.GetTipoPorNombre(comprobarObjeto.name).ToString();
                    Debug.Log("tipocollider es " + tipocolider);
                    switch (tipocolider)
                    {
                        case "Ninguno":  // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Ninguno");
                            break;
                        case "Camino":  // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Camino");
                            break;
                        case "Base":    // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Sumar a la Base 1 de " + recurso1);
                            //int currentPlayerID = PlayerPrefs.GetInt("jugadorId");
                            //int currentPlayerID = TurnManager.Instance.CurrentPlayerID;
                            //Debug.Log("CurrentPlayerID es " + currentPlayerID);
                            Debug.Log("Id jugador que va a AumentarRecursos " + idJugador);
                            PlayerNetwork.Instance.AumentarRecursos(idJugador, recurso1, 1);
                            Instance.UpdateResourceTexts(idJugador);
                            PlayerNetwork.Instance.ImprimirJugadorPorId(idJugador);
                            Debug.Log("ya sumo recurso " + recurso1);
                            /*if (NetworkManager.Singleton.IsServer)
                            {
                                Debug.Log("Soy server aumentando recursos");
                                int hostPlayerID = PlayerPrefs.GetInt("jugadorId");
                                //int currentPlayerID = TurnManager.Instance.CurrentPlayerID;
                                Debug.Log("CurrentPlayerID es " + hostPlayerID);
                                PlayerNetwork.Instance.AumentarRecursos(hostPlayerID, recurso1, 1);
                                Instance.UpdateResourceTexts(hostPlayerID);
                                Debug.Log("ya sumo recurso " + recurso1);
                            }
                            else
                            {
                                Debug.Log("Soy cliente aumentando recursos");
                                int currentPlayerID = PlayerPrefs.GetInt("jugadorId");
                                //int currentPlayerID = TurnManager.Instance.CurrentPlayerID;
                                Debug.Log("CurrentPlayerID es " + currentPlayerID);
                                PlayerNetwork.Instance.AumentarRecursosServerRpc(currentPlayerID, recurso1, 1);
                                PlayerNetwork.Instance.UpdateResourceTextsServerRpc(currentPlayerID);
                                Debug.Log("ya sumo recurso " + recurso1);
                            }*/
                            break;
                        case "Pueblo":  // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Pueblo");
                            //PlayerNetwork.Instance.AumentarRecursos(idJugador, recurso, 2);
                            Debug.Log("sumo 2 " + recurso1);
                            break;
                    }
                }
                else
                {
                    Debug.LogError("El objeto " + empty.name + " no tiene un script de ComprobarObjeto.");
                }
            }
            Debug.Log("Segunda parte");
            identificadorParcela = parcela.GetComponent<IdentificadorParcela>();
            List<Collider> collidersParcela2 = identificadorParcela.GetCollidersParcela(parcela.name);
            if (collidersParcela2.Count > 0)
            {
                comprobarObjeto = collidersParcela2[0].gameObject.GetComponent<ComprobarObjeto>();
            }
            foreach (var empty in collidersParcela2)
            {
                comprobarObjeto = empty.gameObject.GetComponent<ComprobarObjeto>();
                if (comprobarObjeto == null)
                {
                    Debug.LogError("No se pudo obtener el componente ComprobarObjeto de " + empty.gameObject.name);
                }
                if (comprobarObjeto != null)
                {
                    Debug.Log("comprobarObjeto no es null");
                    Debug.Log("Nombre de collider " + comprobarObjeto.name);

                    var tipocolider = ListaColliders.Instance.GetTipoPorNombre(comprobarObjeto.name).ToString();
                    Debug.Log("tipocollider es " + tipocolider);
                    switch (tipocolider)
                    {
                        case "Ninguno":  // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Ninguno");
                            break;
                        case "Camino":  // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Camino");
                            break;
                        case "Base":    // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Sumar a la Base 1 de " + recurso2);
                            //int currentPlayerID = PlayerPrefs.GetInt("jugadorId");
                            //int currentPlayerID = TurnManager.Instance.CurrentPlayerID;
                            //Debug.Log("CurrentPlayerID es " + currentPlayerID);
                            Debug.Log("Id jugador que va a AumentarRecursos " + idJugador);
                            PlayerNetwork.Instance.AumentarRecursos(idJugador, recurso2, 1);
                            Debug.Log("Id jugador que va a UpdatearTextos " + idJugador);
                            Instance.UpdateResourceTexts(idJugador);
                            PlayerNetwork.Instance.ImprimirJugadorPorId(idJugador);
                            Debug.Log("ya sumo recurso " + recurso1);
                            /*if (NetworkManager.Singleton.IsServer)
                            {
                                Debug.Log("Soy server aumentando recursos");
                                int hostPlayerID = PlayerPrefs.GetInt("jugadorId");
                                //int currentPlayerID = TurnManager.Instance.CurrentPlayerID;
                                Debug.Log("HostPlayerID es " + hostPlayerID);
                                PlayerNetwork.Instance.AumentarRecursos(hostPlayerID, recurso2, 1);
                                Instance.UpdateResourceTexts(hostPlayerID);
                                Debug.Log("ya sumo recurso " + recurso2);
                            }
                            else
                            {
                                Debug.Log("Soy cliente aumentando recursos");
                                int currentPlayerID = PlayerPrefs.GetInt("jugadorId");
                                //int currentPlayerID = TurnManager.Instance.CurrentPlayerID;
                                Debug.Log("CurrentPlayerID es " + currentPlayerID);
                                PlayerNetwork.Instance.AumentarRecursosServerRpc(currentPlayerID, recurso2, 1);
                                PlayerNetwork.Instance.UpdateResourceTextsServerRpc(currentPlayerID);
                                Debug.Log("ya sumo recurso " + recurso2);
                            }*/
                            break;
                        case "Pueblo":  // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Pueblo");
                            //PlayerNetwork.Instance.AumentarRecursos(idJugador, recurso, 2);
                            Debug.Log("sumo 2 " + recurso2);
                            break;
                    }
                }
                else
                {
                    Debug.LogError("El objeto " + empty.name + " no tiene un script de ComprobarObjeto.");
                }
            }
        }
    }
    public void UpdateResourcesCamino(PlayerNetwork.DatosJugador jugador) //se usa para disminuir los recursos solamente
    {
        Debug.Log("Entre a UpdateResourceCamino");
        int indexJugador = -1;
        bool jugadorEncontrado = false;

        // Búsqueda del jugador en la lista playerData
        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            //Debug.Log("La lista Ids es " + playerData[i].jugadorId);
            if (PlayerNetwork.Instance.playerData[i].jugadorId == jugador.jugadorId)
            {
                jugadorEncontrado = true;
                indexJugador = i;
                Debug.Log("Jugador encontrado en la posición: " + i);
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
        //jugadorcopia.maderaCount -= 1;
        //jugadorcopia.ladrilloCount -= 1;
        // Reducir los recursos usando NetworkVariable

        PlayerNetwork.Instance.maderaCount.Value -= 1;
        PlayerNetwork.Instance.ladrilloCount.Value -= 1;

        PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
        //PlayerNetwork.Instance.playerData[jugador.jugadorId] = jugador;
        //Debug.Log("Impimir del UpdateResourceCamino");
        //PlayerNetwork.Instance.ImprimirJugador(PlayerNetwork.Instance.playerData[indexJugador]);
        UpdateResourceTexts(indexJugador);
    }
    public void UpdateResourcesBase(PlayerNetwork.DatosJugador jugador) //se usa para disminuir los recursos solamente
    {
        Debug.Log("Entre a UpdateResourceBase");
        int indexJugador = -1;
        bool jugadorEncontrado = false;

        // Búsqueda del jugador en la lista playerData
        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            //Debug.Log("La lista Ids es " + playerData[i].jugadorId);
            if (PlayerNetwork.Instance.playerData[i].jugadorId == jugador.jugadorId)
            {
                jugadorEncontrado = true;
                indexJugador = i;
                Debug.Log("Jugador encontrado en la posición: " + i);
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
        jugadorcopia.maderaCount -= 1;
        jugadorcopia.ladrilloCount -= 1;
        jugadorcopia.trigoCount -= 1;
        jugadorcopia.ovejaCount -= 1;

        PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
        //PlayerNetwork.Instance.playerData[jugador.jugadorId] = jugador;
        //Debug.Log("Impimir del UpdateResourceBase");
        //PlayerNetwork.Instance.ImprimirJugador(PlayerNetwork.Instance.playerData[indexJugador]);
        UpdateResourceTexts(indexJugador);
    }
    public void UpdateResourcesPueblo(PlayerNetwork.DatosJugador jugador) //se usa para disminuir los recursos solamente
    {
        Debug.Log("Entre a UpdateResourcePueblo");
        int indexJugador = -1;
        bool jugadorEncontrado = false;

        // Búsqueda del jugador en la lista playerData
        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            //Debug.Log("La lista Ids es " + playerData[i].jugadorId);
            if (PlayerNetwork.Instance.playerData[i].jugadorId == jugador.jugadorId)
            {
                jugadorEncontrado = true;
                indexJugador = i;
                Debug.Log("Jugador encontrado en la posición: " + i);
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
        jugadorcopia.trigoCount -= 3;
        jugadorcopia.piedraCount -= 2;

        PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
        //PlayerNetwork.Instance.playerData[jugador.jugadorId] = jugador;
        //Debug.Log("Impimir del UpdateResourceBase");
        //PlayerNetwork.Instance.ImprimirJugador(PlayerNetwork.Instance.playerData[indexJugador]);
        UpdateResourceTexts(indexJugador);
    }
    public void UpdateResourceTexts(int jugadorId)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log("Server a UpdateResourceTexts con ID " + jugadorId);
            PlayerNetwork.DatosJugador datosJugador = default;
            // Itera sobre los elementos de playerData para encontrar los datos del jugador
            for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
            {
                if (PlayerNetwork.Instance.playerData[i].jugadorId == jugadorId)
                {
                    datosJugador = PlayerNetwork.Instance.playerData[i];
                    break;
                }
            }
            /*if (datosJugador.jugadorId == 0)  // Suponiendo que 0 no es un ID de jugador v�lido
            {
                Debug.LogError("Jugador con ID " + jugadorId + " no encontrado.");
                return;
            }*/
            // Actualiza los textos de los recursos
            MaderaCountText.text = datosJugador.maderaCount.ToString();
            LadrilloCountText.text = datosJugador.ladrilloCount.ToString();
            OvejaCountText.text = datosJugador.ovejaCount.ToString();
            PiedraCountText.text = datosJugador.piedraCount.ToString();
            TrigoCountText.text = datosJugador.trigoCount.ToString();
        }
        else
        {
            Debug.Log("Cliente a UpdateResourceTexts con ID " + jugadorId);
            PlayerNetwork.Instance.UpdateResourceTextsServerRpc(jugadorId);
        }     
    }

}