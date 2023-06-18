using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 diceVelocity;
    private GameObject dado;
    private Vector3 platformCenter;

    // Use this for initialization
    void Start()
    {
        //rb = dado.GetComponent<Rigidbody>();
        //rb = GetComponent<Rigidbody>();
    }

    // Método público para lanzar el dado
    public void RollDice(GameObject dado,Vector3 initialPosition)
    {
        Debug.Log("Entró a RollDice");
        rb = dado.GetComponent<Rigidbody>();
        DiceNumberTextScript.diceNumber = 0;

        // Aplicar una fuerza y un torque aleatorios
        //Vector3 randomForce = new Vector3(Random.Range(-500, 500), Random.Range(1000, 2000), Random.Range(-500, 500));
        Vector3 randomTorque = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));
         
        transform.SetPositionAndRotation(initialPosition, Quaternion.identity);
        //rb.AddForce(randomForce);
        rb.AddTorque(randomTorque);
    }
}

