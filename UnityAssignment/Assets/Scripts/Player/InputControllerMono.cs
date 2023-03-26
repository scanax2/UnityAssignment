using UnityEngine;

public class InputControllerMono : MonoBehaviour
{
    [SerializeField] private HandleInputMono handleInput;
    [SerializeField] private BallLauncherMono ballLauncher;
    [SerializeField] private GameManagerMono gameManager;


    private void Start()
    {
        handleInput.LaunchBall.OnRelease += LaunchBall;
        handleInput.ResetScene.OnPress += ResetScene;
    }

    private void LaunchBall()
    {
        ballLauncher.LaunchBall(handleInput.LaunchBall.HoldTime);
    }

    private void ResetScene()
    {
        gameManager.ResetScene();
    }

    private void OnDestroy()
    {
        handleInput.LaunchBall.OnRelease -= LaunchBall;
        handleInput.ResetScene.OnPress -= ResetScene;
    }
}
