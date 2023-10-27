using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcadorInteractivo : MonoBehaviour
{
    public bool canPlaceObject; 

    void Start()
    {
        canPlaceObject = true; 

    }

    void OnTriggerEnter(Collider other)
    {

        canPlaceObject = false;
    }

    void OnTriggerExit(Collider other)
    {

        canPlaceObject = true;
    }
}
