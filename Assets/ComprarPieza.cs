using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerNetwork;

public class ComprarPieza : MonoBehaviour
{
    // referencias a los textos que muestran el contador de recursos
    //public Text maderaCountText;
    //public Text ladrilloCountText;
    //public Text ovejaCountText;
    //public Text piedraCountText;
    //public Text trigoCountText;
    private bool esperandoColocarBase = false;
    private bool esperandoColocarCamino = false;
    private bool esperandoColocarPueblo = false;
    //private DatosJugador jugador;

    // variables que guardan la cantidad de cada recurso
    /*private int maderaCount;
    private int ladrilloCount;
    private int ovejaCount;
    private int piedraCount;
    private int trigoCount;*/

    // referencia al botón de comprar camino
    public Button comprarCaminoButton;
    public Button comprarBaseButton;
    public Button comprarPuebloButton;

    //public DiceCheckZoneScript diceCheckZoneScript;
    //public DiceNumberTextScript diceNumberTextScript;
    public ColocarPieza colocarPieza;
    public LayerMask myLayerMask;
    //public IdentificadorParcela identificadorParcelaScript;
    //public DiceNumberTextScript diceNumberTextScript;

    // Start is called before the first frame update
    void Start()
    {
        /*
        // Desactivar los contadores de recursos al inicio
        maderaCountText.gameObject.SetActive(false);
        ladrilloCountText.gameObject.SetActive(false);
        ovejaCountText.gameObject.SetActive(false);
        piedraCountText.gameObject.SetActive(false);
        trigoCountText.gameObject.SetActive(false);
        //Desactivar los botones de comprar piezas al inicio MAS ADELANTE poner que se activen recien cuando el jugador tenga el minimo de recursos para poder comprar
        //comprarBaseButton.gameObject.SetActive(false); 
        //comprarCaminoButton.gameObject.SetActive(false);

        // inicializar contadores de recursos
        maderaCount = 10;
        ladrilloCount = 10;
        ovejaCount = 10;
        piedraCount = 10;
        trigoCount = 10;

        // actualizar texto en la interfaz de usuario
        UpdateResourceCount();

        // inicializar estado del botón de comprar camino
        UpdateComprarCaminoButton();
        // inicializar estado del botón de comprar base
        UpdateComprarBaseButton();
        // inicializar estado del botón de comprar base
        UpdateComprarPuebloButton();
        */
    }
    private void Update()
    {
        if (esperandoColocarBase && Input.touchCount > 0)
        {
            colocarPieza.AllowPlace();
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
            {
                Debug.Log("Antes EjecutarColocarBase");
                colocarPieza.EjecutarColocarBase(hit);
                Debug.Log("Despues EjecutarColocarBase");

                esperandoColocarBase = false;

                ARCursor arCursor = FindObjectOfType<ARCursor>();
                if (arCursor != null)
                {
                    arCursor.ActivatePlacementMode();
                }
            }
        }
        else if (esperandoColocarCamino && Input.touchCount > 0)
        {
            colocarPieza.AllowPlace();
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
            {
                Debug.Log("Antes EjecutarColocarCamino");
                colocarPieza.EjecutarColocarCamino(hit);
                Debug.Log("Despues EjecutarColocarCamino");

                esperandoColocarCamino = false;

                ARCursor arCursor = FindObjectOfType<ARCursor>();
                if (arCursor != null)
                {
                    arCursor.ActivatePlacementMode();
                }
            }
        }
        else if (esperandoColocarPueblo && Input.touchCount > 0)
        {
            colocarPieza.AllowPlace();
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
            {
                Debug.Log("Antes EjecutarColocarPueblo");
                colocarPieza.EjecutarColocarPueblo(hit);
                Debug.Log("Despues EjecutarColocarPueblo");

                esperandoColocarPueblo = false;

                ARCursor arCursor = FindObjectOfType<ARCursor>();
                if (arCursor != null)
                {
                    arCursor.ActivatePlacementMode();
                }
            }
        }
    }

