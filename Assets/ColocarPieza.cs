using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Para manejar los botones

public class ColocarPieza : NetworkBehaviour
{
    //public static ColocarPieza Instance { get; private set; }
    private GameObject prefabCamino;
    private GameObject prefabBase; // Cambiado Casa por Base
    private GameObject prefabPueblo; // Nuevo prefab para el pueblo
    private GameObject currentBase;
    private GameObject currentCamino;
    private GameObject currentPueblo;
    private bool _isTouching = false;
    private bool canPlace = false;
    public ComprobarObjeto comprobarObjeto;
    public LayerMask myLayerMask;
    public Button buttonCamino;
    public Button buttonBase;
    public Button buttonPueblo;
    public Button confirmBaseButton; // Asegúrate de asignar este botón en el inspector de Unity
    public Button confirmCaminoButton;
    public Button confirmPuebloButton;
    public TipoObjeto tipoActual;
    public enum PlayerColor
    {
        Azul,
        Rojo,
        Violeta,
        Naranja
    }

    [System.Serializable]
    public class PlayerPrefabs
    {
        public GameObject Camino;
        public GameObject Base;
        public GameObject Pueblo;
    }

    public Dictionary<PlayerColor, PlayerPrefabs> colorPrefabs = new Dictionary<PlayerColor, PlayerPrefabs>();

    private PlayerColor currentPlayerColor;

    void Start()
    {
        // Inicializa el diccionario con los prefabs para cada jugador
        colorPrefabs[PlayerColor.Azul] = new PlayerPrefabs
        {
            Camino = Resources.Load<GameObject>("Camino Azul"),
            Base = Resources.Load<GameObject>("Base Azul"),
            Pueblo = Resources.Load<GameObject>("Pueblo Azul")
        };
        colorPrefabs[PlayerColor.Rojo] = new PlayerPrefabs
        {
            Camino = Resources.Load<GameObject>("Camino Rojo"),
            Base = Resources.Load<GameObject>("Base Rojo"),
            Pueblo = Resources.Load<GameObject>("Pueblo Rojo")
        };
        colorPrefabs[PlayerColor.Violeta] = new PlayerPrefabs
        {
            Camino = Resources.Load<GameObject>("Camino Violeta"),
            Base = Resources.Load<GameObject>("Base Violeta"),
            Pueblo = Resources.Load<GameObject>("Pueblo Violeta")
        };
        colorPrefabs[PlayerColor.Naranja] = new PlayerPrefabs
        {
            Camino = Resources.Load<GameObject>("Camino Naranja"),
            Base = Resources.Load<GameObject>("Base Naranja"),
            Pueblo = Resources.Load<GameObject>("Pueblo Naranja")
        };

        //prefabBase = Resources.Load("Base Azul") as GameObject;
        //prefabCamino = Resources.Load("Camino Azul") as GameObject;
        //prefabPueblo = Resources.Load("Pueblo Azul") as GameObject;
        // para la busqueda del null reference verifico que arcursor no es null
        if (ARCursor.Instance == null)
        {
            //Debug.LogError("ARCursor is null in object " + gameObject.name);
        }
        else
        {
            //Debug.Log("ARCursor is not null in object " + gameObject.name);
        }
        enabled = false; // Desactivar la colocación de piezas al inicio
        //tipoActual = new TipoObjeto();
        tipoActual = TipoObjeto.Ninguno;

        // Agregar los listeners a los botones
        buttonCamino.onClick.AddListener(() => {
            tipoActual = TipoObjeto.Camino;
            canPlace = true;
        });
        buttonBase.onClick.AddListener(() => {
            tipoActual = TipoObjeto.Base;
            canPlace = true;
        });
        buttonPueblo.onClick.AddListener(() => {
            tipoActual = TipoObjeto.Pueblo;
            canPlace = true;
        });

        confirmBaseButton.gameObject.SetActive(false);
        confirmCaminoButton.gameObject.SetActive(false);
        confirmPuebloButton.gameObject.SetActive(false);
    }

