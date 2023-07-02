using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{

    //public bool IsHost = false;
    // Singleton instance
    public static PlayerNetwork Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Para mantener el objeto al cambiar de escena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private List<NetworkVariable<DatosJugador>> jugadores = new List<NetworkVariable<DatosJugador>>();

    public struct DatosJugador : INetworkSerializable
    {
        public int jugadorId;
        public string nomJugador;
        public int puntaje;
        public int cantidadCartas;
        public bool gano;
        public bool turno;
        public int cantidadCasa;
        public int maderaCount;
        public int ladrilloCount;
        public int ovejaCount;
        public int piedraCount;
        public int trigoCount;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref jugadorId);
            serializer.SerializeValue(ref puntaje);
            serializer.SerializeValue(ref cantidadCartas);
            serializer.SerializeValue(ref gano);
            serializer.SerializeValue(ref turno);
            serializer.SerializeValue(ref maderaCount);
            serializer.SerializeValue(ref ladrilloCount);
            serializer.SerializeValue(ref ovejaCount);
            serializer.SerializeValue(ref piedraCount);
            serializer.SerializeValue(ref trigoCount);
            serializer.SerializeValue(ref cantidadCasa);
            serializer.SerializeValue(ref nomJugador);

        }
    }


    private void Start()
    {
       
    }


    public void CrearJugadores()
    {
        Debug.Log("HOLA QUE TAL");

        // Verificamos si es el host
        //isHost = IsHost;

        // Solo el host crea la lista de jugadores
        if (IsHost)
        {
            Debug.Log("entre al host de start");
            // Crear 4 jugadores con todas las variables en 0
            for (int i = 0; i < 4; i++)
            {
                //Debug.Log("EL CONTADOR DE PLAYER" + i);
                DatosJugador jugador = new DatosJugador
                {
                    jugadorId = i,
                    puntaje = 0,
                    cantidadCartas = 0,
                    gano = false,
                    turno = false,
                    maderaCount = 0,
                    ladrilloCount = 0,
                    ovejaCount = 0,
                    piedraCount = 0,
                    trigoCount = 0,
                    cantidadCasa = 0,
                    nomJugador = "carla"
                };
                Debug.Log("llegue hasta aca");
                NetworkVariable<DatosJugador> networkJugador = new NetworkVariable<DatosJugador>(jugador, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
                Debug.Log("llegue hast aca 2");
                jugadores.Add(networkJugador);
                Debug.Log("ID JUGADOR " + jugadores[i].Value.nomJugador);
                //Debug.Log(jugadores.Count+" info de jugadores");
            }
        }
    }
    public void AgregarDatosJugador(int idJugador, DatosJugador datos)
    {
        if (!IsHost) return; // Solo el host puede agregar datos a la lista de jugadores

        if (idJugador >= 0 && idJugador < jugadores.Count)
        {
            jugadores[idJugador].Value = datos;
        }
    }

    public int GetJugadorId(int idJugador)
    {
        return jugadores[idJugador].Value.jugadorId;
    }

    public void SetJugadorId(int idJugador, int id)
    {
        if (!IsHost) return; // Solo el host puede cambiar el ID del jugador

        if (idJugador >= 0 && idJugador < jugadores.Count)
        {
            var datos = jugadores[idJugador].Value;
            datos.jugadorId = id;
            jugadores[idJugador].Value = datos;
        }
    }
    public string GetNomJugador(int idJugador)
    {
        Debug.Log("Entre a GetNomJugador");
        return jugadores[idJugador].Value.nomJugador;
    }
    /*
    public void SetNomJugador(int idJugador, string nombre)
    {
        //if (!isHost) return; // Solo el host puede cambiar el ID del jugador
        Debug.Log("Entre a SetNomJugador");
        if (idJugador >= 0 && idJugador < jugadores.Count)
        {
            //Debug.Log("Entre a SetNomJugador1");
            DatosJugador aux = new DatosJugador();
            aux = jugadores[idJugador].Value;
            Debug.Log("deberia decir carla :" + aux.nomJugador);
            aux.nomJugador=nombre;
            jugadores[idJugador].Value = aux;
            Debug.Log("deberia decir lucas :" + jugadores[idJugador].Value.nomJugador);
            Debug.Log("Entre a SetNomJugador4");
        }
    }
    */

    [ServerRpc]
    public void SetNomJugadorServerRpc(int idJugador, string nombre)
    {
        // Aquí puedes incluir cualquier validación adicional que necesites
        // ...

        // Asegurarte de que el ID es válido
        //if (idJugador >= 0 && idJugador < jugadores.Count)
        {
            Debug.Log("Entre a SetNomJugadorServerRpc");
            DatosJugador aux = jugadores[idJugador].Value;
            Debug.Log("JAJAS");
            aux.nomJugador = nombre;
            Debug.Log("JAJAS2");
            jugadores[idJugador].Value = aux;
            
            Debug.Log("aca tiene que decir lucas :" + jugadores[idJugador].Value.nomJugador);
            Debug.Log("JAJAS 32");
        }
    }

    public void RequestSetNomJugador(int idJugador, string nombre)
    {
        // Esto enviará la solicitud al servidor para cambiar el nombre del jugador
        Debug.Log("Entre a RequestSetNomJugador");
        if (IsHost)
        {
            Debug.Log("SOY EL HOST");
        }

        SetNomJugadorServerRpc(idJugador, nombre);
    }


    public int GetPuntaje(int idJugador)
    {
        return jugadores[idJugador].Value.puntaje;
    }

    public void SetPuntaje(int idJugador, int puntaje)
    {
        if (!IsHost) return; // Solo el host puede cambiar el puntaje del jugador

        if (idJugador >= 0 && idJugador < jugadores.Count)
        {
            var datos = jugadores[idJugador].Value;
            datos.puntaje = puntaje;
            jugadores[idJugador].Value = datos;
        }
    }

    public int GetCantidadCartas(int idJugador)
    {
        return jugadores[idJugador].Value.cantidadCartas;
    }

    public void SetCantidadCartas(int idJugador, int cantidad)
    {
        if (!IsHost) return; // Solo el host puede cambiar la cantidad de cartas del jugador

        if (idJugador >= 0 && idJugador < jugadores.Count)
        {
            var datos = jugadores[idJugador].Value;
            datos.cantidadCartas = cantidad;
            jugadores[idJugador].Value = datos;
        }
    }

    public bool GetGano(int idJugador)
    {
        return jugadores[idJugador].Value.gano;
    }

    public void SetGano(int idJugador, bool gano)
    {
        if (!IsHost) return; // Solo el host puede cambiar el estado de ganador del jugador

        if (idJugador >= 0 && idJugador < jugadores.Count)
        {
            var datos = jugadores[idJugador].Value;
            datos.gano = gano;
            jugadores[idJugador].Value = datos;
        }
    }

    public bool GetTurno(int idJugador)
    {
        return jugadores[idJugador].Value.turno;
    }

    public void SetTurno(int idJugador, bool turno)
    {
        if (!IsHost) return; // Solo el host puede cambiar el turno del jugador

        if (idJugador >= 0 && idJugador < jugadores.Count)
        {
            var datos = jugadores[idJugador].Value;
            datos.turno = turno;
            jugadores[idJugador].Value = datos;
        }
    }

    public int GetCantidadCasa(int idJugador)
    {
        return jugadores[idJugador].Value.cantidadCasa;
    }

    public void SetCantidadCasa(int idJugador, int cantidad)
    {
        if (!IsHost) return; // Solo el host puede cambiar la cantidad de casas del jugador

        if (idJugador >= 0 && idJugador < jugadores.Count)
        {
            var datos = jugadores[idJugador].Value;
            datos.cantidadCasa = cantidad;
            jugadores[idJugador].Value = datos;
        }
    }

    //hacer get y set de los recursos
    private void Update()
    {
        if (!IsOwner) return;

        // Resto del código...
    }
}
