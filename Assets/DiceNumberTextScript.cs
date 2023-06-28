using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Asegúrate de tener esta línea para utilizar TextMeshPro

public class DiceNumberTextScript : MonoBehaviour
{
    TextMeshProUGUI text;
    //public static int diceNumber;
    // Agregar referencias a ambos scripts de los dados
    public DiceScript diceScript1;
    public DiceScript diceScript2;
    public static int diceNumber1; // Para el primer dado
    public static int diceNumber2; // Para el segundo dado
    public static GameObject dice1; // Para el primer dado
    public static GameObject dice2; // Para el segundo dado
    //public int totalDiceNumber;
    public int TotalDiceNumber { get; private set; }


    // Use this for initialization
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        // Asigna los objetos de los dados
        dice1 = diceScript1.gameObject;
        dice2 = diceScript2.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Chequea si ambos dados han dejado de rodar
        if (!diceScript1.IsDiceRolling && !diceScript2.IsDiceRolling)
        {
            TotalDiceNumber = diceNumber1 + diceNumber2; // La suma de ambos dados
            Debug.Log("Dado1:  " + diceNumber1);
            Debug.Log("Dado2:  " + diceNumber2);
            Debug.Log("La suma:  " + TotalDiceNumber);
            text.text = TotalDiceNumber.ToString();
        }
    }
}

/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DiceNumberTextScript : MonoBehaviour
{

    Text text;
    public Text tf;
    public static int diceNumber;
    private string texto = "hola";

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        tf = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = diceNumber.ToString();
   

    texto=diceNumber.ToString();

        if (tf != null)
        {

            text.text = diceNumber.ToString();
            tf.text = diceNumber.ToString();
            Debug.Log("EL DADO ES " + diceNumber);
        
			
        }
    }
}
*/