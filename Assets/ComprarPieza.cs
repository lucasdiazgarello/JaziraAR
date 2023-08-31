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
        //comprarCaminoButton.interactable = false;
        //comprarBaseButton.interactable = false;
        //comprarPuebloButton.interactable = false;
        UpdateComprarCaminoButton();
        UpdateComprarBaseButton();
        UpdateComprarPuebloButton();


    }
    private void Update()
    {
        if (PlayerNetwork.Instance.IsMyTurn(PlayerPrefs.GetInt("jugadorId")))
        {
            //Debug.Log("Es mi TURNO");
            comprarCaminoButton.interactable = true;
            comprarBaseButton.interactable = true;
            comprarPuebloButton.interactable = true;
            //terminarTurnoButton.interactable = true;
        }
        else
        {
            comprarCaminoButton.interactable = false;
            comprarBaseButton.interactable = false;
            comprarPuebloButton.interactable = false;
            //terminarTurnoButton.interactable = false;
        }
        //UpdateComprarCaminoButton();
        //UpdateComprarBaseButton();
        //UpdateComprarPuebloButton();
    }

    public void ComprarCamino()
    {
        Debug.Log("ComprarCamino");
        if (NetworkManager.Singleton.IsServer)
        {
            int id = PlayerPrefs.GetInt("jugadorId");
            PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
            if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1)
            {
                BoardManager.Instance.UpdateResourcesCamino(jugador);
                BoardManager.Instance.UpdateResourceTexts(id);
                Debug.Log("Imprimir jugador por ID post ");
                PlayerNetwork.Instance.ImprimirJugadorPorId(id);
                UpdateComprarCaminoButton();
            }
        }
        else
        {
            PlayerNetwork.Instance.ComprarCaminoServerRpc(PlayerPrefs.GetInt("jugadorId"));
        }
        colocarPieza.caminosRestantes++;
        Debug.Log("Caminos Restantes:" + colocarPieza.caminosRestantes);
        colocarPieza.buttonCamino.interactable = true;
    }


    public void ComprarBase()
    {
        Debug.Log("ComprarBase");
        if (NetworkManager.Singleton.IsServer)
        {
            int id = PlayerPrefs.GetInt("jugadorId");
            PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
            if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1 && jugador.trigoCount >= 1 && jugador.ovejaCount >= 1)
            {
                BoardManager.Instance.UpdateResourcesBase(jugador);
                BoardManager.Instance.UpdateResourceTexts(id);
                Debug.Log("Imprimir jugador por ID post ");
                PlayerNetwork.Instance.ImprimirJugadorPorId(id);
                UpdateComprarBaseButton();              
            }
        }
        else
        {
            PlayerNetwork.Instance.ComprarBaseServerRpc(PlayerPrefs.GetInt("jugadorId"));
        }
        colocarPieza.basesRestantes++;
        Debug.Log("Bases Restantes:" + colocarPieza.basesRestantes);
        colocarPieza.buttonBase.interactable = true;
    }
    public void ComprarPueblo()
    {
        Debug.Log("ComprarPueblo");
        if (NetworkManager.Singleton.IsServer)
        {
            int id = PlayerPrefs.GetInt("jugadorId");
            PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
            if (jugador.trigoCount >= 3 && jugador.piedraCount >= 2)
            {
                BoardManager.Instance.UpdateResourcesPueblo(jugador);
                BoardManager.Instance.UpdateResourceTexts(id);
                Debug.Log("Imprimir jugador por ID post ");
                PlayerNetwork.Instance.ImprimirJugadorPorId(id);
                UpdateComprarPuebloButton();
            }
        }
        else
        {
            PlayerNetwork.Instance.ComprarPuebloServerRpc(PlayerPrefs.GetInt("jugadorId"));
        }
        colocarPieza.pueblosRestantes++;
        Debug.Log("Pueblos Restantes:" + colocarPieza.pueblosRestantes);
        colocarPieza.buttonPueblo.interactable = true;
    }
    void UpdateComprarCaminoButton()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            int id = PlayerPrefs.GetInt("jugadorId");
            PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
            if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1)
            {
                comprarCaminoButton.interactable = true;
            }
            else
            {
                comprarCaminoButton.interactable = false;
            }
        }
        else
        {
            //PlayerNetwork.Instance.UpdateComprarCaminoButtonClientRpc();
            PlayerNetwork.Instance.UpdateComprarCaminoButtonServerRpc();
        }
    }
    void UpdateComprarBaseButton()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            int id = PlayerPrefs.GetInt("jugadorId");
            PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
            if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1 && jugador.trigoCount >= 1 && jugador.ovejaCount >= 1)
            {
                comprarCaminoButton.interactable = true;
            }
            else
            {
                comprarCaminoButton.interactable = false;
            }
        }
        else
        {
            //PlayerNetwork.Instance.UpdateComprarBaseButtonClientRpc();
            PlayerNetwork.Instance.UpdateComprarBaseButtonServerRpc();
        }
    }
    void UpdateComprarPuebloButton()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            int id = PlayerPrefs.GetInt("jugadorId");
            PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
            if (jugador.piedraCount >= 2 && jugador.trigoCount >= 3)
            {
                comprarCaminoButton.interactable = true;
            }
            else
            {
                comprarCaminoButton.interactable = false;
            }
        }
        else
        {
            //PlayerNetwork.Instance.UpdateComprarPuebloButtonClientRpc();
            PlayerNetwork.Instance.UpdateComprarPuebloButtonServerRpc();
        }
    }
    
}
