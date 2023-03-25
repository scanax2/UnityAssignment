using UnityEngine;

public class HandleInputMono : MonoBehaviour
{
    [SerializeField] private InputButton rotateCamera;
    [SerializeField] private InputButton launchBall;


    public InputButton RotateCamera { get => rotateCamera; }
    public InputButton LaunchBall { get => launchBall; }


    private void Update()
    {
        HandleButton(rotateCamera);
        HandleButton(launchBall);
    }

    private void HandleButton(InputButton button)
    {
        button.IsHold = Input.GetKey(button.Key);

        if (Input.GetKeyDown(button.Key))
        {
            button.OnPress?.Invoke();
        }
    }
}
