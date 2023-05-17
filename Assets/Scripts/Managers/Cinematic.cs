using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic : MonoBehaviour
{
    [SerializeField] private float cinematicTime = 0f;
    [SerializeField] private float cinematicPart;
    void Update()
    {
        cinematicTime += Time.deltaTime;
        if (cinematicTime > 59f && cinematicPart == 1 || Input.GetKeyDown(KeyCode.Escape) && cinematicPart == 1)
        {
            SceneManager.LoadScene(2);
            Global.nivel = 2;
            PlayerPrefs.SetInt("LevelMax",Global.nivel);
 
        }
        if(cinematicTime > 17f && cinematicPart == 2 || Input.GetKeyDown(KeyCode.Escape) && cinematicPart == 2)
        {

            SceneManager.LoadScene(4);
            Global.nivel = 4;
            PlayerPrefs.SetInt("LevelMax",Global.nivel);
        
        }
    }
}