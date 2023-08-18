using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ComprarPieza : NetworkBehaviour
{
    // referencia al botón de comprar camino
    public Button comprarCaminoButton;
    public Button comprarBaseButton;
    public Button comprarPuebloButton;
    public ColocarPieza colocarPieza;
    public LayerMask myLayerMask;

    void Start()
    {
        // Desactivamos los botones por defecto al inicio.
        UpdateComprarCaminoButton();
        UpdateComprarBaseButton();
        UpdateComprarPuebloButton();

    }
    private void Update()
    {
        UpdateComprarCaminoButton();
        UpdateComprarBaseButton();
        UpdateComprarPuebloButton();
    }
    public void ComprarCamino()
    {
        Debug.Log("ComprarCamino");
        if (NetworkManager.Singleton.IsServer)
        {
            int id = PlayerPrefs.GetInt("jugadorId");
            PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
            PlayerNetwork.Instance.ImprimirJugador(jugador);
            if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1)
            {
                BoardManager.Instance.UpdateResourcesCamino(jugador);
                BoardManager.Instance.UpdateResourceTexts(id);

                Debug.Log("Imprimir jugador por ID post ");
                PlayerNetwork.Instance.ImprimirJugadorPorId(id);
            }
        }
        else
        {
            PlayerNetwork.Instance.ComprarCaminoServerRpc(PlayerPrefs.GetInt("jugadorId"));
        }
        colocarPieza.caminosRestantes++;
        colocarPieza.buttonCamino.interactable = true;
    }
    public void ComprarBase()
    {
        Debug.Log("ComprarBase");
        if (NetworkManager.Singleton.IsServer)
        {
            int id = PlayerPrefs.GetInt("jugadorId");
            PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
            PlayerNetwork.Instance.ImprimirJugador(jugador);
            if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1 && jugador.trigoCount >= 1 && jugador.ovejaCount >= 1)
            {
                BoardManager.Instance.UpdateResourcesBase(jugador);
                BoardManager.Instance.UpdateResourceTexts(id);
                Debug.Log("Imprimir jugador por ID post ");
                PlayerNetwork.Instance.ImprimirJugadorPorId(id);
            }
        }
        else
        {
            PlayerNetwork.Instance.ComprarBaseServerRpc(PlayerPrefs.GetInt("jugadorId"));
        }
        colocarPieza.basesRestantes++;
        colocarPieza.buttonBase.interactable = true;
    }
    public void ComprarPueblo()
    {
        Debug.Log("ComprarPueblo");
        if (NetworkManager.Singleton.IsServer)
        {
            int id = PlayerPrefs.GetInt("jugadorId");
            PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
            PlayerNetwork.Instance.ImprimirJugador(jugador);
            if (jugador.trigoCount >= 3 && jugador.piedraCount >= 2)
            {
                BoardManager.Instance.UpdateResourcesPueblo(jugador);
                BoardManager.Instance.UpdateResourceTexts(id);
                Debug.Log("Imprimir jugador por ID post ");
                PlayerNetwork.Instance.ImprimirJugadorPorId(id);
            }
        }
        else
        {
            PlayerNetwork.Instance.ComprarPuebloServerRpc(PlayerPrefs.GetInt("jugadorId"));
        }
        colocarPieza.pueblosRestantes++;
        colocarPieza.buttonPueblo.interactable = true;
    }
    void UpdateComprarCaminoButton()
    {
        int id = PlayerPrefs.GetInt("jugadorId");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);

        comprarCaminoButton.interactable = (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1);
    }
    void UpdateComprarBaseButton()
    {
        int id = PlayerPrefs.GetInt("jugadorId");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        comprarBaseButton.interactable = (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1 && jugador.trigoCount >= 1 && jugador.ovejaCount >= 1);
    }
    void UpdateComprarPuebloButton()
    {
        int id = PlayerPrefs.GetInt("jugadorId");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);

        comprarPuebloButton.interactable = (jugador.piedraCount >= 2 && jugador.trigoCount >= 3);
    }
}
