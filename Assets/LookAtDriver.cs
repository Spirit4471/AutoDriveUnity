using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtDriver : MonoBehaviour
{
    public Transform driver; 

    private void Update()
    {
        transform.LookAt(driver);
    }
}

