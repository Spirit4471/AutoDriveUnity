using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;

    private void Start()
    {
        // default to third person camera
        thirdPersonCamera.SetActive(true);
        firstPersonCamera.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleCameraView();
        }
    }

    private void ToggleCameraView()
    {
        // swap active state of cameras
        firstPersonCamera.SetActive(!firstPersonCamera.activeSelf);
        thirdPersonCamera.SetActive(!thirdPersonCamera.activeSelf);
    }
}
