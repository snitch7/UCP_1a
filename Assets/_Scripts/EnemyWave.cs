using System;
using UnityEngine;

public class EnemyWave : MonoBehaviour, IActorTemplate {
    // vars to be updated by SO - basicwave_enemy
    private int _health;
    private int _travelSpeed;
    private int _fireSpeed;
    private int _hitPower;

    // vars specific to this enemy
    [SerializeField] private float VerticalSpeed = 2;
    [SerializeField] private float VerticalAmplitude = 1;
    private Vector3 _sineVer;
    private float _time;


    private void Update() {
        Attack();
    }

    private void Attack() {
        _time += Time.deltaTime;
        _sineVer.y = Mathf.Sin(_time * VerticalSpeed) * VerticalAmplitude;
        transform.position = new Vector3(transform.position.x + _travelSpeed * Time.deltaTime,
            transform.position.y + _sineVer.y, transform.position.z);
    }

    public int SendDamage() {
        return _hitPower;
    }

    public void TakeDamage(int incomingDamage) {
        _health -= incomingDamage;
    }

    public void Die() {
        Destroy(this.gameObject);
    }

    public void ActorStats(SOActorModel actorModel) {
        _health = actorModel.health;
        _travelSpeed = actorModel.speed;
        _hitPower = actorModel.hitPower;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (_health >= 1) {
                _health -= other.GetComponent<IActorTemplate>().SendDamage();
            }

            if (_health <= 0) {
                Die();
            }
        }
    }
}