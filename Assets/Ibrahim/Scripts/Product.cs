using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product:MonoBehaviour
{
    [field: SerializeField]  public int Id { get; set; }
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public int CategoryId { get; set; }
    [field: SerializeField] public Sprite Image { get; set; }
    [field: SerializeField] public float Price { get; set; }
    [field: SerializeField] public GameObject ProductsOnShelf { get; set; }
}
