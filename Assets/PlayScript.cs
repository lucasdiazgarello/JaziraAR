using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayScript : MonoBehaviour
{
    public GameObject canvasnp;
    public GameObject canvasbp;
    public GameObject nuevapartida;
    public GameObject buscarpartida;

    // Start is called before the first frame update
    void Start()
    {
        canvasbp.SetActive(false);
        canvasnp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NuevaPartida()
    {
        nuevapartida.SetActive(false);
        buscarpartida.SetActive(false);
        canvasnp.SetActive(true);

    }

    public void BuscarPartida()
    {
        nuevapartida.SetActive(false);
        buscarpartida.SetActive(false);
        canvasbp.SetActive(true);

    }

}
