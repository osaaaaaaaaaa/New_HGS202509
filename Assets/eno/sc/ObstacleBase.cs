using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
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
}
