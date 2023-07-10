using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

public class TestRelay : MonoBehaviour
{
 
    public int cantJugadores = 4;
    private string nombreHost;
    
    public InputField cantidadJugadores;
    public InputField nombreHostinput;
    public PlayerNetwork playernetwork;
    /*public Toggle toggleRojo;
    public Toggle toggleAzul;
    public Toggle toggleVioleta;
    public Toggle toggleNaranja;*/
    //private string colorSeleccionado = "verde"; // Un valor predeterminado
    public string colorSeleccionado;
    /*public Text colorrojo;
    public Text colorazul;
    public Text colorvioleta;
    public Text colornaranja;
    */
    private async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        //toggleRojo.onValueChanged.AddListener(OnToggleClicked);
        //toggleAzul.onValueChanged.AddListener(OnToggleClicked);
        //toggleVioleta.onValueChanged.AddListener(OnToggleClicked);
        //toggleNaranja.onValueChanged.AddListener(OnToggleClicked);
        //Fetch the Toggle GameObject
        /*toggleRojo = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        toggleRojo.onValueChanged.AddListener(delegate {
            ToggleValueChangedRojo(toggleRojo);
        });

        toggleAzul = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        toggleAzul.onValueChanged.AddListener(delegate {
            ToggleValueChangedAzul(toggleAzul);
        });

        //Initialise the Text to say the first state of the Toggle
        colorrojo.text = "First Value : " + toggleRojo.isOn;
        colorazul.text = "First Value : " + toggleAzul.isOn;
        */
    }
    //Output the new state of the Toggle into Text
    /*void ToggleValueChangedRojo(Toggle change)
    {
        colorrojo.text = "Rojo : " + toggleRojo.isOn;
    }
    void ToggleValueChangedAzul(Toggle change)
    {
        colorazul.text = "Azul : " + toggleAzul.isOn;
    }
    public void OnToggleClicked()
    {
        if (toggleRojo.isOn)
        {
            colorSeleccionado = "rojo";
            Debug.Log("Rojo seleccionado");
        }
        else if (toggleAzul.isOn)
        {
            colorSeleccionado = "azul";
            Debug.Log("Azul seleccionado");
        }
        else if (toggleVioleta.isOn)
        {
            colorSeleccionado = "violeta";
            Debug.Log("Violeta seleccionado");
        }
        else if (toggleNaranja.isOn)
        {
            colorSeleccionado = "naranja";
            Debug.Log("Naranja seleccionado");
        }
    }*/

    public async void CreateRelay()
    {
        try
        {
            //traer cantJugadores del canvas
            cantJugadores = int.Parse(cantidadJugadores.text);
            nombreHost = nombreHostinput.text;
            /*// Determine el color del jugador
            string colorSeleccionado = "rojo"; // Un valor predeterminado
            if (toggleAzul.isOn) colorSeleccionado = "azul";
            else if (toggleVioleta.isOn) colorSeleccionado = "violeta";
            else if (toggleNaranja.isOn) colorSeleccionado = "naranja";*/

            //playernetwork.SetNomJugador(0, nombreHost);
            Debug.Log("nombre relay" + nombreHost);
            Debug.Log("color relay" + colorSeleccionado);
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(cantJugadores-1); // el host y 3 mas

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            Debug.Log("El codigo es:" + joinCode);
            Debug.Log("Va a iniciar el host");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
                );
            NetworkManager.Singleton.StartHost();
            Debug.Log("Inicio el host");
            Debug.Log("antes de cargar");
            PlayerNetwork.Instance.ImprimirDatosJugador();
            Debug.Log("color pre cargar" + colorSeleccionado);
            //PlayerNetwork.Instance.CargarDatosColorJugador(colorSeleccionado);
            //PlayerNetwork.Instance.CargarDatosColorJugador(colorSeleccionado);
            //Debug.Log("color post cargar" + colorSeleccionado);
            PlayerNetwork.Instance.CargarDatosJugador(1,nombreHost, 100, cantJugadores, false, true, 2, 10, 10, 10, 10, 10,colorSeleccionado);
            // Luego de determinar el color, se lo asigna al jugador:
            //PlayerNetwork.Instance.CargarDatosColorJugador(colorSeleccionado);
            Debug.Log("despues de cargar");
            PlayerNetwork.Instance.ImprimirDatosJugador();
            //playernetwork.CrearJugadores();
            //playernetwork.SetNomJugador(0, nombreHost);

        } catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
    public async void JoinRelay (string codigo, string nombreJugador, string color )
    {
        try
        {


            Debug.Log("Joining Relay with " + codigo);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(codigo);
            //Debug.Log("joinAllocation: " + joinAllocation);
            //Debug.Log("NetworkManager.Singleton: " + NetworkManager.Singleton);
            //Debug.Log("NetworkManager.Singleton.GetComponent<UnityTransport>(): " + NetworkManager.Singleton.GetComponent<UnityTransport>());

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                joinAllocation.RelayServer.IpV4,
                (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData
                );

            NetworkManager.Singleton.StartClient();
            Debug.Log("Se unio " + codigo);
            PlayerNetwork.Instance.CargarDatosJugador(1, nombreJugador, 100, 4, false, true, 2, 10, 10, 10, 10, 10, color);
            Debug.Log("Cargo jugador");
            PlayerNetwork.Instance.ImprimirDatosJugador();
        } catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
}
