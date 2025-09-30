using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    List<Text> ranks = new List<Text>();

    [SerializeField]
    List<Animator> rankinAnims = new List<Animator>();

    static List<int> hightScores = new List<int>();
    bool isRankin = false;

    private void Awake()
    {
        if(hightScores.Count == 0)
        {
            hightScores.Add(0);
            hightScores.Add(0);
            hightScores.Add(0);
            hightScores.Add(0);
        }

        hightScores.Add(GameManager.TotalScore);

        hightScores = hightScores.OrderByDescending(i => i).ToList();
        hightScores.RemoveAt(hightScores.Count - 1);
        if(hightScores.Contains(GameManager.TotalScore)) isRankin = true;

        UpdateRanking();
        Invoke("ChangeTitleScene", 5f);
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
