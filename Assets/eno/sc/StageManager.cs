using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    #region 生成関連

    #region オブジェクト
    [SerializeField]
    float spawnObstacleIntervalMin = 0.5f;
    [SerializeField]
    float spawnObstacleIntervalMax = 2f;

    [SerializeField] 
    List<GameObject> obstaclePrefabs = new List<GameObject>();
    #endregion

    #region チェックポイント
    [SerializeField]
    float spawnPointIntervalMin = 0.5f;
    [SerializeField]
    float spawnPointIntervalMax = 2f;

    [SerializeField]
    GameObject pointPrefab;
    #endregion

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

    const int spawnCntMax = 4;
    const int spawnCntMin = 0;
    #endregion

    public void StartSpawnObstacles()
    {
        StartCoroutine(SpawnObstaclesCoroutine());
        StartCoroutine(SpawnPointCoroutine());
    }

    public void StopSpawnObstacles()
    {
        StopCoroutine(SpawnObstaclesCoroutine());
        StopCoroutine(SpawnPointCoroutine());
    }

    IEnumerator SpawnPointCoroutine()
    {
        while (true)
        {
            var pos = GetGeeneratePos();
            var obj = Instantiate(pointPrefab, pos, Quaternion.identity);
            obj.transform.parent = stageParent;
            yield return new WaitForSeconds(UnityEngine.Random.Range(spawnPointIntervalMin, spawnPointIntervalMax));
        }
    }

    IEnumerator SpawnObstaclesCoroutine()
    {
        while (true)
        {
            SpawnObstacles(UnityEngine.Random.Range(spawnCntMin, spawnCntMax));
            yield return new WaitForSeconds(UnityEngine.Random.Range(spawnObstacleIntervalMin, spawnObstacleIntervalMax));
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
