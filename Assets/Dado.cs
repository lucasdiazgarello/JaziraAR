using UnityEngine;

public class Dado : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Esta funci�n comprueba si el dado ha dejado de moverse
    public bool HasLanded()
    {
        return rb.IsSleeping();
    }

    // Esta funci�n devuelve el valor de la cara del dado que est� hacia arriba
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
            // Esto no deber�a ocurrir, pero si el dado ha aterrizado de alguna manera de lado,
            // podr�a ser �til saberlo para depurar
            Debug.LogWarning("Dado cay� de lado");
            return 0;
        }
    }
}
