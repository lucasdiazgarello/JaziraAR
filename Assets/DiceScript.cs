using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 diceVelocity;
    private Vector3 platformCenter;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Método público para lanzar el dado
    public void RollDice(Vector3 initialPosition)
    {
        Debug.Log("entro a RollDice");
        DiceNumberTextScript.diceNumber = 0;
        float dirX = Random.Range(400, 500);
        float dirY = Random.Range(400, 500);
        float dirZ = Random.Range(400, 500);
        //Debug.Log(dirX+""+dirY+""+dirZ);
        transform.position = initialPosition;
        //Debug.Log("poscicion inicial =" + initialPosition);
        transform.rotation = Quaternion.identity;
        rb.AddForce(transform.up * 1000);
        rb.AddTorque(dirX, dirY, dirZ);
    }
    /*
    public void RollDice()
    {
        DiceNumberTextScript.diceNumber = 0;
        float dirX = Random.Range(0, 500);
        float dirY = Random.Range(0, 500);
        float dirZ = Random.Range(0, 500);
        //transform.position = new Vector3(0, 2, 0);
        transform.position = platformCenter + new Vector3(0, 2, 0);
        transform.rotation = Quaternion.identity;
        rb.AddForce(transform.up * 500);
        rb.AddTorque(dirX, dirY, dirZ);
    }*/
}

