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
    GameObject[] dropItemObjs;

    [SerializeField]
    int hp = 5;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            hp--;
            if (hp <= 0)
            {
                for (var i = 0; i < dropItemCnt; i++)
                {
                    Instantiate(dropItemObjs[Random.Range(0, dropItemObjs.Length)], transform.position + new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f)), Quaternion.identity);
                }

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
