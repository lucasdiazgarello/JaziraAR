using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour
{
    private DiceScript[] diceScripts;  // Almacena las referencias a ambos scripts de los dados
    // Almacena una referencia al script ARCursor para acceder a dicesThrown
    private ARCursor arCursor;
    Vector3 diceVelocity;
    private Dictionary<GameObject, bool> hasRegistered = new Dictionary<GameObject, bool>();

    void Start()
    {
        // Obtén la referencia a ARCursor
        arCursor = GameObject.FindObjectOfType<ARCursor>();
    }

    void Update()
    {
        // Solo busca las instancias de DiceScript si los dados han sido lanzados
        if (arCursor.dicesThrown)
        {
            diceScripts = GameObject.FindObjectsOfType<DiceScript>();
            // Verifica si encontró las instancias de DiceScript
            if (diceScripts.Length < 2)
            {
                Debug.LogWarning("No se encontraron las dos instancias de DiceScript en la escena");
            }
        }
    }

    void FixedUpdate()
    {
        if (diceScripts.Length == 2 && diceScripts[0] != null && diceScripts[1] != null)
        {
            diceVelocity = diceScripts[0].diceVelocity + diceScripts[1].diceVelocity;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (!hasRegistered.ContainsKey(col.gameObject))
        {
            hasRegistered[col.gameObject] = false;
        }

        if (!diceScripts[0].IsDiceRolling && !diceScripts[1].IsDiceRolling && diceVelocity.magnitude == 0f && !hasRegistered[col.gameObject])
        {
            StartCoroutine(CheckIfStill(col));
        }
    }

    IEnumerator CheckIfStill(Collider col)
    {
        Debug.Log("Entro al Checkifstill");
        hasRegistered[col.gameObject] = true;

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
        else
        {
            hasRegistered[col.gameObject] = false;
        }
    }

    private void UpdateDiceNumber(Collider col, int number)
    {
        Debug.Log("Entro UpdateNumber");
        if (col.gameObject.transform.parent.gameObject == DiceNumberTextScript.dice1)
        {
            DiceNumberTextScript.diceNumber1 = number;
        }
        else if (col.gameObject.transform.parent.gameObject == DiceNumberTextScript.dice2)
        {
            DiceNumberTextScript.diceNumber2 = number;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour
{
    //public DiceScript diceScript;
    public DiceScript diceScript1; // Referencia al script del primer dado
    public DiceScript diceScript2; // Referencia al script del segundo dado

    Vector3 diceVelocity;
    private Dictionary<GameObject, bool> hasRegistered = new Dictionary<GameObject, bool>();


    void FixedUpdate()
    {
        if (diceScript1 != null && diceScript2 != null)
        {
            diceVelocity = diceScript1.diceVelocity + diceScript2.diceVelocity;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (!hasRegistered.ContainsKey(col.gameObject))
        {
            hasRegistered[col.gameObject] = false;
        }

        if (!diceScript1.IsDiceRolling && !diceScript2.IsDiceRolling && diceVelocity.magnitude == 0f && !hasRegistered[col.gameObject])
        {
            StartCoroutine(CheckIfStill(col));
        }
    }

    IEnumerator CheckIfStill(Collider col)
    {
        hasRegistered[col.gameObject] = true;

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
        else
        {
            hasRegistered[col.gameObject] = false;
        }
    }

    private void UpdateDiceNumber(Collider col, int number)
    {
        if (col.gameObject.transform.parent.gameObject == DiceNumberTextScript.dice1)
        {
            DiceNumberTextScript.diceNumber1 = number;
        }
        else if (col.gameObject.transform.parent.gameObject == DiceNumberTextScript.dice2)
        {
            DiceNumberTextScript.diceNumber2 = number;
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
*/