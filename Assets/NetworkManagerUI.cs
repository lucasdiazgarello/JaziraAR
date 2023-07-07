using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;


public class NetworkManagerUI : MonoBehaviour
{
    private TestRelay testrelay;

    [SerializeField] private Button hostBtn;
    [SerializeField] private Button joinBtn;

    
    private void Awake()
    {
        /*
        hostBtn.onClick.AddListener(() =>
        {

            //testrelay.CreateRelay();
            testrelay.StartRelayHost();
            NetworkManager.Singleton.StartHost();
            
        });

        joinBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
        */
    }
}
