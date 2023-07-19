using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private GameObject parcela;
    private GameObject parcela2;
    public string recurso;
    private string recurso2;

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
                parcela = GameObject.Find("Parcela 3 Ladrillo");
                parcela2 = GameObject.Find("Parcela 3 Piedra");
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
        Debug.Log("parcela 5: " + parcela);
        identificadorParcela = parcela.GetComponent<IdentificadorParcela>();
        Debug.Log("identificadorParcela: " + identificadorParcela);
        List<Collider> collidersParcela = identificadorParcela.GetCollidersParcela(parcela.name);
        Debug.Log("collidersParcela count: " + collidersParcela.Count); // Ver el tamaño de la lista

        if (collidersParcela.Count > 0)
        {
            Debug.Log("First item in collidersParcela: " + collidersParcela[0]); // Ver el primer elemento
            comprobarObjeto = collidersParcela[0].gameObject.GetComponent<ComprobarObjeto>();
            Debug.Log("comprobarObjeto de la [0] es " + comprobarObjeto);
        }
        foreach (var empty in collidersParcela)
        {
            Debug.Log("entre al foreach");
            Debug.Log("Empty GameObject: " + empty.gameObject.name);
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
                        Debug.Log("Base");
                        //llamar a la funcion de playernetwork que AumenteRecurso(idJugador,recurso,1)
                        Debug.Log("sumo 1 " + recurso);
                        break;
                    case TipoObjeto.Pueblo:  // Aquí se hace uso del tipo enumerado TipoObjeto
                        Debug.Log("Pueblo");
                        Debug.Log("sumo 2 " + recurso);
                        break;
                }
            }
            else
            {
                Debug.LogError("El objeto " + empty.name + " no tiene un script de ComprobarObjeto.");
            }
            // Obtener el script ColocarPieza del objeto.
            //ColocarPieza colocarPieza = empty.gameObject.GetComponent<ColocarPieza>();
            //Debug.Log("empty es " + colocarPieza);
            // Si el script existe, invocar la función DarTipo().
            /*if (colocarPieza != null)
            {
                Debug.Log("colocarPieza no es null");
                int tipo = colocarPieza.DarTipo();
                Debug.Log("el tipo es " + tipo);
                switch (tipo)
                {
                    case 0:
                        Debug.Log("Ninguno");
                        break;
                    case 2:
                        Debug.Log("Base");
                        Debug.Log("sumo 1 " + recurso);
                        break;
                    case 3:
                        Debug.Log("Pueblo");
                        Debug.Log("sumo 2 " + recurso);
                        break;
                }
            }
            else
            {
                Debug.LogError("El objeto " + empty.name + " no tiene un script de ColocarPieza.");
            }*/
        }

        if (parcela2 == null)
        {

        }

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