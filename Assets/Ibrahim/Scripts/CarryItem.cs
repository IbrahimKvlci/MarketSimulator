using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryItem : MonoBehaviour
{
    Outline _outline;

    GameObject _player;
    void Start()
    {
        _outline = GetComponent<Outline>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player.GetComponent<Player>()._isOutlined)
        {
            _outline.enabled = false;
        }
       
    }
}
