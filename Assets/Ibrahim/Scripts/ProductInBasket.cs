using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProductInBasket : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _totalCountText;

    public int _id;
    public int _count;

    void Start()
    {
        
    }


    void Update()
    {
        _totalCountText.text = _count.ToString();
        if (_count == 0)
        {
            Destroy();
        }
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

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
