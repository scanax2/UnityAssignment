using UnityEngine;

public class CameraControllerMono : MonoBehaviour
{
    [SerializeField] private Transform cameraOrbit;
    [SerializeField] private HandleInputMono handleInput;

    [Header("Configuration")]
    [SerializeField] private Vector2 cameraRotationSpeed;
    [SerializeField] private Vector2 cameraXRotationLimits;


    private void LateUpdate()
    {
        if (handleInput.RotateCamera.IsHold)
        {
            float h = cameraRotationSpeed.x * Input.GetAxis("Mouse X");
            float v = cameraRotationSpeed.y * Input.GetAxis("Mouse Y");

            var newEulerX = cameraOrbit.transform.eulerAngles.x + v;
            var newEulerY = cameraOrbit.transform.eulerAngles.y + h;

            newEulerX = Mathf.Clamp(newEulerX, cameraXRotationLimits.x, cameraXRotationLimits.y);

            cameraOrbit.transform.eulerAngles = new Vector3(
                newEulerX,
                newEulerY, 
                cameraOrbit.transform.eulerAngles.z);
        }
    }
}
