using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour
{
    private DiceScript[] diceScripts;  

    //private ARCursor arCursor;
    Vector3 diceVelocity;
    //private Dictionary<GameObject, bool> hasRegistered = new Dictionary<GameObject, bool>();
    private List<GameObject> registeredObjects = new List<GameObject>(); // Lista de objetos registrados

    void Start()
    {
        
        //ARCursor.Instance = GameObject.FindObjectOfType<ARCursor>();
    }

    void Update()
    {
       
        if (ARCursor.Instance.dicesThrown)
        {
            diceScripts = GameObject.FindObjectsOfType<DiceScript>();
            
            if (diceScripts.Length < 2)
            {
                Debug.LogWarning("No se encontraron las dos instancias de DiceScript en la escena");
            }
        }
    }

    void FixedUpdate()
    {
        if (diceScripts != null && diceScripts.Length == 2 && diceScripts[0] != null && diceScripts[1] != null)
        {
            diceVelocity = diceScripts[0].Dice1Velocity + diceScripts[1].Dice2Velocity;
        }
    }

    void OnTriggerStay(Collider col)
    {
        GameObject obj = col.gameObject;

        
        if (registeredObjects.Contains(obj))
        {
            return;
        }

        registeredObjects.Add(obj);
        StartCoroutine(CheckIfStill(col));
        /*if (!diceScripts[0].IsDiceRolling && !diceScripts[1].IsDiceRolling && diceVelocity.magnitude == 0f)
        {
            StartCoroutine(CheckIfStill(col));
            Debug.Log("Mande el CheckIfStill");
        }*/
    }

    IEnumerator CheckIfStill(Collider col)
    {
        //Debug.Log("Entro al Checkifstill");

        GameObject obj = col.gameObject;

        
        registeredObjects.Remove(obj);

        Vector3 initPosition = col.transform.position;
        yield return new WaitForSeconds(1);

        if (initPosition == col.transform.position)
        {
            switch (col.gameObject.name)
            {
                case "Side1":
                   
                    UpdateDiceNumber(col, 6);
                    break;
                case "Side2":
                  
                    UpdateDiceNumber(col, 5);
                    break;
                case "Side3":
                   
                    UpdateDiceNumber(col, 4);
                    break;
                case "Side4":
                    
                    UpdateDiceNumber(col, 3);
                    break;
                case "Side5":
                   
                    UpdateDiceNumber(col, 2);
                    break;
                case "Side6":
                    
                    UpdateDiceNumber(col, 1);
                    break;
            }
        }
    }

    private void UpdateDiceNumber(Collider col, int number)
    {
        //Debug.Log("Entro UpdateNumber");
        if (col.gameObject.transform.parent.gameObject == DiceNumberTextScript.dice1)
        {
            //Debug.Log("el numero del dado 1 es: " + number);
            DiceNumberTextScript.diceNumber1 = number;
        }
        else if (col.gameObject.transform.parent.gameObject == DiceNumberTextScript.dice2)
        {
            //Debug.Log("el numero del dado 2 es: " + number);
            DiceNumberTextScript.diceNumber2 = number;
        }
    }

    public void ResetRegistration(GameObject dice)
    {
        registeredObjects.Remove(dice);
    }
}
