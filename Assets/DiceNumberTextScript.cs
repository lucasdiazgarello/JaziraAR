﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Asegúrate de tener esta línea para utilizar TextMeshPro

public class DiceNumberTextScript : MonoBehaviour
{
    TextMeshProUGUI text;
    public static int diceNumber;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = diceNumber.ToString();
        //Debug.Log("EL DADO ES " + diceNumber);
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