using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    UIManager uiManager;

    [SerializeField]
    StageManager stageManager;

    [SerializeField]
    CameraManeger camManeger;

    #region 状態管理
    const float GameTime = 60f;
    float currentTime = 60;
    public bool isStartGame = false;
    public bool isEndGame = false;
    #endregion

    #region シングルトン
    static GameManager instance;
    public static GameManager Instance {  get { return instance; } }
    #endregion

    #region スコア
    static public int totalScore = 0;
    static public int TotalScore { get { return totalScore; } }
    #endregion

    #region コンボ関連
    int currentItem = 0;
    public int CurrentItem { get { return currentItem; } }
    #endregion

    #region プレイヤー
    [SerializeField] GameObject player;
    static public Vector3 playerScale = Vector3.one;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        totalScore = 0;
        playerScale = Vector3.one;
    }

    private void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    /// <summary>
    /// ゲームタイマー管理
    /// </summary>
    /// <returns></returns>
    IEnumerator TimerCoroutine()
    {
        uiManager.ShowGameCountDownText();
        yield return new WaitForSeconds(2.5f);
        StartGame();

        const float waitSec = 0.1f;
        while (currentTime > 0)
        {
            currentTime -= waitSec;
            uiManager.UpdateTimer(currentTime);
            yield return new WaitForSeconds(waitSec);
        }

        currentTime = 0;
        uiManager.UpdateTimer(currentTime);
        EndGame();
    }

    /// <summary>
    /// ゲーム開始
    /// </summary>
    void StartGame()
    {
        isStartGame = true;
        stageManager.StartSpawnObstacles();
        camManeger.Startmove();
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void EndGame()
    {
        isEndGame = true;
        camManeger.StopMove();
        stageManager.StopSpawnObstacles();
        uiManager.SetGameEndTextVisible(true);

        playerScale = player.transform.localScale;

        // 数秒後にリザルトシーン遷移
        Invoke("ChangeResultScene", 2f);
    }

    void ChangeResultScene()
    {
        Initiate.DoneFading();
        Initiate.Fade("Result", Color.black, 1.0f);
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="addValue"></param>
    public void AddScore(int addValue)
    {
        totalScore += addValue;
        if(totalScore <= 0) totalScore = 0;

        uiManager.UpdateTotalScoreText(totalScore);
    }

    /// <summary>
    /// コンボ加算
    /// </summary>
    public void DisplayItemCnt(int itemCnt)
    {
        currentItem = itemCnt;

        uiManager.UpdateComboText(currentItem);
    }
}
