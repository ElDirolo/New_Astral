using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    
    public class Pool
    {

        public string parentName;

        public GameObject power;
        
        public int size;
        
        public List<GameObject> pooledPowers;        
    }

    public List<Pool> powerList;
    
    public static PoolManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GameObject ball;
        foreach ( Pool pool in powerList)
        {
            GameObject parent = new GameObject(pool.parentName);

            for ( int i = 0; i < pool.size; i++)
            {
                ball = Instantiate(pool.power);
                ball.transform.SetParent(parent.transform);
                ball.SetActive(false);
                pool.pooledPowers.Add(ball);
            }
        }
        
        
    }

    public GameObject GetPooledPower(int powerType, Vector3 position, Quaternion rotation)
    {
       for ( int i = 0; i < powerList[powerType].size; i++)
        {
            if(!powerList[powerType].pooledPowers[i].activeInHierarchy)
            {
                GameObject objectToSpwan;
                objectToSpwan = powerList[powerType].pooledPowers[i];
                objectToSpwan.transform.position = position;
                objectToSpwan.transform.rotation = rotation;
                return objectToSpwan;
            }
        }
        return null;
        
    }
}
