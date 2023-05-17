using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private LayerMask gorundLayer;

    void Update()
    {
        transform.position += transform.forward * 20 * Time.deltaTime;
    }

        
    void OnTriggerEnter(Collider collider)
    {
        if (gorundLayer == (gorundLayer | (1 << collider.gameObject.layer)))
        {
            this.gameObject.SetActive(false);
        }

        

    }
}
