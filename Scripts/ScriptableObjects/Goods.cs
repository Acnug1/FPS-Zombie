using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Good", menuName = "Goods/Create new good", order = 51)]
public class Goods : ScriptableObject
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;

    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon;
}
