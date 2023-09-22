using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProductInShop : MonoBehaviour
{
    [SerializeField] GameObject _productInBasket;
    [SerializeField] TextMeshProUGUI _titleText,_priceText;
    [SerializeField] Image _productImage;

    public GameObject _product;

    GameObject _basket;



    private void Start()
    {
        _titleText.text = _product.GetComponent<Product>().Name;
        _priceText.text = $"${_product.GetComponent<Product>().Price}";
        _productImage.sprite = _product.GetComponent<Product>().Image;
        foreach (var item in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (item.GameObject().tag == "basket")
            {
                _basket = item.GameObject();
                break;
            }
        }
    }

    public void AddBasket()
    {
        bool canAdd = true;
        for (int i = 0; i < _basket.transform.childCount; i++)
        {
            if (_basket.transform.GetChild(i).GetComponent<ProductInBasket>()._product.GetComponent<Product>().Id == _product.GetComponent<Product>().Id)
            {
                _basket.transform.GetChild(i).GetComponent<ProductInBasket>()._count++;
                canAdd = false;
            }
        }
        if (canAdd)
        {
            var product = Instantiate(_productInBasket);
            product.transform.SetParent(_basket.transform);
            product.GetComponent<ProductInBasket>()._product = _product;
            product.GetComponent<ProductInBasket>()._count = 1;
        }
        


        
    }
}
