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
    public Button comprarCasaButton;

    public DiceCheckZoneScript diceCheckZoneScript;

    public DiceNumberTextScript diceNumberTextScript;

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

        // inicializar estado del botón de comprar camino
        UpdateComprarCaminoButton();
        // inicializar estado del botón de comprar casa
        UpdateComprarCasaButton();

    }

    // método para incrementar los recursos cuando se lanza el dado
    /*public void IncrementarRecursos(int numeroDado)
    {
        int resultadoDado = DiceNumberTextScript.diceNumber;
        // Obtener el tipo de recurso correspondiente al número del dado
        string tipoRecurso = ObtenerTipoRecurso(numeroDado);

        // Incrementar la cantidad del recurso correspondiente
        switch (tipoRecurso)
        {
            case "Madera":
                maderaCount += 1;
                break;
            case "Ladrillo":
                ladrilloCount += 1;
                break;
            case "Oveja":
                ovejaCount += 1;
                break;
            case "Piedra":
                piedraCount += 1;
                break;
            case "Trigo":
                trigoCount += 1;
                break;
        }

        // Verificar si el jugador tiene una casa en la esquina correspondiente a la parcela del número del dado
        bool tieneCasa = VerificarCasaEnEsquina(numeroDado);

        // Incrementar el recurso correspondiente si tiene una casa
        if (tieneCasa)
        {
            switch (tipoRecurso)
            {
                case "Madera":
                    maderaCount += 1;
                    break;
                case "Ladrillo":
                    ladrilloCount += 1;
                    break;
                case "Oveja":
                    ovejaCount += 1;
                    break;
                case "Piedra":
                    piedraCount += 1;
                    break;
                case "Trigo":
                    trigoCount += 1;
                    break;
            }
        }

        // Actualizar el texto en la interfaz de usuario
        UpdateResourceCount();

        // Actualizar estado del botón
        UpdateComprarCaminoButton();
    }*/

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
    void UpdateComprarCasaButton()
    {
        // El botón solo está activo si el jugador tiene al menos 1 madera, 1 ladrillo, 1 trigo y 1 oveja
        comprarCasaButton.interactable = (maderaCount >= 1 && ladrilloCount >= 1 && trigoCount >= 1 && ovejaCount >= 1);
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

            // Actualizar estado del botón
            UpdateComprarCasaButton();

            // Aquí va el código para permitir al jugador colocar una casa en el tablero
        }
    }
}
