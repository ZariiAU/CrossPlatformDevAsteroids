using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class AppControlStuff : MonoBehaviour
{
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }

    private void OnPause(InputAction.CallbackContext value)
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
            SwitchScene("MainMenu");
        else
        {
            QuitGame();
        }
    }

    public void SwitchScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
