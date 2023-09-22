using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsShop : MonoBehaviour
{
    [SerializeField] List<GameObject> _products;
    [SerializeField] GameObject _productShop,_shopContent;


    void Start()
    {
        GetProducts();
    }

    void Update()
    {
        
    }

    void GetProducts()
    {
        foreach (GameObject product in _products)
        {
            var productShop=Instantiate(_productShop);
            productShop.transform.SetParent(_shopContent.transform);

            productShop.GetComponent<ProductInShop>()._product = product;
        }
    }
}
