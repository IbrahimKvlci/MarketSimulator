using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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
    [SerializeField] int _productsCount;

    [SerializeField]bool _isCarrying;
    public bool _isOutlined;

    void Start()
    {
        _isCarrying = false;

    }


    void Update()
    {
        PickUp();
        if(_carryObject != null)
        {
            if (_carryObject.GetComponent<Cargo>() != null)
            {
                PutProductsToShelf(ref _carryObject.GetComponent<Cargo>()._product,ref _carryObject.GetComponent<Cargo>()._count);
            }
        }
        
        
        TakeProductOnShelf();
        if (_carryObject == null)
        {
            _isCarrying = false;
        }
        MakeVisibleWhenPlayerLook();
    }

    void MakeVisibleWhenPlayerLook()
    {
        RaycastHit hit;
        if(Physics.Raycast(_camera.transform.position,_camera.transform.forward, out hit,_range))
        {
            if (hit.collider.tag == "makevisible")
            {
                hit.collider.gameObject.SetActive(true);
            }
            else
            {
                foreach (var item in GameObject.FindGameObjectsWithTag("makevisible"))
                {
                    item.SetActive(false);
                }
            }
        }
        
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
        
        else if(_carryObject!= null)
        {
            RaycastHit hit;
            if(Physics.Raycast(_camera.transform.position,_camera.transform.forward,out hit, 5,_layerMask)&& hit.transform.tag == "ground")
            {

                float objectScale = _carryObject.GetComponent<Collider>().bounds.extents.y+0.1f;
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
       
        

        if (_isCarrying&&_carryObject!=null)
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

    void PutProductsToShelf(ref GameObject cargoProduct,ref int productCount)
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, 5))
        {

            if (hit.collider.tag == "shelf")
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (hit.collider.transform.childCount>1&&cargoProduct!=null)
                    {

                        if(cargoProduct.tag== hit.collider.transform.GetChild(1).tag)
                        {
                            int productsOnShelfCount = hit.collider.transform.GetChild(1).GetComponent<ProductsOnShelf>()._currentProductsCount;
                            int productsMaxCount = hit.collider.transform.GetChild(1).GetComponent<ProductsOnShelf>()._maxProductsCount;
                            int productsWillAddCount = productCount;
                            if (productsWillAddCount + productsOnShelfCount <= productsMaxCount)
                            {
                                hit.collider.transform.GetChild(1).GetComponent<ProductsOnShelf>()._currentProductsCount += productsWillAddCount;
                                productCount = 0;
                            }
                            else
                            {
                                hit.collider.transform.GetChild(1).GetComponent<ProductsOnShelf>()._currentProductsCount += productsWillAddCount;
                                productCount = productsWillAddCount - (productsMaxCount - productsOnShelfCount);
                            }
                        }
                        

                    }
                    else if(cargoProduct != null&& productCount > 0)
                    {
                        var product = Instantiate(cargoProduct);
                        product.transform.SetParent(hit.collider.transform);
                        product.transform.localScale= Vector3.one;
                        product.transform.localPosition = Vector3.zero;
                        product.transform.localRotation= Quaternion.identity;
                        if (productCount <= product.GetComponent<ProductsOnShelf>()._maxProductsCount)
                        {
                            product.GetComponent<ProductsOnShelf>()._currentProductsCount = productCount;
                            productCount = 0;
                        }
                        else
                        {
                            product.GetComponent<ProductsOnShelf>()._currentProductsCount = product.GetComponent<ProductsOnShelf>()._maxProductsCount;
                            productCount -= product.GetComponent<ProductsOnShelf>()._maxProductsCount;
                        }
                        
                    }
                    if (_productsCount == 0)
                    {
                        cargoProduct = null;
                    }

                }
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
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (hit.collider.transform.childCount > 1&&!_isCarrying)
                    {
                        GameObject product=Instantiate(hit.collider.transform.GetChild(1).GetComponent<ProductsOnShelf>()._product);
                        product.transform.SetParent(_carryObjectPosition);
                        product.transform.localPosition = Vector3.zero;
                        product.transform.localRotation = Quaternion.identity;
                        if (product.transform.GetComponent<Rigidbody>() == null)
                        {
                            product.transform.AddComponent<Rigidbody>();
                        }
                        product.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        _carryObject = product;
                        _isCarrying = true;
                        hit.collider.transform.GetChild(1).GetComponent<ProductsOnShelf>()._currentProductsCount -= 1;
                    }
                }
            }
        }

    }

    
    
}
