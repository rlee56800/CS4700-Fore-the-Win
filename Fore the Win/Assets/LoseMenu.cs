using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene(2);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
