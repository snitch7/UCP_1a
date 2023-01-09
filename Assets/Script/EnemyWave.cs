using UnityEngine;

public class EnemyWave : MonoBehaviour, IActorTemplate {
    private int health;
    private int travelSpeed;
    private int fireSpeed;

    private int hitPower;

    //wave enemy
    [SerializeField] private float verticalSpeed = 2;
    [SerializeField] private float verticalAmplitude = 1;
    private Vector3 sineVer;
    private float time;
    private int score;

    private void Update() {
        Attack();
    }

    public void ActorStats(SOActorModel actorModel) {
        health = actorModel.health;
        travelSpeed = actorModel.speed;
        hitPower = actorModel.hitPower;
        score = actorModel.score;
    }

    public void Die() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        // if the player or their bullet hits you.
        if (other.tag == "Player") {
            if (health >= 1) health -= other.GetComponent<IActorTemplate>().SendDamage();
            if (health <= 0) {
                GameManager.Instance.GetComponent<ScoreManager>().SetScore(score);
                Die();
            }
        }
    }

    public void TakeDamage(int incomingDamage) {
        health -= incomingDamage;
    }

    public int SendDamage() {
        return hitPower;
    }

    public void Attack() {
        time += Time.deltaTime;
        sineVer.y = Mathf.Sin(time * verticalSpeed) *
                    verticalAmplitude;
        transform.position = new Vector3(transform.position.x
                                         + travelSpeed * Time.deltaTime,
            transform.position.y + sineVer.y,
            transform.position.z);
    }
}