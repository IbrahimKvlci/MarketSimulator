using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsOnShelf : MonoBehaviour
{
    [SerializeField] GameObject _product;

    public int _maxProductsCount,_currentProductsCount;


    void Start()
    {
        _maxProductsCount = transform.childCount;
    }


    void Update()
    {
        if (_maxProductsCount >= _currentProductsCount&&_currentProductsCount>=0)
        {
            SetProducts();
            DeleteProducts();
        }
        else if( _currentProductsCount >= _maxProductsCount)
        {
            _currentProductsCount=_maxProductsCount;
        }
        else
        {
            _currentProductsCount=0;
        }
        
    }

    void SetProducts()
    {
        for (int i = 0; i < _currentProductsCount; i++)
        {
            if (transform.GetChild(i).childCount==0)
            {
                var product = Instantiate(_product);
                product.transform.SetParent(transform.GetChild(i));
                product.transform.localPosition = Vector3.zero;
            }
            
        }
    }

    void DeleteProducts()
    {
        for (int i = _currentProductsCount; i < _maxProductsCount; i++)
        {
            if (transform.GetChild(i).childCount == 1)
            {
                Destroy(transform.GetChild(i).GetChild(0).gameObject);
            }
        }
    }
}
