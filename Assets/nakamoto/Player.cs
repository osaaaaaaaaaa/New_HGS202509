using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    //----------------------
    // フィールド

    // 変数
    int itemCount = 0;
    bool isDead = false;
    bool isShot = true;
    bool isBack = false;
    float time = 0f;

    // 外部設定
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject bulletObj;
    [SerializeField] float shootingSensation = 0.2f;
    [SerializeField] float bulletSpeed = 20f;

    // 定数
    const float MOVE_SPEED = 500f;

    //----------------------
    // メソッド

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return; // 死亡していたら処理しない

        // 入力を取得
        var v1 = Input.GetAxis("Vertical");
        var h1 = Input.GetAxis("Horizontal");

        time += Time.deltaTime;

        // 弾を撃つ
        if (time > shootingSensation)
        {
            time = 0f;

            // スティックが倒されているとき
            if (h1 != 0 || v1 != 0)
            {
                var bullet = Instantiate(bulletObj, transform.position, Quaternion.identity);

                // 飛ぶ方向
                var direction = new Vector3(h1, v1, 0);
                var vector = direction.normalized;

                bullet.GetComponent<Rigidbody>().linearVelocity = vector * bulletSpeed; // 弾を飛ばす
            }
        }

        // スティック入力時
        if (h1 != 0 || v1 != 0)
        {
            var direction = new Vector3(h1, v1, 0);
            transform.localRotation = Quaternion.LookRotation(direction);   // 向きを変える

            var vector = direction.normalized;

            if (Input.GetButtonDown("Shot") && isShot)
            {
                isShot = false; // 連射防止
                rb.linearVelocity = Vector3.zero; // 速度をリセット
                rb.AddForce(vector * MOVE_SPEED); // 力を加える
            }
            else if(Input.GetButtonDown("Shot") && !isShot && !isBack)
            {
                isBack = true;
                rb.linearVelocity = Vector3.zero; // 速度をリセット
                this.transform.DOLocalMove(shotPoint.position, 1).OnComplete(() => {
                    isShot = true; // 移動完了後に撃てるようにする
                    isBack = false;
                });
            }
        }
    }

    /// <summary>
    /// ヒット処理
    /// </summary>
    private void HitObj()
    {
        if (itemCount <= 0)
        {
            isDead = true;
            rb.linearVelocity = Vector3.zero;

            // ゲームオーバー演出

            return;
        }

        this.transform.DOLocalMove(shotPoint.position, 1).OnComplete(() => {
            isBack = false;
            isShot = true; // 移動完了後に撃てるようにする
        });

        if (itemCount < 10) 
            itemCount = 0;
        else
            itemCount = itemCount / 2; // アイテムを半分失う
    }

    /// <summary>
    /// トリガー当たり判定
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Point")
        {
            // ぶつかった新たなポイントを取得
            rb.linearVelocity = Vector3.zero;               // 速度をリセット
            transform.position = other.transform.position;
            shotPoint = other.transform;
            isShot = true;
        }

        if(other.tag == "Food")
        {
            itemCount++;

            //+++++ スコア加算処理 (itemCount * foodObjのスコア)
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Object")
        {
            HitObj();
        }
    }
}
