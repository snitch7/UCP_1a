using UnityEngine;

public class PlayerBulletsShot : MonoBehaviour, IActorTemplate {
    GameObject actor;
    int hitPower;
    int health;
    int travelSpeed;
    [SerializeField] SOActorModel bulletModel;

    void Awake() {
        ActorStats(bulletModel);
    }

    public int SendDamage() {
        return hitPower;
    }

    public void TakeDamage(int incomingDamage) {
        health -= incomingDamage;
    }

    public void Die() {
        Destroy(this.gameObject);
    }

    public void ActorStats(SOActorModel actorModel) {
        hitPower = actorModel.HitPower;
        health = actorModel.Health;
        travelSpeed = actorModel.Speed;
        actor = actorModel.Actor;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            if (health >= 1) {
                health -= other.GetComponent<IActorTemplate>().SendDamage();
            }

            if (health <= 0) {
                Die();
            }
        }
    }

    void Update() {
        transform.position += new Vector3(travelSpeed, 0, 0) * Time.deltaTime;
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}