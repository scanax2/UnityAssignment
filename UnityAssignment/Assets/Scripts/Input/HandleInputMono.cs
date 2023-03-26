using UnityEngine;

public class HandleInputMono : MonoBehaviour
{
    [SerializeField] private InputButton rotateCamera;
    [SerializeField] private InputButton launchBall;
    [SerializeField] private InputButton resetScene;


    public InputButton RotateCamera { get => rotateCamera; }
    public InputButton LaunchBall { get => launchBall; }
    public InputButton ResetScene { get => resetScene; }


    private void Update()
    {
        HandleButton(rotateCamera);
        HandleButton(launchBall);
        HandleButton(resetScene);
    }

    private void HandleButton(InputButton button)
    {
        button.IsHold = Input.GetKey(button.Key);

        if (Input.GetKeyDown(button.Key))
        {
            button.OnPress?.Invoke();
        }

        if (Input.GetKeyUp(button.Key))
        {
            button.OnRelease?.Invoke();
        }


        if (button.IsHold)
        {
            button.HoldTime += Time.deltaTime;
        }
        else
        {
            button.HoldTime = 0f;
        }
    }
}
