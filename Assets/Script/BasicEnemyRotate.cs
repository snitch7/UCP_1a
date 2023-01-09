using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicEnemyRotate : MonoBehaviour {
     private float speed = 0;
     [SerializeField] float _minSpeed = 150;
     [SerializeField] private float _maxSpeed = 250;

     private void Awake() {
         speed = Random.Range(_minSpeed, _maxSpeed);
     }

     private void Update() {
        transform.Rotate(Vector3.left * Time.deltaTime * speed);
    }
}