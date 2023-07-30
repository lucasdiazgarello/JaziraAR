using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestRelay : NetworkBehaviour
{
    public int cantJugadores = 4;
    private FixedString64Bytes nombreHost;
    public InputField cantidadJugadores;
    public InputField nombreHostinput;
    //public PlayerNetwork playernetwork;
    //public string colorSeleccionado;
    public NetworkVariable<FixedString64Bytes> colorSeleccionado = new NetworkVariable<FixedString64Bytes>();
    private async void Start()
    {

        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    private List<string> coloresDisponibles = new List<string>() { "Rojo", "Azul", "Violeta", "Naranja" };
    
    public static TestRelay Instance; // Instancia Singleton

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Esto garantiza que el objeto empty que tiene el script no se destruir� al cargar una nueva escena
        }
        else
        {
            Destroy(gameObject); // Si ya hay una instancia, destruye esta
        }
    }
    public async void CreateRelay()
    {
        try
        {
            //traer cantJugadores del canvas
            cantJugadores = int.Parse(cantidadJugadores.text);
            //nombreHost = nombreHostinput.text;
            nombreHost = new FixedString64Bytes(nombreHostinput.text);

            Debug.Log("nombre relay " + nombreHost);
            Debug.Log("color relay " + colorSeleccionado.Value);
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
            Debug.Log("Inicio Host");
            //Guardar el ID del jugador en PlayerPrefs cuando se selecciona el jugador
            //PlayerPrefs.SetString("jugadorId", AuthenticationService.Instance.PlayerId);
            int num = ConvertirAlfaNumericoAInt(AuthenticationService.Instance.PlayerId);
            PlayerPrefs.SetInt("jugadorId", num);
            Debug.Log("ID NUEVO" + num);
            Debug.Log("Inicio el host");
            Debug.Log("antes de cargar");
            Debug.Log("color antes de agregarjugador " + colorSeleccionado.Value);
            /*
            PlayerNetwork.Instance.ImprimirDatosJugador();
            PlayerNetwork.Instance.AgregarJugador(num, nombreHost, 100, false, true, 2, 10, 10, 10, 10, 10, colorSeleccionado.Value);
            //PlayerNetwork.Instance.AgregarJugador(1, "Juancho", 100, false, true, 2, 10, 10, 10, 10, 10,colorSeleccionado);
            //PlayerNetwork.Instance.AgregarJugador(1, "Pepe", 100, false, true, 2, 10, 10, 10, 10, 10, colorSeleccionado);
            //RemoverColor(colorSeleccionado);
            Debug.Log("Termino CreateRelay");
            PlayerNetwork.Instance.ImprimirTodosLosJugadores();
            */
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void JoinRelay(string codigo)
    {
        try
        {
            Debug.Log("Joining Relay with " + codigo);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(codigo);

            // Check if joinAllocation is not null before attempting to access its properties
            if (joinAllocation != null)
            {
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
                //Guardar el ID del jugador en PlayerPrefs cuando se selecciona el jugador
                //PlayerPrefs.SetString("jugadorId", AuthenticationService.Instance.PlayerId);
                int num = ConvertirAlfaNumericoAInt(AuthenticationService.Instance.PlayerId);
                PlayerPrefs.SetInt("jugadorId", num);
                Debug.Log("MI ID ES" + num);
                Debug.Log("Se unio " + codigo);
            }
            else
            {
                Debug.Log("joinAllocation is null.");
            }
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
        catch (NullReferenceException e)
        {
            Debug.Log("NullReferenceException caught: " + e.Message);
        }
    }
    private string AsignarColorDisponible()
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
    public void RemoverColor(string color)
    {
        coloresDisponibles.Remove(color);
    }
    /*public List<string> ObtenerColoresDisponibles()
    {
        return coloresDisponibles;
    }*/
    public int ConvertirAlfaNumericoAInt(string texto)
    {
        string soloNumeros = string.Empty;

        foreach (char c in texto)
        {
            if (Char.IsDigit(c))
            {
                soloNumeros += c;
            }
        }

        if (Int32.TryParse(soloNumeros, out int resultado))
        {
            return resultado;
        }
        else
        {
            throw new Exception("No se pudo convertir la cadena a un n�mero entero.");
        }
    }
    IEnumerator waiter()
    {
        //Wait for 4 seconds
        Debug.Log("Entre al waiter, espero 4 segundos");
        yield return new WaitForSeconds(4);
        Debug.Log("Volvi al waiter");
        int num = ConvertirAlfaNumericoAInt(AuthenticationService.Instance.PlayerId);
        PlayerPrefs.SetInt("jugadorId", num);

        if (PlayerNetwork.Instance != null)
        {
            PlayerNetwork.Instance.AgregarJugador(num, nombreHost, 100, false, true, 2, 10, 10, 10, 10, 10, colorSeleccionado.Value);
            Debug.Log("Agregue al host");
            PlayerNetwork.Instance.ImprimirTodosLosJugadores();
        }
        else
        {
            Debug.Log("Instancia PlayerNetwork no encontrada");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            Debug.Log("Entre al cambio de escena");
            StartCoroutine(waiter());
            //int num = ConvertirAlfaNumericoAInt(AuthenticationService.Instance.PlayerId);
            //PlayerPrefs.SetInt("jugadorId", num);
            ////PlayerNetwork.Instance.AgregarJugador(num, nombreHost, 100, false, true, 2, 10, 10, 10, 10, 10, colorSeleccionado.Value);
            //PlayerNetwork.Instance.AgregarJugadorWaiter(num, nombreHost, 100, false, true, 2, 10, 10, 10, 10, 10, colorSeleccionado.Value);
            //Debug.Log("Agregue al host");
            //PlayerNetwork.Instance.ImprimirTodosLosJugadoresWaiter();
            ////PlayerNetwork.Instance.ImprimirTodosLosJugadores();
        }
    }
}
