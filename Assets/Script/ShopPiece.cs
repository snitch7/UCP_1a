using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPiece : MonoBehaviour {
    [SerializeField] private SOShopSelection shopSelection;

    public SOShopSelection ShopSelection {
        get => shopSelection;
        set => shopSelection = value;
    }

    private void Awake() {
        //icon slot
        if (GetComponentInChildren<SpriteRenderer>() != null)
            GetComponentInChildren<SpriteRenderer>().sprite = shopSelection.icon;
        //selection value
        if (transform.Find("itemText")) GetComponentInChildren<TextMesh>().text = shopSelection.cost;
    }
}