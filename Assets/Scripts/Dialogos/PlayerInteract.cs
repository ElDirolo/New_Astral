using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private GameObject intercativeObject;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Global.PauseMenu == true && Global.WorldLevels == false)
        {
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere (transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if(collider.TryGetComponent(out NPCInteractable npcInteractable) && collider.gameObject.CompareTag("NPC"))
                {
                    npcInteractable.Interact();
                }

                if(collider.gameObject.CompareTag("Casa") && Global.PlayerScript == false)
                {
                    SceneManager.LoadScene(7);
                    Global.nivel = 7;
                    PlayerPrefs.SetInt("LevelMax",Global.nivel);
                    Global.OutHouse = true;
                }

                if(collider.gameObject.CompareTag("PuertaFinal") && Global.PlayerScript == false)
                {
                    SceneManager.LoadScene(3);
                    Global.nivel = 3;
                    PlayerPrefs.SetInt("LevelMax",Global.nivel);
                }

                if(collider.gameObject.CompareTag("ExitHouse") && Global.PlayerScript == false)
                {
                    SceneManager.LoadScene(4);
                    Global.nivel = 4;
                    PlayerPrefs.SetInt("LevelMax",Global.nivel);
                }

                if(collider.gameObject.CompareTag("EndGame") && Global.PlayerScript == false)
                {
                    Debug.Log("Papas");
                    Global.nivel = 4;
                    PlayerPrefs.SetInt("LevelMax",Global.nivel);
                    SceneManager.LoadScene(8);
                }

            }
        }
        
        if (GetInteractableObject() != null)
        {
            Show();
        }
        else
        {
            Hide();
        }

    }

    public NPCInteractable GetInteractableObject()
    {
        float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere (transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if(collider.TryGetComponent(out NPCInteractable npcInteractable) && collider.gameObject.CompareTag("NPC"))
            {
                return npcInteractable;
            }
            
        }
        return null;
        
    }
    private void Show()
    {
        intercativeObject.SetActive(true);
    }

    private void Hide()
    {
        intercativeObject.SetActive(false);
    }





}

