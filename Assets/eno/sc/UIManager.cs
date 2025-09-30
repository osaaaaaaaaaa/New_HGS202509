using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text timerText;

    public void UpdateTimer(float time)
    {
        timerText.text = time.ToString("f2");
    }
}
