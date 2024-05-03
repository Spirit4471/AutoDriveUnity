using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowThirdPerson : MonoBehaviour
{
    public Transform target;
    
    public Vector3 ThirdPersonOffset;

  

    private void FixedUpdate(){
        
        Vector3 targetPosition = target.position + target.TransformDirection(ThirdPersonOffset);
        transform.position = targetPosition;
        transform.rotation = target.rotation;
    }
    
}