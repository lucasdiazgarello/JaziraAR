using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Asegúrate de tener esta línea para utilizar TextMeshPro

public class DiceNumberTextScript : MonoBehaviour
{
    public static DiceNumberTextScript Instance; 
    //public static DiceNumberTextScript Instance { get; private set; }

    TextMeshProUGUI text;
    //public static int diceNumber;
    // Agregar referencias a ambos scripts de los dados
    public DiceScript diceScript1;
    public DiceScript diceScript2;

    public static int diceNumber1 = 0; // Para el primer dado
    public static int diceNumber2 = 0; // Para el segundo dado
    public static GameObject dice1; // Para el primer dado
    public static GameObject dice2; // Para el segundo dado
    private int previousTotalDiceNumber = 0;
    public static int totalDiceNumber=0;

    //public PlayerNetwork playerNetwork;

    public int TotalDiceNumber { get; private set; }

    //public int randomDiceNumber;

    // Use this for initialization
    void Start()
    {
        //playernetwork = GameObject.FindObjectOfType<PlayerNetwork>();
        text = GetComponent<TextMeshProUGUI>();
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
    // Update is called once per frame
    void Update()
    {
        if (!diceScript1.IsDiceRolling && !diceScript2.IsDiceRolling)
        {
            
            totalDiceNumber = diceNumber1 + diceNumber2;
            if (totalDiceNumber != previousTotalDiceNumber)
            {
                //Debug.Log("Dado1:  " + diceNumber1);
               // Debug.Log("Dado2:  " + diceNumber2);
                //Debug.Log("La suma:  " + totalDiceNumber);
                previousTotalDiceNumber = totalDiceNumber;
            }
            text.text = totalDiceNumber.ToString();
            //Debug.Log("La suma:  " + totalDiceNumber);

        }
    }
    /*public void DarResultadoRandom()
    {
        Debug.Log("Entre a dar resultado random");
        randomDiceNumber = Random.Range(2, 13); // Genera un número aleatorio del 1 al 12
        text.text = randomDiceNumber.ToString();
        Debug.Log("Resultado random es " + randomDiceNumber.ToString());
    }*/
}

