using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{

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

    private bool isHost = false;

    private void Start()
    {
        // Solo el host crea la lista de jugadores
        if (IsHost)
        {
            isHost = true;

            // Crear 4 jugadores con todas las variables en 0
            for (int i = 0; i < 4; i++)
            {
                DatosJugador jugador = new DatosJugador
                {
                    jugadorId = i,
                    puntaje = 0,
                    cantidadCartas = 0,
                    gano = false,
                    turno = false,
                    cantidadCasa = 0,
                    maderaCount = 0,
                    ladrilloCount= 0,
                    ovejaCount = 0,
                    piedraCount= 0,
                    trigoCount= 0
};

                NetworkVariable<DatosJugador> networkJugador = new NetworkVariable<DatosJugador>(jugador, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
                jugadores.Add(networkJugador);
            }
        }
    }

    public void AgregarDatosJugador(int idJugador, DatosJugador datos)
    {
        if (!isHost) return; // Solo el host puede agregar datos a la lista de jugadores

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
        if (!isHost) return; // Solo el host puede cambiar el ID del jugador

        if (idJugador >= 0 && idJugador < jugadores.Count)
        {
            var datos = jugadores[idJugador].Value;
            datos.jugadorId = id;
            jugadores[idJugador].Value = datos;
        }
    }
    public string GetNomJugador(int idJugador)
    {
        return jugadores[idJugador].Value.nomJugador;
    }

    public void SetNomJugador(int idJugador, string nombre)
    {
        if (!isHost) return; // Solo el host puede cambiar el ID del jugador

        if (idJugador >= 0 && idJugador < jugadores.Count)
        {
            var datos = jugadores[idJugador].Value;
            datos.nomJugador = nombre;
            jugadores[idJugador].Value = datos;
        }
    }

    public int GetPuntaje(int idJugador)
    {
        return jugadores[idJugador].Value.puntaje;
    }

    public void SetPuntaje(int idJugador, int puntaje)
    {
        if (!isHost) return; // Solo el host puede cambiar el puntaje del jugador

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
        if (!isHost) return; // Solo el host puede cambiar la cantidad de cartas del jugador

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
        if (!isHost) return; // Solo el host puede cambiar el estado de ganador del jugador

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
        if (!isHost) return; // Solo el host puede cambiar el turno del jugador

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
        if (!isHost) return; // Solo el host puede cambiar la cantidad de casas del jugador

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
