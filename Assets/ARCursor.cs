using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.Netcode;

public class ARCursor : NetworkBehaviour
{
    public GameObject objectToPlace; // Este es el tablero
    public ARRaycastManager raycastManager;
    public Button placeButton;
    public Button confirmButton;
    public List<GameObject> recursos; // Lista de objetos GameObject que contienen las im�genes a mostrar

    private GameObject currentObject; // Guarda una referencia al objeto colocado actualmente
    private bool isPlacementModeActive = false; // Para rastrear si el modo de colocaci�n est� activo o no
    private bool isBoardPlaced = false; // Para rastrear si el tablero ya ha sido colocado o no

    public GameObject dadoToPlace; // Prefab del dado
    public float dadoDistance = 0.5f; // Distancia de desplazamiento del dado (en metros)
    private GameObject currentDado; // Dado actualmente en proceso de colocaci�n
    private GameObject currentDado2;
    public Button tirarDadoButton;

    private ColocarPieza colocarPieza;

    private Vector3 initialDadoPosition; // Para guardar la posici�n inicial del dado
    private GameObject tableromInstance;

    void Start()
    {
        placeButton.onClick.AddListener(ActivatePlacementMode);
        confirmButton.onClick.AddListener(ConfirmPlacement);
        confirmButton.gameObject.SetActive(false); // Desactivar el bot�n de confirmaci�n al inicio
        DisableRecursos();
        objectToPlace = Resources.Load("TableroCC 2") as GameObject;
        tirarDadoButton.onClick.AddListener(OnDiceRollButtonPressed);
        colocarPieza = GetComponentInChildren<ColocarPieza>();
    }

