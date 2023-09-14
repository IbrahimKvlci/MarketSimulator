using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CarMovement : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool _canEnter,_canExit,_isDriving;
    private bool isBreaking;

    Rigidbody _rb;

    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    [SerializeField] private GameObject _player, _camera;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass +=new Vector3(0, 0, 1);
        _isDriving = false;
    }

    private void Update()
    {
        if (_canEnter)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(EnterCar());
                _canEnter = false;
            }
        }
        if (_canExit)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ExitCar();
            }
            
        }
    }

    private void FixedUpdate()
    {
        if (_isDriving)
        {
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }
        
    }

    private void GetInput()
    {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");

        // Breaking Input
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    IEnumerator EnterCar()
    {
        _camera.SetActive(true);
        _player.SetActive(false);
        _isDriving = true;
        yield return new WaitForSeconds(2);
        _canExit = true;
    }

    void ExitCar()
    {
        _camera.SetActive(false);
        _player.transform.position = new Vector3(transform.position.x+3,_player.transform.position.y, transform.position.z);
        _player.SetActive(true);
        
        _isDriving=false;
        _canExit = false;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {


            print("calisit");
            _canEnter = true;


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _canEnter = false;
        }
    }


}