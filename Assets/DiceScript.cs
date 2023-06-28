
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    private class DiceData
    {
        public Rigidbody rb;
        public Vector3 diceVelocity;
        public float timeStopped;
        public bool isRolling;
    }

    private DiceData dice1Data = new DiceData();
    private DiceData dice2Data = new DiceData();

    // Use this for initialization
    void Start()
    {
        dice1Data.diceVelocity = Vector3.zero;
        dice1Data.isRolling = true;
        dice1Data.timeStopped = 0f;

        dice2Data.diceVelocity = Vector3.zero;
        dice2Data.isRolling = true;
        dice2Data.timeStopped = 0f;
    }

    // Método público para lanzar el dado
    public void RollDice(GameObject dado, Vector3 initialPosition)
    {
        Debug.Log("Entró a RollDice");

        DiceData currentDice;
        if (DiceNumberTextScript.dice1 == null)
        {
            DiceNumberTextScript.dice1 = dado;
            currentDice = dice1Data;
        }
        else if (DiceNumberTextScript.dice2 == null)
        {
            DiceNumberTextScript.dice2 = dado;
            currentDice = dice2Data;
        }
        else return;

        currentDice.rb = dado.GetComponent<Rigidbody>();

        if (currentDice.rb == null)
        {
            Debug.Log("Rigidbody not found");
            return;
        }

        if (dado == DiceNumberTextScript.dice1)
        {
            DiceNumberTextScript.diceNumber1 = 0;
        }
        else if (dado == DiceNumberTextScript.dice2)
        {
            DiceNumberTextScript.diceNumber2 = 0;
        }

        Vector3 randomTorque = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));

        dado.transform.SetPositionAndRotation(initialPosition, Quaternion.identity);

        currentDice.rb.AddTorque(randomTorque);
        currentDice.isRolling = true;
        currentDice.timeStopped = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDice(dice1Data, DiceNumberTextScript.dice1);
        UpdateDice(dice2Data, DiceNumberTextScript.dice2);
    }

    private void UpdateDice(DiceData diceData, GameObject diceObject)
    {
        if (diceObject != null)
        {
            diceData.rb = diceObject.GetComponent<Rigidbody>();
            diceData.diceVelocity = diceData.rb.velocity;
            Debug.Log(diceData.diceVelocity);

            if (diceData.diceVelocity.magnitude == 0)
            {
                diceData.timeStopped += Time.deltaTime;
                if (diceData.timeStopped >= 1f)
                {
                    diceData.isRolling = false;
                    Debug.Log("Dice has stopped. Value of dice1: " + DiceNumberTextScript.diceNumber1 + ", Value of dice2: " + DiceNumberTextScript.diceNumber2);
                }
            }
            else
            {
                diceData.timeStopped = 0f;
            }
        }
    }

    // Devuelve si alguno de los dados está rodando
    public bool IsAnyDiceRolling()
    {
        return dice1Data.isRolling || dice2Data.isRolling;
    }
}
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DiceScript : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 diceVelocity;
    private Vector3 posicion1;
    private Vector3 posicion2;
    // Almacena una referencia al script ARCursor para acceder a dicesThrown
    private ARCursor arCursor;
    // Propiedad pública para verificar si el dado está rodando
    public bool IsDiceRolling { get; private set; }

    // Use this for initialization
    void Start()
    {
        posicion1 = new Vector3(5, 5, 5);
        posicion2 = new Vector3(10, 10, 10);
        //alternar = false;
        diceVelocity = Vector3.zero;
        IsDiceRolling = true;
    }

    // Método público para lanzar el dado
    public void RollDice(GameObject dado, Vector3 initialPosition)
    {
        Debug.Log("Entró a RollDice");

        rb = dado.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.Log("Rigidbody not found");
            return;
        }

        if (dado == DiceNumberTextScript.dice1)
        {
            DiceNumberTextScript.diceNumber1 = 0;
        }
        else if (dado == DiceNumberTextScript.dice2)
        {
            DiceNumberTextScript.diceNumber2 = 0;
        }

        Vector3 randomTorque = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));

        dado.transform.SetPositionAndRotation(initialPosition, Quaternion.identity);

        rb.AddTorque(randomTorque);
        IsDiceRolling = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (DiceNumberTextScript.dice1 != null)
        {
            rb = DiceNumberTextScript.dice1.GetComponent<Rigidbody>();
            diceVelocity = rb.velocity;
            //Debug.Log(diceVelocity);

            if (diceVelocity.magnitude == 0)
            {
                IsDiceRolling = false;
            }
            else if (arCursor != null)
            {
                arCursor.dicesThrown = false;
            }
        }
        if (DiceNumberTextScript.dice2 != null)
        {
            rb = DiceNumberTextScript.dice2.GetComponent<Rigidbody>();
            diceVelocity = rb.velocity;
            //Debug.Log(diceVelocity);

            if (diceVelocity.magnitude == 0)
            {
                IsDiceRolling = false;
            }
            else if (arCursor != null)
            {
                arCursor.dicesThrown = false;
            }
        }
    }
}

