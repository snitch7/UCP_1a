using UnityEngine;

public class PlayerBulletsShot : MonoBehaviour, IActorTemplate {
    private GameObject actor;
    private int hitPower;
    private int health;
    private int travelSpeed;
    [SerializeField] private SOActorModel bulletModel;

    private void Awake() {
        ActorStats(bulletModel);
    }

    public int SendDamage() {
        return hitPower;
    }

    public void TakeDamage(int incomingDamage) {
        health -= incomingDamage;
    }

    public void Die() {
        Destroy(gameObject);
    }

    public void ActorStats(SOActorModel actorModel) {
        hitPower = actorModel.hitPower;
        health = actorModel.health;
        travelSpeed = actorModel.speed;
        actor = actorModel.actor;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            if (health >= 1) health -= other.GetComponent<IActorTemplate>().SendDamage();
            if (health <= 0) Die();
        }
    }

    private void Update() {
        transform.position += new Vector3(travelSpeed, 0, 0) * Time.deltaTime;
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}