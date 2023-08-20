

using System;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class ListaColliders : NetworkBehaviour
{
    public NetworkList<ListaColliders.Colliders> listaColliders;
    public static ListaColliders Instance { get; private set; }
    public NetworkVariable<Colliders> listaColls;
    public struct Colliders : INetworkSerializable, IEquatable<Colliders>
    {
        public FixedString64Bytes nombreCollider;
        public FixedString64Bytes tipo;

        public bool Equals(Colliders other)
        {
            return nombreCollider == other.nombreCollider && tipo == other.tipo;
        }
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref nombreCollider);
            serializer.SerializeValue(ref tipo);
        }
    };

    private void Awake()
    {
        //listaColliders = new NetworkList<Colliders>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Para mantener el objeto al cambiar de escena
            Debug.Log("Id de esta instancia de ListaColliders " + this.NetworkObjectId);
            // Inicializar los jugadores aqu�
            //NetworkList must be initialized in Awake.
            listaColliders = new NetworkList<Colliders>();
            if (listaColliders == null)
            {
                Debug.Log("La lista listaColliders NO se ha inicializado correctamente.");
            }
            else
            {
                Debug.Log("La lista listaColliders se ha inicializado correctamente.");
            }

            // Mover inicializaciones aqu�
            listaColls = new NetworkVariable<Colliders>(
                new Colliders
                {
                    nombreCollider = new FixedString64Bytes(),
                    tipo = new FixedString64Bytes(),

                }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        }
        else
        {
            Debug.LogWarning("Multiple instances of ListaColliders detected. Deleting one instance. GameObject: " + gameObject.name);
            Destroy(gameObject);
        }
    }
    public void AgregarCollider(FixedString64Bytes nombre, FixedString64Bytes tipo)
    {
        Debug.Log("el nombre a agregar es " + nombre + " y el  tipo " + tipo);
        Colliders nuevoCollider = new Colliders();
        nuevoCollider.nombreCollider = nombre;
        nuevoCollider.tipo = tipo;
        try
        {
            Debug.Log("Cant elementos de Colliders:" + listaColliders.Count);
            Instance.listaColliders.Add(nuevoCollider);
            Debug.Log("Cant elementos de Colliders:" + listaColliders.Count);
            Debug.Log("Va a imprimir la lista");
            ImprimirListaColliders();
        }
        catch (Exception e)
        {
            Debug.LogError("Error al intentar agregar datos a la lista: " + e);
        }
    }
    void Start()
    {
        /*Debug.Log("Empezo el start de ListaColliders");
        FixedString64Bytes nombre = "Empty casa1";
        FixedString64Bytes tipo = "Ninguno";
        AgregarCollider(nombre, tipo);
        //AgregarCollider("Empty casa2", "Ninguno");
        // AgregarCollider("Empty casa3", "Ninguno");
        //AgregarCollider("Empty casa4", "Ninguno");
        // AgregarCollider("Empty casa5", "Ninguno");
        //AgregarCollider("Empty casa6", "Ninguno");
        ImprimirListaColliders();
        */
    }
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Debug.Log("OnNetworkSpawn de ListaColliders");
            AgregarCollider("Empty casa1", "Ninguno");
            AgregarCollider("Empty casa2", "Ninguno");
            AgregarCollider("Empty casa3", "Ninguno");
            AgregarCollider("Empty casa4", "Ninguno");
            AgregarCollider("Empty casa5", "Ninguno");
            AgregarCollider("Empty casa6", "Ninguno");
            ImprimirListaColliders();
        }
        else
        {
            Debug.Log("cliente de OnNetworkSpawn de ListaColliders");

        }
    }

    public void ModificarTipoPorNombre(FixedString64Bytes nombre, FixedString64Bytes nuevoTipo)
    {
        for (int i = 0; i < listaColliders.Count; i++)
        {
            if (listaColliders[i].nombreCollider.Equals(nombre))
            {
                Debug.Log("Encontre a " + nombre + " en la lista");
                // Encontrado! Cambiemos el tipo
                var colliderModificado = listaColliders[i]; // Copia el struct
                colliderModificado.tipo = nuevoTipo; // Cambia el tipo
                Debug.Log("El nuevo tipo es " + nuevoTipo);
                listaColliders[i] = colliderModificado; // Reemplaza el struct en la lista
                return; // Omitir esto si es posible que haya más de una entrada con el mismo nombre
            }
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void ModificarTipoPorNombreServerRpc(FixedString64Bytes nombre, FixedString64Bytes nuevoTipo)
    {
        for (int i = 0; i < listaColliders.Count; i++)
        {
            if (listaColliders[i].nombreCollider.Equals(nombre))
            {
                Debug.Log("Encontre a " + nombre + " en la lista");
                // Encontrado! Cambiemos el tipo
                var colliderModificado = listaColliders[i]; // Copia el struct
                colliderModificado.tipo = nuevoTipo; // Cambia el tipo
                Debug.Log("El nuevo tipo es " + nuevoTipo);
                listaColliders[i] = colliderModificado; // Reemplaza el struct en la lista
                return; // Omitir esto si es posible que haya más de una entrada con el mismo nombre
            }
        }
    }
    public FixedString64Bytes GetTipoPorNombre(FixedString64Bytes nombre)
    {
        ImprimirListaColliders();
        foreach (var collider in listaColliders)
        {
            Debug.Log("Estoy buscando a " + nombre + " en la lista");
            Debug.Log("Encontre a " + collider.nombreCollider);
            if (collider.nombreCollider.Equals(nombre))
            {
                return collider.tipo;
            }
        }

        return new FixedString64Bytes(); // Devuelve una cadena vacía si no se encontró
    }
    public void ImprimirListaColliders()
    {
        if (listaColliders == null)
        {
            Debug.Log("La lista listCollider no ha sido inicializada.");
            return;
        }

        Debug.Log("Contenido de la lista listCollider:");
        foreach (var data in listaColliders)
        {

            var datosJugadorInfo = $"NombreCollider: {data.nombreCollider}, " +
                                   $"Tipo: {data.tipo}";
        }
    }
    public string RemoverCloneDeNombre(string input)
    {
        return input.Replace("(Clone)", "");
    }
}
