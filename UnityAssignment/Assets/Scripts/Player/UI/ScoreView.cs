using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    private const string TextFormat = "{0} / {1}";
    private const string InfinitySymbol = "∞";

    [SerializeField] private Text textComponent;


    public void UpdateView(Score score)
    {
        // Target value equals -1 in case of infinite amount of balls</param>
        if (score.TargetValue != -1)
        {
            textComponent.text = string.Format(TextFormat, score.CurrentValue, score.TargetValue);
        }
        else
        {
            textComponent.text = string.Format(TextFormat, score.CurrentValue, InfinitySymbol);
        }
    }
}
