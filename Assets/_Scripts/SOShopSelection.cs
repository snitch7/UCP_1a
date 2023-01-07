using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Shop Piece",menuName = "Create Shop Piece")]
public class SOShopSelection : ScriptableObject {
    public Sprite Icon;
    public string IconName;
    public string Description;
    public string Cost;
}
