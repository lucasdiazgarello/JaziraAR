using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlacePlatformScript : MonoBehaviour
{
    public GameObject plataformaDados; // Esta es la plataforma
    public GameObject dado; // Este es el dado
    public float plataformaDistance = 0.05f; // Distancia de desplazamiento de la plataforma (en metros)
    public float dadoDistance = 0.05f; // Distancia de desplazamiento del dado (en metros)
    public ARRaycastManager raycastManager; // Necesitamos esto para hacer raycasts

    public void PlacePlatformAndDado(GameObject currentObject)
    {
        // Haz que la plataforma aparezca justo al lado del tablero
        Vector3 plataformaOffset = new Vector3(-plataformaDistance, 0, 0);  // Ajusta este valor según sea necesario
        Vector3 plataformaPosition = currentObject.transform.position + plataformaOffset;

        // Crear una nueva plataforma
        GameObject currentPlatform = Instantiate(plataformaDados, plataformaPosition, Quaternion.identity);

        // Haz que el dado aparezca por encima de la plataforma
        Vector3 dadoOffset = new Vector3(0, dadoDistance, 0);  // Ajusta este valor según sea necesario
        Vector3 dadoPosition = currentPlatform.transform.position + dadoOffset;

        // Crear un nuevo dado
        GameObject currentDado = Instantiate(dado, dadoPosition, Quaternion.identity);
    }
}
