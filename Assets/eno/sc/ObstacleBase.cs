using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    [SerializeField]
    List<GameObject> itemPrefabs = new List<GameObject>();

    [SerializeField]
    float dropItemCnt;

    [SerializeField]
    int hp = 5;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DeadZone") Destroy(gameObject);
        if(other.gameObject.tag == "Bullet")
        {
            hp--;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void SpawnItems()
    {
        for(int i = 0; i < dropItemCnt; i++)
        {
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            int rnd = UnityEngine.Random.Range(0, itemPrefabs.Count);
            Instantiate(itemPrefabs[rnd], transform.position, Quaternion.identity);
        }
    }
}
