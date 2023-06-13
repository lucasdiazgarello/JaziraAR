﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour
{
    public DiceScript diceScript; // Referencia al script del dado

    Vector3 diceVelocity;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (diceScript != null)
        {
            diceVelocity = diceScript.diceVelocity;
        }
    }

    /*Vector3 diceVelocity;

    // Update is called once per frame
    void FixedUpdate()
    {
        diceVelocity = DiceScript.diceVelocity;
    }*/

    void OnTriggerStay(Collider col)
    {
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
        {
            switch (col.gameObject.name)
            {
                case "Side1":
                    //DiceNumberTextScript.diceNumber = 1;
                    DiceNumberTextScript.diceNumber = 6;
                    //Debug.Log('6');	
                    break;
                case "Side2":
                    //DiceNumberTextScript.diceNumber = 2;
                    DiceNumberTextScript.diceNumber = 5;
                    //Debug.Log('5');
                    break;
                case "Side3":
                    //DiceNumberTextScript.diceNumber = 3;
                    DiceNumberTextScript.diceNumber = 4;
                    //Debug.Log('4');
                    break;
                case "Side4":
                    //DiceNumberTextScript.diceNumber = 4;
                    DiceNumberTextScript.diceNumber = 3;
                    //Debug.Log('3');
                    break;
                case "Side5":
                    //DiceNumberTextScript.diceNumber = 5;
                    //Debug.Log('2');
                    DiceNumberTextScript.diceNumber = 2;
                    break;
                case "Side6":
                    //DiceNumberTextScript.diceNumber = 6;
                    //Debug.Log('1');
                    DiceNumberTextScript.diceNumber = 1;
                    break;
            }
        }
    }
}