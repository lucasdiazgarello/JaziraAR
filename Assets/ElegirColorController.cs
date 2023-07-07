using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
public class ElegirColorController : MonoBehaviour
{
    //carga la escena que queres que abra al tocar el botón
    public void LoadGameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
