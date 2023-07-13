using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using Unity.Services.Relay;
using UnityEngine;
using UnityEngine.UI;

public class Unirse : NetworkBehaviour
{
    // Start is called before the first frame update
    public TestRelay relay;
    //private string nombreJugador;
    public InputField nombreJugadorinput;
    public InputField codigo;
    //public string colorSeleccionado;
    public PlayerNetwork playerNetwork;
    public NetworkVariable<FixedString64Bytes> nombreJugador = new NetworkVariable<FixedString64Bytes>();
    public NetworkVariable<FixedString64Bytes> colorSeleccionado = new NetworkVariable<FixedString64Bytes>();
    void Start()
    {
        
    }

    public void UnirsePartida()
    {
        Debug.Log("Entre a Unirme");
        var code = codigo.text.ToString();
        Debug.Log("Codigo: " + code);
        //nombreJugador = nombreJugadorinput.text.ToString();
        nombreJugador.Value = new FixedString64Bytes(nombreJugadorinput.text); //toma el valor del input y lo pone en la variable 
        Debug.Log("nombre jugador: "+ nombreJugador.Value);
        Debug.Log("antes de join relay");
        Debug.Log("color seleccionado: " + colorSeleccionado.Value);

        try
        {
            relay.JoinRelay(code);
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
        Debug.Log("despues de join relay");
    }

    public override void OnNetworkSpawn() //Se activará para todos los clientes cuando se cree un objeto en la red
    {
        if (IsOwner)
        {
            Debug.Log("nombre y color del OnNetworkSpawn " + nombreJugador.Value + colorSeleccionado.Value);
            playerNetwork.TestServerRpc(nombreJugador.Value, colorSeleccionado.Value);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
