using UnityEngine;
using UnityEngine.UI;

public class ToggleColorScript : MonoBehaviour
{
    Toggle m_Toggle;
    public TestRelay relay;// Asigna esto en el Inspector a la instancia de tu script TestRelay
    public Unirse unirse;

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
            relay.colorSeleccionado = change.name; // Asumiendo que el nombre del GameObject es el color
            unirse.colorSeleccionado = change.name;
        }
    }
}
