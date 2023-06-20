using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DiceScript : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 diceVelocity;
    private GameObject dado;
    private Vector3 platformCenter;
    private Vector3 posicion1;
    private Vector3 posicion2;
    private bool alternar;
    

    // Propiedad pública para verificar si el dado está rodando
    public bool IsDiceRolling { get; private set; }

    // Use this for initialization
    void Start()
    {
        posicion1 = new Vector3 (5, 5, 5);
        posicion2 = new Vector3(10, 10, 10);
        alternar = false;
        //rb = dado.GetComponent<Rigidbody>();
        //rb = GetComponent<Rigidbody>();
        rb = dado.GetComponent<Rigidbody>();
        IsDiceRolling = true;
    }

    // Método público para lanzar el dado
    public void RollDice(GameObject dado, Vector3 initialPosition)
    {
        Debug.Log("Entró a RollDice");
        rb = dado.GetComponent<Rigidbody>();
        DiceNumberTextScript.diceNumber = 0;
        if(alternar)
        {
            posicion1 = rb.position;
        }
        else
        {
            posicion2 = rb.position;
        }
            
        
        // Aplicar una fuerza y un torque aleatorios
        //Vector3 randomForce = new Vector3(Random.Range(-500, 500), Random.Range(1000, 2000), Random.Range(-500, 500));
        Vector3 randomTorque = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));

        transform.SetPositionAndRotation(initialPosition, Quaternion.identity);
        //rb.AddForce(randomForce);
        rb.AddTorque(randomTorque);

        // Set IsDiceRolling to true when the dice is rolled
        // Check if the dice is still moving
        /*
        if (rb.velocity == Vector3.zero)
        {   

            diceVelocity = rb.velocity;
            IsDiceRolling = false;
        }
        else
            IsDiceRolling = true;
        */
        if (posicion1 == posicion2)
        {
            IsDiceRolling = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