    void Update()
    {
        // Obtener el jugador actual (esto puede ser una función que retorna el color del jugador en turno)
        currentPlayerColor = GetCurrentPlayerColor();
        //Debug.Log("color del jugador actual " + currentPlayerColor);
        prefabCamino = colorPrefabs[currentPlayerColor].Camino;
        prefabBase = colorPrefabs[currentPlayerColor].Base;
        prefabPueblo = colorPrefabs[currentPlayerColor].Pueblo;
        if (canPlace && Input.touchCount == 1 && !_isTouching && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _isTouching = true;
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.GetTouch(0).position;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            if (results.Count > 0)  // Si hay algún resultado, el toque está sobre un elemento de la interfaz de usuario
            {
                return;
            }
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
            {
                if (hit.collider.gameObject.CompareTag("Arista") && tipoActual == TipoObjeto.Camino)
                {
                    ColocarCamino();
                }
                else if (hit.collider.gameObject.CompareTag("Esquina") && tipoActual == TipoObjeto.Base)
                {
                    ColocarBase();
                    //EjecutarColocarBase(hit);
                    //canPlace = false; // Desactivar la capacidad de colocar después de que se ha colocado la base
                }
                // Nuevo if para manejar el caso de TipoObjeto.Pueblo
                else if (hit.collider.gameObject.CompareTag("Esquina") && tipoActual == TipoObjeto.Pueblo)
                {
                    ColocarPueblo();
                }
            }
        }
        else if (Input.touchCount == 0)
        {
            _isTouching = false;
        }
    }
    public void AllowPlace() // Método que permitiría colocar una base, podría ser llamado por un botón
    {
        canPlace = true;
    }
    public int DarTipo()
    {
        Debug.Log("Entre a dartipo");
        int tipo = 0; // Valor por defecto
        Debug.Log("tipo actual tiene:" + tipoActual);
        if (tipoActual == TipoObjeto.Camino)
            tipo = 1;
        else if (tipoActual == TipoObjeto.Base)
            tipo = 2;
        else if (tipoActual == TipoObjeto.Pueblo)
            tipo = 3;
        return tipo;
    }

