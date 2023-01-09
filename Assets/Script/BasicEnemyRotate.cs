using UnityEngine;

public class BasicEnemyRotate : MonoBehaviour {
    [SerializeField] private float speed = 0;

    private void Update() {
        transform.Rotate(Vector3.left * Time.deltaTime * speed);
    }
}