using System;
using UnityEngine;

public class PlayerShipBuild : MonoBehaviour {
    [SerializeField] private GameObject[] _shopButtons;
    private GameObject _target;
    GameObject _tempSelection;

    private void Start() {
        TurnOffSelectionHighlights();
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
        throw new NotImplementedException();
    }

    GameObject ReturnClickedObject(RaycastHit hit) {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 100, out hit)) {
            target = hit.collider.gameObject;
        }

        return target;
    }
}