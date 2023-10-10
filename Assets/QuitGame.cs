using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerNetwork;

public class QuitGame : MonoBehaviour
{
    public void Abandonar()
    {
        if(NetworkManager.Singleton.IsServer)
        {           
            SceneManager.LoadScene("AbandonarScene");
            PlayerNetwork.Instance.CargarAbandonarSceneClientRpc();
        }
        else
        {
            PlayerNetwork.Instance.AbandonarServerRpc();
        }
    }
}
