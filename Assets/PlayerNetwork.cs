using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    //hacer aparecer algo
   [SerializeField] private Transform spawnedObjectPrefab;
    
    private NetworkVariable<DatosJugador> numequipo = new NetworkVariable<DatosJugador>(new DatosJugador { jugadorId = -1, puntaje = 0, gano = false, cantidadCartas = 0, turno = false, cantidadCasa = 0 }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public struct DatosJugador : INetworkSerializable
    {
        public int jugadorId;
        public int puntaje;
        public int cantidadCartas;
        public bool gano;
        public bool turno;
        public int cantidadCasa;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref jugadorId);
            serializer.SerializeValue(ref puntaje);
            serializer.SerializeValue(ref cantidadCartas);
            serializer.SerializeValue(ref gano);
            serializer.SerializeValue(ref turno);
            serializer.SerializeValue(ref cantidadCasa);
        }
    }

    public int GetJugadorId()
    {
        return numequipo.Value.jugadorId;
    }

    public void SetJugadorId(int id)
    {
        if (IsOwner)
        {
            var datos = numequipo.Value;
            datos.jugadorId = id;
            numequipo.Value = datos;
        }
    }

    public int GetPuntaje()
    {
        return numequipo.Value.puntaje;
    }

    public void SetPuntaje(int puntaje)
    {
        if (IsOwner)
        {
            var datos = numequipo.Value;
            datos.puntaje = puntaje;
            numequipo.Value = datos;
        }
    }

    public int GetCantidadCartas()
    {
        return numequipo.Value.cantidadCartas;
    }

    public void SetCantidadCartas(int cantidad)
    {
        if (IsOwner)
        {
            var datos = numequipo.Value;
            datos.cantidadCartas = cantidad;
            numequipo.Value = datos;
        }
    }

    public bool GetGano()
    {
        return numequipo.Value.gano;
    }

    public void SetGano(bool gano)
    {
        if (IsOwner)
        {
            var datos = numequipo.Value;
            datos.gano = gano;
            numequipo.Value = datos;
        }
    }

    public bool GetTurno()
    {
        return numequipo.Value.turno;
    }

    public void SetTurno(bool turno)
    {
        if (IsOwner)
        {
            var datos = numequipo.Value;
            datos.turno = turno;
            numequipo.Value = datos;
        }
    }

    public int GetCantidadCasa()
    {
        return numequipo.Value.cantidadCasa;
    }

    public void SetCantidadCasa(int cantidad)
    {
        if (IsOwner)
        {
            var datos = numequipo.Value;
            datos.cantidadCasa = cantidad;
            numequipo.Value = datos;
        }
    }

    private void Update()
    {
        if (!IsOwner) return;

        if(Input.GetKeyDown(KeyCode.T)) {
            Transform spawnedObectTransform = Instantiate(spawnedObjectPrefab);
            spawnedObectTransform.GetComponent<NetworkObject>().Spawn(true);
            Debug.Log("pusiste un coso");
        }
    }
}
