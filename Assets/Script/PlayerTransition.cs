using System.Collections;
using UnityEngine;

public class PlayerTransition : MonoBehaviour {
    private readonly Vector3 _transitionToEnd = new Vector3(-100, 0, 0);
    private readonly Vector3 _transitionToCompleteGame = new Vector3(7000, 0, 0);
    private readonly Vector3 _readyPos = new Vector3(900, 0, 0);
    private Vector3 _startPos;
    private float _distCovered;
    private float _journeyLength;
    private bool _levelStarted = true;
    private bool _speedOff = false;
    private bool _levelEnds = false;
    private bool _gameCompleted = false;

    public bool LevelEnds {
        get => _levelEnds;
        set => _levelEnds = value;
    }

    public bool GameCompleted {
        get => _gameCompleted;
        set => _gameCompleted = value;
    }

    private void Start() {
        var transform1 = transform;
        transform1.localPosition = Vector3.zero;
        _startPos = transform1.position;
        Distance();
    }

    private void Distance() {
        _journeyLength = Vector3.Distance(_startPos, _readyPos);
    }

    private void Update() {
        if (_levelStarted) PlayerMovement(_transitionToEnd, 10);

        if (_levelEnds) {
            GetComponent<Player>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            Distance();
            PlayerMovement(_transitionToEnd, 200);
        }

        if (_gameCompleted) {
            GetComponent<Player>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;

            PlayerMovement(_transitionToCompleteGame, 200);
        }

        if (_speedOff) Invoke("SpeedOff", 1f);
    }

    private void SpeedOff() {
        transform.Translate(Vector3.left * Time.deltaTime * 800);
    }

    private void PlayerMovement(Vector3 point, float transitionSpeed) {
        if (Mathf.Round(transform.localPosition.x) >= _readyPos.x - 5 &&
            Mathf.Round(transform.localPosition.x) <= _readyPos.x + 5 && Mathf.Round(transform.localPosition.y) >= -5f &&
            Mathf.Round(transform.localPosition.y) <= +5f) {
            if (_levelEnds) {
                _levelEnds = false;
                _speedOff = true;
            }

            if (_levelStarted) {
                _levelStarted = false;
                _distCovered = 0;
                GetComponent<Player>().enabled = true;
            }
        }
        else {
            _distCovered += Time.deltaTime * transitionSpeed;
            var fractionOfJourney = _distCovered / _journeyLength;
            transform.position = Vector3.Lerp(transform.position, point, fractionOfJourney);
        }
    }
}