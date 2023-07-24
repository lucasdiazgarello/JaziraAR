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

    /*public enum TipoPieza
    {
        Base,
        Pueblo
    }*/

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

    /*public void IncrementarRecursos(int resultadoDado, ref int maderaCount, ref int ladrilloCount, ref int ovejaCount, ref int piedraCount, ref int trigoCount)
    {
        TipoRecurso recurso = recursosGenerados.Find(r => r.numeroficha == resultadoDado);
        if (recurso != null)
        {
            IncrementarRecurso(recurso.nombreRecurso, ref maderaCount, ref ladrilloCount, ref ovejaCount, ref piedraCount, ref trigoCount);

            ColocarPieza colocarPieza = GetComponent<ColocarPieza>();
            if (colocarPieza != null)
            {
                Collider collider = collidersParcela.Find(c => c.gameObject.CompareTag("Esquina"));
                if (collider != null)
                {
                    if (colocarPieza.tieneBase)
                    {
                        IncrementarRecurso(recurso.nombreRecurso, ref maderaCount, ref ladrilloCount, ref ovejaCount, ref piedraCount, ref trigoCount);
                    }
                    else if (colocarPieza.tienePueblo)
                    {
                        IncrementarRecurso(recurso.nombreRecurso, ref maderaCount, ref ladrilloCount, ref ovejaCount, ref piedraCount, ref trigoCount);
                        IncrementarRecurso(recurso.nombreRecurso, ref maderaCount, ref ladrilloCount, ref ovejaCount, ref piedraCount, ref trigoCount);
                    }
                }
            }
        }
    }*/
    /*
    public void IncrementarRecursos(int resultadoDado, ref int maderaCount, ref int ladrilloCount, ref int ovejaCount, ref int piedraCount, ref int trigoCount)
    {
        TipoRecurso recurso = recursosGenerados.Find(r => r.numeroficha == resultadoDado);
        if (recurso != null)
        {
            IncrementarRecurso(recurso.nombreRecurso, ref maderaCount, ref ladrilloCount, ref ovejaCount, ref piedraCount, ref trigoCount);
        }
    }
    */

    /*private void IncrementarRecurso(string nombreRecurso, ref int maderaCount, ref int ladrilloCount, ref int ovejaCount, ref int piedraCount, ref int trigoCount)
    {
        // Incrementar los recursos según el nombre y el tipo de pieza
        if (nombreRecurso == "Madera")
        {
            maderaCount += 1;
        }
        else if (nombreRecurso == "Ladrillo")
        {
            ladrilloCount += 1;
        }
        else if (nombreRecurso == "Oveja")
        {
            ovejaCount += 1;
        }
        else if (nombreRecurso == "Piedra")
        {
            piedraCount += 1;
        }
        else if (nombreRecurso == "Trigo")
        {
            trigoCount += 1;
        }
    }*/
    /*
    private void IncrementarRecurso(string nombreRecurso, ref int maderaCount, ref int ladrilloCount, ref int ovejaCount, ref int piedraCount, ref int trigoCount)
    {
        if (tipoPieza == TipoPieza.Base)
        {
            switch (nombreRecurso)
            {
                case "Madera":
                    maderaCount += 1;
                    break;
                case "Ladrillo":
                    ladrilloCount += 1;
                    break;
                case "Oveja":
                    ovejaCount += 1;
                    break;
                case "Piedra":
                    piedraCount += 1;
                    break;
                case "Trigo":
                    trigoCount += 1;
                    break;
            }
        }
        switch (nombreRecurso)
        {
            case "Madera":
                maderaCount += 2;
                break;
            case "Ladrillo":
                ladrilloCount += 2;
                break;
            case "Oveja":
                ovejaCount += 2;
                break;
            case "Piedra":
                piedraCount += 2;
                break;
            case "Trigo":
                trigoCount += 2;
                break;
        }
    }*/
}
