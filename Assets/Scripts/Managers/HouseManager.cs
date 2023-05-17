using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public Transform houseRespawn;
    public GameObject JugadorExit;
    void Awake()
    {
        if(Global.OutHouse == true)
        {
            Global.OutHouse = false;
            JugadorExit.transform.position = houseRespawn.position;
        }
    }
}
