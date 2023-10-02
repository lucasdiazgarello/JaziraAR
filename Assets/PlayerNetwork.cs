using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Collections;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEditor;
using System.Linq;
//using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem.OSX;
using UnityEngine.UI;
using static PlayerNetwork;
using Unity.VisualScripting;

public class PlayerNetwork : NetworkBehaviour
{
    private GameObject currentBase;
    private GameObject currentCamino;
    private GameObject currentPueblo;
    private GameObject comprarCaminoButton;
    private GameObject comprarBaseButton;
    private GameObject comprarPuebloButton;
    public string tipoActual;
    public Button confirmBaseButton;
    Dictionary<int, GameObject> gameObjectsByIDServer = new Dictionary<int, GameObject>();
    //private bool canPlace = false;
    public bool IsInitialized { get; private set; } = false; // Añade este campo de estado
    public NetworkList<int> playerIDs;
    public int currentTurnIndex = 0;
    public NetworkList<PlayerNetwork.DatosJugador> playerData;
    private Dictionary<int, Dictionary<string, int>> recursosPorJugador = new Dictionary<int, Dictionary<string, int>>();
    private bool todosListos = false;
    private bool diPiezas = false;
    // Singleton instance
    public static PlayerNetwork Instance { get; private set; }
    public object NetworkVariablePermission { get; private set; }
    public NetworkVariable<DatosJugador> jugador;
    /*public Dictionary<int, PlayerResources> recursosPorJugador = new Dictionary<int, PlayerResources>();
    public class PlayerResources
    {
        public NetworkVariable<int> maderaCount = new NetworkVariable<int>(0);
        public NetworkVariable<int> ladrilloCount = new NetworkVariable<int>(0);
        public NetworkVariable<int> ovejaCount = new NetworkVariable<int>(0);
        public NetworkVariable<int> piedraCount = new NetworkVariable<int>(0);
        public NetworkVariable<int> trigoCount = new NetworkVariable<int>(0);
    }
    */
    public NetworkVariable<int> maderaCount = new NetworkVariable<int>(0);
    public NetworkVariable<int> ladrilloCount = new NetworkVariable<int>(0);
    public NetworkVariable<int> ovejaCount = new NetworkVariable<int>(0);
    public NetworkVariable<int> piedraCount = new NetworkVariable<int>(0);
    public NetworkVariable<int> trigoCount = new NetworkVariable<int>(0);
    private object basesRestantes;

    public struct DatosJugador : INetworkSerializable, IEquatable<DatosJugador>
    {
        public int jugadorId;
        public FixedString64Bytes nomJugador;
        public int puntaje;
        public bool gano;
        public bool turno;
        public bool primerasPiezas;
        public int maderaCount;
        public int ladrilloCount;
        public int ovejaCount;
        public int piedraCount;
        public int trigoCount;       
        public FixedString64Bytes colorJugador;
        public int cantidadCaminos;
        public int cantidadBases;
        public int cantidadPueblos;


        public bool Equals(DatosJugador other)
        {
            return jugadorId == other.jugadorId && nomJugador.Equals(other.nomJugador) && colorJugador.Equals(other.colorJugador);
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref jugadorId);
            serializer.SerializeValue(ref puntaje);
            serializer.SerializeValue(ref gano);
            serializer.SerializeValue(ref turno);
            serializer.SerializeValue(ref primerasPiezas);
            serializer.SerializeValue(ref maderaCount);
            serializer.SerializeValue(ref ladrilloCount);
            serializer.SerializeValue(ref ovejaCount);
            serializer.SerializeValue(ref piedraCount);
            serializer.SerializeValue(ref trigoCount);            
            serializer.SerializeValue(ref nomJugador);
            serializer.SerializeValue(ref colorJugador);
            serializer.SerializeValue(ref cantidadCaminos);
            serializer.SerializeValue(ref cantidadBases);
            serializer.SerializeValue(ref cantidadPueblos);
        }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("Instancia de PlayerNetwork");
            Instance = this;
            DontDestroyOnLoad(gameObject); // Para mantener el objeto al cambiar de escena
            Debug.Log("Id de esta instancia de PlayerNetwork " + this.NetworkObjectId);
            // Inicializar los jugadores aqu�
            //NetworkList must be initialized in Awake.
            playerIDs = new NetworkList<int>();
            playerData = new NetworkList<PlayerNetwork.DatosJugador>();
            if (playerIDs == null)
            {
                Debug.Log("La lista playerIDs NO se ha inicializado correctamente.");
            }
            else
            {
                Debug.Log("La lista playerIDs se ha inicializado correctamente.");
            }

