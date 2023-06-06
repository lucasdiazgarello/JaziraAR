using UnityEngine;
using UnityEngine.UI;

public class TirarDados : MonoBehaviour
{
    public Button tirardadosButton; // Botón UI
    public GameObject dadoPrefab; // Prefab del dado
    public float spawnDistance = 2.0f; // La distancia frente a la cámara donde los dados serán generados
    public float spawnOffset = 0.5f; // La distancia horizontal entre los dos puntos de generación de dados

    private Dado dado1;
    private Dado dado2;
    private bool dadosLanzados;

    void Start()
    {
        // Asegúrate de asignar el botón en el inspector de Unity.
        tirardadosButton.onClick.AddListener(TirarlosDados);
    }

    void Update()
    {
        if (dadosLanzados && dado1.HasLanded() && dado2.HasLanded())
        {
            int valorDado1 = dado1.GetValue();
            int valorDado2 = dado2.GetValue();
            Debug.Log("El valor del dado 1 es: " + valorDado1);
            Debug.Log("El valor del dado 2 es: " + valorDado2);
            Reset();
        }
    }

    void TirarlosDados()
    {
        if (!dadosLanzados)
        {
            dadosLanzados = true;

            Vector3 spawnPosition1 = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
            Vector3 spawnPosition2 = spawnPosition1 + new Vector3(spawnOffset, 0, 0); // Ajusta spawnOffset para cambiar la separación entre los puntos de spawn

            GameObject diceGO1 = Instantiate(dadoPrefab, spawnPosition1, Quaternion.identity);
            GameObject diceGO2 = Instantiate(dadoPrefab, spawnPosition2, Quaternion.identity);

            dado1 = diceGO1.GetComponent<Dado>();
            dado2 = diceGO2.GetComponent<Dado>();

            // Aplica fuerzas a ambos dados
            ThrowSingleDice(dado1);
            ThrowSingleDice(dado2);
        }
    }

    void ThrowSingleDice(Dado dado)
    {
        Rigidbody rb = dado.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        rb.AddForce(Vector3.up * Random.Range(500, 1000));
    }

    void Reset()
    {
        dadosLanzados = false;
    }
}
