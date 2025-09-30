using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    GameObject rankingSet;

    [SerializeField]
    List<Text> ranks = new List<Text>();

    [SerializeField]
    List<Animator> rankinAnims = new List<Animator>();

    static List<int> hightScores = new List<int>();
    bool isRankin = false;

    [SerializeField]
    GameObject selfScoreSet;

    [SerializeField]
    Text selfScoreText;

    [SerializeField]
    GameObject playerHead;

    [SerializeField]
    Animator playerAnim;

    [SerializeField]
    AudioSource audio;

    private void Start()
    {
        playerHead.transform.localScale = GameManager.playerScale;
        Invoke("ShowSelfScore", 2);
        Invoke("ShowRanking", 4f);
    }

    void ShowSelfScore()
    {
        selfScoreText.gameObject.SetActive(true);
        selfScoreText.text = GameManager.TotalScore.ToString();
        audio.Play();
    }

    void ShowRanking()
    {
        audio.Play();
        selfScoreSet.SetActive(false);
        rankingSet.SetActive(true);
        playerAnim.gameObject.SetActive(false);
        if (hightScores.Count == 0)
        {
            hightScores.Add(0);
            hightScores.Add(0);
            hightScores.Add(0);
            hightScores.Add(0);
        }

        hightScores.Add(GameManager.TotalScore);

        hightScores = hightScores.OrderByDescending(i => i).ToList();
        hightScores.RemoveAt(hightScores.Count - 1);
        if (hightScores.Contains(GameManager.TotalScore)) isRankin = true;

        UpdateRanking();
        Invoke("ChangeTitleScene", 4f);
    }

    private void ChangeTitleScene()
    {
        Initiate.DoneFading();
        Initiate.Fade("Title", Color.black, 1.0f);
    }

    void UpdateRanking()
    {
        for (int i = 0; i < hightScores.Count; i++)
        {
            ranks[i].text = hightScores[i].ToString();

            if (isRankin && hightScores[i] == GameManager.TotalScore)
            {
                isRankin = false;
                rankinAnims[i].enabled = true;
            }
        }
    }
}
