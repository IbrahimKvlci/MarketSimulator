using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject _camera,_product;
    [SerializeField] Transform _carryObjectPosition;
    [SerializeField] LayerMask _layerMask;
    Outline _outline;

    GameObject _carryObject;

    [SerializeField] float _range,_rotateSpeed,_carryingDistanceMax,_carryingDistanceMin;

    [SerializeField]bool _isCarrying;
    public bool _isOutlined;

    void Start()
    {
        _isCarrying = false;

    }


    void Update()
    {
        PickUp();

    }

    void PickUp()
    {
        if(!_isCarrying)
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _range))
            {
                if (hit.transform.tag == "carryitem")
                {
                    _outline = hit.transform.GetComponent<Outline>();

                    

                        if (_outline != null)
                        {
                            _outline.enabled = true;
                        }
                        else
                        {
                            _outline = hit.transform.gameObject.AddComponent<Outline>();
                        }
                        if (Input.GetKeyUp(KeyCode.E))
                        {
                            Debug.Log("aldi");
                            _carryObject = hit.transform.gameObject;
                            _outline.enabled = false;
                            hit.transform.gameObject.transform.SetParent(_carryObjectPosition);
                            hit.transform.localPosition = Vector3.zero;
                            hit.transform.localRotation = Quaternion.identity;
                            if (hit.transform.GetComponent<Rigidbody>() == null)
                            {
                                hit.transform.AddComponent<Rigidbody>();
                            }
                            hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                            _isCarrying = true;
                            
                        }
                        _isOutlined = true;
                    



                }

            }
            else
            {
                _isOutlined = false;
            }
        }
        
        else
        {
            RaycastHit hit;
            if(Physics.Raycast(_camera.transform.position,_camera.transform.forward,out hit, 5,_layerMask)&& hit.transform.tag == "ground")
            {

                float objectScale = _carryObject.GetComponent<Renderer>().bounds.extents.y;
                _carryObject.transform.position = hit.point + Vector3.up * objectScale;
                _carryObject.transform.rotation = Quaternion.Euler(0, _carryObject.transform.eulerAngles.y, _carryObject.transform.eulerAngles.z);

                
            }
            else
            {
                _carryObject.transform.localPosition = Vector3.zero;
                _carryObject.transform.localRotation = Quaternion.identity;
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log("atti");

                _carryObjectPosition.DetachChildren();
                _carryObject.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                _carryObject = null;
                if (_outline != null)
                {
                    _outline.enabled = true;

                }

                _isCarrying = false;
            }


        }
        
        if (_isCarrying)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                print("donuyor");
                _carryObject.transform.Rotate(Vector3.up * _rotateSpeed);
            }
            else if (Input.GetKey(KeyCode.R))
            {
                _carryObject.transform.Rotate(Vector3.up * -_rotateSpeed);
            }   
        }


        

    }

    void TakeProductOnShelf()
    {
        RaycastHit hit;
        if(Physics.Raycast(_camera.transform.position,_camera.transform.forward, out hit, 5))
        {
            if (hit.collider.tag == "shelf")
            {

            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {

        }
    }

    
}
