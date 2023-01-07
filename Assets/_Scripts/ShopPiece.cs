using System;
using UnityEngine;

public class ShopPiece : MonoBehaviour {
    [SerializeField] private SOShopSelection _shopSelection;

    public SOShopSelection ShopSelection {
        get => _shopSelection;
        set => _shopSelection = value;
    }

    private void Awake() {
        // icon slot
        if (GetComponentInChildren<SpriteRenderer>() !=null) {
            GetComponentInChildren<TextMesh>().text = _shopSelection.Cost;
            GetComponentInChildren<SpriteRenderer>().sprite = _shopSelection.Icon;
        }
    }
}