            if (playerData == null)
            {
                Debug.Log("La lista playerData NO se ha inicializado correctamente.");
            }
            else
            {
                Debug.Log("La lista playerData se ha inicializado correctamente.");
            }
            // Mover inicializaciones aqu�
            jugador = new NetworkVariable<DatosJugador>(
                new DatosJugador
                {
                    jugadorId = 0,
                    nomJugador = new FixedString64Bytes(),
                    puntaje = 0,
                    gano = false,
                    turno = false,
                    primerasPiezas = false,
                    maderaCount = 0,
                    ladrilloCount = 0,
                    ovejaCount = 0,
                    piedraCount = 0,
                    trigoCount = 0,
                    colorJugador = new FixedString64Bytes(),
                    cantidadCaminos = 0,
                    cantidadBases = 0,
                    cantidadPueblos = 0,

                }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        }
        else
        {
            Debug.LogWarning("Multiple instances of PlayerNetwork detected. Deleting one instance. GameObject: " + gameObject.name);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (NetworkManager.Singleton)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;
        }
    }
    public override void OnDestroy()
    {
        if (NetworkManager.Singleton)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnect;
        }

    }

    void OnClientConnected(ulong clientId)
    {
        Debug.Log($"Client {clientId} connected.");
    }
    void OnClientDisconnect(ulong clientId)
    {
        Debug.Log($"Client {clientId} disconnected.");
    }
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Debug.Log("Entre al IsServer de OnNetworkSpawn");
            int playerId = PlayerPrefs.GetInt("jugadorId");
            FixedString64Bytes nombreHost = new FixedString64Bytes(PlayerPrefs.GetString("nomJugador"));
            FixedString64Bytes colorHost = new FixedString64Bytes(PlayerPrefs.GetString("colorJugador"));
            Debug.Log("el jugador con id:" + playerId + "se llama " + nombreHost + " y es el color " + colorHost);
            AgregarJugador(playerId, nombreHost, 0, false, true, false, 10, 10, 10, 10, 10, colorHost, 2, 2, 0);
            Debug.Log("se agrego jugador host");
            ImprimirJugadorPorId(playerId);
            //ImprimirTodosLosJugadores();
            ARCursor.Instance.EnableRecursos();
            BoardManager.Instance.UpdateResourceTexts(playerId);
        }
        else
        {
            Debug.Log("Entre como cliente de OnNetworkSpawn");
            int playerId = PlayerPrefs.GetInt("jugadorId");
            Debug.Log("id jugador 1 =" + playerId);
            FixedString64Bytes nombreCliente = new FixedString64Bytes(PlayerPrefs.GetString("nomJugador"));
            FixedString64Bytes colorCliente = new FixedString64Bytes(PlayerPrefs.GetString("colorJugador"));
            Debug.Log("el cliente con id:" + playerId + "se llama " + nombreCliente + " y es el color " + colorCliente);
            //AgregarJugador(playerId, nombreCliente, 100, false, true, 2, 10, 10, 10, 10, 10, colorCliente); //EL CLIENTE NO DEBE AGREGARJUGADOR, DEBE MANDAR SU DATA AL HOST
            AddPlayerServerRpc(playerId, nombreCliente, 0, false, false,false, 10, 10, 10, 10, 10, colorCliente, 2, 2, 0);
            Debug.Log("se agrego jugador cliente");
            ImprimirJugadorPorId(playerId);
            //ImprimirTodosLosJugadores();
            ARCursor.Instance.EnableRecursos();
            // Desactivar la detección de planos al confirmar la colocación del tablero
            if (ARCursor.Instance.planeManager)
            {
                ARCursor.Instance.planeManager.enabled = false;
                // Y esto eliminará todos los planos existentes
                foreach (var plane in ARCursor.Instance.planeManager.trackables)
                {
                    Destroy(plane.gameObject);
                }
                // Esto también funcionaría:
                // planeManager.planeDetectionMode = PlaneDetectionMode.None;
            }
            //UpdateResourceTextsServerRpc(playerId);
            PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(playerId);
            PlayerNetwork.Instance.UpdateResourcesTextClientRpc(jugador);
        }
    }

    public void AgregarJugador(int jugadorId, FixedString64Bytes nomJugador, int puntaje, bool gano, bool turno, bool primerasPiezas, int maderaCount, int ladrilloCount, int ovejaCount, int piedraCount, int trigoCount, FixedString64Bytes colorJugador, int cantidadCaminos, int cantidadBases, int cantidadPueblos)
    {

        ImprimirPlayerIDs();
        Debug.Log($"AgregarJugador: {jugadorId}, {nomJugador}, {puntaje}, {gano}, {turno}, {primerasPiezas}, {maderaCount}, {ladrilloCount}, {ovejaCount}, {piedraCount}, {trigoCount}, {colorJugador}, {cantidadCaminos}, {cantidadBases}, {cantidadPueblos}");
        Debug.Log("playerId:" + playerIDs.Count); //.Count dice la cantidad de elementos qeu tiene la lista
        Debug.Log("playerData:" + playerData.Count);

        DatosJugador newDatos = new DatosJugador();
        newDatos.jugadorId = jugadorId;
        newDatos.nomJugador = nomJugador;
        newDatos.puntaje = puntaje;
        newDatos.gano = gano;
        newDatos.turno = turno;
        newDatos.primerasPiezas = primerasPiezas;
        newDatos.maderaCount = maderaCount;
        newDatos.ladrilloCount = ladrilloCount;
        newDatos.ovejaCount = ovejaCount;
        newDatos.piedraCount = piedraCount;
        newDatos.trigoCount = trigoCount;
        newDatos.colorJugador = colorJugador;
        newDatos.cantidadCaminos = cantidadCaminos;
        newDatos.cantidadBases = cantidadBases;
        newDatos.cantidadPueblos = cantidadPueblos;
        Debug.Log("Cargue newDatos");
        try
        {
            if (!playerIDs.Contains(jugadorId) && jugadorId != 1)
            {
                playerIDs.Add(jugadorId);
                Debug.Log("Cant elementos de playerId:" + playerIDs.Count);
                playerData.Add(newDatos);
            }       
        }
        catch (Exception e)
        {
            Debug.LogError("Error al intentar agregar datos a las listas: " + e);
        }
    }

    public void ImprimirPlayerIDs()
    {
        if (playerIDs == null)
        {
            Debug.Log("La lista de IDs de jugadores es null.");
            return; // Si la lista es null, no hay nada más que hacer en este método.
        }

        if (playerIDs.Count == 0)
        {
            Debug.Log("La lista de IDs de jugadores está vacía.");
            return; // Si la lista está vacía, no hay nada más que hacer en este método.
        }

        Debug.Log("Imprimiendo lista de IDs de jugadores:");
        for (int i = 0; i < playerIDs.Count; i++)
        {
            Debug.Log("ID del Jugador " + (i + 1) + ": " + playerIDs[i]);
        }
    }
    public void PrintPlayerIDs()
    {
        if (playerIDs == null)
        {
            Debug.Log("La lista playerIDs no ha sido inicializada.");
            return;
        }

        Debug.Log("Contenido de la lista playerIDs:");
        foreach (var id in playerIDs)
        {
            Debug.Log(id);
        }
    }
    public void PrintPlayerData()
    {
        if (playerData == null)
        {
            Debug.Log("La lista playerData no ha sido inicializada.");
            return;
        }

        Debug.Log("Contenido de la lista playerData:");
        foreach (var data in playerData)
        {
            var datosJugadorInfo = $"ID: {data.jugadorId}, " +
                                   $"Nombre: {data.nomJugador.ToString()}, " +
                                   $"Puntaje: {data.puntaje}, " +
                                   $"Ganó: {data.gano}, " +
                                   $"Turno: {data.turno}, " +
                                   $"PrimerasPiezas: {data.primerasPiezas}, " +
                                   $"Madera: {data.maderaCount}, " +
                                   $"Ladrillo: {data.ladrilloCount}, " +
                                   $"Oveja: {data.ovejaCount}, " +
                                   $"Piedra: {data.piedraCount}, " +
                                   $"Trigo: {data.trigoCount}, " +
                                   $"Color: {data.colorJugador.ToString()}" +
                                   $"CantCaminos: {data.cantidadCaminos}, " +
                                   $"CantBases: {data.cantidadBases}, " +
                                   $"CantPueblos: {data.cantidadPueblos}, ";
            Debug.Log(datosJugadorInfo);
        }
    }

    public void ImprimirJugador(DatosJugador jugador)
    {
        Debug.Log("jugadorId: " + jugador.jugadorId);
        Debug.Log("nomJugador: " + jugador.nomJugador.ToString());
        Debug.Log("puntaje: " + jugador.puntaje);
        Debug.Log("gano: " + jugador.gano);
        Debug.Log("turno: " + jugador.turno);
        Debug.Log("primerasPiezas: " + jugador.primerasPiezas);
        Debug.Log("maderaCount: " + jugador.maderaCount);
        Debug.Log("ladrilloCount: " + jugador.ladrilloCount);
        Debug.Log("ovejaCount: " + jugador.ovejaCount);
        Debug.Log("piedraCount: " + jugador.piedraCount);
        Debug.Log("trigoCount: " + jugador.trigoCount);
        Debug.Log("colorJugador: " + jugador.colorJugador.ToString());
        Debug.Log("CantCaminos: " + jugador.cantidadCaminos);
        Debug.Log("CantBases: " + jugador.cantidadBases);
        Debug.Log("CantPueblos: " + jugador.cantidadPueblos);
    }
    public void ImprimirJugadorPorId(int idJugador)
    {
        //Debug.Log("Buscando al jugador con ID: " + idJugador);
        bool encontrado = false;

        for (int i = 0; i < playerData.Count; i++)
        {
            if (playerData[i].jugadorId == idJugador)
            {
                encontrado = true;
                Debug.Log("Jugador encontrado en la posición: " + i);
                ImprimirJugador(playerData[i]);
                break;
            }
        }

        if (!encontrado)
        {
            Debug.Log("Jugador no encontrado en la lista playerData");
        }
    }
    public void ImprimirTodosLosJugadores()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            Debug.Log("Jugador " + i);
            ImprimirJugador(playerData[i]);
            Debug.Log("----------------------------------");
        }      
    }
    public int GetPlayerByColor(string color)
    {
        var id = 0;
        foreach (DatosJugador jugador in playerData)
        {
            if (jugador.colorJugador.ToString() == color)
            {
                id = jugador.jugadorId;
            }
        }
        return id;
    }

    public int GetPlayerId(DatosJugador jugador)
    {
        return jugador.jugadorId;
    }

    public DatosJugador GetPlayerData(int jugadorId)
    {
        int index = playerIDs.IndexOf(jugadorId);
        if (index != -1)
        {
            return playerData[index];
        }
        else
        {
            Debug.LogError("Jugador no encontrado: " + jugadorId);
            return default(DatosJugador); // Retorna un DatosJugador por defecto
        }
    }

    
    private void Update()
    {



        if (!IsOwner) // si es cliente
        {

        }
        else // si es host
        {

        }
        return;
    }
    
    // Este es tu nuevo método RPC para agregar un jugador
    [ServerRpc(RequireOwnership = false)]
    public void AddPlayerServerRpc(int jugadorId, FixedString64Bytes nomJugador, int puntaje, bool gano, bool turno, bool primerasPiezas, int maderaCount, int ladrilloCount, int ovejaCount, int piedraCount, int trigoCount, FixedString64Bytes colorJugador, int cantidadCaminos, int cantidadBases, int cantidadPueblos)
    {
        try
        {
            Debug.Log("Entre a AddPlayerServerRpc");
            // Verificar que solo el host puede ejecutar este código
            if (!IsServer) return;

            // Agrega al jugador a la lista de jugadores
            DatosJugador newPlayer = new DatosJugador();
            newPlayer.jugadorId = jugadorId;
            newPlayer.nomJugador = nomJugador;
            newPlayer.puntaje = puntaje;
            newPlayer.gano = gano;
            newPlayer.turno = turno;
            newPlayer.primerasPiezas = primerasPiezas;
            newPlayer.maderaCount = maderaCount;
            newPlayer.ladrilloCount = ladrilloCount;
            newPlayer.ovejaCount = ovejaCount;
            newPlayer.piedraCount = piedraCount;
            newPlayer.trigoCount = trigoCount;
            newPlayer.colorJugador = colorJugador;
            newPlayer.cantidadCaminos = cantidadCaminos;
            newPlayer.cantidadBases = cantidadBases;
            newPlayer.cantidadPueblos = cantidadPueblos;
            // ... y puedes agregar los demás valores predeterminados aquí
            Debug.Log("Se va a unir usando AddPlayerServerRpc");
            if (!playerIDs.Contains(jugadorId) && jugadorId != 0)
            {
                playerIDs.Add(jugadorId);
                Debug.Log("Cant elementos de playerId:" + playerIDs.Count);
                playerData.Add(newPlayer);
            }
            ImprimirPlayerIDs();
            ImprimirTodosLosJugadores();
            Debug.Log("Termino AddPlayerServerRpc");
        }
        catch (Exception e)
        {
            Debug.Log("Error en AddPlayerServerRpc: " + e);
        }
    }

    [ServerRpc]
    public void TestServerRpc(FixedString64Bytes nombre, FixedString64Bytes color) //este comunica del cliente al servidor 
    {
        Debug.Log("Entre a TestServerRpc ");
        int myPlayerId = (int)NetworkManager.Singleton.LocalClientId; // Obtén el Id del jugador
        Debug.Log("Nombre, Color y Id del nuevo jugador " + nombre + color + myPlayerId);
        //AgregarJugador(myPlayerId, nombre, 100, false, true, 2, 10, 10, 10, 10, 10, color);
        ImprimirTodosLosJugadores();
    }

    [ServerRpc]
    public void UpdatePlayerColorServerRpc(int playerId, FixedString64Bytes colorName)
    {
        Debug.Log("Entre a UpdatePlayerColorServerRpc ");
        int index = playerIDs.IndexOf(playerId);
        if (index != -1)
        {
            DatosJugador datosActuales = playerData[index];
            datosActuales.colorJugador = colorName;
            playerData[index] = datosActuales;
        }
        else
        {
            Debug.LogError("Jugador no encontrado: " + playerId);
        }
    }

    [ServerRpc]
    public void UpdatePlayerNameServerRpc(int playerId, FixedString64Bytes playerName)
    {
        Debug.Log("Entre a UpdatePlayerNameServerRpc ");
        int index = playerIDs.IndexOf(playerId);
        if (index != -1)
        {
            DatosJugador datosActuales = playerData[index];
            datosActuales.nomJugador = playerName;
            playerData[index] = datosActuales;
        }
        else
        {
            Debug.LogError("Jugador no encontrado: " + playerId);
        }
    }

    // Llamada por el cliente para notificar al servidor que se ha unido
    [ServerRpc]
    public void NotifyServerOfJoinServerRpc()
    {
        Debug.Log("Nuevo cliente conectado: " + NetworkManager.Singleton.LocalClientId);
    }

    [ClientRpc]
    private void TestClientRpc() //este comunica del servidor al cliente 
    {
        Debug.Log("TestClientRpc ");
        //aca irian las funciones que pasa el puntaje o cantidad de recursos por ejemplo
    }
    // Esta función necesita ser implementada para buscar los datos del jugador basado en su ID.
    private bool TryObtenerDatosJugadorPorId(int idJugador, out DatosJugador datosJugador)
    {
        // Busca a través de los datos de los jugadores
        for (int i = 0; i < playerData.Count; i++)
        {
            // Si el ID del jugador coincide, devuelve sus datos
            if (playerData[i].jugadorId == idJugador)
            {
                datosJugador = playerData[i];
                return true;
            }
        }

        // Si no se encontró ningún jugador con el ID proporcionado, devuelve false
        datosJugador = default(DatosJugador);
        return false;
    }

    public void AumentarRecursos(int idJugador, string recurso, int cantidad)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log("Entre a AumentarRecurso");
            Debug.Log("id jugador: " + idJugador + " Recurso: " + recurso + " Cantidad: " + cantidad);
            bool jugadorEncontrado = false;
            int indexJugador = -1;
            // Búsqueda del jugador en la lista playerData
            for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
            {
                Debug.Log("La lista Ids es " + PlayerNetwork.Instance.playerData[i].jugadorId);
                if (PlayerNetwork.Instance.playerData[i].jugadorId == idJugador)
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
            DatosJugador jugador = PlayerNetwork.Instance.playerData[indexJugador];
            // Aumentar el recurso correspondiente
            switch (recurso)
            {
                case "Madera":
                    jugador.maderaCount += cantidad;
                    break;
                case "Ladrillo":
                    jugador.ladrilloCount += cantidad;
                    break;
                case "Oveja":
                    jugador.ovejaCount += cantidad;
                    break;
                case "Piedra":
                    jugador.piedraCount += cantidad;
                    break;
                case "Trigo":
                    jugador.trigoCount += cantidad;
                    break;
                default:
                    Debug.Log("Tipo de recurso desconocido");
                    return;
            }
            // Reemplazar el jugador en la lista con la versión modificada
            PlayerNetwork.Instance.playerData[indexJugador] = jugador;
            // Actualiza los textos


            if ( PlayerPrefs.GetInt("jugadorId") == idJugador)
            {
                BoardManager.Instance.UpdateResourceTextsHost(idJugador);
                
            }
            else
            {
                Debug.Log("ENTRE ACA ANTES CLIENTE");
                PlayerNetwork.DatosJugador datosJugador = default;
                var posicion = -1;

                // Itera sobre los elementos de playerData para encontrar los datos del jugador
                for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
                {
                    if (PlayerNetwork.Instance.playerData[i].jugadorId == idJugador)
                    {
                        datosJugador = PlayerNetwork.Instance.playerData[i];
                        posicion = i;
                        break;
                    }
                }
                UpdateResourcesTextClientRpc(datosJugador);
            }
        }
    }
    /*public void ManejoRecurosCon7()
    {
        Debug.Log("Entre a ManejoRecurosCon7");
        // Búsqueda del jugador en la lista playerData
        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            DatosJugador jugador = PlayerNetwork.Instance.playerData[i];
            int idJugador = GetPlayerId(jugador);
            int sumaRecursos = jugador.maderaCount + jugador.ladrilloCount + jugador.ovejaCount + jugador.trigoCount + jugador.piedraCount;
            Debug.Log("sumaRecursos es " + sumaRecursos);
            while (sumaRecursos > 7)
            {
                if(sumaRecursos > 7)
                {
                    jugador.maderaCount = jugador.maderaCount - 1;
                    sumaRecursos = jugador.maderaCount + jugador.ladrilloCount + jugador.ovejaCount + jugador.trigoCount + jugador.piedraCount;
                    if (sumaRecursos > 7)
                    {
                        jugador.ladrilloCount = jugador.ladrilloCount -1;
                        sumaRecursos = jugador.maderaCount + jugador.ladrilloCount + jugador.ovejaCount + jugador.trigoCount + jugador.piedraCount;
                        if (sumaRecursos > 7)
                        {
                            jugador.ovejaCount = jugador.ovejaCount - 1;
                            sumaRecursos = jugador.maderaCount + jugador.ladrilloCount + jugador.ovejaCount + jugador.trigoCount + jugador.piedraCount;
                            if (sumaRecursos > 7)
                            {
                                jugador.trigoCount = jugador.trigoCount - 1;
                                sumaRecursos = jugador.maderaCount + jugador.ladrilloCount + jugador.ovejaCount + jugador.trigoCount + jugador.piedraCount;
                                if (sumaRecursos > 7)
                                {
                                    jugador.piedraCount = jugador.piedraCount - 1;
                                    sumaRecursos = jugador.maderaCount + jugador.ladrilloCount + jugador.ovejaCount + jugador.trigoCount + jugador.piedraCount;
                                }
                            }
                        }
                    }

                }
            }
            PlayerNetwork.Instance.playerData[i] = jugador;
            if (PlayerPrefs.GetInt("jugadorId") == idJugador)
            {
                BoardManager.Instance.UpdateResourceTextsHost(idJugador);
            }
            else
            {
                Debug.Log("ENTRE ACA ANTES CLIENTE");
                PlayerNetwork.DatosJugador datosJugador = default;
                var posicion = -1;

                // Itera sobre los elementos de playerData para encontrar los datos del jugador
                for (int j = 0; j < PlayerNetwork.Instance.playerData.Count; i++)
                {
                    if (PlayerNetwork.Instance.playerData[j].jugadorId == idJugador)
                    {
                        datosJugador = PlayerNetwork.Instance.playerData[i];
                        posicion = i;
                        break;
                    }
                }
                UpdateResourcesTextClientRpc(datosJugador);
            }
        }
        Debug.Log("Borre recursos del 7");
    }*/
    /*public void ManejoRecursosCon7()
    {
        Debug.Log("Entré a ManejoRecursosCon7");

        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            DatosJugador jugador = PlayerNetwork.Instance.playerData[i];
            int idJugador = GetPlayerId(jugador);
            int sumaRecursos = jugador.maderaCount + jugador.ladrilloCount + jugador.ovejaCount + jugador.trigoCount + jugador.piedraCount;

            while (sumaRecursos > 7)
            {
                int recursosPorReducir = sumaRecursos - 7;
                int reduccionPorTipo = recursosPorReducir / 5; // Dividir equitativamente entre los 5 tipos de recursos

                jugador.maderaCount -= reduccionPorTipo;
                jugador.ladrilloCount -= reduccionPorTipo;
                jugador.ovejaCount -= reduccionPorTipo;
                jugador.trigoCount -= reduccionPorTipo;
                jugador.piedraCount -= reduccionPorTipo;

                sumaRecursos = jugador.maderaCount + jugador.ladrilloCount + jugador.ovejaCount + jugador.trigoCount + jugador.piedraCount;
            }
            PlayerNetwork.Instance.playerData[i] = jugador;
            // Actualizar el jugador original con los cambios
            /*for (int j = 0; j < PlayerNetwork.Instance.playerData.Count; i++)
            {
                if (PlayerNetwork.Instance.playerData[j].jugadorId == jugador.jugadorId)
                {
                    PlayerNetwork.Instance.playerData[j] = jugador;
                    break;
                }
            }*/
    /*if (PlayerPrefs.GetInt("jugadorId") == idJugador)
    {
        BoardManager.Instance.UpdateResourceTextsHost(idJugador);
    }
    else
    {
        Debug.Log("Entré aquí antes cliente");
        PlayerNetwork.DatosJugador datosJugador = default;
        var posicion = -1;

        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            if (PlayerNetwork.Instance.playerData[i].jugadorId == idJugador)
            {
                datosJugador = PlayerNetwork.Instance.playerData[i];
                posicion = i;
                break;
            }
        }
        UpdateResourcesTextClientRpc(datosJugador);
    }
}

Debug.Log("Recursos ajustados a 7 o menos");
}*/
    public void ManejoRecursosCon7(int id)
    {


        int indexJugador = -1;
        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            if (PlayerNetwork.Instance.playerData[i].jugadorId == id)
            {
                indexJugador = i;
            }
        }
        PlayerNetwork.DatosJugador jugadorcopia = PlayerNetwork.Instance.playerData[indexJugador];
        int sumaRecursos = jugadorcopia.maderaCount + jugadorcopia.ladrilloCount + jugadorcopia.ovejaCount + jugadorcopia.trigoCount + jugadorcopia.piedraCount;
        while(sumaRecursos > 7)
        {
            if(sumaRecursos > 7)
            {
                jugadorcopia.maderaCount = jugadorcopia.maderaCount - 1;
                sumaRecursos = jugadorcopia.maderaCount + jugadorcopia.ladrilloCount + jugadorcopia.ovejaCount + jugadorcopia.trigoCount + jugadorcopia.piedraCount;
                if (sumaRecursos > 7)
                {
                    jugadorcopia.ladrilloCount = jugadorcopia.ladrilloCount - 1;
                    sumaRecursos = jugadorcopia.maderaCount + jugadorcopia.ladrilloCount + jugadorcopia.ovejaCount + jugadorcopia.trigoCount + jugadorcopia.piedraCount;
                    if (sumaRecursos > 7)
                    {
                        jugadorcopia.ovejaCount = jugadorcopia.ovejaCount - 1;
                        sumaRecursos = jugadorcopia.maderaCount + jugadorcopia.ladrilloCount + jugadorcopia.ovejaCount + jugadorcopia.trigoCount + jugadorcopia.piedraCount;
                        if (sumaRecursos > 7)
                        {
                            jugadorcopia.trigoCount = jugadorcopia.trigoCount - 1;
                            sumaRecursos = jugadorcopia.maderaCount + jugadorcopia.ladrilloCount + jugadorcopia.ovejaCount + jugadorcopia.trigoCount + jugadorcopia.piedraCount;
                            if (sumaRecursos > 7)
                            {
                                jugadorcopia.piedraCount = jugadorcopia.piedraCount - 1;
                                sumaRecursos = jugadorcopia.maderaCount + jugadorcopia.ladrilloCount + jugadorcopia.ovejaCount + jugadorcopia.trigoCount + jugadorcopia.piedraCount;
                            }
                        }
                    }
                }
            }
            sumaRecursos = jugadorcopia.maderaCount + jugadorcopia.ladrilloCount + jugadorcopia.ovejaCount + jugadorcopia.trigoCount + jugadorcopia.piedraCount;

        }
        PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
        if (PlayerPrefs.GetInt("jugadorId") == id)
        {
            BoardManager.Instance.UpdateResourceTextsHost(id);

        }
        else
        {
            Debug.Log("ENTRE ACA ANTES CLIENTE");
            PlayerNetwork.DatosJugador datosJugador = default;
            var posicion = -1;

            // Itera sobre los elementos de playerData para encontrar los datos del jugador
            for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
            {
                if (PlayerNetwork.Instance.playerData[i].jugadorId == id)
                {
                    datosJugador = PlayerNetwork.Instance.playerData[i];
                    posicion = i;
                    break;
                }
            }
            UpdateResourcesTextClientRpc(datosJugador);
        }




        /*
        try
        {
            Debug.Log("Entré a ManejoRecursosCon7");

            foreach (var jugadorOriginal in PlayerNetwork.Instance.playerData)
            {
                DatosJugador jugador = jugadorOriginal; // Crear una copia del jugador

                int sumaRecursos = jugador.maderaCount + jugador.ladrilloCount + jugador.ovejaCount + jugador.trigoCount + jugador.piedraCount;

                while (sumaRecursos > 7)
                {
                    int recursosPorReducir = sumaRecursos - 7;
                    int reduccionPorTipo = recursosPorReducir / 5; // Dividir equitativamente entre los 5 tipos de recursos

                    jugador.maderaCount -= reduccionPorTipo;
                    jugador.ladrilloCount -= reduccionPorTipo;
                    jugador.ovejaCount -= reduccionPorTipo;
                    jugador.trigoCount -= reduccionPorTipo;
                    jugador.piedraCount -= reduccionPorTipo;

                    sumaRecursos = jugador.maderaCount + jugador.ladrilloCount + jugador.ovejaCount + jugador.trigoCount + jugador.piedraCount;
                }
            }
            
            // Actualizar el jugador original con los cambios
            for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
            {
                if (PlayerNetwork.Instance.playerData[i].jugadorId == jugador.jugadorId)
                {
                    PlayerNetwork.Instance.playerData[i] = jugador;
                    break;
                }
            }

            int idJugador = GetPlayerId(jugador);

            if (PlayerPrefs.GetInt("jugadorId") == idJugador)
            {
                BoardManager.Instance.UpdateResourceTextsHost(idJugador);
            }
            else
            {
                Debug.Log("Entré aquí antes cliente");
                PlayerNetwork.DatosJugador datosJugador = default;
                var posicion = -1;

                for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
                {
                    if (PlayerNetwork.Instance.playerData[i].jugadorId == idJugador)
                    {
                        datosJugador = PlayerNetwork.Instance.playerData[i];
                        posicion = i;
                        break;
                    }
                }
                UpdateResourcesTextClientRpc(datosJugador);
            }
        }

            Debug.Log("Recursos ajustados a 7 o menos");
        }
        catch (Exception e)
        {
            Debug.Log("Error en ManejoRecursosCon7: " + e);
        }*/

    }

    private void PrimerasPiezasEnTrue()
    {
        var listoo = true;
        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            Debug.Log("Primeras Piezas de " + i +" es " + PlayerNetwork.Instance.playerData[i].primerasPiezas);
            if (PlayerNetwork.Instance.playerData[i].primerasPiezas == false)
            {
                listoo = false;
            }
        }
        if (listoo)
        {
            PlayerNetwork.Instance.todosListos = true;
        }        
    }

    [ServerRpc(RequireOwnership = false)]
    private void PrimerasPiezasEnTrueServerRpc()
    {
        var listoo = true;
        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            Debug.Log("Primeras Piezas de " + i + " es " + PlayerNetwork.Instance.playerData[i].primerasPiezas);
            if (PlayerNetwork.Instance.playerData[i].primerasPiezas == false)
            {
                listoo = false;
            }
        }
        if (listoo)
        {
            PlayerNetwork.Instance.todosListos = true;
        }
        if (PlayerNetwork.Instance.todosListos == true && PlayerNetwork.Instance.diPiezas == false)
        {
            DarPrimerosRecursos();
        }
    }

    public void DarPrimerosRecursos()
    {
        Debug.Log("comienza la corutina");
        StartCoroutine(DarRecursosSecuencialmente());
        Debug.Log("termina la corutina");
    }

    private IEnumerator DarRecursosSecuencialmente()
    {
        yield return StartCoroutine(BoardManager.Instance.ManejoParcelassecu(2));
        yield return StartCoroutine(BoardManager.Instance.ManejoParcelassecu(3));
        yield return StartCoroutine(BoardManager.Instance.ManejoParcelassecu(4));
        yield return StartCoroutine(BoardManager.Instance.ManejoParcelassecu(5));
        yield return StartCoroutine(BoardManager.Instance.ManejoParcelassecu(6));
        yield return StartCoroutine(BoardManager.Instance.ManejoParcelassecu(8));
        yield return StartCoroutine(BoardManager.Instance.ManejoParcelassecu(9));
        yield return StartCoroutine(BoardManager.Instance.ManejoParcelassecu(10));
        yield return StartCoroutine(BoardManager.Instance.ManejoParcelassecu(11));
        yield return StartCoroutine(BoardManager.Instance.ManejoParcelassecu(12));

        PlayerNetwork.Instance.diPiezas = true;
        PlayerNetwork.Instance.ImprimirTodosLosJugadores();
    }

    public void EndTurn()
    {
        Debug.Log("Entre al End Turn");


        if (NetworkManager.Singleton.IsServer)  // Asegúrate de que solo el servidor modifique el turno actual.
        {
            Debug.Log("Entre al is server de End Turn");
            //ImprimirPlayerIDs();
            currentTurnIndex++;
            if (currentTurnIndex >= playerIDs.Count)
            {
                currentTurnIndex = 0;
            }
            // Notifica a todos los jugadores sobre el cambio de turno.
            NotifyTurnChangeClientRpc(currentTurnIndex);
            CheckifWon();
            PrimerasPiezasEnTrue();
            if (todosListos==true && diPiezas == false)
            {
                DarPrimerosRecursos();
            }
            Debug.Log("todoslistos es " + todosListos);
            Debug.Log("diPiezas es " + diPiezas);
        }
        else
        {
            Debug.Log("Entre como cliente al End Turn");
            currentTurnIndex++;
            if (currentTurnIndex >= playerIDs.Count)
            {
                currentTurnIndex = 0;
            }
            // Notifica a todos los jugadores sobre el cambio de turno.
            NotifyTurnChangeServerRpc(currentTurnIndex);
            CheckifWonServerRpc();
            PrimerasPiezasEnTrueServerRpc();
           /* if (PlayerNetwork.Instance.todosListos == true && PlayerNetwork.Instance.diPiezas == false)
            {
                DarPrimerosRecursosServerRpc();
            }*/
            Debug.Log("todoslistos es " + todosListos);
            Debug.Log("diPiezas es " + diPiezas);
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void PrimerasPiezasServerRpc(int id)
    {

        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
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
        //UpdateCantidadPiezadClientRpc(jugador, jugador.cantidadCaminos, jugador.cantidadBases, jugador.cantidadPueblos, jugador.primerasPiezas);
    }

    public void SetGano()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            int id = PlayerPrefs.GetInt("jugadorId");
            int indexJugador = -1;
            bool jugadorEncontrado = false;
            for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
            {
                if (PlayerNetwork.Instance.playerData[i].jugadorId == id)
                {
                    jugadorEncontrado = true;
                    indexJugador = i;
                }
            }
            if (!jugadorEncontrado)
            {
                Debug.Log("Jugador no encontrado en la lista playerData");
                return;
            }
            PlayerNetwork.DatosJugador jugadorcopia = PlayerNetwork.Instance.playerData[indexJugador];
            int puntajejugador = (jugadorcopia.puntaje);
            if (puntajejugador == 10)
                jugadorcopia.gano = true;
                jugadorcopia.puntaje = puntajejugador;
            PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
        }
        else
        {
            int id = PlayerPrefs.GetInt("jugadorId");
            SetGanoServerRpc(id);
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void SetGanoServerRpc(int id)
    {
        int indexJugador = -1;
        bool jugadorEncontrado = false;
        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            if (PlayerNetwork.Instance.playerData[i].jugadorId == id)
            {
                jugadorEncontrado = true;
                indexJugador = i;
            }
        }
        if (!jugadorEncontrado)
        {
            Debug.Log("Jugador no encontrado en la lista playerData");
            return;
        }
        PlayerNetwork.DatosJugador jugadorcopia = PlayerNetwork.Instance.playerData[indexJugador];
        int puntajejugador = (jugadorcopia.puntaje);
        if (puntajejugador == 10)
            jugadorcopia.gano = true;
        jugadorcopia.puntaje = puntajejugador;
        PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
    }
    public void CheckifWon()
    {
        Debug.Log("Entre a checkifwon como server");
        for (int i = 0; i < playerData.Count; i++)
        {
            if (playerData[i].gano == true)
            {
                SceneManager.LoadScene("EscenaFinal");
                CargarEscenaFinalClientRpc();
            }
        }      
    }
    [ServerRpc(RequireOwnership = false)]
    public void CheckifWonServerRpc()
    {
        Debug.Log("Entre a checkifwon como Cliente");
        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            Debug.Log("Entro al for");
            if (PlayerNetwork.Instance.playerData[i].gano == true)
            {
                Debug.Log("El jugador ganador es: " + PlayerNetwork.Instance.playerData[i]);
                SceneManager.LoadScene("EscenaFinal");
                PlayerNetwork.Instance.CargarEscenaFinalClientRpc();
            }
        }
    }

    [ClientRpc]
    public void NotifyTurnChangeClientRpc(int newTurnIndex)
    {
        currentTurnIndex = newTurnIndex;
        // Aquí puedes implementar lógica adicional como mostrar un mensaje indicando quién es el próximo.
        Debug.Log("C - Cambio el turno a " + currentTurnIndex);
        ImprimirPlayerIDs();
    }
    [ServerRpc(RequireOwnership = false)]
    public void NotifyTurnChangeServerRpc(int newTurnIndex)
    {
        currentTurnIndex = newTurnIndex;
        // Aquí puedes implementar lógica adicional como mostrar un mensaje indicando quién es el próximo.
        Debug.Log("S - Cambio el turno a " + currentTurnIndex);
        ImprimirPlayerIDs();
    }
    public bool IsMyTurn(int clientId)
    {
        //Debug.Log("Index " + currentTurnIndex + "y es turno de " + playerIDs[currentTurnIndex] + " Y " + clientId);        
        return (playerIDs[currentTurnIndex] == clientId);
    }

    [ServerRpc(RequireOwnership = false)]
    public void ColocarCaminoServerRpc(string color, string currentcamino, string nombreCollider, Vector3 posititon, Quaternion rotation)
    {
        try
        {
            Debug.Log("Entre a ColocarCaminoServerRpc");
            var objetoCamino = Resources.Load(currentcamino) as GameObject;
            Debug.Log("2 preafb base es " + objetoCamino.name);
            currentCamino = Instantiate(objetoCamino, posititon, rotation);
            currentCamino.GetComponent<NetworkObject>().Spawn();
            // Obtener el componente ComprobarObjeto del objeto golpeado
            var nombresinClone = ListaColliders.Instance.RemoverCloneDeNombre(nombreCollider);
            Debug.Log("CPC PlayerNetwo" + nombresinClone);
            ListaColliders.Instance.ModificarTipoPorNombre(nombresinClone, "Camino"); // Aca se debe llamar una serverRpc o como ya es el servidor corriendo no?
            ListaColliders.Instance.ModificarColorPorNombre(nombresinClone, color);
            ListaColliders.Instance.ImprimirColliderPorNombre(nombresinClone);
            tipoActual = "Ninguno";
            int  id = GetPlayerByColor(color);
            //Actualizar recursos:
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
            // Luego de colocar una base, disminuyes el contador y verificas si desactivar el botón.
            jugadorcopia.cantidadCaminos = jugadorcopia.cantidadCaminos - 1;
            PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
            //jugador.cantidadCaminos--;
            Debug.Log("Caminos restantes POST: " + PlayerNetwork.Instance.playerData[indexJugador].cantidadCaminos);
            DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
            UpdateCantidadPiezadClientRpc(jugador, jugador.cantidadCaminos, jugador.cantidadBases, jugador.cantidadPueblos, jugador.primerasPiezas);
        }
        catch (Exception e)
        {
            Debug.Log("Error en ColocarCaminoServerRpc: " + e);
        }

    }
    [ServerRpc(RequireOwnership = false)]
    public void ColocarBaseServerRpc(int id, string color, string currentbase, string nombreCollider, Vector3 posititon)
    {
        try
        {
            Debug.Log("Entre a la BaseServerRpc");
            var objetoBase = Resources.Load(currentbase) as GameObject;
            Debug.Log("2 preafb base es " + objetoBase.name);
            //PlayerPrefs.SetString(colliderName, "collider");
            currentBase = Instantiate(objetoBase, posititon, Quaternion.identity);
            gameObjectsByIDServer.Add(currentBase.GetInstanceID(), currentBase);
            currentBase.GetComponent<NetworkObject>().Spawn();
            int idBase = currentBase.GetInstanceID();
            // Obtener el componente ComprobarObjeto del objeto golpeado
            var nombresinClone = ListaColliders.Instance.RemoverCloneDeNombre(nombreCollider);
            Debug.Log("CPC PlayerNetwo" + nombresinClone);
            ListaColliders.Instance.ModificarTipoPorNombre(nombresinClone, "Base"); // Aca se debe llamar una serverRpc o como ya es el servidor corriendo no?
            ListaColliders.Instance.ModificarColorPorNombre(nombresinClone, color);
            ListaColliders.Instance.ModificarIdPiezaPorNombre(nombresinClone, idBase);
            ListaColliders.Instance.ImprimirColliderPorNombre(nombresinClone);
            /*Debug.Log("Va a sumar puntaje");
            SetPuntajebyId(id, 1);
            Debug.Log("Termino SetPuntajebyID");*/
            tipoActual = "Ninguno";
            //Actualizar recursos:
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
            // Luego de colocar una base, disminuyes el contador y verificas si desactivar el botón.
            jugadorcopia.cantidadBases = jugadorcopia.cantidadBases - 1;
            jugadorcopia.puntaje = jugadorcopia.puntaje + 1;
            PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
            //jugador.cantidadBasess--;
            Debug.Log("Bases restantes POST: " + PlayerNetwork.Instance.playerData[indexJugador].cantidadPueblos);
            DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
            UpdateCantidadPiezadClientRpc(jugador, jugador.cantidadCaminos, jugador.cantidadBases, jugador.cantidadPueblos, jugador.primerasPiezas);
            //Actualizar puntajes jugadores
            PlayerNetwork.DatosJugador datosJugador = default;
            var posicion = -1;
            
            DatosJugador juga1 = new DatosJugador();
            DatosJugador juga2 = new DatosJugador();
            DatosJugador juga3 = new DatosJugador();
            DatosJugador juga4 = new DatosJugador();
            
            // Itera sobre los elementos de playerData para encontrar los datos del jugador
            for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        juga1 = PlayerNetwork.Instance.playerData[i];
                        break;
                    case 1:
                         juga2 = PlayerNetwork.Instance.playerData[i];
                        break;
                    case 2:
                         juga3 = PlayerNetwork.Instance.playerData[i];
                        break;
                    case 3:
                         juga4 = PlayerNetwork.Instance.playerData[i];
                        break;
                }
                if (PlayerNetwork.Instance.playerData[i].jugadorId == id)
                {
                    datosJugador = PlayerNetwork.Instance.playerData[i];
                    posicion = i;
                    break;
                }
            }
            BoardManager.Instance.Puntaje1.text = juga1.puntaje.ToString();
            BoardManager.Instance.Nombre1.text = juga1.nomJugador.ToString();
            BoardManager.Instance.Puntaje2.text = juga2.puntaje.ToString();
            BoardManager.Instance.Nombre2.text = juga2.nomJugador.ToString();
            BoardManager.Instance.Puntaje3.text = juga3.puntaje.ToString();
            BoardManager.Instance.Nombre3.text = juga3.nomJugador.ToString();
            BoardManager.Instance.Puntaje4.text = juga4.puntaje.ToString();
            BoardManager.Instance.Nombre4.text = juga4.nomJugador.ToString();
            UpdatePuntajeTextClientRpc(juga1, juga2, juga3, juga4);           
        }
        catch (Exception e)
        {
            Debug.Log("Error en ColocarBaseServerRpc: " + e);
        }

    }
    [ServerRpc(RequireOwnership = false)]
    public void ColocarPuebloServerRpc(int id, string color, string currentpueblo, string nombreCollider, Vector3 posititon)
    {
        try
        {
            Debug.Log("Entre a la PuebloServerRpc");
            var objetoPueblo = Resources.Load(currentpueblo) as GameObject;
            Debug.Log("2 preafb pueblo es " + objetoPueblo.name);
            //PlayerPrefs.SetString(colliderName, "collider");
            currentPueblo = Instantiate(objetoPueblo, posititon, Quaternion.identity);
            currentPueblo.GetComponent<NetworkObject>().Spawn();
            var nombresinClone = ListaColliders.Instance.RemoverCloneDeNombre(nombreCollider);
            Debug.Log("CPC PlayerNetwo" + nombresinClone);
            int idbase = ListaColliders.Instance.GetIdPiezaPorNombre(nombresinClone);
            var colorCollider = ListaColliders.Instance.GetColorPorNombre(nombresinClone);
            Debug.Log("color es : " + color + " y colorcollider es:" + colorCollider);
            if (gameObjectsByIDServer.ContainsKey(idbase) && color == colorCollider)
            {
                GameObject baseToDespawn = gameObjectsByIDServer[idbase];
                baseToDespawn.GetComponent<NetworkObject>().Despawn(); // Despawn using your networking library
                Destroy(baseToDespawn); // Destroy the object if needed
                gameObjectsByIDServer.Remove(idbase);
            }
            ListaColliders.Instance.ModificarTipoPorNombre(nombresinClone, "Pueblo"); // Aca se debe llamar una serverRpc o como ya es el servidor corriendo no?
            ListaColliders.Instance.ModificarColorPorNombre(nombresinClone, color);
            ListaColliders.Instance.ImprimirColliderPorNombre(nombresinClone);
            /*Debug.Log("Va a sumar puntaje");
            SetPuntajebyId(id, 2);
            Debug.Log("Termino SetPuntajebyID");*/
            tipoActual = "Ninguno";
            //Actualizar recursos:
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
            // Luego de colocar una base, disminuyes el contador y verificas si desactivar el botón.
            jugadorcopia.cantidadPueblos = jugadorcopia.cantidadPueblos - 1;
            jugadorcopia.puntaje = jugadorcopia.puntaje + 1;
            PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
            //jugador.cantidadPueblos--;
            Debug.Log("Pueblos restantes POST: " + PlayerNetwork.Instance.playerData[indexJugador].cantidadPueblos);          
            DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
            UpdateCantidadPiezadClientRpc(jugador, jugador.cantidadCaminos, jugador.cantidadBases, jugador.cantidadPueblos, jugador.primerasPiezas);
            //Actualizar puntajes jugadores
            PlayerNetwork.DatosJugador datosJugador = default;
            var posicion = -1;

            DatosJugador juga1 = new DatosJugador();
            DatosJugador juga2 = new DatosJugador();
            DatosJugador juga3 = new DatosJugador();
            DatosJugador juga4 = new DatosJugador();

            // Itera sobre los elementos de playerData para encontrar los datos del jugador
            for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        juga1 = PlayerNetwork.Instance.playerData[i];
                        break;
                    case 1:
                        juga2 = PlayerNetwork.Instance.playerData[i];
                        break;
                    case 2:
                        juga3 = PlayerNetwork.Instance.playerData[i];
                        break;
                    case 3:
                        juga4 = PlayerNetwork.Instance.playerData[i];
                        break;
                }
                if (PlayerNetwork.Instance.playerData[i].jugadorId == id)
                {
                    datosJugador = PlayerNetwork.Instance.playerData[i];
                    posicion = i;
                    break;
                }
            }
            BoardManager.Instance.Puntaje1.text = juga1.puntaje.ToString();
            BoardManager.Instance.Nombre1.text = juga1.nomJugador.ToString();
            BoardManager.Instance.Puntaje2.text = juga2.puntaje.ToString();
            BoardManager.Instance.Nombre2.text = juga2.nomJugador.ToString();
            BoardManager.Instance.Puntaje3.text = juga3.puntaje.ToString();
            BoardManager.Instance.Nombre3.text = juga3.nomJugador.ToString();
            BoardManager.Instance.Puntaje4.text = juga4.puntaje.ToString();
            BoardManager.Instance.Nombre4.text = juga4.nomJugador.ToString();
            UpdatePuntajeTextClientRpc(juga1, juga2, juga3, juga4);       
        }
        catch (Exception e)
        {
            Debug.Log("Error en ColocarPuebloServerRpc: " + e);
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void ComprarCaminoServerRpc(int jugadorId)
    {
        Debug.Log("Entro a comprarCaminoServerRpc");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(jugadorId);
        if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1 )
        {
            // Restar recursos
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
            jugadorcopia.cantidadCaminos += 1;
            jugadorcopia.maderaCount -= 1;
            jugadorcopia.ladrilloCount -= 1;           
            PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
            PlayerNetwork.Instance.UpdateResourcesTextClientRpc(jugadorcopia);
            UpdateComprarCaminoButtonServerRpc(jugadorId);
            UpdateComprarCaminoButtonClientRpc(jugadorId);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ComprarBaseServerRpc(int jugadorId)
    {
        Debug.Log("Entro a comprarBaseServerRpc con ID " + jugadorId);
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(jugadorId);
        if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1 && jugador.trigoCount >= 1 && jugador.ovejaCount >= 1)
        {
            // Restar recursos
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
            jugadorcopia.cantidadBases += 1;
            jugadorcopia.maderaCount -= 1;
            jugadorcopia.ladrilloCount -= 1;
            jugadorcopia.trigoCount -= 1;
            jugadorcopia.ovejaCount -= 1;
            PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
            PlayerNetwork.Instance.UpdateResourcesTextClientRpc(jugadorcopia);
            UpdateComprarBaseButtonServerRpc(jugadorId);
            UpdateComprarBaseButtonClientRpc(jugadorId);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ComprarPuebloServerRpc(int jugadorId)
    {
        Debug.Log("Entro a comprarPuebloServerRpc con ID " + jugadorId);
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(jugadorId);
        if (jugador.trigoCount >= 3 && jugador.piedraCount >= 2)
        {
            // Restar recursos
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
            jugadorcopia.cantidadPueblos += 1;
            jugadorcopia.trigoCount -= 3;
            jugadorcopia.piedraCount -= 2;
            PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
            PlayerNetwork.Instance.UpdateResourcesTextClientRpc(jugadorcopia);
            UpdateComprarPuebloButtonServerRpc(jugadorId);
            UpdateComprarPuebloButtonClientRpc(jugadorId);
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void UpdateResourcesTextServerRpc(int jugadorId)
    {

        //Debug.Log("Server a UpdateResourceTexts con ID " + jugadorId);
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
        UpdateResourcesTextClientRpc(datosJugador);
    }

    [ServerRpc(RequireOwnership = false)]
    public void UpdatePuntajeTextServerRpc(DatosJugador jugador1, DatosJugador jugador2, DatosJugador jugador3, DatosJugador jugador4)
    {
        Debug.Log("Entre a UpdatePuntaje1TextServerRpc");
        BoardManager.Instance.Puntaje1.text = jugador1.puntaje.ToString();
        BoardManager.Instance.Nombre1.text = jugador1.nomJugador.ToString();
        BoardManager.Instance.Puntaje2.text = jugador2.puntaje.ToString();
        BoardManager.Instance.Nombre2.text = jugador2.nomJugador.ToString();
        BoardManager.Instance.Puntaje3.text = jugador3.puntaje.ToString();
        BoardManager.Instance.Nombre3.text = jugador3.nomJugador.ToString();
        BoardManager.Instance.Puntaje4.text = jugador4.puntaje.ToString();
        BoardManager.Instance.Nombre4.text = jugador4.nomJugador.ToString();
    }
    [ClientRpc]
    public void UpdateResourcesTextClientRpc(DatosJugador jugador)
    {
        Debug.Log("Entre a UpdateResourcesTextClientRpc");
        BoardManager.Instance.MaderaCountText.text = jugador.maderaCount.ToString();
        BoardManager.Instance.LadrilloCountText.text = jugador.ladrilloCount.ToString();
        BoardManager.Instance.OvejaCountText.text = jugador.ovejaCount.ToString();
        BoardManager.Instance.PiedraCountText.text = jugador.piedraCount.ToString();
        BoardManager.Instance.TrigoCountText.text = jugador.trigoCount.ToString();
    }

    [ClientRpc]
    public void UpdatePuntajeTextClientRpc(DatosJugador jugador1, DatosJugador jugador2, DatosJugador jugador3, DatosJugador jugador4)
    {
        Debug.Log("Entre a UpdatePuntaje1TextClientRpc");
        BoardManager.Instance.Puntaje1.text = jugador1.puntaje.ToString();
        BoardManager.Instance.Nombre1.text = jugador1.nomJugador.ToString();
        BoardManager.Instance.Puntaje2.text = jugador2.puntaje.ToString();
        BoardManager.Instance.Nombre2.text = jugador2.nomJugador.ToString();
        BoardManager.Instance.Puntaje3.text = jugador3.puntaje.ToString();
        BoardManager.Instance.Nombre3.text = jugador3.nomJugador.ToString();
        BoardManager.Instance.Puntaje4.text = jugador4.puntaje.ToString();
        BoardManager.Instance.Nombre4.text = jugador4.nomJugador.ToString();
    }

    [ClientRpc]
    public void UpdatePuntaje1TextClientRpc(DatosJugador jugador)
    {
        Debug.Log("Entre a UpdatePuntaje1TextClientRpc");
        BoardManager.Instance.Puntaje1.text = jugador.puntaje.ToString();
        BoardManager.Instance.Nombre1.text = jugador.nomJugador.ToString();
    }
    [ClientRpc]
    public void UpdatePuntaje2TextClientRpc(DatosJugador jugador)
    {
        Debug.Log("Entre a UpdatePuntaje2TextClientRpc");
        BoardManager.Instance.Puntaje2.text = jugador.puntaje.ToString();
        BoardManager.Instance.Nombre2.text = jugador.nomJugador.ToString();
    }
    [ClientRpc]
    public void UpdatePuntaje3TextClientRpc(DatosJugador jugador)
    {
        Debug.Log("Entre a UpdatePuntaje3TextClientRpc");
        BoardManager.Instance.Puntaje3.text = jugador.puntaje.ToString();
        BoardManager.Instance.Nombre3.text = jugador.nomJugador.ToString();
    }
    [ClientRpc]
    public void UpdatePuntaje4TextClientRpc(DatosJugador jugador)
    {
        Debug.Log("Entre a UpdatePuntaje4TextClientRpc");
        BoardManager.Instance.Puntaje4.text = jugador.puntaje.ToString();
        BoardManager.Instance.Nombre4.text = jugador.nomJugador.ToString();
    }

    public void UpdateCantidadPiezad(DatosJugador jugador, int caminos, int bases, int pueblos, bool priPiezas)
    {
        //Debug.Log("Entre a UpdateCantidadPiezadClientRpc");
        jugador.cantidadCaminos = caminos;
        jugador.cantidadBases = bases;
        jugador.cantidadPueblos = pueblos;
        jugador.primerasPiezas = priPiezas;
    }
    [ClientRpc]
    public void UpdateCantidadPiezadClientRpc(DatosJugador jugador, int caminos, int bases, int pueblos, bool priPiezas)
    {
        //Debug.Log("Entre a UpdateCantidadPiezadClientRpc");
        jugador.cantidadCaminos = caminos;
        jugador.cantidadBases = bases;
        jugador.cantidadPueblos = pueblos;
        jugador.primerasPiezas = priPiezas;
    }
    [ServerRpc(RequireOwnership = false)]
    public void UpdateCantidadPiezadServerRpc(int id, int caminos, int bases, int pueblos, bool priPiezas)
    {
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
        jugadorcopia.cantidadCaminos = caminos;
        jugadorcopia.cantidadBases = bases;
        jugadorcopia.cantidadPueblos = pueblos;
        jugadorcopia.primerasPiezas = priPiezas;
        PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
    }

    [ServerRpc(RequireOwnership = false)]
    public void TirarDadosServerRpc()
    {
        Debug.Log("Entro a TirarDadosServerRpc");
        // NO BORRAR ESTO COMENTADO POR SI SURGE DENUEVO EL TEMA DE LOS DADOS
        // Si el tablero no está colocado, regresar
        if (!ARCursor.Instance.isBoardPlaced) return;
        // Comprobar si dadoToPlace o tableromInstance son null antes de proceder
        if (ARCursor.Instance.dadoToPlace == null || ARCursor.Instance.tableromInstance == null) return;
        // Si el dado no existe, crearlo
        if (ARCursor.Instance.currentDado == null)
        {
            // Crear un nuevo dado en la posición por encima del tablero
            ARCursor.Instance.currentDado = Instantiate(ARCursor.Instance.dadoToPlace, ARCursor.Instance.tableromInstance.transform.position + Vector3.up * ARCursor.Instance.dadoDistance + Vector3.right * ARCursor.Instance.dadoDistance / 4, Quaternion.identity);
            ARCursor.Instance.currentDado.GetComponent<NetworkObject>().Spawn();
            DiceNumberTextScript.dice1 = ARCursor.Instance.currentDado;
            //Destroy(currentDado2, 5f);
        }
        else
        {
            // Si el dado existe, reposicionarlo para el nuevo lanzamiento
            ARCursor.Instance.currentDado.transform.position = ARCursor.Instance.tableromInstance.transform.position + Vector3.up * ARCursor.Instance.dadoDistance;
        }
        if (ARCursor.Instance.currentDado2 == null)
        {
            // Crear un segundo dado al costado del primero
            ARCursor.Instance.currentDado2 = Instantiate(ARCursor.Instance.dadoToPlace, ARCursor.Instance.tableromInstance.transform.position + Vector3.up * ARCursor.Instance.dadoDistance + Vector3.right * ARCursor.Instance.dadoDistance / 2, Quaternion.identity);
            ARCursor.Instance.currentDado2.GetComponent<NetworkObject>().Spawn();
            DiceNumberTextScript.dice2 = ARCursor.Instance.currentDado2;
            //Destroy(currentDado2, 5f);
        }
        else
        {
            // Si el segundo dado existe, reposicionarlo para el nuevo lanzamiento
            ARCursor.Instance.currentDado2.transform.position = ARCursor.Instance.tableromInstance.transform.position + Vector3.up * ARCursor.Instance.dadoDistance + Vector3.right * ARCursor.Instance.dadoDistance;
        }
        // Obtén el DiceScript del dado actual y lanza el dado
        DiceScript diceScript = ARCursor.Instance.currentDado.GetComponent<DiceScript>();
        if (diceScript != null)
        {
            diceScript.RollDice(ARCursor.Instance.currentDado, ARCursor.Instance.tableromInstance.transform.position + Vector3.up * ARCursor.Instance.dadoDistance);
        }
        // Obtén el DiceScript del segundo dado y lanza el dado
        DiceScript diceScript2 = ARCursor.Instance.currentDado2.GetComponent<DiceScript>();
        if (diceScript2 != null)
        {
            diceScript2.RollDice(ARCursor.Instance.currentDado2, ARCursor.Instance.tableromInstance.transform.position + Vector3.up * ARCursor.Instance.dadoDistance + Vector3.right * ARCursor.Instance.dadoDistance / 2);
        }
        // Ajustar dicesThrown a true luego de lanzar los dados
        ARCursor.Instance.dicesThrown = true;
        var resu = DiceNumberTextScript.Instance.DarResultadoRandom();
        ResultadoDadoClientRpc(resu);
        DiceNumberTextScript.Instance.ResultadoDadoEnPantalla(resu);
        BoardManager.Instance.ManejoParcelas(resu);
    }
    [ClientRpc]
    public void ResultadoDadoClientRpc(int resu)
    {
        DiceNumberTextScript.Instance.resultadoDado.text = resu.ToString();
    }

    public DatosJugador? GetPlayerByColor(FixedString64Bytes color)
    {
        foreach (DatosJugador jugador in playerData)
        {
            if (jugador.colorJugador.Equals(color))
            {
                return jugador;
            }
        }
        return null; // devuelve null si no se encontró un jugador con ese color
    }

   

    [ServerRpc(RequireOwnership = false)]
    public void UpdateComprarCaminoButtonServerRpc(int id)
    {
        Debug.Log("UpdateComprarCaminoButtonServerRpc");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        comprarCaminoButton = GameObject.Find("Comprar Camino");
        Button componente = comprarCaminoButton.GetComponent<Button>();
        componente.interactable = (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1);
    }
    [ServerRpc(RequireOwnership = false)]
    public void UpdateComprarBaseButtonServerRpc(int id)
    {
        Debug.Log("UpdateBaseCaminoButtonServerRpc");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        comprarBaseButton = GameObject.Find("Comprar Base");
        Button componente = comprarCaminoButton.GetComponent<Button>();
        componente.interactable = (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1 && jugador.trigoCount >= 1 && jugador.ovejaCount >= 1);
    }
    [ServerRpc(RequireOwnership = false)]
    public void UpdateComprarPuebloButtonServerRpc(int id)
    {
        Debug.Log("UpdatePuebloCaminoButtonServerRpc");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        comprarPuebloButton = GameObject.Find("Comprar Pueblo");
        Button componente = comprarCaminoButton.GetComponent<Button>();
        componente.interactable = (jugador.piedraCount >= 2 && jugador.trigoCount >= 3);
    }
    [ClientRpc]
    public void UpdateComprarCaminoButtonClientRpc(int id)
    {
        Debug.Log("UpdateComprarCaminoButtonClientRpc");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        comprarCaminoButton = GameObject.Find("Comprar Camino");
        Button componente = comprarCaminoButton.GetComponent<Button>();
        componente.interactable = (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1);
    }
    [ClientRpc]
    public void UpdateComprarBaseButtonClientRpc(int id)
    {
        Debug.Log("UpdateBaseCaminoButtonClientRpc");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        comprarBaseButton = GameObject.Find("Comprar Base");
        Button componente = comprarCaminoButton.GetComponent<Button>();
        componente.interactable = (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1 && jugador.trigoCount >= 1 && jugador.ovejaCount >= 1);
    }
    [ClientRpc]
    public void UpdateComprarPuebloButtonClientRpc(int id)
    {
        Debug.Log("UpdatePuebloCaminoButtonClientRpc");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        comprarPuebloButton = GameObject.Find("Comprar Pueblo");
        Button componente = comprarCaminoButton.GetComponent<Button>();
        componente.interactable = (jugador.piedraCount >= 2 && jugador.trigoCount >= 3);
    }

    [ServerRpc(RequireOwnership = false)]
    public void AbandonarServerRpc()
    {
        /*
        int puntajeMaximo = 0;
        DatosJugador jugaMaximo = new DatosJugador();
        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {

            DatosJugador jugaActual = PlayerNetwork.Instance.playerData[i];

            // Compara el puntaje actual con el puntaje máximo
            if (jugaActual.puntaje > puntajeMaximo)
            {
                puntajeMaximo = jugaActual.puntaje; // Actualiza el puntaje máximo si es mayor
                jugaMaximo = jugaActual;
            }
        }
        jugaMaximo.gano = true;
        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            //Debug.Log("La lista Ids es " + playerData[i].jugadorId);
            if (PlayerNetwork.Instance.playerData[i].jugadorId == jugaMaximo.jugadorId)
            {
                PlayerNetwork.Instance.playerData[i] = jugaMaximo;
            }
        }
        Debug.Log("El jugador " + jugaMaximo.nomJugador + " es el gandor");
        SceneManager.LoadScene("EscenaFinal");
        PlayerNetwork.Instance.CargarEscenaFinalClientRpc();
        */
        SceneManager.LoadScene("AbandonarScene");
        PlayerNetwork.Instance.CargarAbandonarSceneClientRpc();
    }

    [ClientRpc]
    public void CargarEscenaFinalClientRpc()
    {
        SceneManager.LoadScene("EscenaFinal");
    }
    [ClientRpc]
    public void CargarAbandonarSceneClientRpc()
    {
        SceneManager.LoadScene("AbandonarScene");
    }
}

