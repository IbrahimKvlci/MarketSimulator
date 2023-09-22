using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopControl : MonoBehaviour
{
    [SerializeField] GameObject _middleSection;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void GetScreen(GameObject screen)
    {
        for (int i = 0; i < _middleSection.transform.childCount; i++)
        {
            _middleSection.transform.GetChild(i).gameObject.SetActive(false);
        }
        screen.SetActive(true);
    }
}
