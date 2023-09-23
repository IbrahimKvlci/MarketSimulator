using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasketControl : MonoBehaviour
{
    [SerializeField] GameObject _basket;
    [SerializeField] TextMeshProUGUI _totalCountText,_totalPriceText;
    [SerializeField] GameObject _cargo;

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
        _products = new List<GameObject>();
        if (_basket.transform.childCount > 0)
        {
            for (int i = 0; i < _basket.transform.childCount; i++)
            {
                print(_basket.transform.GetChild(i).gameObject);
                _products.Add( _basket.transform.GetChild(i).gameObject);
            }
        }
        else
        {
            _products = null;
        }
    }

    public void BuyProductsButton()
    {
        GetProducts();
        StartCoroutine(BuyProducts());
    }

    public IEnumerator BuyProducts()
    {
        _cargoPlace = GameObject.FindGameObjectWithTag("cargoplace");
        Transform startPosition = _cargoPlace.transform.Find("1");
        float xPosition = _cargoPlace.transform.Find("x").position.x;
        float yPosition = _cargoPlace.transform.Find("y").position.y;
        float zPosition = _cargoPlace.transform.Find("z").position.z;

        yield return new WaitForSeconds(5);

        foreach (var product in _products)
        {
            float newXPosition = Random.Range(startPosition.position.x, xPosition);
            float newYPosition = Random.Range(startPosition.position.y, yPosition);
            float newZPosition = Random.Range(startPosition.position.z, zPosition);

            Vector3 position = new Vector3(newXPosition, newYPosition, newZPosition);

            GameObject cargo= Instantiate(_cargo);
            cargo.GetComponent<Cargo>()._count = product.GetComponent<ProductInBasket>()._count;
            cargo.GetComponent<Cargo>()._product=product.GetComponent<ProductInBasket>()._product.GetComponent<Product>().ProductsOnShelf;
            
            cargo.transform.position=position;

        }

        



    }

}
