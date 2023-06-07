using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ARCursor : MonoBehaviour
{
    public GameObject objectToPlace; // Este es el tablero
    public ARRaycastManager raycastManager;
    public Button placeButton;
    public Button confirmButton;
    //public GameObject recursos; // Referencia al objeto Canvas que contiene las im�genes a mostrar
    public List<GameObject> recursos; // Lista de objetos GameObject que contienen las im�genes a mostrar
    private GameObject currentObject; // Guarda una referencia al objeto colocado actualmente
    //public Button tirarDadosButton;
    //public GameObject plataformaDados;
    //public float plataformaDistance = 1.0f; // Distancia de desplazamiento de la plataforma
    //private GameObject currentPlatform; // Guarda una referencia a la plataforma instanciada actualmente


    private bool isPlacementModeActive = false; // Para rastrear si el modo de colocaci�n est� activo o no

    void Start()
    {
        placeButton.onClick.AddListener(ActivatePlacementMode);
        confirmButton.onClick.AddListener(ConfirmPlacement);
        confirmButton.gameObject.SetActive(false); // Desactivar el bot�n de confirmaci�n al inicio
        DisableRecursos();
        objectToPlace = Resources.Load("TableroCC 2") as GameObject;

        //tirarDadosButton.gameObject.SetActive(false); // Desactiva el bot�n al inicio
        //plataformaDados.SetActive(false); // Desactiva la plataforma al inicio

    }
    void Update()
    {
        if (isPlacementModeActive && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //Debug.Log("Entrando en el bloque de colocaci�n del tablero.");
            // Comprobar si el toque est� sobre un elemento de la interfaz de usuario
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return; // No colocar el tablero si el toque est� sobre un elemento de la interfaz de usuario
            }

            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

            if (hits.Count > 0)
            {
                // Primero, eliminar el tablero actual si existe
                if (currentObject != null)
                {
                    Destroy(currentObject);
                }

                // Luego, crear un nuevo tablero y guardarlo como currentObject
                currentObject = GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);

                placeButton.gameObject.SetActive(false); // Desactivar el bot�n de colocaci�n despu�s de colocar el tablero
                confirmButton.gameObject.SetActive(true); // Activar el bot�n de confirmaci�n despu�s de colocar el tablero
            }
        }
        /*// Si se confirma la colocaci�n, desactivar el modo de colocaci�n
        else if (!isPlacementModeActive && currentObject != null)
        {
            confirmButton.gameObject.SetActive(false);
            placeButton.gameObject.SetActive(true);
        }
        */
    }

    public void ActivatePlacementMode()
    {
        Debug.Log("Se llam� a ActivatePlacementMode()");
        isPlacementModeActive = true;
    }

    public void ConfirmPlacement()
    {
        //Debug.Log("ConfirmPlacement fue llamado.");
        isPlacementModeActive = false; // Desactivar el modo de colocaci�n
        //Debug.Log("isPlacementModeActive es ahora " + isPlacementModeActive);
        confirmButton.gameObject.SetActive(false); // Desactivar el bot�n de confirmaci�n despu�s de confirmar la colocaci�n
        //Debug.Log("N�mero de componentes ColocarPieza encontrados: " + GetComponentsInChildren<ColocarPieza>().Length);
        // Activar la colocaci�n de las piezas en los marcadores invisibles
        foreach (ColocarPieza colocarPieza in GetComponentsInChildren<ColocarPieza>())
        {
            colocarPieza.enabled = true;
            //Debug.Log("Marcador invisible ACTIVADO: " + gameObject.name); // Mensaje de depuraci�n
        }
        // Mostrar las im�genes en el canvas
        EnableRecursos();
        /*
        //Tirar dados
        // Haz que la plataforma aparezca justo al lado del tablero
        Vector3 offset = new Vector3(1, 0, 0);  // Ajusta este valor seg�n sea necesario
        Vector3 plataformaPosition = currentObject.transform.position + offset;

        // Primero, eliminar la plataforma actual si existe
        if (currentPlatform != null)
        {
            Destroy(currentPlatform);
        }

        // Luego, crear una nueva plataforma y guardarla como currentPlatform
        currentPlatform = Instantiate(plataformaDados, plataformaPosition, Quaternion.identity);
        currentPlatform.SetActive(true);
        */
        /*
        // Haz que la plataforma aparezca justo al lado del tablero
        Vector3 offset = new Vector3(1, 0, 0);  // Ajusta este valor seg�n sea necesario
        Vector3 plataformaPosition = currentObject.transform.position + offset;
        GameObject newPlataforma = Instantiate(plataformaDados, plataformaPosition, Quaternion.identity);
        newPlataforma.SetActive(true);
        */
        //tirarDadosButton.gameObject.SetActive(true);

        //Vector3 plataformaPosition = currentObject.transform.position + currentObject.transform.right * plataformaDistance;
       //plataformaDados.transform.position = plataformaPosition;
        //tirarDadosButton.gameObject.SetActive(true); // Activa el bot�n despu�s de confirmar la ubicaci�n
        //plataformaDados.SetActive(true); // Activa la plataforma despu�s de confirmar la ubicaci�n
        
    }
    private void EnableRecursos()
    {
        foreach (GameObject recurso in recursos)
        {
            recurso.SetActive(true);
        }
    }

    private void DisableRecursos()
    {
        foreach (GameObject recurso in recursos)
        {
            recurso.SetActive(false);
        }
    }
}