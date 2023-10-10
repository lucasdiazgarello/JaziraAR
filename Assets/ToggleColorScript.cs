using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ToggleColorScript : MonoBehaviour
{
    Toggle m_Toggle;
    //public TestRelay relay;// Asigna esto en el Inspector a la instancia de tu script TestRelay
    //public JoinScript unirse;
    private FixedString64Bytes colorNameTemp;
    private bool colorSet = false;

    void Start()
    {
        //Fetch the Toggle GameObject
        m_Toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });
    }
    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change)
    {
        if (change.isOn)
        {
            SetColor(change.name);
            Debug.Log("Toque el toggle " + change.name);
        }
    }

    void Update()
    {
        if (!colorSet && m_Toggle.isOn)
        {
            colorSet = true;
            SetColor(m_Toggle.name);
        }
    }

    void SetColor(string colorName)
    {
        colorNameTemp = new FixedString64Bytes(colorName);
        Debug.Log("el color del toggle es " + colorNameTemp);

        // Check if relay is null
        if (TestRelay.Instance == null)
        {
            Debug.LogError("relay is null. Please assign it in the Inspector.");
        }
        else
        {
            TestRelay.Instance.colorSeleccionado.Value = colorNameTemp;
        }

        // Check if unirse is null
        if (JoinScript.Instance == null)
        {
            Debug.LogError("unirse is null. Please assign it in the Inspector.");
        }
        else
        {
            JoinScript.Instance.colorSeleccionado.Value = colorNameTemp;
        }

        var playerId = NetworkManager.Singleton.LocalClientId;

        // Check if PlayerNetwork.Instance is null
        if (PlayerNetwork.Instance == null)
        {
            Debug.LogError("PlayerNetwork.Instance is null. Make sure PlayerNetwork is properly initialized.");
        }
        else
        {
            PlayerNetwork.Instance.UpdatePlayerColorServerRpc((int)playerId, colorNameTemp);
        }

        colorSet = true;
    }

    //Output the new state of the Toggle into Text

    /*
    public override void OnNetworkSpawn()
    {
        // Establece el valor real una vez que el objeto está en la red
        relay.colorSeleccionado.Value = colorNameTemp;
        unirse.colorSeleccionado.Value = colorNameTemp;
        var playerId = NetworkManager.Singleton.LocalClientId;
        PlayerNetwork.Instance.UpdatePlayerColorServerRpc((int)playerId, colorNameTemp);
    }*/
}
