using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour
{
    public DiceScript diceScript; // Referencia al script del dado

    Vector3 diceVelocity;
    private Dictionary<GameObject, bool> hasRegistered = new Dictionary<GameObject, bool>();

    // Update is called once per frame
    void FixedUpdate()
    {
        if (diceScript != null)
        {
            diceVelocity = diceScript.diceVelocity;
        }
    }

    void OnTriggerStay(Collider col)
    {
        // Si el diccionario no tiene un valor para este dado, lo agregamos con el valor "false"
        if (!hasRegistered.ContainsKey(col.gameObject))
        {
            hasRegistered[col.gameObject] = false;
        }

        if (!diceScript.IsDiceRolling && diceVelocity.magnitude == 0f && !hasRegistered[col.gameObject])
        {
            hasRegistered[col.gameObject] = true;
            //Debug.Log(diceScript.IsDiceRolling);
            Debug.Log(diceScript.diceVelocity);
            switch (col.gameObject.name)
            {
                case "Side1":
                    DiceNumberTextScript.diceNumber = 6;
                    Debug.Log('6');
                    break;
                case "Side2":
                    DiceNumberTextScript.diceNumber = 5;
                    Debug.Log('5');
                    break;
                case "Side3":
                    DiceNumberTextScript.diceNumber = 4;
                    Debug.Log('4');
                    break;
                case "Side4":
                    DiceNumberTextScript.diceNumber = 3;
                    Debug.Log('3');
                    break;
                case "Side5":
                    DiceNumberTextScript.diceNumber = 2;
                    Debug.Log('2');
                    break;
                case "Side6":
                    DiceNumberTextScript.diceNumber = 1;
                    Debug.Log('1');
                    break;
            }
        }
    }

    public void ResetRegistration(GameObject dice)
    {
        if (hasRegistered.ContainsKey(dice))
        {
            hasRegistered[dice] = false;
        }
    }
}

/*
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
     
    void OnTriggerStay(Collider col)
    {
        if (!diceScript.IsDiceRolling)
        {
            //Debug.Log(diceScript.IsDiceRolling);
            Debug.Log(diceScript.diceVelocity);
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
*/