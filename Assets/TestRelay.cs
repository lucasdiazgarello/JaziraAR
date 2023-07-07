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
using UnityEngine.SceneManagement;

public class TestRelay : MonoBehaviour
{
    public int cantJugadores = 4;
    private string nombreHost;
    public InputField codigo;
    public InputField cantidadJugadores;
    public InputField nombreHostInput;
    public InputField nombreJugadorInput;
    public PlayerNetwork playernetwork;
    public string colorSeleccionado;

    private async void Start()
    {
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
            Debug.Log("Entro CreateRelay");
            //traer cantJugadores del canvas
            cantJugadores = int.Parse(cantidadJugadores.text);
            nombreHost = nombreHostInput.text;

            Debug.Log("nombre relay" + nombreHost);
            Debug.Log("color relay" + colorSeleccionado);
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(cantJugadores - 1); // el host y 3 mas

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            Debug.Log(joinCode);
            Debug.Log("va a iniciar host");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
                );
            NetworkManager.Singleton.StartHost();
            Debug.Log("Inicio host");

        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }

        // Cargamos la siguiente escena para elegir el color
        //SceneManager.LoadScene("NombreDeTuEscenaElegirColor"); // Recuerda cambiar "NombreDeTuEscenaElegirColor" al nombre correcto de tu escena.
    }

    public async void JoinRelay(string joinCode)
    {
        try
        {
            joinCode = codigo.text;
            Debug.Log("Joining Relay with " + joinCode);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                joinAllocation.RelayServer.IpV4,
                (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData
                );

            NetworkManager.Singleton.StartClient();
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

    public void CreatePlayer()
    {
        Debug.Log("Entro CreatePlayer");
        string nombreJugador = nombreJugadorInput.text;
        string colorSeleccionado = this.colorSeleccionado;

        // Asegúrate de usar los valores correctos para tu juego.
        PlayerNetwork.Instance.AgregarJugador(nombreJugador, 100, cantJugadores, false, false, 2, 10, 10, 10, 10, 10, colorSeleccionado);
        PlayerNetwork.Instance.MostrarJugadores();
 
        //SceneManager.LoadScene("NombreDeTuEscenaDeJuego"); // Asegúrate de cambiar "NombreDeTuEscenaDeJuego" al nombre correcto de tu escena.
    }
}



/*
using System;
using System.Collections;
using System.Threading.Tasks;
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
    public Button continuarButton;
    public Button crearJugadorButton;
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
    private TaskCompletionSource<bool> startRelayHostCompletionSource;

    private async void Start()
    {
        Debug.Log("Iniciando Start...");
        continuarButton.onClick.AddListener(StartRelayHostt);
        crearJugadorButton.onClick.AddListener(CreatePlayer);
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        Debug.Log("NetworkManager: " + (NetworkManager.Singleton != null));
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(cantJugadores - 1); // el host y 3 mas
        Debug.Log("Allocation created");
        //Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        Debug.Log("Got join code: " + joinCode);
        Debug.Log("ANTES--------------------");
        //Debug.Log(joinCode);
        //Debug.Log("NetworkManager: " + (NetworkManager.Singleton != null));
        Debug.Log("UnityTransport: " + (NetworkManager.Singleton.GetComponent<UnityTransport>() != null));
        Debug.Log("RelayServer.IpV4: " + (allocation.RelayServer.IpV4 != null));
        Debug.Log("Port: " + allocation.RelayServer.Port);
        Debug.Log("AllocationIdBytes: " + (allocation.AllocationIdBytes != null));
        Debug.Log("Key: " + (allocation.Key != null));
        Debug.Log("ConnectionData: " + (allocation.ConnectionData != null));
        
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
               allocation.RelayServer.IpV4,
               (ushort)allocation.RelayServer.Port,
               allocation.AllocationIdBytes,
               allocation.Key,
               allocation.ConnectionData
           );
        //Debug.Log(joinCode);
        Debug.Log("DESPUES--------------------");
        Debug.Log("NetworkManager: " + (NetworkManager.Singleton != null));
        Debug.Log("UnityTransport: " + (NetworkManager.Singleton.GetComponent<UnityTransport>() != null));
        Debug.Log("RelayServer.IpV4: " + (allocation.RelayServer.IpV4 != null));
        Debug.Log("Port: " + allocation.RelayServer.Port);
        Debug.Log("AllocationIdBytes: " + (allocation.AllocationIdBytes != null));
        Debug.Log("Key: " + (allocation.Key != null));
        Debug.Log("ConnectionData: " + (allocation.ConnectionData != null));
        Debug.Log("--------------------");

    }
    public void StartRelayHostt()
    {
        try
        {
            /*
            Debug.Log("Entro a startrealyhost");
            //Allocation allocation = await RelayService.Instance.CreateAllocationAsync(cantJugadores - 1); // el host y 3 mas
            Debug.Log("Allocation created");
            //Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
            //string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log("Got join code: " + joinCode);
            //Debug.Log(joinCode);
            Debug.Log("NetworkManager: " + (NetworkManager.Singleton != null));
            Debug.Log("UnityTransport: " + (NetworkManager.Singleton.GetComponent<UnityTransport>() != null));
            Debug.Log("RelayServer.IpV4: " + (allocation.RelayServer.IpV4 != null));
            Debug.Log("Port: " + allocation.RelayServer.Port);
            Debug.Log("AllocationIdBytes: " + (allocation.AllocationIdBytes != null));
            Debug.Log("Key: " + (allocation.Key != null));
            Debug.Log("ConnectionData: " + (allocation.ConnectionData != null));
            Debug.Log("--------------------");
            
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
            );
            */
/*
Debug.Log("NetworkManager del starthost: " + (NetworkManager.Singleton != null));
Debug.Log("El host se va a inicar");
NetworkManager.Singleton.StartHost();
Debug.Log("El host se inicio");
}
catch (Exception e)
{
Debug.Log("Error: " + e.Message);
}
}*/

/*
public void StartRelayHost()
{
    Debug.Log("Iniciando Host");

    startRelayHostCompletionSource = new TaskCompletionSource<bool>();

    StartCoroutine(StartRelayHostCoroutine());
}
public IEnumerator StartRelayHostCoroutine()
{
    Debug.Log("Botón presionado.");
    // Ejecutar el método asincrónico y obtener la tarea que representa.
    var task = StartRelayHostAsync();
    // Esperar a que la tarea se complete.
    yield return new WaitUntil(() => task.IsCompleted);
    // Verificar si hubo alguna excepción en la tarea.
    if (task.Exception != null)
    {
        Debug.Log(task.Exception);
    }
    else
    {
        Debug.Log("Host iniciado.");
    }
}
private async Task StartRelayHostAsync()
{
    try
    {
        Debug.Log("Entro al try de StartRelayHostAsync");
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(cantJugadores - 1); // el host y 3 mas
        //Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
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
        Debug.Log("Terminó StartRelayHostAsync");
    }
    catch (RelayServiceException)
    {
        // Simplemente lanza la excepción de nuevo, así que será capturada y manejada en StartRelayHostCoroutine.
        throw;
    }
}
/*
private IEnumerator StartRelayHostCoroutine()
{
    try
    {
        //Aquí va tu código existente para iniciar el host...
        Debug.Log("Entro al try de StartRelayHost");
        //Allocation allocation = await RelayService.Instance.CreateAllocationAsync(cantJugadores - 1); // el host y 3 mas
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
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
        Debug.Log("Host iniciado.");
        //Después de que cada operación asíncrona se complete, debe ceder a Unity para continuar la ejecución del marco:
        //...
        yield return null; //Cede a Unity después de la operación asíncrona

        startRelayHostCompletionSource.SetResult(true);
    }
    catch (RelayServiceException e)
    {
        Debug.Log(e);
        startRelayHostCompletionSource.SetException(e);
    }
}*/
/*
public void CreatePlayer()
{
    try
    {
        // Espera a que se complete StartRelayHost
        //await startRelayHostCompletionSource.Task;
        if (PlayerNetwork.Instance == null)
        {
            Debug.LogError("PlayerNetwork.Instance is null");
            return;
        }
        //traer cantJugadores del canvas
        cantJugadores = int.Parse(cantidadJugadores.text);
        nombreHost = nombreHostinput.text;
        Debug.Log("nombre relay" + nombreHost);
        Debug.Log("color relay" + colorSeleccionado);

        Debug.Log("antes de cargar");
        PlayerNetwork.Instance.AgregarJugador(nombreHost, 100, cantJugadores, false, false, 2, 10, 10, 10, 10, 10, colorSeleccionado);
        PlayerNetwork.Instance.AgregarJugador("JugadorManual", 120, cantJugadores, false, false, 1, 8, 9, 7, 6, 8, "azul");
        //PlayerNetwork.Instance.AgregarJugador("manuel", 50, cantJugadores, false, false, 1, 8, 9, 7, 6, 8, "rojo");
        //PlayerNetwork.Instance.AgregarJugador("manolo", 76, cantJugadores, false, false, 1, 8, 9, 7, 6, 8, "naranja");
        Debug.Log("despues de cargar");
        PlayerNetwork.Instance.MostrarJugadores();
    }
    catch (Exception e)
    {
        Debug.Log(e);
    }
}
*/
/*
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

        Debug.Log("color pre cargar" + colorSeleccionado);

        //PlayerNetwork.Instance.CargarDatosJugador(1,nombreHost, 100, cantJugadores, false, true, 2, 10, 10, 10, 10, 10,colorSeleccionado);
        PlayerNetwork.Instance.AgregarJugador(nombreHost, 100, cantJugadores, false, false, 2, 10, 10, 10, 10, 10, colorSeleccionado);
        PlayerNetwork.Instance.AgregarJugador("JugadorManual", 120, cantJugadores, false, false, 1, 8, 9, 7, 6, 8, "azul");
        PlayerNetwork.Instance.AgregarJugador("manuel", 50, cantJugadores, false, false, 1, 8, 9, 7, 6, 8, "rojo");
        PlayerNetwork.Instance.AgregarJugador("manolo", 76, cantJugadores, false, false, 1, 8, 9, 7, 6, 8, "naranja");
        Debug.Log("despues de cargar");
        PlayerNetwork.Instance.MostrarJugadores();

    }
    catch (RelayServiceException e)
    {
        Debug.Log(e);
    }
}
*/
/*
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
*/