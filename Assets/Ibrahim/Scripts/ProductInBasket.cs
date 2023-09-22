using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductInBasket : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _totalCountText;
    [SerializeField] TextMeshProUGUI _titleText,_priceText;
    [SerializeField] Image _productImage;

    public GameObject _product;

    public int _count;
    public float _price;
    

    void Start()
    {
        _titleText.text = _product.GetComponent<Product>().Name;

        _productImage.sprite= _product.GetComponent<Product>().Image;
    }


    void Update()
    {
        _totalCountText.text = _count.ToString();
        _priceText.text = $"${_price}";
        if (_count == 0)
        {
            Destroy();
        }
        SetPrice();
    }

    public void SetProductTotalCount(int value)
    {
        if (value == 0&&_count>0)
        {
            _count -= 1;
        }else if (value == 1)
        {
            _count += 1;
        }
    }

    void SetPrice()
    {
        _price=_product.GetComponent<Product>().Price * _count;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
