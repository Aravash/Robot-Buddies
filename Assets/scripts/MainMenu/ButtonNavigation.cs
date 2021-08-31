using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonNavigation : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("Assets/Scenes/SampleScene.unity");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
