using System;
using UnityEngine;

public class PlayerBullet : MonoBehaviour, IActorTemplate {
    private GameObject actor;
    private int _hitPower;
    private int _health;
    private int _travelSpeed;

    [SerializeField] private SOActorModel BulletModel;

    private void Awake() {
        ActorStats(BulletModel);
    }

    private void Update() {
        transform.position += new Vector3(_travelSpeed, 0, 0) * Time.deltaTime;
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
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
        _hitPower = actorModel.hitPower;
        _health = actorModel.health;
        _travelSpeed = actorModel.speed;
        actor = actorModel.actor;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            if (other.GetComponent<IActorTemplate>() != null) {
                if (_health >= 1) {
                    _health -= other.GetComponent<IActorTemplate>().SendDamage();
                }

                if (_health <= 0) {
                    Die();
                }
            }
        }
    }
}