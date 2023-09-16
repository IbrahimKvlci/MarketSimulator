using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _productsCountText;

     
    public GameObject _products;

    public int _producsCount;


    void Start()
    {

    }


    void Update()
    {



        if (transform.childCount > 1)
        {
            _products = transform.GetChild(1).gameObject;
            SetProductCountText();
        }
        else
        {
            _productsCountText.text = "0";
        }
    }

    void SetProductCountText()
    {
        _productsCountText.text=_products.GetComponent<ProductsOnShelf>()._currentProductsCount.ToString();
    }


}
