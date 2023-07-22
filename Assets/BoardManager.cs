using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;
using static PlayerNetwork;

public class BoardManager : MonoBehaviour
{
    private GameObject parcela;
    private GameObject parcela2;
    public string recurso;
    private string recurso2;
    public Text MaderaCountText;
    public Text LadrilloCountText;
    public Text OvejaCountText;
    public Text PiedraCountText;
    public Text TrigoCountText;

    public IdentificadorParcela identificadorParcela;
    public ComprobarObjeto comprobarObjeto;
    public static BoardManager Instance; // Instancia Singleton

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Esto garantiza que el objeto no se destruirá al cargar una nueva escena
        }
        else
        {
            Destroy(gameObject); // Si ya hay una instancia, destruye esta
        }
    }
    // Llamar a esta función cuando se tira el dado.
    void Start()
    {
        // Obtén el ID del jugador desde donde lo tengas almacenado.
        // En este ejemplo, simplemente lo he establecido como 1.
        int currentPlayerID = PlayerPrefs.GetInt("jugadorId");
        Instance.UpdateResourceTexts(currentPlayerID);
        // Actualiza los textos de recursos para el jugador al inicio.
        //UpdateResourceTexts(jugadorId);
    }
    public void ManejoParcelas(int diceNumber)
    {
        Debug.Log("Entre a manejo parcelas");
        // Obtener las parcelas correspondientes al número del dado.
        string parcelName = "Parcela " + diceNumber.ToString();

        switch (parcelName)
        {
            case "Parcela 2":
                Debug.Log("Busque parcela 2");
                parcela = GameObject.Find("Parcela 2 Trigo");
                recurso = "Trigo";
                break;
            case "Parcela 3":
                Debug.Log("Busque parcela 3");
                parcela = GameObject.Find("Parcela 3 Trigo");
                parcela2 = GameObject.Find("Parcela 3 Madera");
                break;
            case "Parcela 4":
                Debug.Log("Busque parcela 4");
                parcela = GameObject.Find("Parcela 4 Ladrillo");
                parcela2 = GameObject.Find("Parcela 4 Madera");
                break;
            case "Parcela 5":
                Debug.Log("Busque parcela 5");
                parcela = GameObject.Find("Parcela 5 Ladrillo");
                parcela2 = GameObject.Find("Parcela 5 Piedra");
                recurso = "Piedra";
                break;
            case "Parcela 6":
                Debug.Log("Busque parcela 6");
                parcela = GameObject.Find("Parcela 6 Piedra");
                parcela2 = GameObject.Find("Parcela 6 Madera");
                break;
            case "Parcela 8":
                Debug.Log("Busque parcela 8");
                parcela = GameObject.Find("Parcela 8 Trigo");
                parcela2 = GameObject.Find("Parcela 8 Ladrillo");
                break;
            case "Parcela 9":
                Debug.Log("Busque parcela 9");
                parcela = GameObject.Find("Parcela 9 Trigo");
                parcela2 = GameObject.Find("Parcela 9 Oveja");
                break;
            case "Parcela 10":
                Debug.Log("Busque parcela 10");
                parcela = GameObject.Find("Parcela 10 Oveja");
                parcela2 = GameObject.Find("Parcela 10 Oveja");
                break;
            case "Parcela 11":
                Debug.Log("Busque parcela 11");
                parcela = GameObject.Find("Parcela 11 Trigo");
                parcela2 = GameObject.Find("Parcela 11 Madera");
                break;
            case "Parcela 12":
                Debug.Log("Busque parcela 12");
                parcela = GameObject.Find("Parcela 12 Oveja");
                break;

        }
        parcela = GameObject.Find("Parcela 5 Piedra");
        recurso = "Piedra";

        //Debug.Log("parcela 5: " + parcela);
        identificadorParcela = parcela.GetComponent<IdentificadorParcela>();
        //Debug.Log("identificadorParcela: " + identificadorParcela);
        List<Collider> collidersParcela = identificadorParcela.GetCollidersParcela(parcela.name);
        //Debug.Log("collidersParcela count: " + collidersParcela.Count); // Ver el tamaño de la lista

        if (collidersParcela.Count > 0)
        {
            //Debug.Log("First item in collidersParcela: " + collidersParcela[0]); // Ver el primer elemento
            comprobarObjeto = collidersParcela[0].gameObject.GetComponent<ComprobarObjeto>();
            Debug.Log("comprobarObjeto de la [0] es " + comprobarObjeto);
        }
        foreach (var empty in collidersParcela)
        {
           // Debug.Log("entre al foreach");
            //Debug.Log("Empty GameObject: " + empty.gameObject.name);
            // Obtener el script ComprobarObjeto del objeto.
            comprobarObjeto = empty.gameObject.GetComponent<ComprobarObjeto>();
            if (comprobarObjeto == null)
            {
                Debug.LogError("No se pudo obtener el componente ComprobarObjeto de " + empty.gameObject.name);
            }
            Debug.Log("comprobarObjeto es " + comprobarObjeto);
            // Si el script existe, invocar la función DarTipo().
            if (comprobarObjeto != null)
            {
                Debug.Log("comprobarObjeto no es null");
                TipoObjeto tipo = comprobarObjeto.tipoObjeto; // Aquí utilizas la variable tipoObjeto de tu instancia comprobarObjeto
                Debug.Log("el tipo es " + tipo);
                switch (tipo)
                {
                    case TipoObjeto.Ninguno:  // Aquí se hace uso del tipo enumerado TipoObjeto
                        Debug.Log("Ninguno");
                        break;
                    case TipoObjeto.Base:    // Aquí se hace uso del tipo enumerado TipoObjeto
                        Debug.Log("Sumar a la Base 1 de " + recurso);
                        int currentPlayerID = PlayerPrefs.GetInt("jugadorId");
                        //int currentPlayerID = TurnManager.Instance.CurrentPlayerID;
                        Debug.Log("CurrentPlayerID es " + currentPlayerID);
                        PlayerNetwork.Instance.AumentarRecursos(currentPlayerID, recurso, 1);
                        Instance.UpdateResourceTexts(currentPlayerID);
                        Debug.Log("ya sumo recurso "+ recurso);
                        break;
                    case TipoObjeto.Pueblo:  // Aquí se hace uso del tipo enumerado TipoObjeto
                        Debug.Log("Pueblo");
                        //PlayerNetwork.Instance.AumentarRecursos(idJugador, recurso, 2);
                        Debug.Log("sumo 2 " + recurso);
                        break;
                }
            }
            else
            {
                Debug.LogError("El objeto " + empty.name + " no tiene un script de ComprobarObjeto.");
            }
        }

        if (parcela2 == null)
        {

        }

    }

    public void UpdateResourceTexts(int jugadorId)
    {
        Debug.Log("Entre a UpdateResourceTexts");
        DatosJugador datosJugador = default;

        // Itera sobre los elementos de playerData para encontrar los datos del jugador
        for (int i = 0; i < PlayerNetwork.Instance.playerData.Count; i++)
        {
            if (PlayerNetwork.Instance.playerData[i].jugadorId == jugadorId)
            {
                datosJugador = PlayerNetwork.Instance.playerData[i];
                break;
            }
        }

        if (datosJugador.jugadorId == 0)  // Suponiendo que 0 no es un ID de jugador válido
        {
            Debug.LogError("Jugador con ID " + jugadorId + " no encontrado.");
            return;
        }

        // Actualiza los textos de los recursos
        MaderaCountText.text = datosJugador.maderaCount.ToString();
        LadrilloCountText.text = datosJugador.ladrilloCount.ToString();
        OvejaCountText.text = datosJugador.ovejaCount.ToString();
        PiedraCountText.text = datosJugador.piedraCount.ToString();
        TrigoCountText.text = datosJugador.trigoCount.ToString();
    }

    /*
    GameObject[] parcelArray = GameObject.FindGameObjectsWithTag(parcelName);
    GameObject[] parcelArray = GameObject.Find("Parcela  ")GameObjectsWithTag(parcelName);

    foreach (var parcel in parcelArray)
    {
        ParcelScript parcelScript = parcel.GetComponent<ParcelScript>();

        foreach (var corner in parcelScript.cornerList)
        {
            CornerScript cornerScript = corner.GetComponent<CornerScript>();

            if (cornerScript.hasBase)
            {
                cornerScript.IncreaseResource(parcelScript.resourceType);
            }
        }
    }*/

}