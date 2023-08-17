using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentificadorParcela : MonoBehaviour
{
    //public string identificador;
    //public TipoPieza tipoPieza;
    public List<TipoRecurso> recursosGenerados;
    public List<Collider> collidersParcela;
    //public ColocarPieza colocarPieza;

    [System.Serializable]
    public class TipoRecurso
    {
        public int numeroficha;
        public string nombreRecurso;
    }
    public List<Collider> GetCollidersParcela(string parcelaName)
    {
        GameObject parcelaObject = GameObject.Find(parcelaName);
        if (parcelaObject == null)
        {
            Debug.LogError("No se pudo encontrar una parcela con el nombre " + parcelaName);
            return null;
        }

        IdentificadorParcela identificador = parcelaObject.GetComponent<IdentificadorParcela>();
        if (identificador == null)
        {
            Debug.LogError("El objeto encontrado no tiene un componente IdentificadorParcela");
            return null;
        }

        return identificador.collidersParcela;
    }
}
