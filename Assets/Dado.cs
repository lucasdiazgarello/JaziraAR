using UnityEngine;

public class Dado : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Esta función comprueba si el dado ha dejado de moverse
    public bool HasLanded()
    {
        return rb.IsSleeping();
    }

    // Esta función devuelve el valor de la cara del dado que está hacia arriba
    public int GetValue()
    {
        Vector3 upVector = transform.up;

        if (upVector == Vector3.up)
        {
            return 2;
        }
        else if (upVector == -Vector3.up)
        {
            return 5;
        }
        else if (upVector == Vector3.right)
        {
            return 4;
        }
        else if (upVector == -Vector3.right)
        {
            return 3;
        }
        else if (upVector == Vector3.forward)
        {
            return 6;
        }
        else if (upVector == -Vector3.forward)
        {
            return 1;
        }
        else
        {
            // Esto no debería ocurrir, pero si el dado ha aterrizado de alguna manera de lado,
            // podría ser útil saberlo para depurar
            Debug.LogWarning("Dado cayó de lado");
            return 0;
        }
    }
}
