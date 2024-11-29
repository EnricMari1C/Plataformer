using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_canvasMainMenu : MonoBehaviour
{

    public void ChangeScene(int scene) 
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }

}
