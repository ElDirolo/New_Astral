using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjetoInteractivo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject intercativeObject;
    public Transform interctPlayer;

    void Update()
    {
        float zoneInteract = 2f;

        if(Vector3.Distance(transform.position,interctPlayer.position) < zoneInteract )
        {       
            intercativeObject.SetActive(true);
            //Poner que se carage la escena de la casa
        }
        else
        {
            intercativeObject.SetActive(false);
        }

    }


        
}
