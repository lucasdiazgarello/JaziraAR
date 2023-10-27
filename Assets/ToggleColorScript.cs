using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ToggleColorScript : MonoBehaviour
{
    Toggle m_Toggle;
    private FixedString64Bytes colorNameTemp;
    private bool colorSet = false;

    void Start()
    {
        //Fetch the Toggle GameObject
        m_Toggle = GetComponent<Toggle>();
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });
    }
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

        if (TestRelay.Instance == null)
        {
            Debug.LogError("relay is null. Please assign it in the Inspector.");
        }
        else
        {
            TestRelay.Instance.colorSeleccionado.Value = colorNameTemp;
        }

        if (JoinScript.Instance == null)
        {
            Debug.LogError("unirse is null. Please assign it in the Inspector.");
        }
        else
        {
            JoinScript.Instance.colorSeleccionado.Value = colorNameTemp;
        }

        var playerId = NetworkManager.Singleton.LocalClientId;

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

}
