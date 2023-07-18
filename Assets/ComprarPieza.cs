using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComprarPieza : MonoBehaviour
{
    // referencias a los textos que muestran el contador de recursos
    public TextMeshProUGUI maderaCountText;
    public TextMeshProUGUI ladrilloCountText;
    public TextMeshProUGUI ovejaCountText;
    public TextMeshProUGUI piedraCountText;
    public TextMeshProUGUI trigoCountText;

    // variables que guardan la cantidad de cada recurso
    private int maderaCount;
    private int ladrilloCount;
    private int ovejaCount;
    private int piedraCount;
    private int trigoCount;

    // referencia al botón de comprar camino
    public Button comprarCaminoButton;
    public Button comprarBaseButton;
    public Button comprarPuebloButton;

    //public DiceCheckZoneScript diceCheckZoneScript;
    //public DiceNumberTextScript diceNumberTextScript;
    public ColocarPieza colocarPiezaScript;
    public IdentificadorParcela identificadorParcelaScript;
    //public DiceNumberTextScript diceNumberTextScript;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // método para incrementar los recursos cuando se lanza el dado
    public void IncrementarRecursos()
    {
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
    }

    // método para actualizar el texto de los contadores de recursos
    void UpdateResourceCount()
    {
        maderaCountText.text = maderaCount.ToString();
        ladrilloCountText.text = ladrilloCount.ToString();
        ovejaCountText.text = ovejaCount.ToString();
        piedraCountText.text = piedraCount.ToString();
        trigoCountText.text = trigoCount.ToString();
    }

    // método para actualizar el estado del botón de comprar camino
    void UpdateComprarCaminoButton()
    {
        // el botón solo está activo si el jugador tiene al menos 1 madera y 1 ladrillo
        comprarCaminoButton.interactable = (maderaCount >= 1 && ladrilloCount >= 1);
    }

    void UpdateComprarBaseButton()
    {
        // El botón solo está activo si el jugador tiene al menos 1 madera, 1 ladrillo, 1 trigo y 1 oveja
        comprarBaseButton.interactable = (maderaCount >= 1 && ladrilloCount >= 1 && trigoCount >= 1 && ovejaCount >= 1);
    }
    void UpdateComprarPuebloButton()
    {
        // El botón solo está activo si el jugador tiene al menos 1 madera, 1 ladrillo, 1 trigo y 1 oveja
        comprarPuebloButton.interactable = (piedraCount >= 2 && trigoCount >= 3);
    }

    // método para comprar un camino
    public void ComprarCamino()
    {
        // solo proceder si el jugador tiene suficientes recursos
        if (maderaCount >= 1 && ladrilloCount >= 1)
        {
            // restar recursos
            maderaCount -= 1;
            ladrilloCount -= 1;

            // actualizar el texto en la interfaz de usuario
            UpdateResourceCount();

            // actualizar estado del botón
            UpdateComprarCaminoButton();

            // aquí va el código para permitir al jugador colocar un camino en el tablero
            // Llamar a ColocarCasa en el script ColocarPieza para instanciar la casa
            colocarPiezaScript.ColocarCamino();

            // Llamar a ActivarColocacion en el script ColocarPieza para permitir al jugador colocar un camino
            colocarPiezaScript.ActivarColocacion(ColocarPieza.TipoObjeto.Camino);
        }
    }

    public void ComprarBase()
    {
        // Solo proceder si el jugador tiene suficientes recursos
        if (maderaCount >= 1 && ladrilloCount >= 1 && trigoCount >= 1 && ovejaCount >= 1)
        {
            // Restar recursos
            maderaCount -= 1;
            ladrilloCount -= 1;
            trigoCount -= 1;
            ovejaCount -= 1;

            // Actualizar el texto en la interfaz de usuario
            UpdateResourceCount();

            // Actualizar estado del botón
            UpdateComprarBaseButton();

            // Aquí va el código para permitir al jugador colocar una casa en el tablero

            // Llamar a ColocarCasa en el script ColocarPieza para instanciar la casa
            colocarPiezaScript.ColocarBase();
            // Llamar a ActivarColocacion en el script ColocarPieza para permitir al jugador colocar una base
            colocarPiezaScript.ActivarColocacion(ColocarPieza.TipoObjeto.Base);
        }
    }
    public void ComprarPueblo()
    {
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
            colocarPiezaScript.ActivarColocacion(ColocarPieza.TipoObjeto.Pueblo);
        }
    }
}
