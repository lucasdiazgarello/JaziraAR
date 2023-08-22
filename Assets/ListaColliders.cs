

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
        public FixedString64Bytes color;

        public bool Equals(Colliders other)
        {
            return nombreCollider == other.nombreCollider && tipo == other.tipo && color == other.color;
        }
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref nombreCollider);
            serializer.SerializeValue(ref tipo);
            serializer.SerializeValue(ref color);
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
                    color = new FixedString64Bytes(),

                }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        }
        else
        {
            Debug.LogWarning("Multiple instances of ListaColliders detected. Deleting one instance. GameObject: " + gameObject.name);
            Destroy(gameObject);
        }
    }
    public void AgregarCollider(FixedString64Bytes nombre, FixedString64Bytes tipo, FixedString64Bytes color)
    {
        //Debug.Log("el nombre a agregar es " + nombre + " y el  tipo " + tipo);
        Colliders nuevoCollider = new Colliders();
        nuevoCollider.nombreCollider = nombre;
        nuevoCollider.tipo = tipo;
        nuevoCollider.color = color;
        try
        {
            //Debug.Log("Cant elementos de Colliders:" + listaColliders.Count);
            Instance.listaColliders.Add(nuevoCollider);
            //Debug.Log("Cant elementos de Colliders:" + listaColliders.Count);
            //Debug.Log("Va a imprimir la lista");
            //ImprimirListaColliders();
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
            AgregarCollider("Empty casa1", "Ninguno", "Vacio");
            AgregarCollider("Empty casa2", "Ninguno", "Vacio");
            AgregarCollider("Empty casa3", "Ninguno", "Vacio");
            AgregarCollider("Empty casa4", "Ninguno", "Vacio");
            AgregarCollider("Empty casa5", "Ninguno", "Vacio");
            AgregarCollider("Empty casa6", "Ninguno", "Vacio");
            AgregarCollider("Empty casa7", "Ninguno", "Vacio");
            AgregarCollider("Empty casa8", "Ninguno", "Vacio");
            AgregarCollider("Empty casa9", "Ninguno", "Vacio");
            AgregarCollider("Empty casa10", "Ninguno", "Vacio");
            AgregarCollider("Empty casa11", "Ninguno", "Vacio");
            AgregarCollider("Empty casa12", "Ninguno", "Vacio");
            AgregarCollider("Empty casa13", "Ninguno", "Vacio");
            AgregarCollider("Empty casa14", "Ninguno", "Vacio");
            AgregarCollider("Empty casa15", "Ninguno", "Vacio");
            AgregarCollider("Empty casa16", "Ninguno", "Vacio");
            AgregarCollider("Empty casa17", "Ninguno", "Vacio");
            AgregarCollider("Empty casa18", "Ninguno", "Vacio");
            AgregarCollider("Empty casa19", "Ninguno", "Vacio");
            AgregarCollider("Empty casa20", "Ninguno", "Vacio");
            AgregarCollider("Empty casa21", "Ninguno", "Vacio");
            AgregarCollider("Empty casa22", "Ninguno", "Vacio");
            AgregarCollider("Empty casa23", "Ninguno", "Vacio");
            AgregarCollider("Empty casa24", "Ninguno", "Vacio");
            AgregarCollider("Empty casa25", "Ninguno", "Vacio");
            AgregarCollider("Empty casa26", "Ninguno", "Vacio");
            AgregarCollider("Empty casa27", "Ninguno", "Vacio");
            AgregarCollider("Empty casa28", "Ninguno", "Vacio");
            AgregarCollider("Empty casa29", "Ninguno", "Vacio");
            AgregarCollider("Empty casa30", "Ninguno", "Vacio");
            AgregarCollider("Empty casa31", "Ninguno", "Vacio");
            AgregarCollider("Empty casa32", "Ninguno", "Vacio");
            AgregarCollider("Empty casa33", "Ninguno", "Vacio");
            AgregarCollider("Empty casa34", "Ninguno", "Vacio");
            AgregarCollider("Empty casa35", "Ninguno", "Vacio");
            AgregarCollider("Empty casa36", "Ninguno", "Vacio");
            AgregarCollider("Empty casa37", "Ninguno", "Vacio");
            AgregarCollider("Empty casa37", "Ninguno", "Vacio");
            AgregarCollider("Empty casa39", "Ninguno", "Vacio");
            AgregarCollider("Empty casa40", "Ninguno", "Vacio");
            AgregarCollider("Empty casa41", "Ninguno", "Vacio");
            AgregarCollider("Empty casa42", "Ninguno", "Vacio");
            AgregarCollider("Empty casa43", "Ninguno", "Vacio");
            AgregarCollider("Empty casa44", "Ninguno", "Vacio");
            AgregarCollider("Empty casa45", "Ninguno", "Vacio");
            AgregarCollider("Empty casa46", "Ninguno", "Vacio");
            AgregarCollider("Empty casa47", "Ninguno", "Vacio");
            AgregarCollider("Empty casa48", "Ninguno", "Vacio");
            AgregarCollider("Empty casa49", "Ninguno", "Vacio");
            AgregarCollider("Empty casa50", "Ninguno", "Vacio");
            AgregarCollider("Empty casa51", "Ninguno", "Vacio");
            AgregarCollider("Empty casa52", "Ninguno", "Vacio");
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
    public void ModificarColorPorNombre(FixedString64Bytes nombre, FixedString64Bytes nuevoColor)
    {
        for (int i = 0; i < listaColliders.Count; i++)
        {
            if (listaColliders[i].nombreCollider.Equals(nombre))
            {
                Debug.Log("Encontre a " + nombre + " en la lista");
                // Encontrado! Cambiemos el tipo
                var colliderModificado = listaColliders[i]; // Copia el struct
                colliderModificado.color = nuevoColor; // Cambia el tipo
                Debug.Log("El nuevo color es " + nuevoColor);
                listaColliders[i] = colliderModificado; // Reemplaza el struct en la lista
                return; // Omitir esto si es posible que haya más de una entrada con el mismo nombre
            }
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void ModificarColorPorNombreServerRpc(FixedString64Bytes nombre, FixedString64Bytes nuevoColor)
    {
        for (int i = 0; i < listaColliders.Count; i++)
        {
            if (listaColliders[i].nombreCollider.Equals(nombre))
            {
                Debug.Log("Encontre a " + nombre + " en la lista");
                // Encontrado! Cambiemos el tipo
                var colliderModificado = listaColliders[i]; // Copia el struct
                colliderModificado.color = nuevoColor; // Cambia el tipo
                Debug.Log("El nuevo Color es " + nuevoColor);
                listaColliders[i] = colliderModificado; // Reemplaza el struct en la lista
                return; // Omitir esto si es posible que haya más de una entrada con el mismo nombre
            }
        }
    }
    public FixedString64Bytes GetTipoPorNombre(FixedString64Bytes nombre)
    {
        //ImprimirListaColliders();
        foreach (var collider in listaColliders)
        {
            //Debug.Log("Estoy buscando a " + nombre + " en la lista");
            //Debug.Log("Encontre a " + collider.nombreCollider);
            if (collider.nombreCollider.Equals(nombre))
            {
                return collider.tipo;
            }
        }

        return new FixedString64Bytes(); // Devuelve una cadena vacía si no se encontró
    }
    public FixedString64Bytes GetColorPorNombre(FixedString64Bytes nombre)
    {
        //ImprimirListaColliders();
        foreach (var collider in listaColliders)
        {
            //Debug.Log("Estoy buscando a " + nombre + " en la lista");
            //Debug.Log("Encontre a " + collider.nombreCollider);
            if (collider.nombreCollider.Equals(nombre))
            {
                return collider.color;
            }
        }

        return new FixedString64Bytes(); // Devuelve una cadena vacía si no se encontró
    }
    public void ImprimirColliderPorNombre(FixedString64Bytes nombre)
    {
        if (listaColliders == null)
        {
            Debug.Log("La lista listaColliders no ha sido inicializada.");
            return;
        }

        foreach (var data in listaColliders)
        {
            if (data.nombreCollider.Equals(nombre))
            {
                var datosColliderInfo = $"NombreCollider: {data.nombreCollider}, " +
                                        $"Tipo: {data.tipo}, " +
                                        $"Color: {data.color}";
                Debug.Log(datosColliderInfo);
                return;  // Si solo esperas que haya un Collider con ese nombre, entonces puedes salir del bucle inmediatamente después de imprimirlo.
            }
        }

        Debug.Log($"Collider con nombre {nombre} no encontrado en la lista.");
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
