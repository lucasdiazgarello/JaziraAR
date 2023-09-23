using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerNetwork;

public class AbandonarPartida : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Abandonar()
    {
        if(NetworkManager.Singleton.IsServer)
        {
            int puntajeMaximo = 0;
            DatosJugador jugaMaximo = new DatosJugador();
            for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
            {

                DatosJugador jugaActual = PlayerNetwork.Instance.playerData[i];

                // Compara el puntaje actual con el puntaje máximo
                if (jugaActual.puntaje > puntajeMaximo)
                {
                    puntajeMaximo = jugaActual.puntaje; // Actualiza el puntaje máximo si es mayor
                    jugaMaximo = jugaActual;
                }
            }
            jugaMaximo.gano = true;
            for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
            {
                //Debug.Log("La lista Ids es " + playerData[i].jugadorId);
                if (PlayerNetwork.Instance.playerData[i].jugadorId == jugaMaximo.jugadorId)
                {
                    PlayerNetwork.Instance.playerData[i] = jugaMaximo;
                }
            }
            Debug.Log("El jugador " + jugaMaximo.nomJugador + " es el gandor");
            SceneManager.LoadScene("EscenaFinal");
            PlayerNetwork.Instance.CargarEscenaFinalClientRpc();
        }
        else
        {
            PlayerNetwork.Instance.AbandonarServerRpc();
        }
        
    }
}
