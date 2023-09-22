using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasketControl : MonoBehaviour
{
    [SerializeField] GameObject _basket;
    [SerializeField] TextMeshProUGUI _totalCountText;

    GameObject _cargoPlace;
    List<GameObject> _products;

    int _totalCount;

    void Start()
    {
        
    }


    void Update()
    {
        GetTotalCount();
    }

    void GetTotalCount()
    {
        if(_basket.transform.childCount > 0)
        {
            int totalCount = 0;
            for (int i = 0; i < _basket.transform.childCount; i++)
            {
                totalCount += _basket.transform.GetChild(i).GetComponent<ProductInBasket>()._count;
            }
            _totalCount = totalCount;
        }
        else
        {
            _totalCount = 0;
        }
        _totalCountText.text = $"Total Products: {_totalCount}";
    }

    void GetProducts()
    {
        if (_basket.transform.childCount > 0)
        {
            for (int i = 0; i < _basket.transform.childCount; i++)
            {
                _products.Add(_basket.transform.GetChild(i).gameObject);
            }
        }
        else
        {
            _products = null;
        }
    }

    //public IEnumerator BuyProducts()
    //{
    //    _cargoPlace = GameObject.FindGameObjectWithTag("cargoplace");


    //}

}
