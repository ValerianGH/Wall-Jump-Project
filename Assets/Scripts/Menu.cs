using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PLAY()
    {
        SceneManager.LoadScene("Main");
    }

    public void CONTROLS()
    {
        SceneManager.LoadScene("Controls");
    }

    public void CREDITS()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LOADINGSCREEN()
    {
        SceneManager.LoadScene("LoadingScreen");
    }

    public void MENU()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QUIT()
    {
        Application.Quit();
    }
}