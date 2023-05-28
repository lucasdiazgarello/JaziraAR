using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject objectToPlace; // Este ser� el objeto que quieres colocar
    public bool canPlaceObject = true; // Inicialmente permitir la colocaci�n de objetos

    void Start()
    {
        // Aqu� puedes realizar cualquier otra configuraci�n inicial necesaria para el marcador
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && canPlaceObject)
        {
            GameObject newObject = Instantiate(objectToPlace, transform.position, transform.rotation);
            canPlaceObject = false; // Prevenir la colocaci�n de m�s objetos hasta que este se mueva
        }
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
