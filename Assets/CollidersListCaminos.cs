using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class CollidersListCaminos : NetworkBehaviour
{
    public NetworkList<CollidersListCaminos.Colliders> listaCollidersCaminos;
    public static CollidersListCaminos Instance { get; private set; }
    public NetworkVariable<Colliders> listaCollsCaminos;
    public struct Colliders : INetworkSerializable, IEquatable<Colliders>
    {
        public FixedString64Bytes nombreColliderCamino;
        public bool hayCamino;
        public FixedString64Bytes color;
        public FixedString64Bytes nombreCamino1;
        public FixedString64Bytes nombreCamino2;
        public FixedString64Bytes nombreCamino3;
        public FixedString64Bytes nombreCamino4;
        public bool Equals(Colliders other)
        {
            return nombreColliderCamino == other.nombreColliderCamino && hayCamino == other.hayCamino && color == other.color &&  nombreCamino1 == other.nombreCamino1 && nombreCamino2 == other.nombreCamino2 && nombreCamino3 == other.nombreCamino3 && nombreCamino4 == other.nombreCamino4;
        }
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref nombreColliderCamino);
            serializer.SerializeValue(ref hayCamino);
            serializer.SerializeValue(ref color);
            serializer.SerializeValue(ref nombreCamino1);
            serializer.SerializeValue(ref nombreCamino2);
            serializer.SerializeValue(ref nombreCamino3);
            serializer.SerializeValue(ref nombreCamino4);
        }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Para mantener el objeto al cambiar de escena
            Debug.Log("Id de esta instancia de CollidersList " + this.NetworkObjectId);
            // Inicializar los jugadores aqu�
            //NetworkList must be initialized in Awake.
            listaCollidersCaminos = new NetworkList<Colliders>();
            if (listaCollidersCaminos == null)
            {
                Debug.Log("La lista listaColliders NO se ha inicializado correctamente.");
            }
            else
            {
                Debug.Log("La lista listaColliders se ha inicializado correctamente.");
            }

            // Mover inicializaciones aqu�
            listaCollsCaminos = new NetworkVariable<Colliders>(
                new Colliders
                {
                    nombreColliderCamino = new FixedString64Bytes(),
                    hayCamino = new bool(),
                    color = new FixedString64Bytes(),
                    nombreCamino1 = new FixedString64Bytes(),
                    nombreCamino2 = new FixedString64Bytes(),
                    nombreCamino3 = new FixedString64Bytes(),
                    nombreCamino4 = new FixedString64Bytes(),

                }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        }
        else
        {
            Debug.LogWarning("Multiple instances of CollidersList detected. Deleting one instance. GameObject: " + gameObject.name);
            Destroy(gameObject);
        }
    }
    public void AgregarColliderCaminos(FixedString64Bytes nombre, bool hayCamino, FixedString64Bytes color, FixedString64Bytes nombreCamino1, FixedString64Bytes nombreCamino2, FixedString64Bytes nombreCamino3, FixedString64Bytes nombreCamino4)
    {
        //Debug.Log("el nombre a agregar es " + nombre + " y el  tipo " + tipo);
        Colliders nuevoCollider = new Colliders();
        nuevoCollider.nombreColliderCamino = nombre;
        nuevoCollider.hayCamino = hayCamino;
        nuevoCollider.color = color;
        nuevoCollider.nombreCamino1 = nombreCamino1;
        nuevoCollider.nombreCamino2 = nombreCamino2;
        nuevoCollider.nombreCamino3 = nombreCamino3;
        nuevoCollider.nombreCamino4 = nombreCamino4;
        try
        {
            Instance.listaCollidersCaminos.Add(nuevoCollider);
        }
        catch (Exception e)
        {
            Debug.LogError("Error al intentar agregar datos a la lista: " + e);
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Debug.Log("OnNetworkSpawn de CollidersList");
            AgregarColliderCaminos("Empty camino1", false , "Vacio", "Empty camino rot der (20)", "Empty camino 3 rotado (4)", "no", "Empty camino rot der (5)");
            AgregarColliderCaminos("Empty camino1 (1)", false, "Vacio", "Empty camino rot der (4)", "Empty camino 3 rotado", "Empty camino 3 rotado (5)", "Empty camino rot der (1)");
            AgregarColliderCaminos("Empty camino1 (2)", false, "Vacio", "Empty camino rot der ", "Empty camino 3 rotado (2)", "Empty camino 3 rotado (1)", "Empty camino rot der (3)");
            AgregarColliderCaminos("Empty camino1 (3)", false, "Vacio", "Empty camino rot der (2)", "Empty camino 3 rotado (22)", "Empty camino 3 rotado (3)", "no");
            AgregarColliderCaminos("Empty camino1 (4)", false, "Vacio", "no", "Empty camino 3 rotado (20)", "no", "Empty camino rot der (20)");
            AgregarColliderCaminos("Empty camino1 (5)", false, "Vacio", "Empty camino rot der (19)", "Empty camino 3 rotado (21)", "Empty camino 3 rotado (4)", "Empty camino rot der (4)");
            AgregarColliderCaminos("Empty camino1 (6)", false, "Vacio", "Empty camino rot der (11)", "Empty camino 3 rotado (11)", "Empty camino 3 rotado", "Empty camino rot der");
            AgregarColliderCaminos("Empty camino1 (7)", false, "Vacio", "Empty camino rot der (7)", "Empty camino 3 rotado (7)", "Empty camino 3 rotado (2)", "Empty camino rot der (2)");
            AgregarColliderCaminos("Empty camino1 (8)", false, "Vacio", "Empty camino rot der (9)", "Empty camino 3 rotado (9)", "Empty camino 3 rotado (22)", "no");
            AgregarColliderCaminos("Empty camino1 (9)", false, "Vacio", "Empty camino rot der (21)", "Empty camino 3 rotado (18)", "Empty camino 3 rotado (20)", "Empty camino rot der (19)");
            AgregarColliderCaminos("Empty camino1 (10)", false, "Vacio", "Empty camino rot der (18)", "Empty camino 3 rotado (10)", "Empty camino 3 rotado (21)", "Empty camino rot der (11)");
            AgregarColliderCaminos("Empty camino1 (11)", false, "Vacio", "Empty camino rot der (10)", "Empty camino 3 rotado (6)", "Empty camino 3 rotado (11)", "Empty camino rot der (7)");
            AgregarColliderCaminos("Empty camino1 (12)", false, "Vacio", "Empty camino rot der (6)", "Empty camino 3 rotado (8)", "Empty camino 3 rotado (7)", "Empty camino rot der (9)");
            AgregarColliderCaminos("Empty camino1 (13)", false, "Vacio", "Empty camino rot der (8)", "no", "Empty camino 3 rotado (9)", "no");
            AgregarColliderCaminos("Empty camino1 (14)", false, "Vacio", "no", "Empty camino 3 rotado (19)", "no", "Empty camino rot der (21)");
            AgregarColliderCaminos("Empty camino1 (15)", false, "Vacio", "Empty camino rot der (17)", "Empty camino 3 rotado (17)", "Empty camino 3 rotado (18)", "Empty camino rot der (18)");
            AgregarColliderCaminos("Empty camino1 (16)", false, "Vacio", "Empty camino rot der (13)", "Empty camino 3 rotado (13)", "Empty camino 3 rotado (10)", "Empty camino rot der (10)");
            AgregarColliderCaminos("Empty camino1 (17)", false, "Vacio", "Empty camino rot der (15)", "Empty camino 3 rotado (15)", "Empty camino 3 rotado (6)", "Empty camino rot der (6)");
            AgregarColliderCaminos("Empty camino1 (18)", false, "Vacio", "Empty camino rot der (22)", "no", "Empty camino 3 rotado (8)", "no");
            AgregarColliderCaminos("Empty camino1 (19)", false, "Vacio", "no", "Empty camino 3 rotado (16)", "Empty camino 3 rotado (19)", "Empty camino rot der (17)");
            AgregarColliderCaminos("Empty camino1 (20)", false, "Vacio", "Empty camino rot der (16)", "Empty camino 3 rotado (12)", "Empty camino 3 rotado (17)", "Empty camino rot der (13)");
            AgregarColliderCaminos("Empty camino1 (21)", false, "Vacio", "Empty camino rot der (12)", "Empty camino 3 rotado (14)", "Empty camino 3 rotado (13)", "Empty camino rot der (15)");
            AgregarColliderCaminos("Empty camino1 (22)", false, "Vacio", "Empty camino rot der (14)", "no", "Empty camino 3 rotado 15)", "Empty camino rot der (22)");
            AgregarColliderCaminos("Empty camino 3 rotado", false, "Vacio", "Empty camino rot der (4)", "Empty camino1 (1)", "Empty camino1 (6)", "Empty camino rot der ");
            AgregarColliderCaminos("Empty camino 3 rotado (1)", false, "Vacio", "Empty camino rot der (1)", "no", "Empty camino1 (2)", "Empty camino rot der (3)");
            AgregarColliderCaminos("Empty camino 3 rotado (2)", false, "Vacio", "Empty camino rot der", "Empty camino1 (2)", "Empty camino1 (7)", "Empty camino rot der (2)");
            AgregarColliderCaminos("Empty camino 3 rotado (3)", false, "Vacio", "Empty camino rot der (3)", "no", "Empty camino1 (3)", "no");
            AgregarColliderCaminos("Empty camino 3 rotado (4)", false, "Vacio", "Empty camino rot der (20)", "Empty camino1", "Empty camino1 (5)", "Empty camino rot der (4)");
            AgregarColliderCaminos("Empty camino 3 rotado (5)", false, "Vacio", "Empty camino rot der (5)", "no", "Empty camino1 (1)", "Empty camino rot der (1)");
            AgregarColliderCaminos("Empty camino 3 rotado (6)", false, "Vacio", "Empty camino rot der (10)", "Empty camino1 (11)", "Empty camino1 (17)", "Empty camino rot der (6)");
            AgregarColliderCaminos("Empty camino 3 rotado (7)", false, "Vacio", "Empty camino rot der (7)", "Empty camino1 (7)", "Empty camino1 (12)", "Empty camino rot der (9)");
            AgregarColliderCaminos("Empty camino 3 rotado (8)", false, "Vacio", "Empty camino rot der (6)", "Empty camino1 (12)", "Empty camino1 (18)", "Empty camino rot der (8)");
            AgregarColliderCaminos("Empty camino 3 rotado (9)", false, "Vacio", "Empty camino rot der (9)", "Empty camino1 (8)", "Empty camino1 (13)", "no");
            AgregarColliderCaminos("Empty camino 3 rotado (10)", false, "Vacio", "Empty camino rot der (18)", "Empty camino1 (10)", "Empty camino1 (16)", "Empty camino rot der (10)");
            AgregarColliderCaminos("Empty camino 3 rotado (11)", false, "Vacio", "Empty camino rot der (11)", "Empty camino1 (6)", "Empty camino1 (11)", "Empty camino rot der (7)");
            AgregarColliderCaminos("Empty camino 3 rotado (12)", false, "Vacio", "Empty camino rot der (16)", "Empty camino1 (20)", "no", "Empty camino rot der (12)");
            AgregarColliderCaminos("Empty camino 3 rotado (13)", false, "Vacio", "Empty camino rot der (13)", "Empty camino1 (16)", "Empty camino1 (21)", "Empty camino rot der (15)");
            AgregarColliderCaminos("Empty camino 3 rotado (14)", false, "Vacio", "Empty camino rot der (12)", "Empty camino1 (21)", "no", "Empty camino rot der (14)");
            AgregarColliderCaminos("Empty camino 3 rotado (15)", false, "Vacio", "Empty camino rot der (15)", "Empty camino1 (17)", "Empty camino1 (22)", "Empty camino rot der (22)");
            AgregarColliderCaminos("Empty camino 3 rotado (16)", false, "Vacio", "no", "Empty camino1 (19)", "no", "Empty camino rot der (16)");
            AgregarColliderCaminos("Empty camino 3 rotado (17)", false, "Vacio", "Empty camino rot der (17)", "Empty camino1 (15)", "Empty camino1 (20)", "Empty camino rot der (13)");
            AgregarColliderCaminos("Empty camino 3 rotado (18)", false, "Vacio", "Empty camino rot der (21)", "Empty camino1 (9)", "Empty camino1 (15)", "Empty camino rot der (18)");
            AgregarColliderCaminos("Empty camino 3 rotado (19)", false, "Vacio", "no", "Empty camino1 (9)", "Empty camino1 (19)", "Empty camino rot der (17)");
            AgregarColliderCaminos("Empty camino 3 rotado (20)", false, "Vacio", "no", "Empty camino1 (4)", "Empty camino1 (9)", "Empty camino rot der (19)");
            AgregarColliderCaminos("Empty camino 3 rotado (21)", false, "Vacio", "Empty camino rot der (19)", "Empty camino1 (5)", "Empty camino1 (10)", "Empty camino rot der (11)");
            AgregarColliderCaminos("Empty camino 3 rotado (22)", false, "Vacio", "Empty camino rot der (2)", "Empty camino1 (3)", "Empty camino1 (8)", "no");
            AgregarColliderCaminos("Empty camino rot der", false, "Vacio", "Empty camino1 (6)", "Empty camino 3 rotado", "Empty camino 3 rotado (2)", "Empty camino1 (2)");
            AgregarColliderCaminos("Empty camino rot der (1)", false, "Vacio", "Empty camino1 (1)", "Empty camino 3 rotado (5)", "Empty camino 3 rotado (2)", "no");
            AgregarColliderCaminos("Empty camino rot der (2)", false, "Vacio", "Empty camino1 (7)", "Empty camino 3 rotado (2)", "Empty camino 3 rotado (22)", "Empty camino1 (3)");
            AgregarColliderCaminos("Empty camino rot der (3)", false, "Vacio", "Empty camino1 (2)", "Empty camino 3 rotado (1)", "Empty camino 3 rotado (3)", "no");
            AgregarColliderCaminos("Empty camino rot der (4)", false, "Vacio", "Empty camino1 (5)", "Empty camino 3 rotado (4)", "Empty camino 3 rotado", "Empty camino1 (1)");
            AgregarColliderCaminos("Empty camino rot der (5)", false, "Vacio", "Empty camino1", "no", "Empty camino 3 rotado (5)", "no");
            AgregarColliderCaminos("Empty camino rot der (6)", false, "Vacio", "Empty camino1 (17)", "Empty camino 3 rotado (6)", "Empty camino 3 rotado (8)", "Empty camino1 (12)");
            AgregarColliderCaminos("Empty camino rot der (7)", false, "Vacio", "Empty camino1 (11)", "Empty camino 3 rotado (11)", "Empty camino 3 rotado (7)", "Empty camino1 (7)");
            AgregarColliderCaminos("Empty camino rot der (8)", false, "Vacio", "Empty camino1 (18)", "Empty camino 3 rotado (8)", "no", "Empty camino1 (13)");
            AgregarColliderCaminos("Empty camino rot der (9)", false, "Vacio", "Empty camino1 (12)", "Empty camino 3 rotado (7)", "Empty camino 3 rotado (9)", "Empty camino1 (8)");
            AgregarColliderCaminos("Empty camino rot der (10)", false, "Vacio", "Empty camino1 (16)", "Empty camino 3 rotado (10)", "Empty camino 3 rotado (6)", "Empty camino1 (11)");
            AgregarColliderCaminos("Empty camino rot der (11)", false, "Vacio", "Empty camino1 (10)", "Empty camino 3 rotado (21)", "Empty camino 3 rotado (11)", "Empty camino1 (6)");
            AgregarColliderCaminos("Empty camino rot der (12)", false, "Vacio", "Empty camino1 (10)", "Empty camino 3 rotado (21)", "Empty camino 3 rotado (11)", "Empty camino1 (6)");
            AgregarColliderCaminos("Empty camino rot der (13)", false, "Vacio", "Empty camino1 (20)", "Empty camino 3 rotado (17)", "Empty camino 3 rotado (13)", "Empty camino1 (16)");
            AgregarColliderCaminos("Empty camino rot der (14)", false, "Vacio", "no", "Empty camino 3 rotado (14)", "no", "Empty camino1 (22)");
            AgregarColliderCaminos("Empty camino rot der (15)", false, "Vacio", "Empty camino1 (21)", "Empty camino 3 rotado (13)", "Empty camino 3 rotado (15)", "Empty camino1 (17)");
            AgregarColliderCaminos("Empty camino rot der (16)", false, "Vacio", "no", "Empty camino 3 rotado (16)", "Empty camino 3 rotado (12)", "Empty camino1 (20)");
            AgregarColliderCaminos("Empty camino rot der (17)", false, "Vacio", "Empty camino1 (19)", "Empty camino 3 rotado (19)", "Empty camino 3 rotado (17)", "Empty camino1 (15)");
            AgregarColliderCaminos("Empty camino rot der (18)", false, "Vacio", "Empty camino1 (15)", "Empty camino 3 rotado (18)", "Empty camino 3 rotado (10)", "Empty camino1 (10)");
            AgregarColliderCaminos("Empty camino rot der (19)", false, "Vacio", "Empty camino1 (9)", "Empty camino 3 rotado (20)", "Empty camino 3 rotado (21)", "Empty camino1 (5)");
            AgregarColliderCaminos("Empty camino rot der (20)", false, "Vacio", "Empty camino1 (4)", "no", "Empty camino 3 rotado (4)", "Empty camino1");
            AgregarColliderCaminos("Empty camino rot der (21)", false, "Vacio", "Empty camino1 (14)", "no", "Empty camino 3 rotado (18)", "Empty camino1 (9)");
            AgregarColliderCaminos("Empty camino rot der (22)", false, "Vacio", "Empty camino1 (22)", "Empty camino 3 rotado (15)", "no", "Empty camino1 (18)");
            //ImprimirListaColliders();
        }
        else
        {
            Debug.Log("cliente de OnNetworkSpawn de CollidersList");
        }
    }

    public void ModificarHayCaminoPorNombre(FixedString64Bytes nombre, bool haycamino)
    {
        for (int i = 0; i < Instance.listaCollidersCaminos.Count; i++)
        {
            if (listaCollidersCaminos[i].nombreColliderCamino.Equals(nombre))
            {
                Debug.Log("Encontre el camino" + nombre + " en la lista");
                // Encontrado! Cambiemos el tipo
                var colliderModificado = listaCollidersCaminos[i]; // Copia el struct
                colliderModificado.hayCamino = true; // Cambia el tipo
                Debug.Log("Ahora si hay camino es" + haycamino);
                listaCollidersCaminos[i] = colliderModificado; // Reemplaza el struct en la lista
                return; // Omitir esto si es posible que haya más de una entrada con el mismo nombre
            }
        }
    }
    public bool VerificarHayCaminoPorNombre(FixedString64Bytes nombre)
    {
        FixedString64Bytes camino1;
        FixedString64Bytes camino2;
        FixedString64Bytes camino3;
        var si = false;
        for (int i = 0; i < CollidersList.Instance.listaColliders.Count; i++)
        {
            if (CollidersList.Instance.listaColliders[i].nombreCollider.Equals(nombre))
            {
                Debug.Log("Encontre el collider" + nombre + " en la lista");
                // Encontrado! Cambiemos el tipo
                 camino1 = CollidersList.Instance.listaColliders[i].nombreCamino1;
                 camino2 = CollidersList.Instance.listaColliders[i].nombreCamino2;
                 camino3 = CollidersList.Instance.listaColliders[i].nombreCamino3;
                for (int j = 0; j < CollidersListCaminos.Instance.listaCollidersCaminos.Count; j++)
                {
                    if (CollidersListCaminos.Instance.listaCollidersCaminos[i].nombreCamino1 == camino1)
                    {
                        if(CollidersListCaminos.Instance.listaCollidersCaminos[i].hayCamino)
                        {
                            si = true;
                        }


                    }
                    if (CollidersListCaminos.Instance.listaCollidersCaminos[i].nombreCamino1 == camino2)
                    {
                        if (CollidersListCaminos.Instance.listaCollidersCaminos[i].hayCamino)
                        {
                            si = true;
                        }


                    }
                    if (CollidersListCaminos.Instance.listaCollidersCaminos[i].nombreCamino1 == camino3)
                    {
                        if (CollidersListCaminos.Instance.listaCollidersCaminos[i].hayCamino)
                        {
                            si = true;
                        }


                    }




                }
            }
            
        }
        return si;
    }
    /*
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
    public void ModificarIdPiezaPorNombre(FixedString64Bytes nombre, int nuevoId)
    {
        for (int i = 0; i < listaColliders.Count; i++)
        {
            if (listaColliders[i].nombreCollider.Equals(nombre))
            {
                Debug.Log("Encontre a " + nombre + " en la lista");
                var colliderModificado = listaColliders[i]; // Copia el struct
                colliderModificado.idInstancia = nuevoId; // Cambia el tipo
                Debug.Log("El nuevo idInstania es " + nuevoId);
                listaColliders[i] = colliderModificado; // Reemplaza el struct en la lista
                return; // Omitir esto si es posible que haya más de una entrada con el mismo nombre
            }
        }
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
    public int GetIdPiezaPorNombre(FixedString64Bytes nombre)
    {
        //ImprimirListaColliders();
        foreach (var collider in listaColliders)
        {
            //Debug.Log("Estoy buscando a " + nombre + " en la lista");
            //Debug.Log("Encontre a " + collider.nombreCollider);
            if (collider.nombreCollider.Equals(nombre))
            {
                return collider.idInstancia;
            }
        }
        return new int(); // no se que int da sino encuentra...
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
    }*/
}
