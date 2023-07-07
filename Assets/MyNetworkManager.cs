using Unity.Netcode;

public class MyNetworkManager : NetworkManager
{
    public static MyNetworkManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Resto de tu código...
}
