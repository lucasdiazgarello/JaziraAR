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

    public bool IsInitialized { get; private set; } = false; // A�ade este campo de estado
    public Button buttonToPress;
    public Button buttonPrint;
    public Button buttonLoad;
    public ulong PlayerId => NetworkObjectId;

    // Singleton instance
    public static PlayerNetwork Instance { get; private set; }
    public object NetworkVariablePermission { get; private set; }

    public NetworkVariable<DatosJugador> jugador;

    public struct DatosJugador : INetworkSerializable, IEquatable<DatosJugador>
    {
        public ulong jugadorId;
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

    public NetworkList<ulong> playerIDs;
    private int myInt;
    public NetworkList<PlayerNetwork.DatosJugador> playerData;

    private void Awake()
    {
        //myInt = EditorPrefs.GetInt("myIntKey");
        //playerData = new NetworkList<DatosJugador>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Para mantener el objeto al cambiar de escena
            // Inicializar los jugadores aqu�
            //CrearJugadores();
            // Mover inicializaciones aqu�
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

            playerIDs = new NetworkList<ulong>();
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


    public void AgregarJugador(string nomJugador, int puntaje, int cantidadJugadores, bool gano, bool turno, int cantidadCasa, int maderaCount, int ladrilloCount, int ovejaCount, int piedraCount, int trigoCount, string colorJugador)
    {
        if (playerData.Count >= 4 || playerData.Count >= cantidadJugadores)
        {
            Debug.LogWarning("No se agregan mas de 4 jugadores.");
            return;
        }
        DatosJugador newDatos = new DatosJugador();
        newDatos.jugadorId = PlayerId;
        //newDatos.jugadorId = jugadorId;
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

        playerIDs.Add(PlayerId);
        playerData.Add(newDatos);
    }

    public DatosJugador GetPlayerData(ulong jugadorId)
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

    public void ImprimirDatosJugador(int index)
    {
        Debug.Log("Imprimiendo datos del jugador " + index);
        Debug.Log("id: " + playerData[index].jugadorId);
        Debug.Log("Nombre Jugador: " + playerData[index].nomJugador.ToString());
        Debug.Log("Puntaje: " + playerData[index].puntaje);
        Debug.Log("Cantidad de Jugadores: " + playerData[index].cantidadJugadores);
        Debug.Log("Gan�?: " + playerData[index].gano);
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

    private void Update()
    {
        if (!IsOwner) return;


        // Resto del c�digo...
    }
}