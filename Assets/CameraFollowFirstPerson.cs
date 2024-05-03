using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowFirstPerson : MonoBehaviour
{
    public Transform target;
  
    public Vector3 firstPersonOffset;



    private void FixedUpdate(){

        Vector3 targetPosition = target.position + target.TransformDirection(firstPersonOffset);
        transform.position = targetPosition;
        transform.rotation = target.rotation;
    }
   

   
}