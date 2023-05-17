using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class NPCInteractable : MonoBehaviour
{
    //public Textos texto;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text nameText; 
    [SerializeField] private GameObject camaraNPC;
    [SerializeField] private float unScaledTime;
    [SerializeField, TextArea(4,6)]private string[] dialogueLines;
    [SerializeField, TextArea(4,6)]private string[] nameLines;
    [SerializeField, TextArea(4,6)]private string[] secondDialogue;
    public Transform respawnPoint;

  //animNPC
    
    private bool didDialogueStart;
    private int lineIndex;

    private AudioSource NPCAudio;
    public AudioClip TalkNPC;
    [SerializeField]private float typingTime;
    [SerializeField] private int NPCTimeToSound;

    


    private Animator animNPC;

    void Awake()
    {
        animNPC = GetComponent<Animator>();
        NPCAudio = GetComponent<AudioSource>();
        NPCAudio.clip = TalkNPC;

        if(Global.ReturnHome == true)
        {
            this.gameObject.transform.position = respawnPoint.position;
            this.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            Global.ReturnHome = true;
            for (int i = 0; i <= dialogueLines[i].Length; i++) 
            {
                dialogueLines[i] = secondDialogue[i];
            }
                
        }
        
    }
    public void Interact()
    {
        Debug.Log("Interact!");
        if(!didDialogueStart)
        {
           StartDialogue(); 
        }
        else if(dialogueText.text == dialogueLines[lineIndex])
        {
            NextDialogueLine();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[lineIndex];
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        camaraNPC.SetActive(true);
        lineIndex = 0;
        animNPC.SetBool("Charla", true);
        nameText.text = nameLines[lineIndex];
        Time.timeScale = 0;
        Global.PlayerScript = true;
        StartCoroutine(ShowLine());
        //NPCAudio.PlayOneShot(TalkNPC);
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            camaraNPC.SetActive(false);
            Global.PlayerScript = false;
            animNPC.SetBool("Charla", false);
            Time.timeScale = 1;
            
            if(this.gameObject.layer == 6)
            {
                this.gameObject.transform.position = respawnPoint.position;
                this.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                Global.ReturnHome = true;
                for (int i = 0; i <= dialogueLines[i].Length; i++) 
                {
                    dialogueLines[i] = secondDialogue[i];
                }
            }
            if(this.gameObject.layer == 7)
            {
                Debug.Log("MenuNiveles");
                InGameMenu.Instance.LevelSelect();
                Time.timeScale = 0;
            }
            
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        //NPCAudio.PlayOneShot(TalkNPC);
        int NPCIndex = 0;
        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            if(NPCIndex % NPCTimeToSound == 0)
            {
                NPCAudio.Play();
            }
            
            NPCIndex++;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    /*public void EntrarCasa()
    {
        Debug.Log("Entrar a casa");
        
    }*/
}
