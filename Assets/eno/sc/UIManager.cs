using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text timerText;

    [SerializeField]
    Text comboText;

    [SerializeField]
    Text gameStartCountText;
    [SerializeField]
    Text gameStartText;
    [SerializeField]
    Text gameEndText;

    [SerializeField]
    Text totalScoreText;

    public void UpdateTimer(float time)
    {
        timerText.text = time.ToString("f2");
    }

    public void UpdateComboText(int combo)
    {
        comboText.text = combo.ToString();
    }

    public void UpdateTotalScoreText(int score)
    {
        totalScoreText.text = score.ToString();
    }

    public void ShowGameCountDownText()
    {
        gameStartCountText.gameObject.SetActive(true);
        InvokeRepeating("CountDown", 1f, 1f);
    }

    void CountDown()
    {
        int count = int.Parse(gameStartCountText.text);
        count--;
        if (count == 0)
        {
            gameStartCountText.gameObject.SetActive(false);
            gameStartText.gameObject.SetActive(true);
            Invoke("HideGameStartText", 0.5f);
            CancelInvoke("CountDown");
        }
        else
        {
            gameStartCountText.text = count.ToString();
        }
    }

    void HideGameStartText()
    {
        gameStartText.gameObject.SetActive(false);
    }

    public void SetGameEndTextVisible(bool isVisible)
    {
        gameEndText.gameObject.SetActive(isVisible);
    }
}
