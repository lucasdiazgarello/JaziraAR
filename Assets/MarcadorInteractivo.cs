using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcadorInteractivo : MonoBehaviour
{
    public bool canPlaceObject; // Indica si se puede colocar un objeto en este marcador
    public GameObject objectToPlace; // Objeto que se puede colocar en este marcador

    void Start()
    {
        canPlaceObject = true; // Inicialmente permitir la colocaci�n de objetos
        // Aqu� puedes realizar cualquier otra configuraci�n inicial necesaria para el marcador
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisi�n es con un objeto que puede ser colocado
        if (collision.gameObject.CompareTag("Colocable"))
        {
            canPlaceObject = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Verificar si el objeto que sali� de la colisi�n era un objeto colocado
        if (collision.gameObject.CompareTag("Colocable"))
        {
            canPlaceObject = true;
        }
    }
}
