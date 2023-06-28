
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
    Rigidbody rb1;
    Rigidbody rb2;
    public Vector3 diceVelocity;
    private Vector3 posicion1;
    private Vector3 posicion2;
    // Almacena una referencia al script ARCursor para acceder a dicesThrown
    private ARCursor arCursor;
    // Propiedad pública para verificar si el dado está rodando
    //public bool IsDiceRolling { get; private set; }
    // Propiedad pública para verificar si el dado está rodando
    public bool IsDice1Rolling { get; private set; }
    public bool IsDice2Rolling { get; private set; }
    // Propiedad para verificar si algún dado está rodando
    public bool IsDiceRolling => IsDice1Rolling || IsDice2Rolling;

    public Vector3 Dice1Velocity { get; private set; }
    public Vector3 Dice2Velocity { get; private set; }
    //private Vector3 dice1Velocity;
    //private Vector3 dice2Velocity;

    void Start()
    {
        posicion1 = new Vector3(5, 5, 5);
        posicion2 = new Vector3(10, 10, 10);
        //alternar = false;
        diceVelocity = Vector3.zero;
        //IsDiceRolling = true;
        IsDice1Rolling = IsDice2Rolling = true;

    }

    // Método público para lanzar el dado
    public void RollDice(GameObject dado, Vector3 initialPosition)
    {
        Debug.Log("Entró a RollDice");

        Rigidbody rb = dado.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.Log("Rigidbody not found");
            return;
        }

        if (dado == DiceNumberTextScript.dice1)
        {
            DiceNumberTextScript.diceNumber1 = 0;
            IsDice1Rolling = true; // Modificación aquí
        }
        else if (dado == DiceNumberTextScript.dice2)
        {
            DiceNumberTextScript.diceNumber2 = 0;
            IsDice2Rolling = true; // Modificación aquí
        }

        Vector3 randomTorque = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));

        dado.transform.SetPositionAndRotation(initialPosition, Quaternion.identity);

        rb.AddTorque(randomTorque);
    }

    // Update is called once per frame
    void Update()
    {
        if (DiceNumberTextScript.dice1 != null)
        {
            Rigidbody rb1 = DiceNumberTextScript.dice1.GetComponent<Rigidbody>();
            if (rb1 != null)
            {
                Dice1Velocity = rb1.velocity;
                if (Dice1Velocity.magnitude == 0)
                {
                    IsDice1Rolling = false;
                }
                else if (arCursor != null)
                {
                    arCursor.dicesThrown = false;
                }
            }
        }
        if (DiceNumberTextScript.dice2 != null)
        {
            Rigidbody rb2 = DiceNumberTextScript.dice2.GetComponent<Rigidbody>();
            if (rb2 != null)
            {
                Dice2Velocity = rb2.velocity;
                if (Dice2Velocity.magnitude == 0)
                {
                    IsDice2Rolling = false;
                }
                else if (arCursor != null)
                {
                    arCursor.dicesThrown = false;
                }
            }
        }
    }
}

