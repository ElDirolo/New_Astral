using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryZone : MonoBehaviour
{
    private AudioSource recoveryHeald;
    public AudioClip healdSound;

    void Awake() 
    {
        recoveryHeald = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Curateputa");
            Global.vidas = 3;
            GameManager.Instance.Recuperacion();
            recoveryHeald.PlayOneShot(healdSound);
        }
    }
}
