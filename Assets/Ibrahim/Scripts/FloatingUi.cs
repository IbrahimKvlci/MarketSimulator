using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingUi : MonoBehaviour
{
    [SerializeField] GameObject _cam;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_cam.transform);
    }
}
