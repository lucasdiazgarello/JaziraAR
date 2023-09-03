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
        UpdateComprarCaminoButton();
        UpdateComprarBaseButton();
        UpdateComprarPuebloButton();
    }
    private void Update()
    {
        int id = PlayerPrefs.GetInt("jugadorId");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        //primero desactivo botones si no es mi turno
        if (PlayerNetwork.Instance.IsMyTurn(PlayerPrefs.GetInt("jugadorId")))
        {
            if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1)
            {
                comprarCaminoButton.interactable = true;
            }
            else
            {
                comprarCaminoButton.interactable = false;
            }
            if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1 && jugador.ovejaCount >= 1 && jugador.trigoCount >= 1)
            {
                comprarBaseButton.interactable = true;
            }
            else
            {
                comprarBaseButton.interactable = false;
            }
            if (jugador.piedraCount >= 2 && jugador.trigoCount >= 3)
            {
                comprarPuebloButton.interactable = true;
            }
            else
            {
                comprarPuebloButton.interactable = false;
            }
        }
        else
        {
            comprarCaminoButton.interactable = false;
            comprarBaseButton.interactable = false;
            comprarPuebloButton.interactable = false;
        }
        /*if (PlayerNetwork.Instance.IsMyTurn(PlayerPrefs.GetInt("jugadorId")))
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
        }*/
    }

    public void ComprarCamino()
    {
        Debug.Log("ComprarCamino");
        int id = PlayerPrefs.GetInt("jugadorId");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        if (NetworkManager.Singleton.IsServer)
        {
            if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1)
            {
                int indexJugador = -1;
                bool jugadorEncontrado = false;
                // Búsqueda del jugador en la lista playerData
                for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
                {
                    //Debug.Log("La lista Ids es " + playerData[i].jugadorId);
                    if (PlayerNetwork.Instance.playerData[i].jugadorId == id)
                    {
                        jugadorEncontrado = true;
                        indexJugador = i;
                        break;
                    }
                }
                if (!jugadorEncontrado)
                {
                    Debug.Log("Jugador no encontrado en la lista playerData");
                    return;
                }
                // Crear una copia del jugador, modificarla y luego reemplazar el elemento original
                PlayerNetwork.DatosJugador jugadorcopia = PlayerNetwork.Instance.playerData[indexJugador];
                jugadorcopia.cantidadCaminos = jugadorcopia.cantidadCaminos + 1;
                PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
                //jugador.cantidadCaminos++;
                UpdateComprarCaminoButton();
                Debug.Log("Caminos Restantes:" + PlayerNetwork.Instance.playerData[indexJugador].cantidadCaminos);
                BoardManager.Instance.UpdateResourcesCamino(jugador);
                //BoardManager.Instance.UpdateResourceTexts(id);
                Debug.Log("Imprimir jugador por ID post ");
                PlayerNetwork.Instance.ImprimirJugadorPorId(id);
                colocarPieza.buttonCamino.interactable = true;
            }           
        }
        else
        {
            PlayerNetwork.Instance.ComprarCaminoServerRpc(id);
        }       
    }


    public void ComprarBase()
    {
        Debug.Log("ComprarBase");
        int id = PlayerPrefs.GetInt("jugadorId");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        if (NetworkManager.Singleton.IsServer)
        {
            if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1 && jugador.trigoCount >= 1 && jugador.ovejaCount >= 1)
            {
                int indexJugador = -1;
                bool jugadorEncontrado = false;
                // Búsqueda del jugador en la lista playerData
                for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
                {
                    //Debug.Log("La lista Ids es " + playerData[i].jugadorId);
                    if (PlayerNetwork.Instance.playerData[i].jugadorId == id)
                    {
                        jugadorEncontrado = true;
                        indexJugador = i;
                        break;
                    }
                }
                if (!jugadorEncontrado)
                {
                    Debug.Log("Jugador no encontrado en la lista playerData");
                    return;
                }
                // Crear una copia del jugador, modificarla y luego reemplazar el elemento original
                PlayerNetwork.DatosJugador jugadorcopia = PlayerNetwork.Instance.playerData[indexJugador];
                jugadorcopia.cantidadBases = jugadorcopia.cantidadBases + 1;
                PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
                //jugador.cantidadBases++;
                Debug.Log("Bases Restantes:" + PlayerNetwork.Instance.playerData[indexJugador].cantidadBases);
                UpdateComprarBaseButton();
                BoardManager.Instance.UpdateResourcesBase(jugador);
                BoardManager.Instance.UpdateResourceTexts(id);
                Debug.Log("Imprimir jugador por ID post ");
                PlayerNetwork.Instance.ImprimirJugadorPorId(id);
                colocarPieza.buttonBase.interactable = true;
            }                        
        }
        else
        {
            PlayerNetwork.Instance.ComprarBaseServerRpc(id);
        }              
    }
    public void ComprarPueblo()
    {
        Debug.Log("ComprarPueblo");
        int id = PlayerPrefs.GetInt("jugadorId");
        PlayerNetwork.DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        if (NetworkManager.Singleton.IsServer)
        {
            if (jugador.trigoCount >= 3 && jugador.piedraCount >= 2)
            {
                //Actualizacion de cantidad de pueblos:
                int indexJugador = -1;
                bool jugadorEncontrado = false;
                for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
                {
                    if (PlayerNetwork.Instance.playerData[i].jugadorId == id)
                    {
                        jugadorEncontrado = true;
                        indexJugador = i;
                        break;
                    }
                }
                if (!jugadorEncontrado)
                {
                    Debug.Log("Jugador no encontrado en la lista playerData");
                    return;
                }
                PlayerNetwork.DatosJugador jugadorcopia = PlayerNetwork.Instance.playerData[indexJugador];
                jugadorcopia.cantidadPueblos = jugadorcopia.cantidadPueblos + 1;
                PlayerNetwork.Instance.playerData[indexJugador] = jugadorcopia;
                //jugador.cantidadPueblos++;
                Debug.Log("Pueblos Restantes:" + PlayerNetwork.Instance.playerData[indexJugador].cantidadPueblos);
                UpdateComprarPuebloButton();
                BoardManager.Instance.UpdateResourcesPueblo(jugador);
                BoardManager.Instance.UpdateResourceTexts(id);
                Debug.Log("Imprimir jugador por ID post ");
                PlayerNetwork.Instance.ImprimirJugadorPorId(id);
                colocarPieza.buttonPueblo.interactable = true;
            }                     
        }
        else
        {
            PlayerNetwork.Instance.ComprarPuebloServerRpc(id);
            //PlayerNetwork.Instance.UpdateCantidadPiezadServerRpc(id, jugador.cantidadCaminos, jugador.cantidadBases, jugador.cantidadPueblos, jugador.primerasPiezas);
        }
    }
    void UpdateComprarCaminoButton()
    {
        int id = PlayerPrefs.GetInt("jugadorId");
        if (NetworkManager.Singleton.IsServer)
        {           
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
    }
    void UpdateComprarBaseButton()
    {
        int id = PlayerPrefs.GetInt("jugadorId");
        if (NetworkManager.Singleton.IsServer)
        {
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
    }
    void UpdateComprarPuebloButton()
    {
        int id = PlayerPrefs.GetInt("jugadorId");
        if (NetworkManager.Singleton.IsServer)
        {
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
    }
    
}
