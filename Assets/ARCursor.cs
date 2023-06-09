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
    public List<GameObject> recursos; // Lista de objetos GameObject que contienen las imágenes a mostrar
    private GameObject currentObject; // Guarda una referencia al objeto colocado actualmente

    private bool isPlacementModeActive = false; // Para rastrear si el modo de colocación está activo o no

    //public Button placePlatformButton; // Nuevo botón para colocar la plataforma
    //public Button confirmPlatformButton; // Nuevo botón para confirmar la colocación de la plataforma
    private bool isPlatformPlacementModeActive = false; // Para rastrear si el modo de colocación de la plataforma está activo o no

    //public PlacePlatformController placePlatformController;
    public GameObject platformToPlace; // Prefab de la plataforma
    public Button placePlatformButton; // Botón para colocar la plataforma
    public Button confirmPlatformButton; // Botón para confirmar la ubicación de la plataforma
    private GameObject currentPlatform; // Plataforma actualmente en proceso de colocación
    private bool isBoardPlaced = false; // Para rastrear si el tablero ya ha sido colocado o no

    public GameObject dadoToPlace; // Prefab del dado
    public float dadoDistance = 0.5f; // Distancia de desplazamiento del dado (en metros)
    private GameObject currentDado; // Dado actualmente en proceso de colocación

    void Start()
    {
        placeButton.onClick.AddListener(ActivatePlacementMode);
        confirmButton.onClick.AddListener(ConfirmPlacement);
        confirmButton.gameObject.SetActive(false); // Desactivar el botón de confirmación al inicio
        DisableRecursos();
        objectToPlace = Resources.Load("TableroCC 2") as GameObject;
        //placePlatformButton.onClick.AddListener(PlacePlatform); // Añadido el Listener para el nuevo botón
        //placePlatformButton.gameObject.SetActive(false); // Desactivar el botón de colocar plataforma al inicio
        
        //placePlatformButton.onClick.AddListener(ActivatePlacementMode);
        //confirmPlatformButton.onClick.AddListener(ConfirmPlatformPlacement); //duplicado
        confirmPlatformButton.gameObject.SetActive(false); // Desactivar el botón de confirmación de la plataforma al inicio
                                                           // Agrega los listeners de los botones para la plataforma
        placePlatformButton.onClick.AddListener(ActivatePlatformPlacementMode);
        confirmPlatformButton.onClick.AddListener(ConfirmPlatformPlacement);
    }
    void Update()
    {
        //colocar tablero
        if (isPlacementModeActive && !isBoardPlaced && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //Debug.Log("Entrando en el bloque de colocación del tablero.");
            // Comprobar si el toque está sobre un elemento de la interfaz de usuario
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return; // No colocar el tablero si el toque está sobre un elemento de la interfaz de usuario
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

                placeButton.gameObject.SetActive(false); // Desactivar el botón de colocación después de colocar el tablero
                confirmButton.gameObject.SetActive(true); // Activar el botón de confirmación después de colocar el tablero
            }
        }//colocar plataforma
        else if (isPlatformPlacementModeActive && isBoardPlaced && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Comprobar si el toque está sobre un elemento de la interfaz de usuario
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return; // No colocar la plataforma si el toque está sobre un elemento de la interfaz de usuario
            }

            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

            if (hits.Count > 0)
            {
                // Primero, eliminar la plataforma actual si existe
                if (currentPlatform != null)
                {
                    Destroy(currentPlatform);
                }

                // Luego, crear una nueva plataforma y guardarlo como currentPlatform
                currentPlatform = GameObject.Instantiate(platformToPlace, hits[0].pose.position, hits[0].pose.rotation);

                // Haz que el dado aparezca por encima de la plataforma
                Vector3 dadoOffset = new Vector3(0, dadoDistance, 0);  // Ajusta este valor según sea necesario
                Vector3 dadoPosition = currentPlatform.transform.position + dadoOffset;

                // Primero, eliminar el dado actual si existe
                if (currentDado != null)
                {
                    Destroy(currentDado);
                }

                // Luego, crear un nuevo dado y guardarlo como currentDado
                currentDado = Instantiate(dadoToPlace, dadoPosition, Quaternion.identity);

                placePlatformButton.gameObject.SetActive(false); // Desactivar el botón de colocación después de colocar la plataforma
                confirmPlatformButton.gameObject.SetActive(true); // Activar el botón de confirmación después de colocar la plataforma
            }
        }
    }

    public void ActivatePlacementMode()
    {
        Debug.Log("Se llamó a ActivatePlacementMode()");
        isPlacementModeActive = true;
    }

    public void ConfirmPlacement()
    {
        isPlacementModeActive = false; // Desactivar el modo de colocación
        confirmButton.gameObject.SetActive(false); // Desactivar el botón de confirmación después de confirmar la colocación
        // Activar la colocación de las piezas en los marcadores invisibles
        foreach (ColocarPieza colocarPieza in GetComponentsInChildren<ColocarPieza>())
        {
            colocarPieza.enabled = true;
        }
        // Mostrar las imágenes en el canvas
        EnableRecursos();
        //placePlatformButton.gameObject.SetActive(true); // Activar el botón de colocar plataforma después de confirmar la colocación del tablero
        isBoardPlaced = true;
        placePlatformButton.gameObject.SetActive(true); // Activar el botón de colocar plataforma después de confirmar la colocación del tablero
        // Desactiva este script y activa PlacePlatformController
        //this.enabled = false;
        //GetComponent<PlacePlatformController>().enabled = true;
        // Activar el script PlacePlatformController
        //placePlatformController.enabled = true;
    }
    public void ActivatePlatformPlacementMode()
    {
        isPlatformPlacementModeActive = true;
    }
    public void ConfirmPlatformPlacement()
    {
        if (currentPlatform != null)
        {
            // Desactivar el modo de colocación de la plataforma
            isPlatformPlacementModeActive = false;

            // Desactivar el botón de confirmación después de confirmar la colocación
            confirmPlatformButton.gameObject.SetActive(false);

            // Aquí puedes agregar cualquier otra lógica que necesites después de confirmar la colocación de la plataforma
        }
    }
    /*public void ConfirmPlatformPlacement()
    {
        if (currentPlatform != null)
        {
            // Desactivar el modo de colocación
            isPlacementModeActive = false;

            // Desactivar el botón de confirmación después de confirmar la colocación
            confirmPlatformButton.gameObject.SetActive(false);

            // Aquí puedes agregar cualquier otra lógica que necesites después de confirmar la colocación de la plataforma
        }
    }*/

    /*public void PlacePlatform()
    {
        Debug.Log("Boton Colocar plataforma()");
        // Aquí invocamos el script que coloca la plataforma y el dado
        // Pero antes de eso, nos aseguramos de que currentObject (el tablero) no sea null
        if (currentObject != null)
        {
            // Aquí asumimos que tu script para colocar la plataforma se llama PlacePlatformScript
            PlacePlatformScript placePlatformScript = GetComponent<PlacePlatformScript>();
            placePlatformScript.PlacePlatformAndDado();  // No pasamos currentObject
        }
        isPlatformPlacementModeActive = true;
        confirmPlatformButton.gameObject.SetActive(true); // Activar el botón de confirmación de la plataforma
    }*/
    /*public void PlacePlatform()
    {
        Debug.Log("Boton Colocar plataforma()");
        if (currentObject != null)
        {
            PlacePlatformScript placePlatformScript = GetComponent<PlacePlatformScript>();
            placePlatformScript.PlacePlatformAndDado();
        }
        isPlacementModeActive = false; // Desactivar el modo de colocación del tablero cuando inicias la colocación de la plataforma
        isPlatformPlacementModeActive = true;
        confirmPlatformButton.gameObject.SetActive(true);
    }

    /*public void ConfirmPlatformPlacement()
    {
        isPlatformPlacementModeActive = false; // Desactivar el modo de colocación de la plataforma
        confirmPlatformButton.gameObject.SetActive(false); // Desactivar el botón de confirmación de la plataforma después de confirmar la colocación
    }*/
    /*public void ConfirmPlatformPlacement()
    {
        isPlatformPlacementModeActive = false;
        confirmPlatformButton.gameObject.SetActive(false);
        PlacePlatformScript placePlatformScript = GetComponent<PlacePlatformScript>();
        placePlatformScript.StopPlatformPlacement();  // Añadido un método para detener la colocación de la plataforma
    }
    */
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