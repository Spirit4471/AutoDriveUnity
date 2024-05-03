using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 thirdPersonOffset;
    public Vector3 firstPersonOffset;

    private bool isFirstPerson = false;

    private void FixedUpdate(){
        Vector3 offset = isFirstPerson ? firstPersonOffset : thirdPersonOffset;
        Vector3 targetPosition = target.position + target.TransformDirection(offset);
        transform.position = targetPosition;
        transform.rotation = target.rotation;
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
        isFirstPerson = !isFirstPerson;
    }
}