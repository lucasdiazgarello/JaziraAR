using Unity.Netcode;
using UnityEngine;

public class TurnManager : NetworkBehaviour
{
    public static TurnManager Instance { get; private set; }
    public int CurrentPlayerID { get; private set; }
    private int currentPlayerIndex = 0;
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
        Debug.Log("va a cambiar de turno");
        // Asignar turno falso al jugador actual
        int currentPlayerId = PlayerIds[currentPlayerIndex];
        var currentPlayerData = GetPlayerDataById(currentPlayerId);
        currentPlayerData.turno = false;

        // Cambiar al siguiente jugador
        currentPlayerIndex = (currentPlayerIndex + 1) % PlayerIds.Count;

        // Asignar turno verdadero al nuevo jugador actual
        int newPlayerId = PlayerIds[currentPlayerIndex];
        var newPlayerData = GetPlayerDataById(newPlayerId);
        newPlayerData.turno = true;
    }

    public PlayerNetwork.DatosJugador GetPlayerDataById(int playerId)
    {
        foreach (var playerData in PlayerNetwork.Instance.playerData)
        {
            if (playerData.jugadorId == playerId)
            {
                return playerData;
            }
        }
        return default; // Retorna un valor predeterminado si no encuentra el jugador. Puedes manejar esto de manera diferente si lo prefieres.
    }
    public void EndTurnButton()
    {
        // Verificar si es el turno del jugador actual
        Debug.Log("LocalCLientID: " + NetworkManager.Singleton.LocalClientId);
        Debug.Log("CurrentPLyaerId: " + TurnManager.Instance.CurrentPlayerId);
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


