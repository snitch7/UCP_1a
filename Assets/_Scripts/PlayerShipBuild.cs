using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipBuild : MonoBehaviour {
    [SerializeField] private GameObject[] _shopButtons;
    private GameObject _target;
    GameObject _tempSelection;
    private GameObject _textBoxPanel;

    private void Start() {
        TurnOffSelectionHighlights();
        _textBoxPanel = GameObject.Find("textBoxPanel");
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
            if (_target!=null) {
                if (_target.transform.Find("itemText")) {
                    TurnOffSelectionHighlights();
                    Select();
                    UpdateDescriptionBox();
                }
            }
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