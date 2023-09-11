using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController _controller;

    Vector3 _velocity;


    [SerializeField] float _speed,_gravitiy=-9.81f,_jumpHight;
    float _horizontal, _vertical;

    bool _isGrounded=true;
    void Start()
    {

        _controller= GetComponent<CharacterController>();
    }


    void FixedUpdate()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        Movement(_horizontal,_vertical);
        Jump(_jumpHight);
    }

    void Jump(float jumpHight)
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _isGrounded = false;
            _velocity.y = Mathf.Sqrt(jumpHight * -2 * _gravitiy);
        }
    }

    void Movement(float horizontal,float vertical)
    {
        Vector3 inputVector=transform.right* horizontal+transform.forward*vertical;

        _controller.Move(inputVector*_speed*Time.deltaTime);
        _velocity.y += _gravitiy * Time.deltaTime;
        _controller.Move(_velocity*Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
        Debug.Log("degdi");
    }
}
