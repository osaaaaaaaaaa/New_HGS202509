using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    UIManager uiManager;

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

    void StartGame()
    {
        isStartGame = true;
    }

    void EndGame()
    {
        isEndGame = true;
        // リザルトシーン遷移
    }
}
