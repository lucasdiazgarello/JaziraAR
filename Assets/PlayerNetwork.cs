using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNetwork : NetworkBehaviour
{
    public NetworkVariable<string> nombreJugador = new NetworkVariable<string>("alejandro",NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    //public List<NetworkVariable<DatosJugador>> jugadores = new List<NetworkVariable<DatosJugador>>();
    
    public bool IsInitialized { get; private set; } = false; // Añade este campo de estado
    public Button buttonToPress;
    public Button buttonPrint;
    public Button buttonLoad;

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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Para mantener el objeto al cambiar de escena
            // Inicializar los jugadores aquí
            //CrearJugadores();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public NetworkVariable<DatosJugador> jugador = new NetworkVariable<DatosJugador>(
        new DatosJugador {
            jugadorId = 0,
            nomJugador = "Unity",
            puntaje = 0,
            cantidadJugadores= 0,
            gano = false,
            turno = false,
            cantidadCasa = 0,
            maderaCount = 0,
            ladrilloCount = 0,
            ovejaCount = 0,
            piedraCount = 0,
            trigoCount = 0,
            colorJugador = "rojo",

        },NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    public struct DatosJugador : INetworkSerializable
    {
        public int jugadorId;
        public string nomJugador;
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
        public string colorJugador;

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

    private void Start()
    {
        //buttonToPress.onClick.AddListener(PrintPlayerName);
        //Debug.Log("antes de cargar");
        //buttonPrint.onClick.AddListener(ImprimirDatosJugador);
        //buttonLoad.onClick.AddListener(() => CargarDatosJugador(1, "Jugador", 100, 5, false, true, 2, 10, 10, 10, 10, 10));
        //Debug.Log("despues de cargar");
        //buttonPrint.onClick.AddListener(ImprimirDatosJugador);

    }

    public void ImprimirDatosJugador()
    {
        Debug.Log("ID Jugador: " + jugador.Value.jugadorId);
        Debug.Log("Nombre Jugador: " + jugador.Value.nomJugador);
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
        Debug.Log("Color de Jugador: " + jugador.Value.colorJugador);
    }

    public void CargarDatosJugador(int jugadorId, string nomJugador, int puntaje, int cantidadJugadores, bool gano, bool turno, int cantidadCasa, int maderaCount, int ladrilloCount, int ovejaCount, int piedraCount, int trigoCount)
    {
        DatosJugador newDatos = new DatosJugador();
        newDatos.jugadorId = jugadorId;
        newDatos.nomJugador = nomJugador;
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
        //newDatos.colorJugador = colorJugador;

        jugador.Value = newDatos;
    }

    public void PrintPlayerName()
    {
        Debug.Log("Nombre de Jugador antes: " + nombreJugador.Value);
        nombreJugador.Value = "eugenia";
        Debug.Log("Nombre de Jugador despues: " + nombreJugador.Value);
    }
    public void CargarDatosColorJugador(string colorSeleccionado)
    {
        DatosJugador datosActuales = jugador.Value;
        datosActuales.colorJugador = colorSeleccionado;
        jugador.Value = datosActuales;
        Debug.Log("color neuvo es: " + jugador.Value.colorJugador);
    }

    private void Update()
    {
        if (!IsOwner) return;


        // Resto del código...
    }
}





    /*public void InicializarJugadores()
    {
        // Asegúrate de llamar a este método antes de usar la lista de jugadores.
        for (int i = 0; i < 4; i++)
        {

            NetworkVariable<DatosJugador> jugador = new NetworkVariable<DatosJugador>(new NetworkVariableSettings
            {
                ReadPermission = NetworkVariablePermission.Everyone,
                WritePermission = NetworkVariablePermission.Owner
            });
            jugadores.Add(jugador);
        }
    }*/
    /*
    public void CrearJugadores()
    {
        Debug.Log("HOLA QUE TAL");

        // Solo el host crea la lista de jugadores
        if (IsHost)
        {
            Debug.Log("entre al host de start");
            //InicializarJugadores();
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
                    maderaCount = 0,
                    ladrilloCount = 0,
                    ovejaCount = 0,
                    piedraCount = 0,
                    trigoCount = 0,
                    cantidadCasa = 0,
                    nomJugador = "carla"
                };
                jugadores[i].Value = jugador;
                Debug.Log("nombre JUGADOR " + jugadores[i].Value.nomJugador);
            }
        }
    }*/
        /*
            public void CrearJugadores()
            {
                Debug.Log("Entro a Crear jugadores");

                // Solo el host crea la lista de jugadores
                if (IsHost)
                {
                    //Debug.Log("entre al host de start");
                    //InicializarJugadores();
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
                        //Debug.Log("llegue hasta aca");
                        NetworkVariable<DatosJugador> networkJugador = new NetworkVariable<DatosJugador>(jugador, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
                        //Debug.Log("llegue hast aca 2");
                        Debug.Log("CrearJugadores(): i = " + i);
                        jugadores.Add(networkJugador);
                        Debug.Log("ID JUGADOR " + jugadores[i].Value.nomJugador);
                        //Debug.Log(jugadores.Count+" info de jugadores");
                    }
                    IsInitialized = true;
                }
            }
        */
        /*
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

            public void SetNomJugador(int idJugador, string nombre)
            {
                if (!IsHost) return; // Solo el host puede cambiar el ID del jugador

                if (jugadores == null)
                {
                    Debug.Log("jugadores is null");
                    return;
                }

                if (idJugador < 0 || idJugador >= jugadores.Count)
                {
                    Debug.Log("Invalid idJugador: " + idJugador);
                    return;
                }

                if (!IsInitialized)
                {
                    Debug.Log("Not initialized");
                    return;
                }

                Debug.Log("Entre a SetNomJugador y se inicializo");
                Debug.Log("SetNomJugador(): Setting name for player " + idJugador);

                var datos = jugadores[idJugador].Value;
                datos.nomJugador = nombre;
                jugadores[idJugador].Value = datos;

                Debug.Log("aca tiene que decir euge :" + jugadores[idJugador].Value.nomJugador);
            }
        */
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
        /*
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
        }*/
        /*
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
        */
        /*
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
        */
        //hacer get y set de los recursos

