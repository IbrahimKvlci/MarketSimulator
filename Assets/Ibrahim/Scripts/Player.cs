using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject _camera;

    [SerializeField] float _range;

    bool _isCarrying;
    public bool _isOutlined;

    void Start()
    {
        _isCarrying = false;
    }


    void Update()
    {
        CanPickUp();
    }

    void CanPickUp()
    {
        RaycastHit hit;
        if(Physics.Raycast(_camera.transform.position,_camera.transform.forward,out hit,_range))
        {
            Outline outline = hit.transform.GetComponent<Outline>();
            if (hit.transform.tag == "carryitem" && !_isCarrying)
            {
                
                if (outline != null)
                {
                    outline.enabled = true;
                }
                else
                {
                    outline=hit.transform.gameObject.AddComponent<Outline>();
                }
                if (Input.GetKeyUp(KeyCode.E))
                {
                    Debug.Log("aldi");
                    outline.enabled = false;
                    hit.transform.gameObject.transform.SetParent(_camera.transform);
                    hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    _isCarrying = true;
                }
                _isOutlined = true;
            }
            
            else if (Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log("atti");
                _camera.transform.DetachChildren();
                hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                outline.enabled = true;

                _isCarrying=false;
            }
        }
        else
        {
            _isOutlined = false;
        }

        
    }

}
