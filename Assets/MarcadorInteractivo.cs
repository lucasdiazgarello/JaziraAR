using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcadorInteractivo : MonoBehaviour
{
    public bool canPlaceObject; // Indica si se puede colocar un objeto en este marcador

    void Start()
    {
        canPlaceObject = true; // Inicialmente permitir la colocaci�n de objetos
        // Aqu� puedes realizar cualquier otra configuraci�n inicial necesaria para el marcador
    }

    void OnTriggerEnter(Collider other)
    {
        // Ejemplo de detecci�n de colisi�n con otro objeto
        // Si deseas permitir la colocaci�n solo cuando no haya otros objetos en el marcador,
        // puedes desactivar canPlaceObject cuando se detecte una colisi�n con otro objeto.
        canPlaceObject = false;
    }

    void OnTriggerExit(Collider other)
    {
        // Ejemplo de salida de colisi�n con otro objeto
        // Si deseas permitir la colocaci�n solo cuando no haya otros objetos en el marcador,
        // puedes activar canPlaceObject cuando se detecte que el otro objeto ha salido del marcador.
        canPlaceObject = true;
    }
}
