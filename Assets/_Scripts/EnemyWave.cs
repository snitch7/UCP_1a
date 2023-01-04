using UnityEngine;

public class EnemyWave : MonoBehaviour, IActorTemplate {
    // vars to be updated by SO - basicwave_enemy
    private int _health;
    private int _travelSpeed;
    private int _fireSpeed;
    private int _hitPower;
    private int _score;

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
        var transform1 = transform;
        var position = transform1.position;
        position = new Vector3(position.x + _travelSpeed * Time.deltaTime,
            position.y + _sineVer.y, position.z);
        transform1.position = position;
    }

    public int SendDamage() {
        return _hitPower;
    }

    public void TakeDamage(int incomingDamage) {
        _health -= incomingDamage;
    }

    public void Die() {
        Destroy(this.gameObject);
        GameManager.Instance.GetComponent<ScoreManager>().SetScore(_score);
    }

    public void ActorStats(SOActorModel actorModel) {
        _health = actorModel.Health;
        _travelSpeed = actorModel.Speed;
        _hitPower = actorModel.HitPower;

        _score = actorModel.Score;
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