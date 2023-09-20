using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;
using static PlayerNetwork;
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
    public Text Puntaje1;
    public Text Puntaje2;
    public Text Puntaje3;
    public Text Puntaje4;
    public Text Nombre1;
    public Text Nombre2;
    public Text Nombre3;
    public Text Nombre4;
    private IdentificadorParcela identificadorParcela;
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
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {

        }
        else
        {

        }
    }
    public void ManejoParcelas(int diceNumber)
    {
        // Obtener las parcelas correspondientes al número del dado.
        string parcelName = "Parcela " + diceNumber.ToString();
        Debug.Log("parcelaName: " + parcelName);
        parcela = null;
        parcela2 = null;
        recurso1 = null;
        recurso2 = null;

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

        Debug.Log("Parcela 1 es: " + parcela);
        Debug.Log("Recurso 1 es: " + recurso1);
        if (parcela2 != null)
        {
            Debug.Log("Parcela 2 es: " + parcela2);
            Debug.Log("Recurso 2 es: " + recurso2);
        }
        
        //parcela = GameObject.Find("Parcela 5 Piedra");
        //recurso1 = "Piedra";
        //recurso2 = null;
        if (recurso2 == null)
        {
            identificadorParcela = parcela.GetComponent<IdentificadorParcela>();
            List<Collider> collidersParcela = identificadorParcela.GetCollidersParcela(parcela.name);
            foreach (var empty in collidersParcela)
            {
                int idJugador = 0;
                Debug.Log("el collider de la parcela se llama " + empty.name);
                var tipoCollider = ListaColliders.Instance.GetTipoPorNombre(empty.name).ToString();
                //OBTENER COLOR DE LA PIEZA COLOCADA EN ESE COLLIDER
                var colorCollider = ListaColliders.Instance.GetColorPorNombre(empty.name).ToString();
                Debug.Log("tipocollider es " + tipoCollider);
                Debug.Log("colorcollider es " + colorCollider);

                idJugador = PlayerNetwork.Instance.GetPlayerByColor(colorCollider);
                if (colorCollider != "Vacio")
                {
                    switch (tipoCollider)
                    {
                        case "Ninguno":  // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Ninguno");
                            break;
                        case "Camino":  // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Camino");
                            break;
                        case "Base":    // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Sumar a la Base 1 de " + recurso1+" al id "+idJugador);                           
                            PlayerNetwork.Instance.AumentarRecursos(idJugador, recurso1, 1);
                            //UpdateResourceTexts(idJugador);
                            //UpdateResources(idJugador);
                            //PlayerNetwork.Instance.ImprimirJugadorPorId(idJugador);
                            Debug.Log("ya sumo recurso " + recurso1);
                            break;
                        case "Pueblo":  // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Sumar Pueblo 2 de " + recurso1 + " al id " + idJugador);
                            //Debug.Log("Id jugador que va a AumentarRecursos " + idJugador);
                            PlayerNetwork.Instance.AumentarRecursos(idJugador, recurso1, 2);
                            //UpdateResourceTexts(idJugador);
                            //UpdateResources(idJugador);
                            //PlayerNetwork.Instance.ImprimirJugadorPorId(idJugador);
                            Debug.Log("ya sumo recurso " + recurso1);
                            break;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Primera parte");
            identificadorParcela = parcela.GetComponent<IdentificadorParcela>();
            List<Collider> collidersParcela = identificadorParcela.GetCollidersParcela(parcela.name);
            foreach (var empty in collidersParcela)
            {
                int idJugador = 0;
                //Debug.Log("el collider de la parcela se llama " + empty.name);
                var tipoCollider = ListaColliders.Instance.GetTipoPorNombre(empty.name).ToString();
                //OBTENER COLOR DE LA PIEZA COLOCADA EN ESE COLLIDER
                var colorCollider = ListaColliders.Instance.GetColorPorNombre(empty.name).ToString();
                //Debug.Log("tipocollider es " + tipoCollider);
                //Debug.Log("colorcollider es " + colorCollider);
                idJugador = PlayerNetwork.Instance.GetPlayerByColor(colorCollider);
                if (colorCollider != "Vacio")
                {
                    switch (tipoCollider)
                    {
                        case "Ninguno":  // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Ninguno");
                            break;
                        case "Camino":  // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Camino");
                            break;
                        case "Base":    // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Sumar a la Base 1 de " + recurso1 + " al id " + idJugador);
                            PlayerNetwork.Instance.AumentarRecursos(idJugador, recurso1, 1);
                            //UpdateResourceTexts(idJugador);
                            //UpdateResources(idJugador);
                            //PlayerNetwork.Instance.ImprimirJugadorPorId(idJugador);
                            Debug.Log("ya sumo recurso " + recurso1);
                            break;
                        case "Pueblo":  // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Sumar Pueblo 2 de " + recurso1 + " al id " + idJugador);
                            //Debug.Log("Id jugador que va a AumentarRecursos " + idJugador);
                            PlayerNetwork.Instance.AumentarRecursos(idJugador, recurso1, 2);
                            //UpdateResourceTexts(idJugador);
                            //UpdateResources(idJugador);
                            //PlayerNetwork.Instance.ImprimirJugadorPorId(idJugador);
                            Debug.Log("ya sumo recurso " + recurso1);
                            break;
                    }
                }
            }      
            Debug.Log("Segunda parte");
            identificadorParcela = parcela2.GetComponent<IdentificadorParcela>();
            List<Collider> collidersParcela2 = identificadorParcela.GetCollidersParcela(parcela2.name);
            foreach (var empty in collidersParcela2)
            {
                int idJugador = 0;
                Debug.Log("el collider de la parcela se llama " + empty.name);
                var tipoCollider = ListaColliders.Instance.GetTipoPorNombre(empty.name).ToString();
                //OBTENER COLOR DE LA PIEZA COLOCADA EN ESE COLLIDER
                var colorCollider = ListaColliders.Instance.GetColorPorNombre(empty.name).ToString();
                //Debug.Log("tipocollider es " + tipoCollider);
                //Debug.Log("colorcollider es " + colorCollider);
                idJugador = PlayerNetwork.Instance.GetPlayerByColor(colorCollider);
                if (colorCollider != "Vacio")
                {
                    switch (tipoCollider)
                    {
                        case "Ninguno":  // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Ninguno");
                            break;
                        case "Camino":  // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Camino");
                            break;
                        case "Base":    // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Sumar a la Base 1 de " + recurso2 + " al id " + idJugador);
                            //Debug.Log("Id jugador que va a AumentarRecursos " + idJugador);
                            PlayerNetwork.Instance.AumentarRecursos(idJugador, recurso2, 1);
                            //UpdateResourceTexts(idJugador);
                            //UpdateResources(idJugador);
                            //PlayerNetwork.Instance.ImprimirJugadorPorId(idJugador);
                            Debug.Log("ya sumo recurso " + recurso2);
                            break;
                        case "Pueblo":  // Aquí se hace uso del tipo enumerado TipoObjeto
                            Debug.Log("Sumar Pueblo 2 de " + recurso2 + " al id " + idJugador);
                            //Debug.Log("Id jugador que va a AumentarRecursos " + idJugador);
                            PlayerNetwork.Instance.AumentarRecursos(idJugador, recurso2, 2);
                            //UpdateResourceTexts(idJugador);
                            //UpdateResources(idJugador);
                            //PlayerNetwork.Instance.ImprimirJugadorPorId(idJugador);
                            Debug.Log("ya sumo recurso " + recurso2);
                            break;
                    }
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
        jugadorcopia.maderaCount -= 1;
        jugadorcopia.ladrilloCount -= 1;

        PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
        //PlayerNetwork.Instance.playerData[jugador.jugadorId] = jugador;
        //Debug.Log("Impimir del UpdateResourceBase");
        //PlayerNetwork.Instance.ImprimirJugador(PlayerNetwork.Instance.playerData[indexJugador]);
        UpdateResources(indexJugador);
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
        UpdateResources(indexJugador);
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
        UpdateResources(indexJugador);
    }
    public void UpdateResources(int jugadorId)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            UpdateResourceTexts(jugadorId);
            foreach (PlayerNetwork.DatosJugador player in PlayerNetwork.Instance.playerData)
            {
                PlayerNetwork.Instance.UpdateResourcesTextClientRpc(player);
            }
        }
        else
        {
            UpdateResourceTexts(jugadorId);
        }
            

    }
    public void UpdateResourceTextsHost(int jugadorId)
    {
        Debug.Log("Server a UpdateResourceTexts con ID " + jugadorId);
        PlayerNetwork.DatosJugador jugador = default;
        // Itera sobre los elementos de playerData para encontrar los datos del jugador
        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            Debug.Log("ENTRE ACA 1");
            if (PlayerNetwork.Instance.playerData[i].jugadorId == jugadorId)
            {
                Debug.Log("ENTRE ACA 2");
                jugador = PlayerNetwork.Instance.playerData[i];
                var puntaje = "Puntaje" + (i+1).ToString();
                Debug.Log("string puntaje es: " + puntaje);

                switch (puntaje)
                {
                    case "Puntaje1":
                        Puntaje1.text = jugador.puntaje.ToString();
                        Nombre1.text = jugador.nomJugador.ToString();
                        break;
                    case "Puntaje2":
                        Puntaje2.text = jugador.puntaje.ToString();
                        Nombre2.text = jugador.nomJugador.ToString();
                        break;
                    case "Puntaje3":
                        Puntaje3.text = jugador.puntaje.ToString();
                        Nombre3.text = jugador.nomJugador.ToString();
                        break;
                    case "Puntaje4":
                        Puntaje4.text = jugador.puntaje.ToString();
                        Nombre4.text = jugador.nomJugador.ToString();
                        break;
                }
            }
        }
        /*if (datosJugador.jugadorId == 0)  // Suponiendo que 0 no es un ID de jugador v�lido
        {
            Debug.LogError("Jugador con ID " + jugadorId + " no encontrado.");
            return;
        }*/
        // Actualiza los textos de los recursos
        MaderaCountText.text = jugador.maderaCount.ToString();
        LadrilloCountText.text = jugador.ladrilloCount.ToString();
        OvejaCountText.text = jugador.ovejaCount.ToString();
        PiedraCountText.text = jugador.piedraCount.ToString();
        TrigoCountText.text = jugador.trigoCount.ToString();   
        
    }
    public void UpdateResourceTexts(int jugadorId)
    {

        if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log("Server a UpdateResourceTexts con ID " + jugadorId);
            PlayerNetwork.DatosJugador jugador = default;
            // Itera sobre los elementos de playerData para encontrar los datos del jugador
            for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
            {
                if (PlayerNetwork.Instance.playerData[i].jugadorId == jugadorId)
                {
                    jugador = PlayerNetwork.Instance.playerData[i];
                    break;
                }
            }
            /*if (datosJugador.jugadorId == 0)  // Suponiendo que 0 no es un ID de jugador v�lido
            {
                Debug.LogError("Jugador con ID " + jugadorId + " no encontrado.");
                return;
            }*/
            // Actualiza los textos de los recursos
            MaderaCountText.text = jugador.maderaCount.ToString();
            LadrilloCountText.text = jugador.ladrilloCount.ToString();
            OvejaCountText.text = jugador.ovejaCount.ToString();
            PiedraCountText.text = jugador.piedraCount.ToString();
            TrigoCountText.text = jugador.trigoCount.ToString();
        }
        else
        {
            if(PlayerPrefs.GetInt("jugadorId") == jugadorId)
            {
                Debug.Log("Cliente a UpdateResourceTexts con ID " + jugadorId);
                //PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(jugadorId);
                PlayerNetwork.Instance.UpdateResourcesTextServerRpc(jugadorId);
            }

        }     
    }
    /*[ClientRpc]
    public void UpdateResourceTextsClientRpc(int jugadorId)
    {
        Debug.Log("Entre a UpdateResourceTextsClientRpc con ID " + jugadorId);
        //BoardManager.Instance.UpdateResourceTexts(jugadorId);
        //Debug.Log("Termino UpdateResourceTextsServerRpc");
        //Debug.Log("Entre a UpdateResourceTexts con ID " + jugadorId);
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
        // Actualiza los textos de los recursos
        MaderaCountText.text = datosJugador.maderaCount.ToString();
        LadrilloCountText.text = datosJugador.ladrilloCount.ToString();
        OvejaCountText.text = datosJugador.ovejaCount.ToString();
        PiedraCountText.text = datosJugador.piedraCount.ToString();
        TrigoCountText.text = datosJugador.trigoCount.ToString();
        Debug.Log("Termino UpdateResourceTextsServerRpc");
    }*/

}