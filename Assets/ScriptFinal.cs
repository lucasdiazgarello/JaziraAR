using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerNetwork;

public class ScriptFinal : MonoBehaviour
{
    public Text NombreGanadorText;
    void Start()
    {
        //Buscar el jugador que tiene gano en true
        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            if (PlayerNetwork.Instance.playerData[i].gano == true)
            {
                NombreGanadorText.text = PlayerNetwork.Instance.playerData[i].nomJugador.ToString();
            }
        }       
    }

    void Update()
    {
        
    }
}
