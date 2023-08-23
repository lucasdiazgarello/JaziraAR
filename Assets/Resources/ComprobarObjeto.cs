using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class ComprobarObjeto : NetworkBehaviour
{
    //public ColocarPieza objetoColocado; // la referencia al objeto colocado
    public TipoObjeto tipoObjeto; // el tipo de objeto que está colocado
    //public bool esArista;
    /*public void CambiarTipoObjeto(string nombre)
    {
        switch (nombre)
        {
            case "Camino":
                tipoObjeto = TipoObjeto.Camino;
                break;
            case "Base":
                tipoObjeto = TipoObjeto.Base;
                break;
            case "Pueblo":
                tipoObjeto = TipoObjeto.Pueblo;
                break;
        }
    }*/
}
