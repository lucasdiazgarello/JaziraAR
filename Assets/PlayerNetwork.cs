using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Collections;
using Unity.Netcode;
using UnityEditor;
//using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem.OSX;
using UnityEngine.UI;

public class PlayerNetwork : NetworkBehaviour
{
    //public List<NetworkVariable<DatosJugador>> jugadores = new List<NetworkVariable<DatosJugador>>();

    public bool IsInitialized { get; private set; } = false; // Añade este campo de estado
    public Button buttonToPress;
    public Button buttonPrint;
    public Button buttonLoad;



    private Dictionary<int, Dictionary<string, int>> recursosPorJugador = new Dictionary<int, Dictionary<string, int>>();
    //private int myInt;

    /*public NetworkVariable<Dictionary<int, DatosJugador>> jugadores =
        new NetworkVariable<Dictionary<int, DatosJugador>>(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.Everyone,
            ReadPermission = NetworkVariablePermission.Everyone
        });
    */
    // Singleton instance
    public static PlayerNetwork Instance { get; private set; }
    public object NetworkVariablePermission { get; private set; }

    public NetworkVariable<DatosJugador> jugador;

    public struct DatosJugador : INetworkSerializable, IEquatable<DatosJugador>
    {
        public int jugadorId;
        public FixedString64Bytes nomJugador;
        public int puntaje;
        //public int cantidadJugadores;
        public bool gano;
        public bool turno;
        public int cantidadCasa;
        public int maderaCount;
        public int ladrilloCount;
        public int ovejaCount;
        public int piedraCount;
        public int trigoCount;
        public FixedString64Bytes colorJugador;

        /*public bool Equals(DatosJugador other)
        {
            throw new NotImplementedException();
        }*/
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
    }

    public NetworkList<int> playerIDs;
    private int myInt;
    public NetworkList<PlayerNetwork.DatosJugador> playerData;

    /*public NetworkList<int> playerIDs = new NetworkList<int>();
    private int myInt;

    //public NetworkList<DatosJugador> playerData = new NetworkList<DatosJugador>();
    public NetworkList<PlayerNetwork.DatosJugador> playerData { get; } = new NetworkList<PlayerNetwork.DatosJugador>();
    */

