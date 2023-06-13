using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentificadorParcela : MonoBehaviour
{
    public string identificador;
    public TipoPieza tipoPieza;
    public List<TipoRecurso> recursosGenerados;

    public enum TipoPieza
    {
        Casa,
        Pueblo
    }

    [System.Serializable]
    public class TipoRecurso
    {
        public int numeroDado;
        public string nombreRecurso;
    }

    public void IncrementarRecursos(int resultadoDado, ref int maderaCount, ref int ladrilloCount, ref int ovejaCount, ref int piedraCount, ref int trigoCount)
    {
        TipoRecurso recurso = recursosGenerados.Find(r => r.numeroDado == resultadoDado);
        if (recurso != null)
        {
            IncrementarRecurso(recurso.nombreRecurso, ref maderaCount, ref ladrilloCount, ref ovejaCount, ref piedraCount, ref trigoCount);
        }
    }

    private void IncrementarRecurso(string nombreRecurso, ref int maderaCount, ref int ladrilloCount, ref int ovejaCount, ref int piedraCount, ref int trigoCount)
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
}
