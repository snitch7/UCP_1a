using UnityEngine;

public class BShotComponent : MonoBehaviour {
    [SerializeField] private GameObject bShotBullet;

    private void Start() {
        if (GetComponentInParent<Player>()) GetComponentInParent<Player>().Fire = bShotBullet;
    }
}