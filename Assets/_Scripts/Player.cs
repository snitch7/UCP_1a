using UnityEngine;

public class Player : MonoBehaviour, IActorTemplate {
    int _travelSpeed;
    int _health;
    int _hitPower;
    GameObject _actor;
    GameObject _fire;

    public int Health {
        get { return _health; }
        set => _health = value;
    }

    public GameObject Fire {
        get { return _fire; }
        set => _fire = value;
    }

    GameObject _player;
    float _width;
    float _height;

    private void Start() {
        if (Camera.main != null) {
            _height = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).y - 0.5f);
            _width = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).x - 0.5f);
        }

        _player = GameObject.Find("_Player");
    }

    private void Update() {
        Movement();
        Attack();
    }

    private void Attack() {
        if (!Input.GetButtonDown("Fire1")) return;
        GameObject bullet = GameObject.Instantiate(_fire, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        bullet.transform.SetParent(_player.transform);
        bullet.transform.localScale = new Vector3(7, 7, 7);
    }

    public int SendDamage() {
        return _hitPower;
    }

    public void TakeDamage(int incomingDamage) {
        _health -= incomingDamage;
    }

    public void Die() {
        Destroy(this.gameObject);
        GameManager.Instance.LifeLost();
    }

    public void ActorStats(SOActorModel actorModel) {
        _health = actorModel.Health;
        _travelSpeed = actorModel.Speed;
        _hitPower = actorModel.HitPower;
        _fire = actorModel.ActorsBullets;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            if (_health >= 1) {
                if (transform.Find("energy +1(Clone)")) {
                    Destroy(transform.Find("energy +1(Clone)").gameObject);
                    _health -= other.GetComponent<IActorTemplate>().SendDamage();
                }
                else {
                    _health -= 1;
                }
            }

            if (_health <= 0) {
                Die();
            }
        }
    }

    void Movement() {
        if (Input.GetAxisRaw("Horizontal") > 0) {
            if (transform.localPosition.x < _width + _width / 0.9f) {
                transform.localPosition +=
                    new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * _travelSpeed, 0, 0);
            }
        }

        if (Input.GetAxisRaw("Horizontal") < 0) {
            if (transform.localPosition.x > _width + _width / 6) {
                transform.localPosition +=
                    new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * _travelSpeed, 0, 0);
            }
        }

        if (Input.GetAxisRaw("Vertical") < 0) {
            if (transform.localPosition.y > -_height / 3f) {
                transform.localPosition +=
                    new Vector3(0, Input.GetAxisRaw("Vertical") * Time.deltaTime * _travelSpeed, 0);
            }
        }

        if (Input.GetAxisRaw("Vertical") > 0) {
            if (transform.localPosition.y < _height / 2.5f) {
                transform.localPosition +=
                    new Vector3(0, Input.GetAxisRaw("Vertical") * Time.deltaTime * _travelSpeed, 0);
            }
        }
    }
}