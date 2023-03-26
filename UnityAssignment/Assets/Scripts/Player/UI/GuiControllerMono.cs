using UnityEngine;

public class GuiControllerMono : MonoBehaviour
{
    [SerializeField] private ScoreView launchedBallsScoreView;
    [SerializeField] private ScoreView collectedPointsScoreView;
    [SerializeField] private PopupView endOfLevelPopup;

    private System.Action updateLaunchedBallsScoreDelegate;
    private System.Action updatCollectedPointsScoreViewDelegate;


    public void InitializeViews(Score ballsScore, Score pointsScore)
    {
        launchedBallsScoreView.UpdateView(ballsScore);
        collectedPointsScoreView.UpdateView(pointsScore);

        updateLaunchedBallsScoreDelegate = delegate () { launchedBallsScoreView.UpdateView(ballsScore); };
        updatCollectedPointsScoreViewDelegate = delegate () { collectedPointsScoreView.UpdateView(pointsScore); };

        ballsScore.OnScoreChanged += updateLaunchedBallsScoreDelegate;
        pointsScore.OnScoreChanged += updatCollectedPointsScoreViewDelegate;
    }

    public void FinishGame(float points)
    {
        endOfLevelPopup.gameObject.SetActive(true);
        endOfLevelPopup.TextComponent.text = $"Score: {Mathf.CeilToInt(points)}";
    }

    public void Dispose(Score ballsScore, Score pointsScore)
    {
        ballsScore.OnScoreChanged -= updateLaunchedBallsScoreDelegate;
        pointsScore.OnScoreChanged -= updatCollectedPointsScoreViewDelegate;
    }
}