    void Update()
    {
        //colocar tablero
        if (isPlacementModeActive && !isBoardPlaced && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
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
                if (tableromInstance != null)
                {
                    var networkObject = tableromInstance.GetComponent<NetworkObject>();
                    if (networkObject != null && networkObject.IsSpawned)
                    {
                        networkObject.Despawn();
                    }
                }
                // Luego, crear un nuevo tablero y guardarlo como currentObject
                tableromInstance = Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                tableromInstance.GetComponent<NetworkObject>().Spawn();
                placeButton.gameObject.SetActive(false); // Desactivar el bot�n de colocaci�n despu�s de colocar el tablero
                confirmButton.gameObject.SetActive(true); // Activar el bot�n de confirmaci�n despu�s de colocar el tablero
            }
        }
        if (colocarPieza != null && colocarPieza.enabled)
        {
            if (colocarPieza.tipoActual == ColocarPieza.TipoObjeto.Base)
            {
                colocarPieza.ColocarBase();
            }
            else if (colocarPieza.tipoActual == ColocarPieza.TipoObjeto.Camino)
            {
                colocarPieza.ColocarCamino();
            }
        }
    }

    public void ActivatePlacementMode()
    {
        isPlacementModeActive = true;
        if (isBoardPlaced)
        {
            // Si el tablero ya est� colocado, desactivar el modo de colocaci�n
            isPlacementModeActive = false;
        }
    }
    public void ConfirmPlacement()
    {
        // Confirmar la colocaci�n del tablero y desactivar el modo de colocaci�n
        isBoardPlaced = true;
        isPlacementModeActive = false;
        confirmButton.gameObject.SetActive(false); // Desactivar el bot�n de confirmaci�n
        // Activar la colocaci�n de las piezas en los marcadores invisibles
        foreach (ColocarPieza colocarPieza in GetComponentsInChildren<ColocarPieza>())
        {
            colocarPieza.enabled = true;
        }
        EnableRecursos();
    }

    private void EnableRecursos()
    {
        foreach (var recurso in recursos)
        {
            recurso.SetActive(true);
        }
    }

    private void DisableRecursos()
    {
        foreach (var recurso in recursos)
        {
            recurso.SetActive(false);
        }
    }

    private void OnDiceRollButtonPressed()
    {
        // Si el tablero no est� colocado, regresar
        if (!isBoardPlaced) return;

        // Si ya hay un dado, destruirlo
        if (currentDado != null)
        {
            Debug.Log("Destruyendo dado actual");
            Destroy(currentDado);
            currentDado = null; // Aseg�rate de que currentDado es null despu�s de destruirlo
        }

        if (currentDado2 != null)
        {
            Debug.Log("Destruyendo dado 2 actual");
            Destroy(currentDado2);
            currentDado2 = null; // Aseg�rate de que currentDado2 es null despu�s de destruirlo
        }

        if (tableromInstance != null)
        {
            Debug.Log("dadoToPlace es " + (dadoToPlace == null ? "null" : "no null")); // Comprobar si dadoToPlace es null antes de instanciar

            // Crear un nuevo dado en la posici�n por encima del tablero
            currentDado = Instantiate(dadoToPlace, tableromInstance.transform.position + Vector3.up * dadoDistance, Quaternion.identity);
            currentDado.GetComponent<NetworkObject>().Spawn();

            // Crear un segundo dado al costado del primero
            currentDado2 = Instantiate(dadoToPlace, tableromInstance.transform.position + Vector3.up * dadoDistance + Vector3.right * dadoDistance, Quaternion.identity);
            currentDado2.GetComponent<NetworkObject>().Spawn();

            Debug.Log("currentDado es " + (currentDado == null ? "null" : "no null")); // Comprobar si currentDado es null despu�s de instanciar

            // Obt�n el DiceScript del dado actual y lanza el dado
            DiceScript diceScript = currentDado.GetComponent<DiceScript>();
            if (diceScript != null)
            {
                Debug.Log("Lanzando el dado");
                diceScript.RollDice(currentDado,tableromInstance.transform.position + Vector3.up * dadoDistance);

                //Destroy(currentDado, 2f);
                
            }

            // Obt�n el DiceScript del segundo dado y lanza el dado
            DiceScript diceScript2 = currentDado2.GetComponent<DiceScript>();
            if (diceScript2 != null)
            {
                Debug.Log("Lanzando el dado 2");
                diceScript2.RollDice(currentDado2,tableromInstance.transform.position + Vector3.up * dadoDistance + Vector3.right * dadoDistance);
                Destroy(currentDado2, 2f);
            }



        }

        /*// Si el tablero no est� colocado, regresar
        if (!isBoardPlaced) return;

        // Si ya hay un dado, destruirlo
        if (currentDado != null)
        {
            Destroy(currentDado);
        }
        if (tableromInstance != null)
        {
            // Crear un nuevo dado en la posici�n por encima del tablero
            currentDado = Instantiate(dadoToPlace, tableromInstance.transform.position + Vector3.up * dadoDistance, Quaternion.identity);

            // Obt�n el DiceScript del dado actual y lanza el dado
            DiceScript diceScript = currentDado.GetComponent<DiceScript>();
            if (diceScript != null)
            {
                diceScript.RollDice(tableromInstance.transform.position + Vector3.up * dadoDistance);
            }
        }
        */
        /*
        // Si el tablero no est� colocado, regresar
        if (!isBoardPlaced) return;

        // Si ya hay un dado, destruirlo
        if (currentDado != null)
        {
            Destroy(currentDado);
        }
        // Crear un nuevo dado y guardar su posici�n inicial
        currentDado = Instantiate(dadoToPlace, tableromInstance.transform.position + Vector3.up * dadoDistance, Quaternion.identity);
        initialDadoPosition = currentDado.transform.position;

        Rigidbody rb = currentDado.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }*/
    }

    private void ResetDicePosition()
    {
        // Restablecer la posici�n del dado a la inicial
        if (currentDado != null)
        {
            currentDado.transform.position = initialDadoPosition;
        }
    }

    public void RespawnDice()
    {
        // Restablecer la posici�n del dado y reactivar su cuerpo r�gido
        ResetDicePosition();
        Rigidbody rb = currentDado.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}

