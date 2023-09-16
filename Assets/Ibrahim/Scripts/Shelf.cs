using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _productsCountText;

     
    public GameObject _products;


    void Start()
    {

    }


    void Update()
    {
        if (_products != null && transform.childCount == 1)
        {
            var products=Instantiate(_products);
            products.transform.SetParent(transform);
            SetProductCountText();
        }
        else if(_products==null)
        {
            _productsCountText.text = "0";
        }

        //if (transform.childCount > 1)
        //{
        //    _products = transform.GetChild(1).gameObject;
        //    SetProductCountText();
        //}
        //else
        //{
        //    _productsCountText.text = "0";
        //}
    }

    void SetProductCountText()
    {
        _productsCountText.text=_products.GetComponent<ProductsOnShelf>()._currentProductsCount.ToString();
    }
}
