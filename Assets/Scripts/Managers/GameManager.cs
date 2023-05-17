using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject[] puntosVidas;

    public GameObject IconoAgua;

    public GameObject IconoFuego;

    public static GameManager Instance;

    public GameObject CorrientesAgua;

    public GameObject puntoApuntando;  
    
    public GameObject jugador;


    void Awake() 
    {
        if( Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && Global.PlayerScript == false)
        {

            CorrientesAgua.GetComponent<Collider>().enabled = false;
            IconoAgua.SetActive(false);
            IconoFuego.SetActive(true);
        }


        if(Input.GetKeyDown(KeyCode.Alpha4) && Global.PlayerScript == false)
        {

            CorrientesAgua.GetComponent<Collider>().enabled = true;
            IconoAgua.SetActive(true);
            IconoFuego.SetActive(false);
        }
                
        if(Input.GetButton("Fire2") && Global.PlayerScript == false)
        {
            puntoApuntando.SetActive(true);
        }
        else
        {
            puntoApuntando.SetActive(false);
        }
    }
    
    public void Impacto()
    {
        Global.vidas--;

        if(Global.vidas == 0)
        {
            puntosVidas[0].SetActive(false);
            puntosVidas[1].SetActive(false);
            puntosVidas[2].SetActive(false);
            Global.nivel = PlayerPrefs.GetInt("LevelMax");
            SceneManager.LoadScene(Global.nivel);
            Global.vidas = 3;
        }


        if(Global.vidas == 1)
        {
            puntosVidas[0].SetActive(true);
            puntosVidas[1].SetActive(false);
            puntosVidas[2].SetActive(false);

        }

        if(Global.vidas == 2)
        {
            puntosVidas[0].SetActive(true);
            puntosVidas[1].SetActive(true);
            puntosVidas[2].SetActive(false);

        }
        
        if(Global.vidas == 3)
        {
            puntosVidas[0].SetActive(true);
            puntosVidas[1].SetActive(true);
            puntosVidas[2].SetActive(true);

        }
    }
    public void Recuperacion()
    {
        puntosVidas[0].SetActive(true);
        puntosVidas[1].SetActive(true);
        puntosVidas[2].SetActive(true);        
    }
}
