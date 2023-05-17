using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuFinalCanvas : MonoBehaviour
{

    public Button backButton;
    void Start()
    {
        backButton.Select();
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void ExitGameFinal()
    {
        Debug.Log("Se termino");
        Application.Quit();
    }


    public void ReturnLobby()
    {

        Time.timeScale = 1f;
        Global.PlayerScript = false;
        SceneManager.LoadScene(0);
    }
}
