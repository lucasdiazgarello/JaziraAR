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
    public InputField codigo;
    public InputField cantidadJugadores;
    public InputField nombreHostinput;
    public PlayerNetwork playernetwork;
    public Toggle toggleRojo;
    public Toggle toggleAzul;
    public Toggle toggleVioleta;
    public Toggle toggleNaranja;
    //private string colorSeleccionado = "verde"; // Un valor predeterminado
    public string colorSeleccionado;
    public Text colorrojo;
    public Text colorazul;
    public Text colorvioleta;
    public Text colornaranja;

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

            //playernetwork.SetNomJugador(0, nombreHost);
            Debug.Log("nombre relay" + nombreHost);
            Debug.Log("color relay" + colorSeleccionado);
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(cantJugadores-1); // el host y 3 mas

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            Debug.Log(joinCode);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
                );
            NetworkManager.Singleton.StartHost();
            Debug.Log("antes de cargar");
            //PlayerNetwork.Instance.ImprimirDatosJugador();
            PlayerNetwork.Instance.MostrarJugadores();
            /*for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
            {
                PlayerNetwork.Instance.ImprimirDatosJugador(i);
            }*/
            Debug.Log("color pre cargar" + colorSeleccionado);

            //PlayerNetwork.Instance.CargarDatosJugador(1,nombreHost, 100, cantJugadores, false, true, 2, 10, 10, 10, 10, 10,colorSeleccionado);
            PlayerNetwork.Instance.AgregarJugador(nombreHost, 100, cantJugadores, false, false, 2, 10, 10, 10, 10, 10, colorSeleccionado);
            PlayerNetwork.Instance.AgregarJugador("JugadorManual", 120, cantJugadores, false, false, 1, 8, 9, 7, 6, 8, "azul");
            PlayerNetwork.Instance.AgregarJugador("manuel", 50, cantJugadores, false, false, 1, 8, 9, 7, 6, 8, "rojo");
            PlayerNetwork.Instance.AgregarJugador("manolo", 76, cantJugadores, false, false, 1, 8, 9, 7, 6, 8, "naranja");
            Debug.Log("despues de cargar");
            PlayerNetwork.Instance.MostrarJugadores();
            /*for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
            {
                PlayerNetwork.Instance.ImprimirDatosJugador(i);
            }*/
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
    public async void JoinRelay (string joinCode)
    {
        try
        {
            joinCode = codigo.text;
            Debug.Log("Joining Relay with " + joinCode);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
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
        } catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
}
