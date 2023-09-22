using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasketControl : MonoBehaviour
{
    [SerializeField] GameObject _basket;
    [SerializeField] TextMeshProUGUI _totalCountText,_totalPriceText;

    GameObject _cargoPlace;
    List<GameObject> _products;

    int _totalCount;
    float _totalPrice;

    void Start()
    {
        
    }


    void Update()
    {
        GetTotalCountandPrice();
        
    }

    void GetTotalCountandPrice()
    {
        if(_basket.transform.childCount > 0)
        {
            int totalCount = 0;
            float totalPrice = 0;
            for (int i = 0; i < _basket.transform.childCount; i++)
            {
                totalCount += _basket.transform.GetChild(i).GetComponent<ProductInBasket>()._count;
                totalPrice += _basket.transform.GetChild(i).GetComponent<ProductInBasket>()._price;
            }
            _totalCount = totalCount;
            _totalPrice = totalPrice;
        }
        else
        {
            _totalCount = 0;
            _totalPrice=0;
        }
        _totalCountText.text = $"Total Products: {_totalCount}";
        _totalPriceText.text = $"Total Price: ${_totalPrice}";
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
