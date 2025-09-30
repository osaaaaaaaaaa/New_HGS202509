using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Score : MonoBehaviour
{
    [SerializeField] GameObject ScoreText; 

    #region ÉXÉRÉA
    int totalScore = 0;
    public int TotalScore { get { return totalScore; } }
    #endregion

}
