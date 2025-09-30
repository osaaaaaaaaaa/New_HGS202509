using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    UIManager uiManager;

    [SerializeField]
    StageManager stageManager;

    #region 状態管理
    const float GameTime = 60f;
    float currentTime = 60;
    bool isStartGame = false;
    bool isEndGame = false;
    #endregion

    #region シングルトン
    static GameManager instance;
    public static GameManager Instance {  get { return instance; } }
    #endregion

    #region スコア
    int totalScore = 0;
    public int TotalScore { get { return totalScore; } }
    #endregion

    #region コンボ関連
    int currentCombo = 0;
    public int CurrentCombo { get { return currentCombo; } }
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
        yield return new WaitForSeconds(2f);
        StartGame();

        const float waitSec = 0.1f;
        while (currentTime > 0)
        {
            currentTime -= waitSec;
            uiManager.UpdateTimer(currentTime);
            yield return new WaitForSeconds(waitSec);
        }
        EndGame();
    }

    /// <summary>
    /// ゲーム開始
    /// </summary>
    void StartGame()
    {
        isStartGame = true;
        stageManager.StartSpawnObstacles();
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    void EndGame()
    {
        isEndGame = true;
        // リザルトシーン遷移
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="addValue"></param>
    public void AddScore(int addValue)
    {
        totalScore += addValue;
        if(totalScore <= 0) totalScore = 0;
    }

    /// <summary>
    /// コンボ加算
    /// </summary>
    public void AddCombo(int addValue)
    {
        currentCombo += addValue;
        if(currentCombo <= 0) currentCombo = 0;
    }
}
