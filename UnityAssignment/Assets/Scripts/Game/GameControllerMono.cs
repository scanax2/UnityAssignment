using UnityEngine;

public class GameControllerMono : MonoBehaviour
{
    [SerializeField] private BallLauncherMono ballLauncher;
    [SerializeField] private GuiControllerMono guiController;

    [Header("ReadOnly")]
    [SerializeField] private Score launchedBallsScore;
    [SerializeField] private Score collectedPointsScore;

    [Header("Blocks")]
    [SerializeField] private PointsBlockMono[] blocks;


    // Use this method to initialize level data, after level design is finished
    [ContextMenu("Initialize level")]
    private void InitializeLevel()
    {
        this.blocks = transform.GetComponentsInChildren<PointsBlockMono>();

        int totalPoints = 0;
        foreach (var block in blocks)
        {
            totalPoints += block.Points;
        }

        launchedBallsScore = new Score(-1);
        collectedPointsScore = new Score(totalPoints);
    }

    private void Start()
    {
        guiController.InitializeViews(launchedBallsScore, collectedPointsScore);

        foreach (var block in blocks)
        {
            block.OnGroundCollision += OnBlockGroundCollision;
        }

        ballLauncher.OnBallLaunched += OnBallLaunched;
        ballLauncher.OnBallFellOnGround += CheckGameConditions;
    }

    private void OnBallLaunched()
    {
        launchedBallsScore.CurrentValue += 1;
    }

    private void OnBlockGroundCollision(int points)
    {
        collectedPointsScore.CurrentValue += points;
        CheckGameConditions();
    }

    private void CheckGameConditions()
    {
        if (IsWinningConditionAchieved())
        {
            guiController.FinishGame(GetTotalPoints());
        }
        if (IsLoosingConditionAchieved())
        {
            guiController.FinishGame(GetTotalPoints());
        }
    }

    private bool IsWinningConditionAchieved()
    {
        return collectedPointsScore.IsScoreAchieved();
    }

    private bool IsLoosingConditionAchieved()
    {
        return collectedPointsScore.IsScoreAchieved();
    }

    private float GetTotalPoints()
    {
        return (1f / launchedBallsScore.CurrentValue) * 
            collectedPointsScore.CurrentValue * 1000;
    }

    private void OnDestroy()
    {
        guiController.Dispose(launchedBallsScore, collectedPointsScore);

        foreach (var block in blocks)
        {
            if (block != null)
            {
                block.OnGroundCollision -= OnBlockGroundCollision;
            }
        }
    }
}
