using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    //----------------------
    // フィールド

    // 変数
    int itemCount = 0;
    public int ItemCount { get { return itemCount; } set { itemCount = value; } }

    bool isDead = false;
    bool isShot = true;
    bool isBack = false;
    float plScale = 0.75f;
    float time = 0f;

    // 外部設定
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject bulletObj;
    [SerializeField] GameObject arrowObj;
    [SerializeField] float shootingSensation = 0.2f;
    [SerializeField] float bulletSpeed = 20f;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip eatSE;
    [SerializeField] AudioClip hitSE;
    [SerializeField] AudioClip moveSE;

    // 定数
    const float MOVE_SPEED = 500f;
    const float ROTATE_SPEED = 20f;

    //----------------------
    // メソッド

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arrowObj.SetActive(false);
        transform.localScale = new Vector3(plScale, plScale, plScale);  
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isStartGame || GameManager.Instance.isEndGame) return; // 死亡していたら処理しない

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
            arrowObj.SetActive(true);
            var direction = new Vector3(h1, v1, 0);

            // ベクトルから回転を作成（forward を XY 平面に対応させる）
            Quaternion targetRot = Quaternion.LookRotation(Vector3.forward, direction);

            // スムーズに向きを変える
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, ROTATE_SPEED * Time.deltaTime);

            var vector = direction.normalized;

            if (Input.GetButtonDown("Shot") && isShot || Input.GetMouseButtonDown(0) && isShot)
            {
                audioSource.PlayOneShot(moveSE);
                isShot = false; // 連射防止
                rb.linearVelocity = Vector3.zero; // 速度をリセット
                rb.angularVelocity = Vector3.zero; // 回転速度をリセット
                rb.AddForce(vector * MOVE_SPEED); // 力を加える
            }
            else if(Input.GetButtonDown("Shot") && !isShot && !isBack || Input.GetMouseButtonDown(0) && !isShot && !isBack)
            {
                audioSource.PlayOneShot(moveSE);
                isBack = true;
                rb.linearVelocity = Vector3.zero; // 速度をリセット
                rb.angularVelocity = Vector3.zero; // 回転速度をリセット

                this.transform.DOLocalMove(shotPoint.position, 1).OnComplete(() => {
                    isShot = true; // 移動完了後に撃てるようにする
                    isBack = false;
                });
            }
        }
        else
        {
            arrowObj.SetActive(false);
        }
    }

    /// <summary>
    /// ヒット処理
    /// </summary>
    private void HitObj()
    {
        if (itemCount <= 0)
        {
            rb.linearVelocity = Vector3.zero;

            // ゲームオーバー演出
            GameManager.Instance.EndGame();

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

        plScale = 0.75f;

        transform.localScale = new Vector3(plScale, plScale, plScale);

        GameManager.Instance.DisplayItemCnt(itemCount);
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

        if (other.tag == "DeadZone")
        {
            // ゲームオーバー演出
            GameManager.Instance.EndGame();
        }

        if (other.tag == "Item")
        {
            if(plScale <= 1.25f)
                plScale += 0.03f; // プレイヤーの大きさを増やす
            else
                plScale = 1.25f;

            transform.localScale = new Vector3(plScale, plScale, plScale);

            audioSource.PlayOneShot(eatSE);

            itemCount++;

            GameManager.Instance.DisplayItemCnt(itemCount); // アイテム数表示更新

            // スコア加算処理 (itemCount * foodObjのスコア)
            GameManager.Instance.AddScore(other.GetComponent<Item>().score * itemCount);

            Destroy(other.gameObject); // アイテムを消す
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Object")
        {
            audioSource.PlayOneShot(hitSE);
            HitObj();
        }
    }
}
