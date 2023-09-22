using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductInShop : MonoBehaviour
{
    [SerializeField] GameObject _basket,_productInBasket;

    [SerializeField] int _productId;

    

    public void AddBasket()
    {
        bool canAdd = true;
        for (int i = 0; i < _basket.transform.childCount; i++)
        {
            if (_basket.transform.GetChild(i).GetComponent<ProductInBasket>()._id == _productId)
            {
                _basket.transform.GetChild(i).GetComponent<ProductInBasket>()._count++;
                canAdd = false;
            }
        }
        if (canAdd)
        {
            var product = Instantiate(_productInBasket);
            product.transform.SetParent(_basket.transform);
            product.GetComponent<ProductInBasket>()._id = _productId;
            product.GetComponent<ProductInBasket>()._count = 1;
        }
        


        
    }
}
