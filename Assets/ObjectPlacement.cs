using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject objectToPlace; 
    public bool canPlaceObject = true; 

    void Start()
    {

    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && canPlaceObject)
        {
            GameObject newObject = Instantiate(objectToPlace, transform.position, transform.rotation);
            canPlaceObject = false; 
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Colocable"))
        {
            canPlaceObject = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Colocable"))
        {
            canPlaceObject = true;
        }
    }
}
