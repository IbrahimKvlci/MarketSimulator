using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CharacterMovement : MonoBehaviour
{
    CharacterController _controller;

    Vector3 _velocity;


    [SerializeField] float _speed,_gravitiy=-9.81f,_jumpHeight;
    float _horizontal, _vertical;

    bool _isGrounded;
    void Start()
    {

        _controller= GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_controller.collisionFlags == CollisionFlags.Below)
        {
            _isGrounded = true;
            Debug.Log("calisti");
        }
        Jump(_jumpHeight);
    }

    void FixedUpdate()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        Movement(_horizontal,_vertical);
        
        
    }

    void Jump(float jumpHeight)
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _isGrounded = false;
            _velocity.y = Mathf.Sqrt(jumpHeight * -2 * _gravitiy);
        }
    }

    void Movement(float horizontal,float vertical)
    {
        Vector3 inputVector=transform.right* horizontal+transform.forward*vertical;

        _controller.Move(inputVector*_speed*Time.deltaTime);
        _velocity.y += _gravitiy * Time.deltaTime;
        _controller.Move(_velocity*Time.deltaTime);
    }

}