    // método para comprar un camino
    public void ComprarCamino()
    {
        Debug.Log("Entre a ComprarCamino");
        int id = PlayerPrefs.GetInt("jugadorId");
        //PlayerNetwork.Instance.ImprimirPlayerIDs();
        //Debug.Log("ID CB: " + id);
        DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        //Debug.Log("El JUGADOR CB es: ");
        PlayerNetwork.Instance.ImprimirJugador(jugador);

        if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1 )
        {
            BoardManager.Instance.UpdateResourcesCamino(jugador);
            esperandoColocarCamino = true;
            //Debug.Log("Jugador " + jugador.jugadorId + " ahora tiene " + jugador.maderaCount + " maderas ");
            //Debug.Log("Jugador " + jugador.jugadorId + " ahora tiene " + jugador.ladrilloCount + " ladrillos ");

            //BoardManager.Instance.UpdateResourceTexts(id);

            //Debug.Log("Imprimir jugador post ");
            //PlayerNetwork.Instance.ImprimirJugador(jugador);
            Debug.Log("Imprimir jugador por ID post ");
            PlayerNetwork.Instance.ImprimirJugadorPorId(id);

            /*PlayerNetwork.Instance.playerData[id] = jugador;
            Debug.Log("Imprimir jugador 2");
            PlayerNetwork.Instance.ImprimirJugadorPorId(id);
            */
        }
    }

    public void ComprarBase()
    {
        Debug.Log("Entre a ComprarBase");
        int id = PlayerPrefs.GetInt("jugadorId");
        PlayerNetwork.Instance.ImprimirPlayerIDs();
        Debug.Log("ID CB: " + id);
        DatosJugador jugador = PlayerNetwork.Instance.GetPlayerData(id);
        Debug.Log("El JUGADOR CB es: ");
        PlayerNetwork.Instance.ImprimirJugador(jugador);

        if (jugador.maderaCount >= 1 && jugador.ladrilloCount >= 1 && jugador.trigoCount >= 1 && jugador.ovejaCount >= 1)
        {
            BoardManager.Instance.UpdateResourcesBase(jugador);
            /*jugador.maderaCount -= 1;
            jugador.ladrilloCount -= 1;
            jugador.trigoCount -= 1;
            jugador.ovejaCount -= 1;
            */
            esperandoColocarBase = true;
            //Debug.Log("Jugador " + jugador.jugadorId + " ahora tiene " + jugador.maderaCount + " maderas ");
            //Debug.Log("Jugador " + jugador.jugadorId + " ahora tiene " + jugador.ladrilloCount + " ladrillos ");
            //Debug.Log("Jugador " + jugador.jugadorId + " ahora tiene " + jugador.trigoCount + " trigos ");
            //Debug.Log("Jugador " + jugador.jugadorId + " ahora tiene " + jugador.ovejaCount + " ovejas ");

            //Debug.Log("Imprimir jugador post ");
            //PlayerNetwork.Instance.ImprimirJugador(jugador);
            Debug.Log("Imprimir jugador por ID post ");
            PlayerNetwork.Instance.ImprimirJugadorPorId(id);
            
            /*PlayerNetwork.Instance.playerData[id] = jugador;
            Debug.Log("Imprimir jugador 2");
            PlayerNetwork.Instance.ImprimirJugadorPorId(id);
            */
        }
    }

 
    public void ComprarPueblo()
    {
        /*
        // Solo proceder si el jugador tiene suficientes recursos
        if (trigoCount >= 3 && piedraCount >= 2)
        {
            // Restar recursos
            trigoCount -= 3;
            piedraCount -= 2;

            // Actualizar el texto en la interfaz de usuario
            UpdateResourceCount();

            // Actualizar estado del botón
            UpdateComprarBaseButton();

            // Aquí va el código para permitir al jugador colocar una casa en el tablero
            // Llamar a ColocarCasa en el script ColocarPieza para instanciar el pueblo
            colocarPiezaScript.ColocarPueblo();
            // Llamar a ActivarColocacion en el script ColocarPieza para permitir al jugador colocar un pueblo
            colocarPiezaScript.ActivarColocacion(TipoObjeto.Pueblo);
        }
        */
    }
    // método para incrementar los recursos cuando se lanza el dado
    public void IncrementarRecursos()
    {
        /*
        int numeroDado = DiceNumberTextScript.totalDiceNumber;
        Debug.Log("el valor para incrementar es: " + numeroDado);
        // Activar los contadores de recursos al lanzar el dado por primera vez
        maderaCountText.gameObject.SetActive(true);
        ladrilloCountText.gameObject.SetActive(true);
        ovejaCountText.gameObject.SetActive(true);
        piedraCountText.gameObject.SetActive(true);
        trigoCountText.gameObject.SetActive(true);
        identificadorParcelaScript.IncrementarRecursos(numeroDado, ref maderaCount, ref ladrilloCount, ref ovejaCount, ref piedraCount, ref trigoCount);

        // Actualizar el texto en la interfaz de usuario
        UpdateResourceCount();

        // Actualizar estado de los  botones
        UpdateComprarCaminoButton();
        UpdateComprarBaseButton();
        UpdateComprarPuebloButton();
        */
    }

    // método para actualizar el texto de los contadores de recursos
    void UpdateResourceCount()
    {
        /*maderaCountText.text = maderaCount.ToString();
        ladrilloCountText.text = ladrilloCount.ToString();
        ovejaCountText.text = ovejaCount.ToString();
        piedraCountText.text = piedraCount.ToString();
        trigoCountText.text = trigoCount.ToString();*/
    }

    // método para actualizar el estado del botón de comprar camino
    void UpdateComprarCaminoButton()
    {
        // el botón solo está activo si el jugador tiene al menos 1 madera y 1 ladrillo
        //comprarCaminoButton.interactable = (maderaCount >= 1 && ladrilloCount >= 1);
    }

    void UpdateComprarBaseButton()
    {
        // El botón solo está activo si el jugador tiene al menos 1 madera, 1 ladrillo, 1 trigo y 1 oveja
        //comprarBaseButton.interactable = (maderaCount >= 1 && ladrilloCount >= 1 && trigoCount >= 1 && ovejaCount >= 1);
    }
    void UpdateComprarPuebloButton()
    {
        // El botón solo está activo si el jugador tiene al menos 1 madera, 1 ladrillo, 1 trigo y 1 oveja
        //comprarPuebloButton.interactable = (piedraCount >= 2 && trigoCount >= 3);
    }

}
