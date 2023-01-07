using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
    private SOActorModel _actorModel;
    private GameObject _playerShip;

    private void Start() {
        CreatePlayer();
    }

    void CreatePlayer() {
        _actorModel = Instantiate(Resources.Load("Player_Default")) as SOActorModel;
        if (_actorModel != null) {
            _playerShip = Instantiate(_actorModel.Actor, this.transform, true);
            _playerShip.GetComponent<Player>().ActorStats(_actorModel);
        }

        _playerShip.transform.rotation = Quaternion.Euler(0, 180, 0);
        _playerShip.transform.localScale = new Vector3(60, 60, 60);
        _playerShip.GetComponentInChildren<ParticleSystem>().transform.localScale = new Vector3(25, 25, 25);
        _playerShip.name = "Player";
        _playerShip.transform.position = Vector3.zero;
    }
}