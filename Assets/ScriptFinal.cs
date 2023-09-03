using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerNetwork;

public class ScriptFinal : MonoBehaviour
{
    public Text NombreGanadorText;
    // Start is called before the first frame update
    void Start()
    {
        //Buscar el jmjugador que tiene gano en true
        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            if (PlayerNetwork.Instance.playerData[i].gano == true)
            {
                NombreGanadorText.text = PlayerNetwork.Instance.playerData[i].nomJugador.ToString();
            }
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
