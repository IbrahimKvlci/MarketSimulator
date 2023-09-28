using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    public GameObject _product;

    public int _count;

    [SerializeField] TextMeshProUGUI _countText, _nameText;

    void Start()
    {
        _nameText.text = _product.name;
    }

    
    void Update()
    {
        
        if( _count == 0)
        {
            Destroy(gameObject);
        }
        SetCountText();
    }

    void SetCountText()
    {
        _countText.text = $"{_count}";
    }
}