    private void Awake()
    {
        //myInt = EditorPrefs.GetInt("myIntKey");
        //playerData = new NetworkList<DatosJugador>();
        if (Instance == null)
        {
            Debug.Log("Instancia de PlayerNetwork");
            Instance = this;
            DontDestroyOnLoad(gameObject); // Para mantener el objeto al cambiar de escena
            // Inicializar los jugadores aquí
            //CrearJugadores();
            // Mover inicializaciones aquí
            jugador = new NetworkVariable<DatosJugador>(
                new DatosJugador
                {
                    jugadorId = 0,
                    nomJugador = new FixedString64Bytes(),
                    puntaje = 0,
                    //cantidadJugadores = 0,
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

            playerIDs = new NetworkList<int>();
            playerData = new NetworkList<PlayerNetwork.DatosJugador>();
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

    /*private void Start()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }
    }
    public override void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
        base.OnDestroy(); // Esto llama al método OnDestroy() en la clase base.
        playerIDs.Dispose();
        playerData.Dispose();
    }

    void OnClientConnected(ulong clientId)
    {
        // Este es el código que se ejecutará cuando un cliente se conecte.
        // Aquí es donde podrías llamar a tu función para agregar el jugador al servidor.
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            Debug.Log("Id CLIENTE A CONECTAR: " + Unirse.Instance.clientePlayerID);
            AddPlayerServerRpc(Unirse.Instance.clientePlayerID, Unirse.Instance.nombreTemporal.Value, Unirse.Instance.nombreTemporal.Value);
            Debug.Log("POST AddPlayerServerRpc");
            ImprimirTodosLosJugadores();
        }
    }*/
    public void AgregarJugador(int jugadorId, FixedString64Bytes nomJugador, int puntaje, bool gano, bool turno, int cantidadCasa, int maderaCount, int ladrilloCount, int ovejaCount, int piedraCount, int trigoCount, FixedString64Bytes colorJugador)
    {
        Debug.Log($"AgregarJugador: {jugadorId}, {nomJugador}, {puntaje}, {gano}, {turno}, {cantidadCasa}, {maderaCount}, {ladrilloCount}, {ovejaCount}, {piedraCount}, {trigoCount}, {colorJugador}");
        DatosJugador newDatos = new DatosJugador();
        newDatos.jugadorId = jugadorId;
        //newDatos.nomJugador = new FixedString64Bytes(nomJugador ?? string.Empty);
        newDatos.nomJugador = nomJugador;
        newDatos.puntaje = puntaje;
        //newDatos.cantidadJugadores = cantidadJugadores;
        newDatos.gano = gano;
        newDatos.turno = turno;
        newDatos.cantidadCasa = cantidadCasa;
        newDatos.maderaCount = maderaCount;
        newDatos.ladrilloCount = ladrilloCount;
        newDatos.ovejaCount = ovejaCount;
        newDatos.piedraCount = piedraCount;
        newDatos.trigoCount = trigoCount;
        //newDatos.colorJugador = new FixedString64Bytes(colorJugador ?? string.Empty);
        newDatos.colorJugador = colorJugador;

        playerIDs.Add(jugadorId);
        playerData.Add(newDatos);
    }

    /*public void ImprimirDatosJugador()
    {
        Debug.Log("ID Jugador: " + jugador.Value.jugadorId);
        Debug.Log("Nombre Jugador: " + jugador.Value.nomJugador.ToString());
        Debug.Log("Puntaje: " + jugador.Value.puntaje);
        Debug.Log("Cantidad de Jugadores: " + jugador.Value.cantidadJugadores);
        Debug.Log("Ganó?: " + jugador.Value.gano);
        Debug.Log("Turno?: " + jugador.Value.turno);
        Debug.Log("Cantidad de Casas: " + jugador.Value.cantidadCasa);
        Debug.Log("Cuenta de Madera: " + jugador.Value.maderaCount);
        Debug.Log("Cuenta de Ladrillos: " + jugador.Value.ladrilloCount);
        Debug.Log("Cuenta de Ovejas: " + jugador.Value.ovejaCount);
        Debug.Log("Cuenta de Piedras: " + jugador.Value.piedraCount);
        Debug.Log("Cuenta de Trigo: " + jugador.Value.trigoCount);
        Debug.Log("Color de Jugador: " + jugador.Value.colorJugador.ToString());
    }*/
    public void ImprimirPlayerIDs()
    {
        Debug.Log("Imprimiendo lista de IDs de jugadores:");
        for (int i = 0; i < playerIDs.Count; i++)
        {
            Debug.Log("ID del Jugador " + (i + 1) + ": " + playerIDs[i]);
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
            //Debug.Log("Cantidad de Jugadores: " + jugadorActual.cantidadJugadores);
            Debug.Log("Ganó?: " + jugadorActual.gano);
            Debug.Log("Turno?: " + jugadorActual.turno);
            Debug.Log("Cantidad de Casas: " + jugadorActual.cantidadCasa);
            Debug.Log("Cuenta de Madera: " + jugadorActual.maderaCount);
            Debug.Log("Cuenta de Ladrillos: " + jugadorActual.ladrilloCount);
            Debug.Log("Cuenta de Ovejas: " + jugadorActual.ovejaCount);
            Debug.Log("Cuenta de Piedras: " + jugadorActual.piedraCount);
            Debug.Log("Cuenta de Trigo: " + jugadorActual.trigoCount);
            //Debug.Log("Color de Jugador: " + jugadorActual.colorJugador.ToString(Encoding.UTF8));
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
        //newDatos.cantidadJugadores = cantidadJugadores;
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

    /*public void CargarDatosColorJugador(string colorSeleccionado)
    {
        Debug.Log("CargarDatosColorJugador fue llamado con: " + colorSeleccionado);
        DatosJugador datosActuales = jugador.Value;
        datosActuales.colorJugador = colorSeleccionado;
        jugador.Value = datosActuales;
        Debug.Log("color neuvo es: " + jugador.Value.colorJugador);
    }*/

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Debug.Log("Entre a OnNetworkSpawn");
            // Asigna los valores temporales a las NetworkVariables
            Unirse.Instance.nombreJugador.Value = Unirse.Instance.nombreTemporal;
            Unirse.Instance.colorSeleccionado.Value = Unirse.Instance.colorTemporal;

            // Obtiene el ID del jugador
            //int myPlayerId = (int)NetworkManager.Singleton.LocalClientId;
            int myPlayerId = PlayerPrefs.GetInt("jugadorId");
            //AddPlayerServerRpc(currentPlayerID, nombreTemporal.Value, nombreTemporal.Value);
            // Llama a los métodos ServerRpc para actualizar los datos del jugador en el servidor
            UpdatePlayerColorServerRpc(myPlayerId, Unirse.Instance.colorSeleccionado.Value);
            UpdatePlayerNameServerRpc(myPlayerId, Unirse.Instance.nombreJugador.Value);

            // Supongo que este es otro método ServerRpc que tienes para notificar al servidor de que un jugador se ha unido
            NotifyServerOfJoinServerRpc();
        }
    }
    private void Update()
    {
        if (!IsOwner) // si es cliente
        {
            //Debug.Log("Soy cliente");
            /*
            var nombre = "prueba";
            var color = "magenta";
            TestServerRpc(nombre,color); 
            */
        }
        else // si es host
        {
            /*var nombre = "prueba";
            var color = "magenta";
            TestServerRpc(nombre, color);*/
        }
        return;
        //ACA HAY QUE HACER TODAS LAS FUNCIONES QUE TRANSMITEN AL CLIENTE DATA
        //El cliente no agrega jugadores, solo le pasa los datos para que el host lo agregue y a menos que invoque a una funcion que traiga info del host no se entera de nada 
        //mirar min 35, COMPLETE Unity Multiplayer Tutorial (Netcode for Game Objects)
        // Resto del código... 
        //TestServerRpc(nombre, color); //de alguna manera hay que traer las variables de testrelay para usarlas aca
    }
    
    // Este es tu nuevo método RPC para agregar un jugador
    [ServerRpc(RequireOwnership = false)]
    public void AddPlayerServerRpc(int jugadorId, string nomJugador, FixedString64Bytes colorJugador, ServerRpcParams rpcParams = default)
    {
        try
        {
            Debug.Log("Entre a AddPlayerServerRpc");
            // Verificar que solo el host puede ejecutar este código
            if (!IsServer) return;

            // Agrega al jugador a la lista de jugadores
            DatosJugador newPlayer = new DatosJugador();
            newPlayer.jugadorId = jugadorId;
            newPlayer.nomJugador = new FixedString64Bytes(nomJugador);
            newPlayer.colorJugador = colorJugador;
            // ... y puedes agregar los demás valores predeterminados aquí
            playerData.Add(newPlayer);
            playerIDs.Add(newPlayer.jugadorId);
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
        AgregarJugador(myPlayerId, nombre, 100, false, true, 2, 10, 10, 10, 10, 10, color);
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
        Debug.Log("Entre a AumentarRecurso");
        Debug.Log("id jugador: " + idJugador);
        Debug.Log("Recurso: " + recurso);
        Debug.Log("Cantidad: " + cantidad);

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

    /*
    public void BuscarElementosDeUI()
    {
        // Buscar la escena por nombre
        Scene escenaDeJuego = SceneManager.GetSceneByName("SampleScene");

        // Asegurarse de que la escena esté cargada antes de intentar buscar objetos en ella
        if (escenaDeJuego.isLoaded)
        {
            // Buscar los objetos de texto en la escena
            foreach (GameObject obj in escenaDeJuego.GetRootGameObjects())
            {
                switch (obj.name)
                {
                    case "NombreDelObjetoDeTextoDeMadera":
                        MaderaCountText = obj.GetComponent<Text>();
                        break;
                    case "NombreDelObjetoDeTextoDeLadrillo":
                        LadrilloCountText = obj.GetComponent<Text>();
                        break;
                    case "NombreDelObjetoDeTextoDeOveja":
                        OvejaCountText = obj.GetComponent<Text>();
                        break;
                    case "NombreDelObjetoDeTextoDePiedra":
                        PiedraCountText = obj.GetComponent<Text>();
                        break;
                    case "NombreDelObjetoDeTextoDeTrigo":
                        TrigoCountText = obj.GetComponent<Text>();
                        break;
                }
            }
        }
        else
        {
            Debug.LogWarning("La escena de juego no está cargada.");
        }
    }
    */

}


