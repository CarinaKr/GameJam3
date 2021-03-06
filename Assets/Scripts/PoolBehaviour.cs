﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBehaviour : MonoBehaviour
{
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private GameObject collectablePrefab;


    private GameObject[] pool;
    private int poolIndex;

    // Start is called before the first frame update
    void Awake()
    {
        pool = new GameObject[poolSize];
        for(int i=0;i<poolSize;i++)
        {
            pool[i] = Instantiate(collectablePrefab, transform);
            pool[i].SetActive(false);
        }
    }
    
    public GameObject GetObject()
    {
        foreach(GameObject poolObject in pool)
        {
            if(!poolObject.gameObject.activeSelf)
            {
                poolObject.gameObject.SetActive(true);
                return poolObject;
            }
        }

        return null;
    }

    public void ReleaseObject(GameObject poolObject)
    {
        poolObject.SetActive(false);
    }
}
