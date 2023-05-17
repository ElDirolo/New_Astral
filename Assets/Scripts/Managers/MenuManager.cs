using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuStart;
    public GameObject playMenu;
    public GameObject profileMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public Button playButton;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMainMenu();
        }
    }
    public void OpenMainMenu()
    {
        menuStart.SetActive(false);
        playMenu.SetActive(true);
        profileMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        playButton.Select();

    }

    public void OpenPlay()
    {
        menuStart.SetActive(false);
        playMenu.SetActive(false);
        profileMenu.SetActive(true);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);

    }

    public void OpenOptions()
    {
        menuStart.SetActive(false);
        playMenu.SetActive(false);
        profileMenu.SetActive(false);
        optionsMenu.SetActive(true);     
        creditsMenu.SetActive(false);

    }

    public void OpenAudioOptions()
    {
        menuStart.SetActive(false);
        playMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);

    }

    public void OpenGraphicsOptions()
    {
        menuStart.SetActive(false);
        playMenu.SetActive(false);
        profileMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);

    }

    public void OpenControlsOptions()
    {
        menuStart.SetActive(false);
        playMenu.SetActive(false);
        profileMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);

    }

    public void OpenCredits()
    {
        menuStart.SetActive(false);
        playMenu.SetActive(false);
        profileMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(true);

    }

    public void OpenExit()
    {
        menuStart.SetActive(false);
        playMenu.SetActive(false);
        profileMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);

    }

    

    public void LoadScene()
    {
        SceneManager.LoadScene("CinematicaPart1");
    }

    public void ContinueGame()
    {
        Global.nivel = PlayerPrefs.GetInt("LevelMax");
        SceneManager.LoadScene(Global.nivel);
    }
    public void NewGame()
    {
        Global.nivel = 1;
        //SceneManager.LoadScene(Global.nivel);
        Invoke("ReseteoJuego", 2);
        PlayerPrefs.SetInt("LevelMax",Global.nivel);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReseteoJuego()
    {
        SceneManager.LoadScene(Global.nivel);
    }


}
