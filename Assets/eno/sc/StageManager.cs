using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    #region オブジェクト生成関連

    [SerializeField]
    float spawnInterval = 0;

    [SerializeField] 
    List<GameObject> obstaclePrefabs = new List<GameObject>();

    [SerializeField]
    Transform stageParent;

    [SerializeField]
    Transform spawnTopLeft;

    [SerializeField]
    Transform spawnTopRight;

    enum SpawnPoint
    {
        Left,
        Right,
    }
    Dictionary<GameObject, SpawnPoint> spawnedObjs = new Dictionary<GameObject, SpawnPoint>();

    const int spawnCntMax = 6;
    const int spawnCntMin = 0;
    #endregion

    public void StartSpawnObstacles()
    {
        StartCoroutine(SpawnObstaclesCoroutine());
    }

    public void StopSpawnObstacles()
    {
        StopCoroutine(SpawnObstaclesCoroutine());
    }

    IEnumerator SpawnObstaclesCoroutine()
    {
        while (true)
        {
            SpawnObstacles(UnityEngine.Random.Range(spawnCntMin, spawnCntMax));
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector2 GetGeeneratePos()
    {
        float posX = UnityEngine.Random.Range(spawnTopLeft.position.x, spawnTopRight.position.x);
        return new Vector2(posX, spawnTopLeft.position.y);
    }

    void SpawnObstacles(int num)
    {
        for (int i = 0; i < num; i++)
        {
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            int rndPick = UnityEngine.Random.Range(0, obstaclePrefabs.Count);
            Vector3 pos = GetGeeneratePos();
            GameObject obj = Instantiate(obstaclePrefabs[rndPick], pos, Quaternion.identity);
            obj.transform.parent = stageParent;
        }
    }
}
