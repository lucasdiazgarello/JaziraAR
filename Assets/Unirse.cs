using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using Unity.Services.Relay;
using UnityEngine;
using UnityEngine.UI;

public class Unirse : NetworkBehaviour
{
    public static Unirse Instance { get; private set; } // Instancia Singleton
    // Start is called before the first frame update
    public TestRelay relay;
    //private string nombreJugador;
    public InputField nombreJugadorinput;
    public InputField codigo;
    //public string colorSeleccionado;
    public PlayerNetwork playerNetwork;
    public NetworkVariable<FixedString64Bytes> nombreJugador = new NetworkVariable<FixedString64Bytes>();
    public NetworkVariable<FixedString64Bytes> colorSeleccionado = new NetworkVariable<FixedString64Bytes>();

    // Variables temporales para almacenar el nombre y el color
    public FixedString64Bytes nombreTemporal;
    public FixedString64Bytes colorTemporal;
    public int clientePlayerID;

    void Start()
    {
        
    }
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Esto garantiza que el objeto no se destruirá al cargar una nueva escena
        }
        else
        {
            Destroy(gameObject); // Si ya hay una instancia, destruye esta
        }
    }

    public void UnirsePartida()
    {
        Debug.Log("Entre a Unirme");
        var code = codigo.text.ToString();
        Debug.Log("Codigo: " + code);
        // Almacena los valores de los campos de entrada en las variables temporales
        nombreTemporal = new FixedString64Bytes(nombreJugadorinput.text);
        colorTemporal = colorSeleccionado.Value;
        //nombreJugador = nombreJugadorinput.text.ToString();
        // Cambia la línea donde estableces el nombreJugador.Value a esta
        //nombreTemporal = new FixedString64Bytes(nombreJugadorinput.text);
        //nombreJugador.Value = new FixedString64Bytes(nombreJugadorinput.text); //toma el valor del input y lo pone en la variable 
        Debug.Log("nombre jugador: "+ nombreTemporal.Value);
        Debug.Log("color seleccionado: " + colorSeleccionado.Value);
        Debug.Log("antes de join relay");

        try
        {
            relay.JoinRelay(code);
            clientePlayerID = PlayerPrefs.GetInt("jugadorId");
            //int currentPlayerID = TurnManager.Instance.CurrentPlayerID;
            Debug.Log("Id Jugador a unir: " + clientePlayerID);
            //PlayerNetwork.Instance.AddPlayerServerRpc(clientePlayerID, nombreTemporal.Value, nombreTemporal.Value);
            //Debug.Log("PASO el ADDplayer " + clientePlayerID);
            PlayerNetwork.Instance.ImprimirTodosLosJugadores();
            // Comprueba si el objeto ya ha sido generado antes de llamar a Spawn()
            /*if (!NetworkObject.IsSpawned)
            {
                NetworkObject.Spawn(); // hago spawnear el networkobject para ver si entra a la funcion OnNetworkSpawn
            }*/
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
        Debug.Log("despues de join relay");
    }

    /*public override void OnNetworkSpawn() //Se activará para todos los clientes cuando se cree un objeto en la red
    {
        //if (IsOwner)
        if (IsServer)
        {
            Debug.Log("Entre a OnNetworkSpawn");
            // Asigna los valores temporales a las NetworkVariables
            nombreJugador.Value = nombreTemporal;
            colorSeleccionado.Value = colorTemporal;

            // Obtiene el ID del jugador
            //int myPlayerId = (int)NetworkManager.Singleton.LocalClientId;
            int myPlayerId 

            // Llama a los métodos ServerRpc para actualizar los datos del jugador en el servidor
            playerNetwork.UpdatePlayerColorServerRpc(myPlayerId, colorSeleccionado.Value);
            playerNetwork.UpdatePlayerNameServerRpc(myPlayerId, nombreJugador.Value);

            // Supongo que este es otro método ServerRpc que tienes para notificar al servidor de que un jugador se ha unido
            playerNetwork.NotifyServerOfJoinServerRpc();
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
