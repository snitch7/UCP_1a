
using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerSpawner : MonoBehaviour {
    private SOActorModel actorModel;
    private GameObject playerShip;

    private void Start() {
        CreatePlayer();
    }

    void CreatePlayer() {
        actorModel = Object.Instantiate(Resources.Load("Player_Default")) as SOActorModel;
        if (actorModel != null) {
            playerShip = GameObject.Instantiate(actorModel.actor, this.transform, true) as GameObject;
            playerShip.GetComponent<Player>().ActorStats(actorModel);
        }

        playerShip.transform.rotation=Quaternion.Euler(0,180,0);
        playerShip.transform.localScale = new Vector3(60, 60, 60);
        playerShip.GetComponentInChildren<ParticleSystem>().transform.localScale = new Vector3(25, 25, 25);
        playerShip.name = "Player";
        playerShip.transform.position=Vector3.zero;
    }
}
