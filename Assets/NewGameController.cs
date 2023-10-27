using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
public class NewGameController : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("JuegoScene");
    }
}
