using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIController : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5.0f;
    public float turnSpeed = 3.0f;
    public float obstacleDetectionDistance = 5.0f;
    public LayerMask obstacleLayerMask;

    private int currentWaypointIndex;

    private void Update()
    {
        DriveTowardsWaypoint();
        AvoidObstacles();
    }

    private void DriveTowardsWaypoint()
    {
        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 1.0f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void AvoidObstacles()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, obstacleDetectionDistance, obstacleLayerMask))
        {
            if (hit.collider.CompareTag("AI_Vehicle") || hit.collider.CompareTag("Roads"))
            {
                Vector3 hitNormal = hit.normal;
                Vector3 avoidanceDirection = hitNormal * turnSpeed;
                transform.position += avoidanceDirection * Time.deltaTime;
            }
        }
    }
}
