using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private SOActorModel actorModel;
    [SerializeField] private float spawnRate;
    [SerializeField] [Range(0, 10)] private int quantity;

    private GameObject enemies;

    private void Start() {
        enemies = GameObject.Find("_Enemies");
        StartCoroutine(FireEnemy(quantity, spawnRate));
    }

    private IEnumerator FireEnemy(int qty, float spwnRte) {
        for (var i = 0; i < qty; i++) {
            var enemyUnit = CreateEnemy();
            enemyUnit.gameObject.transform.SetParent(transform);
            enemyUnit.transform.position = transform.position;
            yield return new WaitForSeconds(spwnRte);
        }

        yield return null;
    }

    private GameObject CreateEnemy() {
        var enemy = Instantiate(actorModel.actor) as GameObject;
        enemy.GetComponent<IActorTemplate>().ActorStats(actorModel);
        enemy.name = actorModel.actorName.ToString();
        return enemy;
    }
}