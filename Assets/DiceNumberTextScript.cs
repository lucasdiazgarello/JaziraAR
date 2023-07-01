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

    //public PlayerNetwork playerNetwork;

    public int TotalDiceNumber { get; private set; }


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

    // Update is called once per frame
    void Update()
    {

        // Chequea si ambos dados han dejado de rodar
        if (!diceScript1.IsDiceRolling && !diceScript2.IsDiceRolling)
        {
            /*
            if (playerNetwork != null)
            {
                Debug.Log("nombre host 2" + playerNetwork.GetNomJugador(0));
            }
            else
            {
                Debug.Log("PlayerNetwork instance is null");
            }*/
            //Debug.Log("nombre host 2" + playerNetwork.GetNomJugador(0));
            TotalDiceNumber = diceNumber1 + diceNumber2; // La suma de ambos dados
            Debug.Log("Dado1:  " + diceNumber1);
            Debug.Log("Dado2:  " + diceNumber2);
            Debug.Log("La suma:  " + TotalDiceNumber);
            text.text = TotalDiceNumber.ToString();
        }
    }
}
