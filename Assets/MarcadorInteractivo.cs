using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcadorInteractivo : MonoBehaviour
{
    public bool canPlaceObject; // Indica si se puede colocar un objeto en este marcador
    public GameObject objectToPlace; // Objeto que se puede colocar en este marcador

    void Start()
    {
        canPlaceObject = true; // Inicialmente permitir la colocación de objetos
        // Aquí puedes realizar cualquier otra configuración inicial necesaria para el marcador
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisión es con un objeto que puede ser colocado
        if (collision.gameObject.CompareTag("Colocable"))
        {
            canPlaceObject = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Verificar si el objeto que salió de la colisión era un objeto colocado
        if (collision.gameObject.CompareTag("Colocable"))
        {
            canPlaceObject = true;
        }
    }
}
