using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine;

public class TurnManager : NetworkBehaviour
{
    public static TurnManager Instance { get; private set; }
    public int CurrentPlayerID { get; private set; }
    private int currentPlayerIndex = 0;
    //public Button endTurnButton;
    public Button endTurnButton;
    public CanvasGroup endTurnButtonCanvasGroup;
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
    public void BotonParaEndTurn()
    {
        PlayerNetwork.Instance.EndTurn();
    }
    public void UpdateButtonState(bool isMyTurn)
    {
        endTurnButton.interactable = isMyTurn;
        endTurnButtonCanvasGroup.alpha = isMyTurn ? 1 : 0.5f;
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
    private void Update()
    {
        /*if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log("activo boton Terminar turno");
            endTurnButton.interactable = true;
        }*/
    }
    public void AdvanceTurn()
    {
        Debug.Log("va a cambiar de turno");
        // Asignar turno falso al jugador actual
        Debug.Log("El turno actual lo tiene el de id " + currentPlayerIndex);
        int currentPlayerId = PlayerIds[currentPlayerIndex];
        var currentPlayerData = GetPlayerDataById(currentPlayerId);
        currentPlayerData.turno = false;

        // Cambiar al siguiente jugador
        currentPlayerIndex = (currentPlayerIndex + 1) % PlayerIds.Count;
        Debug.Log("El turno pasa a ser del de id " + currentPlayerIndex);
        // Asignar turno verdadero al nuevo jugador actual
        int newPlayerId = PlayerIds[currentPlayerIndex];
        var newPlayerData = GetPlayerDataById(newPlayerId);
        newPlayerData.turno = true;
        endTurnButton.interactable = false;
        //UpdateButtonInteractivity();
    }
    public void EndTurnButton()
    {
        // Verificar si es el turno del jugador actual
        Debug.Log("LocalCLientID: " + NetworkManager.Singleton.LocalClientId); // este da 0 
        Debug.Log("CurrentPLyaerId: " + TurnManager.Instance.CurrentPlayerId); //este da lo que tiene que dar
        if (NetworkManager.Singleton.LocalClientId == (ulong)TurnManager.Instance.CurrentPlayerId)
        {
            TurnManager.Instance.AdvanceTurn();
            endTurnButton.interactable = true;
        }
        else
        {
            endTurnButton.interactable = false; // No es el turno de este jugador, desactiva el botón
        }
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
    public void UpdateButtonInteractivity()
    {
        if (NetworkManager.Singleton.LocalClientId == (ulong)TurnManager.Instance.CurrentPlayerId)
        {
            endTurnButton.interactable = true; // Es el turno de este jugador, activa el botón
        }
        else
        {
            endTurnButton.interactable = false; // No es el turno de este jugador, desactiva el botón
        }
    }

    /*private void OnEnable()
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
        Debug.Log("Entre a onclientconnected de TurnManager");
        // El cliente se ha conectado, agregar a la lista de jugadores
        TurnManager.Instance.PlayerIds.Add((int)clientId);
        UpdateButtonInteractivity();
    }
    private void OnClientDisconnected(ulong clientId)
    {
        // El cliente se ha desconectado, eliminar de la lista de jugadores
        TurnManager.Instance.PlayerIds.Remove((int)clientId);
        UpdateButtonInteractivity();
    }*/
}