    public void EjecutarColocarCamino(RaycastHit hit)
    {
        Debug.Log("EjecutarColocarCamino");
        // El objeto golpeado es una esquina.
        //currentCamino = Instantiate(prefabCamino, hit.collider.gameObject.transform.position, Quaternion.identity);
        currentCamino = Instantiate(prefabCamino, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation); //con rotacion
        currentCamino.GetComponent<NetworkObject>().Spawn();
        comprobarObjeto = hit.collider.gameObject.GetComponent<ComprobarObjeto>();

        if (comprobarObjeto != null)
        {
            // Almacenar el tipo de objeto que acabamos de colocar
            comprobarObjeto.tipoObjeto = TipoObjeto.Camino; // Puedes cambiar esto al tipo de objeto que corresponda
            Debug.Log("puse el tipo camino a: " + comprobarObjeto.tipoObjeto);
        }
        else
        {
            Debug.LogError("El objeto " + hit.collider.gameObject.name + " no tiene un script ComprobarObjeto.");
        }
        Debug.Log("el tipo del camino es " + tipoActual);
        tipoActual = TipoObjeto.Ninguno;
        //Debug.Log("Termino EjecutarColocarBase");
    }
    public void EjecutarColocarBase(RaycastHit hit)
    {
        Debug.Log("EntroColocar 2");

        // El objeto golpeado es una esquina.
        currentBase = Instantiate(prefabBase, hit.collider.gameObject.transform.position, Quaternion.identity);
        //Debug.Log("Luego de instatiate");
        currentBase.GetComponent<NetworkObject>().Spawn();

        // Obtener el componente ComprobarObjeto del objeto golpeado
        comprobarObjeto = hit.collider.gameObject.GetComponent<ComprobarObjeto>();
        //Debug.Log("el collider es : " + hit.collider.gameObject.name);
        //Debug.Log("comprobarobjeto al poner la base: " + comprobarObjeto);
        // Asegurarse de que el componente existe
        if (comprobarObjeto != null)
        {
            // Guardar una referencia a la pieza que acabamos de colocar
            //comprobarObjeto.objetoColocado = this; // esto pone ControldorColocarPieza
            //Debug.Log("EL OBJETO colocado es: " + comprobarObjeto.objetoColocado);

            // Almacenar el tipo de objeto que acabamos de colocar
            comprobarObjeto.tipoObjeto = TipoObjeto.Base; // Puedes cambiar esto al tipo de objeto que corresponda
            Debug.Log("puse el tipo de la base a: " + comprobarObjeto.tipoObjeto);
        }
        else
        {
            Debug.LogError("El objeto " + hit.collider.gameObject.name + " no tiene un script ComprobarObjeto.");
        }
        //Debug.Log("Antes de la asignación");
        //tipoActual = TipoObjeto.Base;
        //Debug.Log("Después de la asignación");
        Debug.Log("el tipo de la base colocada es " + tipoActual);
        tipoActual = TipoObjeto.Ninguno;
    }
    public void EjecutarColocarPueblo(RaycastHit hit)
    {
        //Debug.Log("EntroColocar 2");
        // El objeto golpeado es una esquina.
        currentPueblo = Instantiate(prefabPueblo, hit.collider.gameObject.transform.position, Quaternion.identity);
        //currentPueblo = Instantiate(prefabPueblo, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation); //con rotacion
        currentPueblo.GetComponent<NetworkObject>().Spawn();
        comprobarObjeto = hit.collider.gameObject.GetComponent<ComprobarObjeto>();

        if (comprobarObjeto != null)
        {
            // Almacenar el tipo de objeto que acabamos de colocar
            comprobarObjeto.tipoObjeto = TipoObjeto.Pueblo; // Puedes cambiar esto al tipo de objeto que corresponda
            Debug.Log("puse el tipo del pueblo a: " + comprobarObjeto.tipoObjeto);
        }
        else
        {
            Debug.LogError("El objeto " + hit.collider.gameObject.name + " no tiene un script ComprobarObjeto.");
        }
        Debug.Log("el tipo del camino es " + tipoActual);
        tipoActual = TipoObjeto.Ninguno;
        //Debug.Log("Termino EjecutarColocarBase");
    }
    /*public void ColocarPueblo(RaycastHit hit)
    {
        //Debug.Log("EntroColocar 2");
        // El objeto golpeado es una esquina.
        currentPueblo = Instantiate(prefabPueblo, hit.collider.gameObject.transform.position, Quaternion.identity);
        currentPueblo.GetComponent<NetworkObject>().Spawn();
        comprobarObjeto = hit.collider.gameObject.GetComponent<ComprobarObjeto>();

        if (comprobarObjeto != null)
        {
            // Almacenar el tipo de objeto que acabamos de colocar
            comprobarObjeto.tipoObjeto = TipoObjeto.Pueblo; // Puedes cambiar esto al tipo de objeto que corresponda
            Debug.Log("puse el tipo del pueblo a: " + comprobarObjeto.tipoObjeto);
        }
        else
        {
            Debug.LogError("El objeto " + hit.collider.gameObject.name + " no tiene un script ComprobarObjeto.");
        }
        Debug.Log("el tipo de la base colocada es " + tipoActual);
        tipoActual = TipoObjeto.Ninguno;
        //Debug.Log("Termino EjecutarColocarBase");
    }*/
    public void ColocarCamino()
    {
        AllowPlace();
        //Debug.Log("EntroColocar 1");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            EjecutarColocarCamino(hit);
            confirmCaminoButton.gameObject.SetActive(true);
        }
        //ARCursor arCursor = FindObjectOfType<ARCursor>();
        if (ARCursor.Instance != null)
        {
            ARCursor.Instance.ActivatePlacementMode();
        }
    }

    public void ColocarBase()
    {
        Debug.Log("EntroColocar 1");
        AllowPlace();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //Debug.Log("Lanzando raycast");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            //Debug.Log("Antes EjecutarColocarBase");
            EjecutarColocarBase(hit);
            //Debug.Log("Despues EjecutarColocarBase");
            confirmBaseButton.gameObject.SetActive(true); // Habilita el botón de confirmación
        }
        //ARCursor arCursor = FindObjectOfType<ARCursor>();
        if (ARCursor.Instance != null)
        {
            ARCursor.Instance.ActivatePlacementMode();
        }
    }
    
    public void ColocarPueblo()
    {
        AllowPlace();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            //Debug.Log("Antes EjecutarColocarBase");
            EjecutarColocarPueblo(hit);
            //Debug.Log("Despues EjecutarColocarBase");
            confirmPuebloButton.gameObject.SetActive(true); // Habilita el botón de confirmación
        }
        //ARCursor arCursor = FindObjectOfType<ARCursor>();
        if (ARCursor.Instance != null)
        {
            ARCursor.Instance.ActivatePlacementMode();
        }
    }

    public void ConfirmarBase()
    {
        if (currentBase == null)
        {
            Debug.Log("currentBase es null");
        }
        // Verifica si la base actual no es nula y se puede colocar
        if (currentBase != null && canPlace)
        {
            // Deshabilita el botón de confirmación
            confirmBaseButton.gameObject.SetActive(false);

            // Desactiva la capacidad de mover la base
            canPlace = false;

            // Borra la referencia a la base actual
            currentBase = null;
        }
        else
        {
            // Puedes mostrar algún mensaje o realizar alguna acción si la base no puede ser confirmada
            Debug.Log("La base no puede ser confirmada");
        }
    }
    public void ConfirmarCamino()
    {
        if (currentCamino == null)
        {
            Debug.Log("currentBase es null");
        }
        // Verifica si la base actual no es nula y se puede colocar
        if (currentCamino != null && canPlace)
        {
            confirmCaminoButton.gameObject.SetActive(false);
            canPlace = false;
            currentCamino = null;
        }
        else
        {
            Debug.Log("El camino no puede ser confirmado");
        }
    }

    public void ConfirmarPueblo()
    {
        if (currentPueblo != null && canPlace)
        {
            // Deshabilita el botón de confirmación
            confirmPuebloButton.gameObject.SetActive(false);
            canPlace = false;
            currentPueblo = null;
        }
    }
    public PlayerColor GetCurrentPlayerColor()
    {
        // Asegúrate de que hay jugadores y de que el índice es válido.
        if (PlayerNetwork.Instance.playerData.Count > 0 && PlayerNetwork.Instance.currentTurnIndex < PlayerNetwork.Instance.playerData.Count)
        {
            // Convierte el FixedString64Bytes a string
            string colorString = PlayerNetwork.Instance.playerData[PlayerNetwork.Instance.currentTurnIndex].colorJugador.ToString();
            Debug.Log("Color obtenido del jugador: " + colorString);

            // Intenta convertir la cadena al valor enum PlayerColor
            if (Enum.TryParse(colorString, true, out PlayerColor parsedColor))
            {
                Debug.Log("El color se convirtió exitosamente al enum: " + parsedColor.ToString());
                return parsedColor;
            }
            else
            {
                Debug.LogError("El color del jugador en playerData no corresponde a un valor en PlayerColor. Color problemático: " + colorString);
            }
        }
        else
        {
            Debug.LogError("No se pudo obtener el color del jugador porque el índice del turno actual es inválido o no hay datos de jugador.");
        }

        return PlayerColor.Azul; // Devuelve un valor predeterminado en caso de error.
    }


    /*public void ActivarColocacion(TipoObjeto tipo)
    {
        tipoActual = tipo;
    }
    */

}
