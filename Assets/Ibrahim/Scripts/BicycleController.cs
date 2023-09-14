using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class BicycleController : MonoBehaviour
{
    [SerializeField] GameObject _bicyleHandleBar,_camera,_player;
    [SerializeField] WheelCollider _front, _back;
    [SerializeField] float _speed, _turnSpeed,_brakeAcceleration;
    float _horizontal,_vertical;
    Vector3 _bicylePosition;
    [SerializeField] bool _isRiding, _canExit = false,_canEnter;
    Rigidbody _rb;
   
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _bicylePosition = transform.position;
        _isRiding = false;
    }

    private void Update()
    {
        print(_rb.velocity.magnitude * _vertical);
        if (_canEnter&&Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(EnterCar());
            _canEnter = false;
        }
        if (_isRiding&&_canExit)
        {


                if (Input.GetKeyDown(KeyCode.F))
                {
                    _bicylePosition.y = transform.position.y;
                    _rb.constraints = RigidbodyConstraints.None;
                    _camera.SetActive(false);
                    _player.transform.position= transform.position+Vector3.right*2;
                    _player.SetActive(true);
                    _isRiding = false;
                    _canExit = false;

                }



        }
    }

    void FixedUpdate()
    {
        if (_isRiding)
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
            _back.motorTorque = _speed * _vertical;
            _front.steerAngle = _turnSpeed * _horizontal;
            if (_horizontal != 0)
            {
                _bicyleHandleBar.transform.localRotation = Quaternion.Euler(0, _horizontal * 20, 0);
            }
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }
        Brake();
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            print("fren");
            _back.brakeTorque = _brakeAcceleration;
        }
        else
        {
            _back.brakeTorque = 0;
        }
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

    IEnumerator EnterCar()
    {
        _camera.SetActive(true);
        _player.SetActive(false);
        transform.position = new Vector3(transform.position.x, _bicylePosition.y, transform.position.z);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        _rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        _isRiding = true;
        yield return new WaitForSeconds(2);
        _canExit = true;
    }

    
}
