using System.Collections;
using System.Collections.Generic;
using Unity.Services.Relay;
using UnityEngine;
using UnityEngine.UI;

public class Unirse : MonoBehaviour
{
    // Start is called before the first frame update
    public TestRelay relay;
    private string nombreJugador;
    public InputField nombreJugadorinput;
    public InputField codigo;
    public string colorSeleccionado;
    void Start()
    {
        
    }

    public void UnirsePartida()
    {
        Debug.Log("Entre a Unirme");
        var code = codigo.text.ToString();
        Debug.Log("Codigo: " + code);
        nombreJugador = nombreJugadorinput.text.ToString();
        Debug.Log("nombre jugador: "+ nombreJugador);
        Debug.Log("antes de join relay");
        Debug.Log("color seleccionado: " + colorSeleccionado);
        try
        {
            relay.JoinRelay(code, nombreJugador, colorSeleccionado);
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
        Debug.Log("despues de join relay");


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
