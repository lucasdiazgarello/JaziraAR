using System;
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
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public enum ColoresJugadorEnum
{
    Rojo,
    Azul,
    Violeta,
    Naranja
}

public struct ColoresJugador : IEquatable<ColoresJugador>
{
    public ColoresJugadorEnum Color { get; set; }

    public bool Equals(ColoresJugador other)
    {
        return Color == other.Color;
    }
}


public class TestRelay : NetworkBehaviour
{

    public int cantJugadores = 4;
    private string nombreHost;
    public InputField cantidadJugadores;
    public InputField nombreHostinput;
    public PlayerNetwork playernetwork;
    public string colorSeleccionado;
    //public ColoresJugadorEnum colorSeleccionado;
    public NetworkList<ColoresJugador> coloresDisponibles = new NetworkList<ColoresJugador>();


    private async void Start()
    {
        if (IsServer)
        {
            coloresDisponibles.Add(new ColoresJugador { Color = ColoresJugadorEnum.Rojo });
            coloresDisponibles.Add(new ColoresJugador { Color = ColoresJugadorEnum.Azul });
            coloresDisponibles.Add(new ColoresJugador { Color = ColoresJugadorEnum.Violeta });
            coloresDisponibles.Add(new ColoresJugador { Color = ColoresJugadorEnum.Naranja });
        }

        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
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
            //RemoverColor(colorSeleccionado);
            ColoresJugadorEnum colorEnum = (ColoresJugadorEnum)Enum.Parse(typeof(ColoresJugadorEnum), colorSeleccionado, true);
            coloresDisponibles.Remove(new ColoresJugador { Color = colorEnum });
            //coloresDisponibles.Remove(new ColoresJugador { Color = colorSeleccionado });
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
            // Si el color no está disponible, asigna uno diferente
            Debug.Log("Color que se busca" + color);
            Debug.Log("Cantidad disponibles" + coloresDisponibles.Count);
            Debug.Log("Colores disponibles" + coloresDisponibles.ToShortString());

            colorSeleccionado = color;
            ColoresJugadorEnum colorEnum = (ColoresJugadorEnum)Enum.Parse(typeof(ColoresJugadorEnum), colorSeleccionado, true);

            if (!coloresDisponibles.Contains(new ColoresJugador { Color = colorEnum }))
            {
                Debug.Log("Color seleccionado no está disponible. Asignando un color diferente...");
                colorSeleccionado = AsignarColorDisponible()?.Color.ToString();

                if (colorSeleccionado == null)
                {
                    Debug.Log("No hay colores disponibles. No se pudo unirse al juego.");
                    return;
                }
            }

            colorEnum = (ColoresJugadorEnum)Enum.Parse(typeof(ColoresJugadorEnum), colorSeleccionado, true);
            RemoverColor(new ColoresJugador { Color = colorEnum });

            PlayerNetwork.Instance.ImprimirTodosLosJugadores();

        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }


    /*public List<string> ObtenerColoresDisponibles()
    {
        return coloresDisponibles;
    }*/


    private ColoresJugador? AsignarColorDisponible()
    {
        if (coloresDisponibles.Count > 0)
        {
            return coloresDisponibles[0]; // Asigna el primer color disponible
        }
        else
        {
            return null; // No hay colores disponibles
        }
    }

    public void RemoverColor(ColoresJugador color)
    {
        coloresDisponibles.Remove(color);
    }
}


