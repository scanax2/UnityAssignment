using System;
using System.Collections;
using UnityEngine;

public class BallLauncherMono : MonoBehaviour
{
    public Action OnBallLaunched;
    public Action OnBallFellOnGround;

    [SerializeField] private CameraControllerMono cameraController;
    [SerializeField] private BallMono ball;
    [SerializeField] private Transform ballLaunchSpot;

    [SerializeField] private float maxHoldTime;
    [SerializeField] private Vector2 launchForceRange;
    [SerializeField] private float reloadTime;

    private WaitForSeconds waitForReload;
    private bool isReload;


    private void Start()
    {
        OnBallFellOnGround += ball.OnGroundCollision;

        isReload = false;
        waitForReload = new WaitForSeconds(reloadTime);
    }

    public void LaunchBall(float holdTime)
    {
        if (isReload)
        {
            return;
        }

        ball.StopAllCoroutines();

        float clampedTime = Mathf.Clamp(holdTime, 0, maxHoldTime);
        float forceLerpValue = clampedTime / maxHoldTime;
        float launchForce = Mathf.Lerp(launchForceRange.x, launchForceRange.y, forceLerpValue);

        // Reuse object, insted of creating new one
        var launchPosition = ballLaunchSpot.position + ballLaunchSpot.forward;

        ball.transform.position = launchPosition;
        ball.gameObject.SetActive(true);

        var ballRigidbody = ball.GetComponent<Rigidbody>();
        
        Ray ray = cameraController.MainCamera.ScreenPointToRay(Input.mousePosition);

        ballRigidbody.velocity = ray.direction * launchForce;

        ball.IsFirstTouch = true;

        OnBallLaunched?.Invoke();

        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        isReload = true;
        yield return waitForReload;
        isReload = false;
    }

    private void OnDestroy()
    {
        OnBallFellOnGround -= ball.OnGroundCollision;
    }
}
