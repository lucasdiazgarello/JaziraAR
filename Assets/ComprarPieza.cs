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

    // referencia al bot�n de comprar camino
    public Button comprarCaminoButton;
    public Button comprarCasaButton;

    public DiceCheckZoneScript diceCheckZoneScript;
    public DiceNumberTextScript diceNumberTextScript;
    public ColocarPieza colocarPiezaScript;
    public IdentificadorParcela identificadorParcelaScript;

    // Start is called before the first frame update
    void Start()
    {
        // inicializar contadores de recursos
        maderaCount = 0;
        ladrilloCount = 0;
        ovejaCount = 0;
        piedraCount = 0;
        trigoCount = 0;

        // actualizar texto en la interfaz de usuario
        UpdateResourceCount();

        // inicializar estado del bot�n de comprar camino
        UpdateComprarCaminoButton();
        // inicializar estado del bot�n de comprar casa
        UpdateComprarCasaButton();
    }

    // m�todo para incrementar los recursos cuando se lanza el dado
    public void IncrementarRecursos(int numeroDado)
    {
        identificadorParcelaScript.IncrementarRecursos(numeroDado, ref maderaCount, ref ladrilloCount, ref ovejaCount, ref piedraCount, ref trigoCount);

        // Actualizar el texto en la interfaz de usuario
        UpdateResourceCount();

        // Actualizar estado del bot�n
        UpdateComprarCaminoButton();
    }

    // m�todo para actualizar el texto de los contadores de recursos
    void UpdateResourceCount()
    {
        maderaCountText.text = maderaCount.ToString();
        ladrilloCountText.text = ladrilloCount.ToString();
        ovejaCountText.text = ovejaCount.ToString();
        piedraCountText.text = piedraCount.ToString();
        trigoCountText.text = trigoCount.ToString();
    }

    // m�todo para actualizar el estado del bot�n de comprar camino
    void UpdateComprarCaminoButton()
    {
        // el bot�n solo est� activo si el jugador tiene al menos 1 madera y 1 ladrillo
        comprarCaminoButton.interactable = (maderaCount >= 1 && ladrilloCount >= 1);
    }

    void UpdateComprarCasaButton()
    {
        // El bot�n solo est� activo si el jugador tiene al menos 1 madera, 1 ladrillo, 1 trigo y 1 oveja
        comprarCasaButton.interactable = (maderaCount >= 1 && ladrilloCount >= 1 && trigoCount >= 1 && ovejaCount >= 1);
    }

    // m�todo para comprar un camino
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

            // actualizar estado del bot�n
            UpdateComprarCaminoButton();

            // aqu� va el c�digo para permitir al jugador colocar un camino en el tablero
        }
    }

    public void ComprarCasa()
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

            // Actualizar estado del bot�n
            UpdateComprarCasaButton();

            // Aqu� va el c�digo para permitir al jugador colocar una casa en el tablero

            // Llamar a ColocarCasa en el script ColocarPieza para instanciar la casa
            //ESTO TIRA ERROR PORQUE NO EXISTE METODO COLOCAR CASA
            //colocarPiezaScript.ColocarCasa();
        }
    }
}
