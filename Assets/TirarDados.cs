using UnityEngine;
using UnityEngine.UI;

public class TirarDados : MonoBehaviour
{
    public Rigidbody rb;
    private bool hasLanded;
    private bool tirado;

    public Vector3 InitialPosition;
    public Button tirardadosButton; // Botón UI
    public GameObject dadoPrefab; // Prefab del dado
    //public Transform diceSpawnPoint1, diceSpawnPoint2; // Puntos de lanzamiento para los dados
    public float spawnDistance = 2.0f; // La distancia frente a la cámara donde los dados serán generados
    public float spawnOffset = 0.5f; // La distancia horizontal entre los dos puntos de generación de dados

    // el resto de tu código va aquí...

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitialPosition = transform.position;

        // Asegúrate de asignar el botón en el inspector de Unity.
        tirardadosButton.onClick.AddListener(TirarlosDados);
    }

    void TirarlosDados()
    {
        if (!tirado && !hasLanded)
        {
            tirado = true;

            Vector3 spawnPosition1 = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
            Vector3 spawnPosition2 = spawnPosition1 + new Vector3(spawnOffset, 0, 0); // Ajusta spawnOffset para cambiar la separación entre los puntos de spawn

            GameObject dice1 = Instantiate(dadoPrefab, spawnPosition1, Quaternion.identity);
            GameObject dice2 = Instantiate(dadoPrefab, spawnPosition2, Quaternion.identity);

            //GameObject dice1 = Instantiate(dadoPrefab, diceSpawnPoint1.position, Quaternion.identity);
            //GameObject dice2 = Instantiate(dadoPrefab, diceSpawnPoint2.position, Quaternion.identity);

            Rigidbody rb1 = dice1.GetComponent<Rigidbody>();
            Rigidbody rb2 = dice2.GetComponent<Rigidbody>();

            // Aplica fuerzas a ambos dados
            ThrowSingleDice(rb1);
            ThrowSingleDice(rb2);
        }
        else if (tirado && hasLanded)
        {
            Reset();
        }
    }
    void ThrowSingleDice(Rigidbody rb)
    {
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        rb.AddForce(Vector3.up * Random.Range(500, 1000));
    }

    void Reset()
    {
        transform.position = InitialPosition;
        tirado = false;
        hasLanded = false;
        rb.useGravity = false;
    }

    void OnCollisionEnter()
    {
        if (tirado)
        {
            hasLanded = true;
        }
    }
}
