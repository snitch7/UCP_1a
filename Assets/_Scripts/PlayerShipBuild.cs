using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipBuild : MonoBehaviour {
    [SerializeField] private GameObject[] _shopButtons;

    [SerializeField] GameObject[] _visualWeapons;
    [SerializeField] private SOActorModel _defaultPlayerShip;
    
    private GameObject _playerShip;
    private GameObject _buyButton;
    private GameObject _bankObject;
    private int _bank = 600;
    private bool _purchaseMade = false;

    private GameObject _target;
    GameObject _tempSelection;
    private GameObject _textBoxPanel;

    private void Start() {
        TurnOffSelectionHighlights();
        _textBoxPanel = GameObject.Find("textBoxPanel");
        _purchaseMade = false;
        _bankObject = GameObject.Find("bank");
        _bankObject.GetComponentInChildren<TextMesh>().text = _bank.ToString();
        _buyButton = _textBoxPanel.transform.Find("BUY ?").gameObject;

        TurnOffPlayerShipVisuals();
        PreparePlayerShipForUpgrade();
    }

    private void PreparePlayerShipForUpgrade() {
        throw new NotImplementedException();
    }

    private void TurnOffPlayerShipVisuals() {
        for (int i = 0; i < _visualWeapons.Length; i++) {
            _visualWeapons[i].gameObject.SetActive(false);
        }
    }

    private void TurnOffSelectionHighlights() {
        for (int i = 0; i < _shopButtons.Length; i++) {
            _shopButtons[i].SetActive(false);
        }
    }

    private void Update() {
        AttemptSelection();
    }

    private void AttemptSelection() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hitInfo;
            _target = ReturnClickedObject(out hitInfo);
            string itemTextCached = _target.transform.Find("itemText").GetComponent<TextMesh>().text;
            Transform itemTransformCached = _target.transform.Find("itemText");
            if (_target != null) {
                if (itemTransformCached) {
                    TurnOffSelectionHighlights();
                    Select();
                    UpdateDescriptionBox();

                    // if not already sold:
                    if (itemTextCached != "SOLD") {
                        Affordable();
                        LackOfFunds();
                    }
                    else if (itemTextCached == "SOLD") {
                        SoldOut();
                    }
                }
            }
        }
    }

    private void SoldOut() {
        print("Sold Out!");
    }

    private void LackOfFunds() {
        int itemCost = System.Int32.Parse(_target.transform.GetComponent<ShopPiece>().ShopSelection.Cost);
        if (_bank < itemCost) {
            print("Cannot buy");
            _buyButton.SetActive(false);
        }
    }

    private void Affordable() {
        int itemCost = System.Int32.Parse(_target.transform.GetComponent<ShopPiece>().ShopSelection.Cost);
        if (_bank >= itemCost) {
            print("Can buy");
            _buyButton.SetActive(true);
        }
    }

    private void UpdateDescriptionBox() {
        _textBoxPanel.transform.Find("name").gameObject.GetComponent<TextMesh>().text =
            _tempSelection.GetComponentInParent<ShopPiece>().ShopSelection.IconName;
        _textBoxPanel.transform.Find("desc").gameObject.GetComponent<TextMesh>().text =
            _tempSelection.GetComponentInParent<ShopPiece>().ShopSelection.Description;
    }

    private void Select() {
        _tempSelection = _target.transform.Find("SelectionQuad").gameObject;
        _tempSelection.SetActive(true);
    }

    GameObject ReturnClickedObject(out RaycastHit hit) {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 100, out hit)) {
            target = hit.collider.gameObject;
        }

        return target;
    }
}