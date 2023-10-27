using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class DiceNumberTextScript : MonoBehaviour
{
    public static DiceNumberTextScript Instance { get; private set; }
    //TextMeshProUGUI text;
    public Text resultadoDado;
    //public static int diceNumber;

    public DiceScript diceScript1;
    public DiceScript diceScript2;

    public static int diceNumber1 = 0; // Para el primer dado
    public static int diceNumber2 = 0; // Para el segundo dado
    public static GameObject dice1; // Para el primer dado
    public static GameObject dice2; // Para el segundo dado
    //private int previousTotalDiceNumber = 0;
    public static int totalDiceNumber=0;
    //public static int totalDados = 0;

    //public PlayerNetwork playerNetwork;

    public int TotalDiceNumber { get; private set; }

    public int randomDiceNumber;
    //private int contador = 0;

    void Start()
    {
        //playernetwork = GameObject.FindObjectOfType<PlayerNetwork>();
        //text = GetComponent<TextMeshProUGUI>();
        // Asigna los objetos de los dados
        dice1 = diceScript1.gameObject;
        dice2 = diceScript2.gameObject;
        //playerNetwork = PlayerNetwork.Instance;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /*void Update()
    {
        bool isAnyDiceRolling = diceScript1.IsDiceRolling || diceScript2.IsDiceRolling;

        if (!isAnyDiceRolling)
        {
            if (diceScript1.Dice1HasJustStopped || diceScript2.Dice2HasJustStopped)
            {
                totalDiceNumber = diceNumber1 + diceNumber2;
                TotalDiceNumber = totalDiceNumber;
                resultadoDado.text = totalDiceNumber.ToString();


                diceScript1.Dice1HasJustStopped = false;
                diceScript2.Dice2HasJustStopped = false;
            }
        }
    }*/

    /*void Update()
    {
        if (!diceScript1.IsDiceRolling && !diceScript2.IsDiceRolling)
        {           
            totalDiceNumber = diceNumber1 + diceNumber2;
            if (totalDiceNumber != previousTotalDiceNumber)
            {
                //Debug.Log("Dado1:  " + diceNumber1);
                //Debug.Log("Dado2:  " + diceNumber2);
                //Debug.Log("La suma:  " + totalDiceNumber);
                previousTotalDiceNumber = totalDiceNumber;
                totalDados = totalDiceNumber;
                Debug.Log("TotalDados;  " + totalDados);
                //contador++;
                //Debug.Log("Contador: " + contador);

            }
            text.text = totalDiceNumber.ToString();
            //Debug.Log("La suma:  " + totalDiceNumber);            
            /*if(contador >= 10)
            {
                Debug.Log("Llego a 10");
                TotalDiceNumber = totalDiceNumber;
                Debug.Log("TotalDiceNumber:  " + TotalDiceNumber);
                contador = 0;
            }

    }*/
    public int DarResultadoRandom()
    {
        Debug.Log("Entre a dar resultado random");
        randomDiceNumber = Random.Range(2, 13); // Genera un número aleatorio del 1 al 12
        Debug.Log("Resultado random es " + randomDiceNumber.ToString());
        return randomDiceNumber;
    }
    public void ResultadoDadoEnPantalla(int resu)
    {
        Instance.resultadoDado.text = resu.ToString();
    }
}

