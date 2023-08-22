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
//using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem.OSX;
using UnityEngine.UI;

public class PlayerNetwork : NetworkBehaviour
{
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
    public TipoObjeto tipoActual;
    private ComprobarObjeto comprobarObjeto;
    public Button confirmBaseButton;
    //private bool canPlace = false;
    public bool IsInitialized { get; private set; } = false; // Añade este campo de estado
    public NetworkList<int> playerIDs;
    public int currentTurnIndex = 0;
    public NetworkList<PlayerNetwork.DatosJugador> playerData;
    private Dictionary<int, Dictionary<string, int>> recursosPorJugador = new Dictionary<int, Dictionary<string, int>>();

    // Singleton instance
    public static PlayerNetwork Instance { get; private set; }
    public object NetworkVariablePermission { get; private set; }

    public NetworkVariable<DatosJugador> jugador;

    public struct DatosJugador : INetworkSerializable, IEquatable<DatosJugador>
    {
        public int jugadorId;
        public FixedString64Bytes nomJugador;
        public int puntaje;
        public bool gano;
        public bool turno;
        public int cantidadCasa;
        public int maderaCount;
        public int ladrilloCount;
        public int ovejaCount;
        public int piedraCount;
        public int trigoCount;
        public FixedString64Bytes colorJugador;

