using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float maxSteeringAngle = 30f;
    public float acceleration = 10f;
    public float braking = 10f;

    private float currentSpeed = 0f;
    private float currentSteeringAngle = 0f;

    public WheelCollider frontLeftWheel, frontRightWheel;
    public WheelCollider rearLeftWheel, rearRightWheel;

    public Transform steeringWheel;

    void Start()
    {
        frontLeftWheel.ConfigureVehicleSubsteps(5f, 12, 15);
        frontRightWheel.ConfigureVehicleSubsteps(5f, 12, 15);
        rearLeftWheel.ConfigureVehicleSubsteps(5f, 12, 15);
        rearRightWheel.ConfigureVehicleSubsteps(5f, 12, 15);
    }

    void FixedUpdate()
    {
        // get input from keyboard or joystick
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // calculate speed based on acceleration and braking input
        currentSpeed += verticalInput * acceleration * Time.deltaTime;
        currentSpeed -= braking * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

        // calculate steering angle based on horizontal input
        currentSteeringAngle = horizontalInput * maxSteeringAngle;

        // apply steering angle to front wheels
        frontLeftWheel.steerAngle = currentSteeringAngle;
        frontRightWheel.steerAngle = currentSteeringAngle;

        // apply torque to rear wheels
        rearLeftWheel.motorTorque = currentSpeed;
        rearRightWheel.motorTorque = currentSpeed;

        // rotate steering wheel based on steering angle
        steeringWheel.localEulerAngles = new Vector3(steeringWheel.localEulerAngles.x, currentSteeringAngle, steeringWheel.localEulerAngles.z);
    }
}

