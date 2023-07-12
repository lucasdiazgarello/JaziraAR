using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

public class TestRelay : NetworkBehaviour
{

    public int cantJugadores = 4;
    private string nombreHost;
    public InputField cantidadJugadores;
    public InputField nombreHostinput;
    public PlayerNetwork playernetwork;
    public string colorSeleccionado;

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

    }
    private List<string> coloresDisponibles = new List<string>() { "Rojo", "Azul", "Violeta", "Naranja" };


    void OnClientConnected(ulong clientId)
    {
        // Verifica si el cliente conectado es el cliente local.
        if (NetworkManager.Singleton.LocalClientId == clientId)
        {
            // Aquí puedes llamar a tu RPC
            //playernetwork.TestServerRpc(nombreJugador, color);
            Debug.Log("Me conecte?");
        }
    }

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
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(cantJugadores - 1); // el host y 3 mas

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
            //PlayerNetwork.Instance.ImprimirDatosJugador();
            PlayerNetwork.Instance.AgregarJugador(1, nombreHost, 100, false, true, 2, 10, 10, 10, 10, 10, colorSeleccionado);
            //PlayerNetwork.Instance.AgregarJugador(1, "Juancho", 100, false, true, 2, 10, 10, 10, 10, 10,colorSeleccionado);
            //PlayerNetwork.Instance.AgregarJugador(1, "Pepe", 100, false, true, 2, 10, 10, 10, 10, 10, colorSeleccionado);
            RemoverColor(colorSeleccionado);
            Debug.Log("despues de cargar");
            PlayerNetwork.Instance.ImprimirTodosLosJugadores();

        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void JoinRelay(string codigo, string nombreJugador, string color)
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
            //List<string> coloresDisponibles = ObtenerColoresDisponibles();
            NetworkManager.Singleton.StartClient();
            Debug.Log("Se unio " + codigo);
            // Si el color no está disponible, asigna uno diferente
            if (!coloresDisponibles.Contains(color))
            {
                Debug.Log("Color seleccionado no está disponible. Asignando un color diferente...");
                color = AsignarColorDisponible();  // Necesitamos implementar este método
                if (color == null)
                {
                    Debug.Log("No hay colores disponibles. No se pudo unirse al juego.");
                    return;
                }
            }
            RemoverColor(color);

            playernetwork.TestServerRpc(nombreJugador, color);
            await Task.Delay(500);

            PlayerNetwork.Instance.ImprimirTodosLosJugadores();

        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
    private string AsignarColorDisponible()
    {
        if (coloresDisponibles.Count > 0)
        {
            return coloresDisponibles[0]; // Asigna el primer color disponible
            Debug.Log("Cambie el color");
        }
        else
        {
            return null; // No hay colores disponibles
        }
    }
    public void RemoverColor(string color)
    {
        coloresDisponibles.Remove(color);
    }
    /*public List<string> ObtenerColoresDisponibles()
    {
        return coloresDisponibles;
    }*/
}
