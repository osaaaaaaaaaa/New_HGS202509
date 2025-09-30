using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
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
}
