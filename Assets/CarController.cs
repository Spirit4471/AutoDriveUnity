using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class CarController : MonoBehaviour
{
    internal enum driveType
    {
        frontWheelDrive,
        rearWheelDrive,
        allWheelDrive
    }
    [SerializeField]private driveType Drive;

    private inputManager IM;
    public GameObject wheelmeshes, wheelcolliders;
    private WheelCollider[] WheelColliders = new WheelCollider[4]; // 4 wheel collider
    private GameObject[] WheelMesh = new GameObject[4]; // 4 wheel mesh
    private GameObject centerofMass;
    public float motortorque = 500;
    public float steeringMax = 20;
    public float radius = 6;
    private Rigidbody rigidbody;
    public float KPH;
    public float DownForceValue = 50;
    public float brakePower;

    public float[] slip = new float[4];

    public GameObject steerObject;

    public void DisplayTrafficLightWarning(bool display)
{
    
    SpeedDisplay trafficLightText = GetComponent<SpeedDisplay>();
}


    void Start()
    {
        if (!gameObject.CompareTag("Player"))
    {
        enabled = false;
    }
        for (int i = 0; i < Display.displays.Length; i++)
    {
        Display.displays[i].Activate();
    }
        
        getObjects();
        centerofMass = GameObject.Find("CenterOfMass");
    }

    private void FixedUpdate()
    {
        addDownForce();
        animateWheels();
        moveVehicle();
        steerVehicle();
        getFriction();
        steerRotate();
    }

    private void moveVehicle()
    {
        float totalPower;

        if(Drive == driveType.allWheelDrive)
        {
            for (int i = 0; i < WheelColliders.Length; i++)
            {
                WheelColliders[i].motorTorque = IM.vertical * (motortorque/4);
            }
        }else if(Drive == driveType.rearWheelDrive)
        {
            for (int i = 2; i < WheelColliders.Length; i++)
            {
                WheelColliders[i].motorTorque = IM.vertical * (motortorque / 2);
            }
        }else if(Drive == driveType.frontWheelDrive)
        {
            for (int i = 0; i < WheelColliders.Length-2; i++)
            {
                WheelColliders[i].motorTorque = IM.vertical * (motortorque / 2);
            }
        }

        KPH = rigidbody.velocity.magnitude * 3.6f;
        if (IM.handbrake)
        {
            WheelColliders[3].brakeTorque = WheelColliders[2].brakeTorque = brakePower;
        }else
        {
            WheelColliders[3].brakeTorque = WheelColliders[2].brakeTorque = 0;
        }
    }

    private void steerVehicle()
    {
        //ackermann formula
        

        if(IM.horizontal > 0)
        {
            WheelColliders[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
            WheelColliders[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
        }else if (IM.horizontal < 0)
        {
            WheelColliders[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
            WheelColliders[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
        }
        else
        {
            WheelColliders[0].steerAngle = 0;
            WheelColliders[1].steerAngle = 0;
        }       
        
    }
    private void steerRotate(){
        float leftSteerAngle, rightSteerAngle;
         if (IM.horizontal > 0)
        {
            leftSteerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
            rightSteerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
        }
        else if (IM.horizontal < 0)
        {
            leftSteerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
            rightSteerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
        }
        else
        {
            leftSteerAngle = 0;
            rightSteerAngle = 0;
        }

        WheelColliders[0].steerAngle = leftSteerAngle;
        WheelColliders[1].steerAngle = rightSteerAngle;

        // upodate steer rotation
        float steerAngle = (leftSteerAngle + rightSteerAngle) / 2.0f;
        steerObject.transform.localRotation = Quaternion.Euler(0, 0, -steerAngle); // account for the fact that the steering wheel is rotated 90 degrees in the model
    
    }
    void animateWheels()
    {


        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;


        for (int i = 0; i < 4; i++)
        {
            WheelColliders[i].GetWorldPose(out wheelPosition, out wheelRotation);
            WheelMesh[i].transform.position = wheelPosition;
            WheelMesh[i].transform.rotation = wheelRotation;
            
        }

    }

    private void getObjects()
    {
        IM = GetComponent<inputManager>();
        rigidbody = GetComponent<Rigidbody>();
        wheelcolliders = GameObject.Find("wheelcolliders");
        wheelmeshes = GameObject.Find("wheelmeshes");

        WheelColliders[0] = wheelcolliders.transform.Find("0").gameObject.GetComponent<WheelCollider>();
        WheelColliders[1] = wheelcolliders.transform.Find("1").gameObject.GetComponent<WheelCollider>();
        WheelColliders[2] = wheelcolliders.transform.Find("2").gameObject.GetComponent<WheelCollider>();
        WheelColliders[3] = wheelcolliders.transform.Find("3").gameObject.GetComponent<WheelCollider>();

        WheelMesh[0] = wheelmeshes.transform.Find("0").gameObject;
        WheelMesh[1] = wheelmeshes.transform.Find("1").gameObject;
        WheelMesh[2] = wheelmeshes.transform.Find("2").gameObject;
        WheelMesh[3] = wheelmeshes.transform.Find("3").gameObject;

        centerofMass = GameObject.Find("mass");
        rigidbody.centerOfMass = centerofMass.transform.localPosition;
    }

    private void addDownForce()
    {
        rigidbody.AddForce(-transform.up * DownForceValue * rigidbody.velocity.magnitude);
    }

    private void getFriction()
    {
        for (int i =0; i < WheelColliders.Length; i++)
        {
            WheelHit wheelhit;
            WheelColliders[i].GetGroundHit(out wheelhit);

            slip[i] = wheelhit.sidewaysSlip;
        }
    }






}
