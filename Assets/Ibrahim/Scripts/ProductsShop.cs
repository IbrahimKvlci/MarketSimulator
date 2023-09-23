using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductsShop : MonoBehaviour
{
    [SerializeField] List<GameObject> _products;
    [SerializeField] GameObject _productShop,_shopContent;

    [SerializeField] TMP_InputField _inputField;

    void Start()
    {
        _inputField.onValueChanged.AddListener(delegate { GetProductsByName(_inputField.text); });
    }

    void Update()
    {
        
    }

    public void GetProductsByCategoryId(int categoryId)
    {
        ClearProducts();
        foreach (GameObject product in _products)
        {
            if (product.GetComponent<Product>().CategoryId == categoryId)
            {
                var productShop = Instantiate(_productShop);
                productShop.transform.SetParent(_shopContent.transform);

                productShop.GetComponent<ProductInShop>()._product = product;
            }
            
        }
    }

    public void GetProductsByName(string name)
    {
        ClearProducts();
        if (name != "")
        {
            foreach (GameObject product in _products)
            {
                bool canShow = true;
                int i = 0;
                foreach (var item in product.GetComponent<Product>().Name)
                {
                    if (item.ToString().ToLower() != name[i].ToString().ToLower())
                    {
                        canShow = false;
                        break;
                    }
                    if (i == name.Length - 1) { break; }
                    i++;
                }
                if (canShow)
                {
                    var productShop = Instantiate(_productShop);
                    productShop.transform.SetParent(_shopContent.transform);

                    productShop.GetComponent<ProductInShop>()._product = product;
                }

            }
        }
        
    }

    void ClearProducts()
    {
        if (_shopContent.transform.childCount > 0)
        {
            for (int i = 0; i < _shopContent.transform.childCount; i++)
            {
                Destroy(_shopContent.transform.GetChild(i).gameObject);
            }
        }
    }
}
