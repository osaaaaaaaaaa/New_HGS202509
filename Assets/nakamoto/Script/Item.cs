using DG.Tweening;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public int score = 100;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed = 8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ƒ‰ƒ“ƒ_ƒ€‚È—Í‚ð—^‚¦‚é
        var x = Random.Range(0, 360f);
        var y = Random.Range(0, 360f);
        var direction = new Vector3(x, y, 0);
        rb.AddForce(direction.normalized * speed, ForceMode.Impulse);

        // ƒ‰ƒ“ƒ_ƒ€‚È‰ñ“]—Í‚ð—^‚¦‚é
        var x2 = Random.Range(0, 50f);
        var y2 = Random.Range(0, 50f);
        var z = Random.Range(0, 50f);
        rb.AddTorque(new Vector3(x2,y2,z));

        transform.DOScale(2.2f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeadZone") Destroy(gameObject);
    }
}
