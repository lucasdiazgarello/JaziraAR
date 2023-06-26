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

    // Use this for initialization
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Chequea si ambos dados han dejado de rodar
        if (!diceScript1.IsDiceRolling && !diceScript2.IsDiceRolling)
        {
            int totalDiceNumber = diceNumber1 + diceNumber2; // La suma de ambos dados
            text.text = totalDiceNumber.ToString();
            //Debug.Log("EL DADO ES " + diceNumber);
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