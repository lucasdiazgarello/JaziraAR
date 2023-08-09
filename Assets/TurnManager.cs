using Unity.Netcode;
using UnityEngine;

public class TurnManager : NetworkBehaviour
{
    public static TurnManager Instance { get; private set; }
    public int CurrentPlayerID { get; private set; }
    private int currentPlayerIndex = 0;

    // Referencia a PlayerNetwork
    //public PlayerNetwork playerNetwork;

    // Usa playerIDs desde PlayerNetwork en lugar de PlayerIds
    public NetworkList<int> PlayerIds
    {
        get
        {
            if (PlayerNetwork.Instance != null)
            {
                return PlayerNetwork.Instance.playerIDs;
            }

            Debug.LogWarning("PlayerNetwork.Instance es null, por lo que no se puede obtener playerIDs.");
            return null;
        }
    }

    public int CurrentPlayerId
    {
        get
        {
            return PlayerIds[currentPlayerIndex];
        }
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

    public void AdvanceTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % PlayerIds.Count;
    }

    public void EndTurnButton()
    {
        // Verificar si es el turno del jugador actual
        if (NetworkManager.Singleton.LocalClientId == (ulong)TurnManager.Instance.CurrentPlayerId)
        {
            TurnManager.Instance.AdvanceTurn();
        }
    }

    private void OnEnable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;

        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    private void OnDisable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
    }

    private void OnClientConnected(ulong clientId)
    {
        // El cliente se ha conectado, agregar a la lista de jugadores
        TurnManager.Instance.PlayerIds.Add((int)clientId);
    }

    private void OnClientDisconnected(ulong clientId)
    {
        // El cliente se ha desconectado, eliminar de la lista de jugadores
        TurnManager.Instance.PlayerIds.Remove((int)clientId);
    }

}