        public bool Equals(DatosJugador other)
        {
            return jugadorId == other.jugadorId && nomJugador.Equals(other.nomJugador) && colorJugador.Equals(other.colorJugador);
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref jugadorId);
            serializer.SerializeValue(ref puntaje);
            //serializer.SerializeValue(ref cantidadJugadores);
            serializer.SerializeValue(ref gano);
            serializer.SerializeValue(ref turno);
            serializer.SerializeValue(ref maderaCount);
            serializer.SerializeValue(ref ladrilloCount);
            serializer.SerializeValue(ref ovejaCount);
            serializer.SerializeValue(ref piedraCount);
            serializer.SerializeValue(ref trigoCount);
            serializer.SerializeValue(ref cantidadCasa);
            serializer.SerializeValue(ref nomJugador);
            serializer.SerializeValue(ref colorJugador);
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
                    cantidadCasa = 0,
                    maderaCount = 0,
                    ladrilloCount = 0,
                    ovejaCount = 0,
                    piedraCount = 0,
                    trigoCount = 0,
                    colorJugador = new FixedString64Bytes(),
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
            AgregarJugador(playerId, nombreHost, 100, false, true, 2, 10, 10, 10, 10, 10, colorHost);
            Debug.Log("se agrego jugador host");
            ImprimirTodosLosJugadores();
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
            AddPlayerServerRpc(playerId, nombreCliente, 100, false, false, 2, 10, 10, 10, 10, 10, colorCliente);
            Debug.Log("se agrego jugador cliente");
            ImprimirTodosLosJugadores();
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
            BoardManager.Instance.UpdateResourceTexts(playerId);
        }
    }

    public void AgregarJugador(int jugadorId, FixedString64Bytes nomJugador, int puntaje, bool gano, bool turno, int cantidadCasa, int maderaCount, int ladrilloCount, int ovejaCount, int piedraCount, int trigoCount, FixedString64Bytes colorJugador)
    {
        ImprimirPlayerIDs();
        Debug.Log($"AgregarJugador: {jugadorId}, {nomJugador}, {puntaje}, {gano}, {turno}, {cantidadCasa}, {maderaCount}, {ladrilloCount}, {ovejaCount}, {piedraCount}, {trigoCount}, {colorJugador}");       
        Debug.Log("playerId:" + playerIDs.Count); //.Count dice la cantidad de elementos qeu tiene la lista
        Debug.Log("playerData:" + playerData.Count);

        DatosJugador newDatos = new DatosJugador();
        newDatos.jugadorId = jugadorId;
        newDatos.nomJugador = nomJugador;
        newDatos.puntaje = puntaje;
        newDatos.gano = gano;
        newDatos.turno = turno;
        newDatos.cantidadCasa = cantidadCasa;
        newDatos.maderaCount = maderaCount;
        newDatos.ladrilloCount = ladrilloCount;
        newDatos.ovejaCount = ovejaCount;
        newDatos.piedraCount = piedraCount;
        newDatos.trigoCount = trigoCount;
        newDatos.colorJugador = colorJugador;
        Debug.Log("Cargue newDatos");
        try
        {
            Debug.Log("id jugador 2 =" + jugadorId);
            playerIDs.Add(jugadorId);
            Debug.Log("Cant elementos de playerId:" + playerIDs.Count);
            playerData.Add(newDatos);
            Debug.Log("Cant elementos de playerId:" + playerIDs.Count);         
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
                                   $"Cantidad de Casas: {data.cantidadCasa}, " +
                                   $"Madera: {data.maderaCount}, " +
                                   $"Ladrillo: {data.ladrilloCount}, " +
                                   $"Oveja: {data.ovejaCount}, " +
                                   $"Piedra: {data.piedraCount}, " +
                                   $"Trigo: {data.trigoCount}, " +
                                   $"Color: {data.colorJugador.ToString()}";

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
        Debug.Log("cantidadCasa: " + jugador.cantidadCasa);
        Debug.Log("maderaCount: " + jugador.maderaCount);
        Debug.Log("ladrilloCount: " + jugador.ladrilloCount);
        Debug.Log("ovejaCount: " + jugador.ovejaCount);
        Debug.Log("piedraCount: " + jugador.piedraCount);
        Debug.Log("trigoCount: " + jugador.trigoCount);
        Debug.Log("colorJugador: " + jugador.colorJugador.ToString());
    }
    public void ImprimirJugadorPorId(int idJugador)
    {
        Debug.Log("Buscando al jugador con ID: " + idJugador);
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
            Debug.Log("Información del jugador número " + (i + 1));
            DatosJugador jugadorActual = playerData[i];
            Debug.Log("ID Jugador: " + jugadorActual.jugadorId);
            Debug.Log("Nombre Jugador: " + jugadorActual.nomJugador.ToString());
            Debug.Log("Puntaje: " + jugadorActual.puntaje);
            Debug.Log("Ganó?: " + jugadorActual.gano);
            Debug.Log("Turno?: " + jugadorActual.turno);
            Debug.Log("Cantidad de Casas: " + jugadorActual.cantidadCasa);
            Debug.Log("Cuenta de Madera: " + jugadorActual.maderaCount);
            Debug.Log("Cuenta de Ladrillos: " + jugadorActual.ladrilloCount);
            Debug.Log("Cuenta de Ovejas: " + jugadorActual.ovejaCount);
            Debug.Log("Cuenta de Piedras: " + jugadorActual.piedraCount);
            Debug.Log("Cuenta de Trigo: " + jugadorActual.trigoCount);
            Debug.Log("Color de Jugador: " + jugadorActual.colorJugador.ToString());
            Debug.Log("----------------------------------------------------");
        }
    }
    public void CargarDatosJugador(int jugadorId, string nomJugador, int puntaje, int cantidadJugadores, bool gano, bool turno, int cantidadCasa, int maderaCount, int ladrilloCount, int ovejaCount, int piedraCount, int trigoCount, string colorJugador)
    {
        DatosJugador newDatos = new DatosJugador();
        newDatos.jugadorId = jugadorId;
        newDatos.nomJugador = new FixedString64Bytes(nomJugador ?? string.Empty); // si es null, asigna una cadena vacía
        newDatos.puntaje = puntaje;
        newDatos.gano = gano;
        newDatos.turno = turno;
        newDatos.cantidadCasa = cantidadCasa;
        newDatos.maderaCount = maderaCount;
        newDatos.ladrilloCount = ladrilloCount;
        newDatos.ovejaCount = ovejaCount;
        newDatos.piedraCount = piedraCount;
        newDatos.trigoCount = trigoCount;
        newDatos.colorJugador = new FixedString64Bytes(colorJugador ?? string.Empty); // si es null, asigna una cadena vacía

        jugador.Value = newDatos;
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
    public void AddPlayerServerRpc(int jugadorId, FixedString64Bytes nomJugador, int puntaje, bool gano, bool turno, int cantidadCasa,  int maderaCount, int ladrilloCount, int ovejaCount, int piedraCount, int trigoCount, FixedString64Bytes colorJugador)
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
            newPlayer.cantidadCasa = cantidadCasa;
            newPlayer.maderaCount = maderaCount;
            newPlayer.ladrilloCount = ladrilloCount;
            newPlayer.ovejaCount = ovejaCount;
            newPlayer.piedraCount = piedraCount;
            newPlayer.trigoCount = trigoCount;
            newPlayer.colorJugador = colorJugador;
            // ... y puedes agregar los demás valores predeterminados aquí
            Debug.Log("Se va a unir usando AddPlayerServerRpc");

            playerData.Add(newPlayer);
            playerIDs.Add(newPlayer.jugadorId);
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
/*
#pragma warning disable CS0114 // El miembro oculta el miembro heredado. Falta una contraseña de invalidación
    private void OnDestroy()
#pragma warning restore CS0114 // El miembro oculta el miembro heredado. Falta una contraseña de invalidación
    {
        // Reemplaza esto con las listas de red que estás utilizando.
        playerIDs.Dispose();
        playerData.Dispose();
    }
*/
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
            for (int i = 0; i < playerData.Count; i++)
            {
                Debug.Log("La lista Ids es " + playerData[i].jugadorId);
                if (playerData[i].jugadorId == idJugador)
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
            DatosJugador jugador = playerData[indexJugador];
            Debug.Log("piedra antes de sumar: " + playerData[indexJugador].piedraCount);
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
            playerData[indexJugador] = jugador;
            // Podemos usar un Debug.Log para ver los resultados.
            Debug.Log("Jugador " + playerData[indexJugador].jugadorId + " ahora tiene " + playerData[indexJugador].piedraCount + "piedras ");
        }
        else
        {
            AumentarRecursosServerRpc(idJugador, recurso, cantidad);
        } 
    }

    [ServerRpc(RequireOwnership = false)]
    public void AumentarRecursosServerRpc(int idJugador, string recurso, int cantidad)
    {
        Debug.Log("Entre a AumentarRecursoServerRpc");
        //AumentarRecursos(idJugador, recurso, cantidad);
        //Debug.Log("Termino AumentarRecursoServerRpc");
        Debug.Log("id jugador: " + idJugador+ " Recurso: " + recurso + " Cantidad: " + cantidad);
        bool jugadorEncontrado = false;
        int indexJugador = -1;

        // Búsqueda del jugador en la lista playerData
        for (int i = 0; i < playerData.Count; i++)
        {
            Debug.Log("La lista Ids es " + playerData[i].jugadorId);
            if (playerData[i].jugadorId == idJugador)
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
        DatosJugador jugador = playerData[indexJugador];
        Debug.Log("piedra antes de sumar: " + playerData[indexJugador].piedraCount);
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
        playerData[indexJugador] = jugador;
        // Podemos usar un Debug.Log para ver los resultados.
        Debug.Log("Jugador " + playerData[indexJugador].jugadorId + " ahora tiene " + playerData[indexJugador].piedraCount + "piedras ");    
    }

    [ServerRpc(RequireOwnership = false)]
    public void UpdateResourceTextsServerRpc(int jugadorId)
    {
        Debug.Log("Entre a UpdateResourceTextsServerRpc con ID "+ jugadorId);
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
        /*if (datosJugador.jugadorId == 0)  // Suponiendo que 0 no es un ID de jugador v�lido
        {
            Debug.LogError("Jugador con ID " + jugadorId + " no encontrado.");
            return;
        }*/
        // Actualiza los textos de los recursos
        BoardManager.Instance.MaderaCountText.text = datosJugador.maderaCount.ToString();
        BoardManager.Instance.LadrilloCountText.text = datosJugador.ladrilloCount.ToString();
        BoardManager.Instance.OvejaCountText.text = datosJugador.ovejaCount.ToString();
        BoardManager.Instance.PiedraCountText.text = datosJugador.piedraCount.ToString();
        BoardManager.Instance.TrigoCountText.text = datosJugador.trigoCount.ToString();
        Debug.Log("Termino UpdateResourceTextsServerRpc");
    }
    public void EndTurn()
    {
        Debug.Log("Entre al End Turn");
        if (NetworkManager.Singleton.IsServer)  // Asegúrate de que solo el servidor modifique el turno actual.
        {
            Debug.Log("Entre al is server de End Turn");
            currentTurnIndex++;
            if (currentTurnIndex >= playerIDs.Count)
            {
                currentTurnIndex = 0;
            }
            // Notifica a todos los jugadores sobre el cambio de turno.
            NotifyTurnChangeClientRpc(currentTurnIndex);
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
    public void PruebaServerRpc()
    {
        Debug.Log("Entre a la prueba");
    }
    [ServerRpc(RequireOwnership = false)]
    public void ColocarCaminoServerRpc(string color, string currentcamino, Vector3 posititon, Quaternion rotation)
    {
        try
        {
            Debug.Log("Entre a ColocarCaminoServerRpc");
            var objetoCamino = Resources.Load(currentcamino) as GameObject;
            Debug.Log("2 preafb base es " + objetoCamino.name);
            currentCamino = Instantiate(objetoCamino, posititon, rotation);
            currentCamino.GetComponent<NetworkObject>().Spawn();
            comprobarObjeto = objetoCamino.gameObject.GetComponent<ComprobarObjeto>();
            if (comprobarObjeto != null)
            {
                // Almacenar el tipo de objeto que acabamos de colocar
                comprobarObjeto.tipoObjeto = TipoObjeto.Camino; // Puedes cambiar esto al tipo de objeto que corresponda
                Debug.Log("puse el tipo del camino a: " + comprobarObjeto.tipoObjeto);
            }
            else
            {
                //Debug.LogError("El objeto " + objetoCollider.gameObject.name + " no tiene un script ComprobarObjeto.");
            }
            Debug.Log("el tipo del camino colocado es " + tipoActual);
            tipoActual = TipoObjeto.Ninguno;

        }
        catch (Exception e)
        {
            Debug.Log("Error en ColocarCaminoServerRpc: " + e);
        }

    }
    [ServerRpc(RequireOwnership = false)]
    public void ColocarBaseServerRpc(string color, string currentbase, string nombreCollider, Vector3 posititon)
    {
        try
        {
            Debug.Log("Entre a la BaseServerRpc");
            var objetoBase = Resources.Load(currentbase) as GameObject;
            Debug.Log("2 preafb base es " + objetoBase.name);
            //PlayerPrefs.SetString(colliderName, "collider");
            currentBase = Instantiate(objetoBase, posititon, Quaternion.identity);
            currentBase.GetComponent<NetworkObject>().Spawn();
            // Obtener el componente ComprobarObjeto del objeto golpeado
            var nombresinClone = ListaColliders.Instance.RemoverCloneDeNombre(nombreCollider);
            Debug.Log("CPC PlayerNetwo" + nombresinClone);
            ListaColliders.Instance.ModificarTipoPorNombre(nombresinClone, "Base");
            ListaColliders.Instance.ImprimirListaColliders();
            //comprobarObjeto = currentBase.GetComponent<ComprobarObjeto>();
            //comprobarObjeto = objetoBase.gameObject.GetComponent<ComprobarObjeto>();
            //Debug.Log("el collider es : " + hit.collider.gameObject.name);
            //Debug.Log("comprobarobjeto base Cliente: " + comprobarObjeto.name);
            // Asegurarse de que el componente existe

            /*if (comprobarObjeto != null)
            {
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
                //Debug.Log("Asignando tipo a: " + hit.collider.gameObject.name + " - Instancia: " + hit.collider.gameObject.GetInstanceID());
            }
            else
            {
                Debug.LogError("El objeto " + objetoBase.gameObject.name + " no tiene un script ComprobarObjeto.");
            }
            */
            //Debug.Log("el tipo de la base colocada es " + tipoActual);
            tipoActual = TipoObjeto.Ninguno;
        }
        catch (Exception e)
        {
            Debug.Log("Error en ColocarBaseServerRpc: " + e);
        }

    }
    [ServerRpc(RequireOwnership = false)]
    public void ColocarPuebloServerRpc(string color, string currentpueblo, Vector3 posititon)
    {
        try
        {
            Debug.Log("Entre a la PuebloServerRpc");
            var objetoPueblo = Resources.Load(currentpueblo) as GameObject;
            Debug.Log("2 preafb pueblo es " + objetoPueblo.name);
            //PlayerPrefs.SetString(colliderName, "collider");
            currentPueblo = Instantiate(objetoPueblo, posititon, Quaternion.identity);
            currentPueblo.GetComponent<NetworkObject>().Spawn();
            // Obtener el componente ComprobarObjeto del objeto golpeado
            comprobarObjeto = objetoPueblo.gameObject.GetComponent<ComprobarObjeto>();
            //Debug.Log("el collider es : " + hit.collider.gameObject.name);
            //Debug.Log("comprobarobjeto al poner la base: " + comprobarObjeto);
            // Asegurarse de que el componente existe
            if (comprobarObjeto != null)
            {
                // Guardar una referencia a la pieza que acabamos de colocar
                //comprobarObjeto.objetoColocado = this; // esto pone ControldorColocarPieza
                //Debug.Log("EL OBJETO colocado es: " + comprobarObjeto.objetoColocado);

                // Almacenar el tipo de objeto que acabamos de colocar
                comprobarObjeto.tipoObjeto = TipoObjeto.Pueblo; // Puedes cambiar esto al tipo de objeto que corresponda
                Debug.Log("puse el tipo del pueblo a: " + comprobarObjeto.tipoObjeto);
            }
            else
            {
                //Debug.LogError("El objeto " + objetoCollider.gameObject.name + " no tiene un script ComprobarObjeto.");
            }
            //Debug.Log("el tipo del pueblo colocado es " + tipoActual);
            tipoActual = TipoObjeto.Ninguno;
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

        if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1)
        {
            // Restar recursos
            BoardManager.Instance.UpdateResourcesCamino(jugador);

            // Actualizar la UI del cliente con la nueva cantidad de recursos
            UpdateClientResourcesClientRpc(jugadorId);

        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ComprarBaseServerRpc(int jugadorId)
    {
        Debug.Log("Entro a comprarBaseServerRpc");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(jugadorId);

        if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1 && jugador.trigoCount >= 1 && jugador.ovejaCount >= 1)
        {
            // Restar recursos
            BoardManager.Instance.UpdateResourcesBase(jugador);

            // Actualizar la UI del cliente con la nueva cantidad de recursos
            UpdateClientResourcesClientRpc(jugadorId);
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void ComprarPuebloServerRpc(int jugadorId)
    {
        Debug.Log("Entro a comprarPuebloServerRpc");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(jugadorId);

        if (jugador.trigoCount >= 3 && jugador.piedraCount >= 2)
        {
            // Restar recursos
            BoardManager.Instance.UpdateResourcesPueblo(jugador);

            // Actualizar la UI del cliente con la nueva cantidad de recursos
            UpdateClientResourcesClientRpc(jugadorId);
        }
    }
    [ClientRpc]
    public void UpdateClientResourcesClientRpc(int jugadorId)
    {
        if (PlayerPrefs.GetInt("jugadorId") == jugadorId)
        {
            BoardManager.Instance.UpdateResourceTexts(jugadorId);
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void TirarDadosServerRpc(int idJugador)
    {
        Debug.Log("Entro a TirarDadosServerRpc con ID "+idJugador);
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
        DiceNumberTextScript.Instance.DarResultadoRandom();
        //int playerId = PlayerPrefs.GetInt("jugadorId");
        //int currentPlayerID = TurnManager.Instance.CurrentPlayerID;
        //Debug.Log("el id que toco TirarDados es" + currentPlayerID);
        ManejoParcelasServerRpc(DiceNumberTextScript.Instance.randomDiceNumber, idJugador);
        //tirarDadoButton.interactable = false;
    }
    [ServerRpc(RequireOwnership = false)]
    public void ManejoParcelasServerRpc(int num, int jugadorId)
    {
        Debug.Log("Cliente - ManejoParcelasServerRpcs ID "+jugadorId);
        BoardManager.Instance.ManejoParcelas(num, jugadorId);
    }

}

