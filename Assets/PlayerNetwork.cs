using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Netcode;
//using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNetwork : NetworkBehaviour
{
    //public List<NetworkVariable<DatosJugador>> jugadores = new List<NetworkVariable<DatosJugador>>();

    public bool IsInitialized { get; private set; } = false; // Añade este campo de estado
    public Button buttonToPress;
    public Button buttonPrint;
    public Button buttonLoad;
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

    /*public NetworkVariable<DatosJugador> jugador = new NetworkVariable<DatosJugador>(
        new DatosJugador
        {
            jugadorId = 0,
            nomJugador = new FixedString64Bytes(),
            puntaje = 0,
            cantidadJugadores = 0,
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
    */
    public struct DatosJugador : INetworkSerializable, IEquatable<DatosJugador>
    {
        public int jugadorId;
        public FixedString64Bytes nomJugador;
        public int puntaje;
        public int cantidadJugadores;
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
            serializer.SerializeValue(ref cantidadJugadores);
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
                    cantidadJugadores = 0,
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
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //buttonToPress.onClick.AddListener(PrintPlayerName);
        //Debug.Log("antes de cargar");
        //buttonPrint.onClick.AddListener(ImprimirDatosJugador);
        //buttonLoad.onClick.AddListener(() => CargarDatosJugador(1, "Jugador", 100, 5, false, true, 2, 10, 10, 10, 10, 10));
        //Debug.Log("despues de cargar");
        //buttonPrint.onClick.AddListener(ImprimirDatosJugador);

    }

    /*public void CargarDatosJugador(int jugadorId, string nomJugador, int puntaje, int cantidadJugadores, bool gano, bool turno, int cantidadCasa, int maderaCount, int ladrilloCount, int ovejaCount, int piedraCount, int trigoCount, string colorJugador)
    {
        DatosJugador newDatos = new DatosJugador();
        newDatos.jugadorId = jugadorId;
        newDatos.nomJugador = new FixedString64Bytes(nomJugador ?? string.Empty); // si es null, asigna una cadena vacía
        newDatos.puntaje = puntaje;
        newDatos.cantidadJugadores = cantidadJugadores;
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
    }*/

    public void AgregarJugador(int jugadorId, string nomJugador, int puntaje, int cantidadJugadores, bool gano, bool turno, int cantidadCasa, int maderaCount, int ladrilloCount, int ovejaCount, int piedraCount, int trigoCount, string colorJugador)
    {
        DatosJugador newDatos = new DatosJugador();
        newDatos.jugadorId = jugadorId;
        newDatos.nomJugador = new FixedString64Bytes(nomJugador ?? string.Empty);
        newDatos.puntaje = puntaje;
        newDatos.cantidadJugadores = cantidadJugadores;
        newDatos.gano = gano;
        newDatos.turno = turno;
        newDatos.cantidadCasa = cantidadCasa;
        newDatos.maderaCount = maderaCount;
        newDatos.ladrilloCount = ladrilloCount;
        newDatos.ovejaCount = ovejaCount;
        newDatos.piedraCount = piedraCount;
        newDatos.trigoCount = trigoCount;
        newDatos.colorJugador = new FixedString64Bytes(colorJugador ?? string.Empty); 

        playerIDs.Add(jugadorId);
        playerData.Add(newDatos);
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
    public void ImprimirDatosJugador(int index)
    {
        Debug.Log("Imprimiendo datos del jugador " + index);
        Debug.Log("id: " + playerData[index].jugadorId);
        Debug.Log("Nombre Jugador: " + playerData[index].nomJugador.ToString());
        Debug.Log("Puntaje: " + playerData[index].puntaje);
        Debug.Log("Cantidad de Jugadores: " + playerData[index].cantidadJugadores);
        Debug.Log("Ganó?: " + playerData[index].gano);
        Debug.Log("Turno?: " + playerData[index].turno);
        Debug.Log("Cantidad de Casas: " + playerData[index].cantidadCasa);
        Debug.Log("Cuenta de Madera: " + playerData[index].maderaCount);
        Debug.Log("Cuenta de Ladrillos: " + playerData[index].ladrilloCount);
        Debug.Log("Cuenta de Ovejas: " + playerData[index].ovejaCount);
        Debug.Log("Cuenta de Piedras: " + playerData[index].piedraCount);
        Debug.Log("Cuenta de Trigo: " + playerData[index].trigoCount);
        Debug.Log("Color de Jugador: " + playerData[index].colorJugador.ToString());
        Debug.Log("--------------------------------");
    }

    public void MostrarJugadores()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            ImprimirDatosJugador(i);
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

    private void Update()
    {
        if (!IsOwner) return;


        // Resto del código...
    }
}