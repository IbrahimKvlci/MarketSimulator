using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    public GameObject _product;
    public int _count;

    void Start()
    {
        
    }

    
    void Update()
    {
        if( _count == 0)
        {
            Destroy(gameObject);
        }
    }
}
