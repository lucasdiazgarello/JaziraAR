using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject objectToPlace; // Este será el objeto que quieres colocar
    public bool canPlaceObject = true; // Inicialmente permitir la colocación de objetos

    void Start()
    {
        // Aquí puedes realizar cualquier otra configuración inicial necesaria para el marcador
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && canPlaceObject)
        {
            GameObject newObject = Instantiate(objectToPlace, transform.position, transform.rotation);
            canPlaceObject = false; // Prevenir la colocación de más objetos hasta que este se mueva
        }
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
