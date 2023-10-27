using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
using Unity.Services.Authentication;

public class MainMenuController : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("NuevaPartidaScene");
    }

    public void LoadJoinScene()
    {
        SceneManager.LoadScene("BuscarPartidaScene");
    }

    public void LoadJugarScene()
    {
        SceneManager.LoadScene("JugarScene");
    }
}
