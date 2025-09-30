using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    [SerializeField]
    float dropItemCnt;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Default")
        {
            Destroy(gameObject);
        }
    }
}
