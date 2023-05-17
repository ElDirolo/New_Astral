using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPoint;

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Activa");
            other.gameObject.transform.position = respawnPoint.position;
            GameManager.Instance.Impacto();
        }
    }
}
