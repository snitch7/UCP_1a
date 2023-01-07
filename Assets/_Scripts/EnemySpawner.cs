using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private SOActorModel ActorModel;
    [SerializeField] private float SpawnRate;
    [SerializeField] [Range(0, 10)] private int Quantity;
    private GameObject _enemies;

    private void Awake() {
        _enemies = GameObject.Find("_Enemies");
        StartCoroutine(FireEnemy(Quantity, SpawnRate));
    }

    private IEnumerator FireEnemy(int quantity, float spawnRate) {
        for (int i = 0; i < quantity; i++) {
            GameObject enemyUnit = CreateEnemy();
            enemyUnit.transform.SetParent(this.transform);
            enemyUnit.transform.position = this.transform.position;
            yield return new WaitForSeconds(spawnRate);
        }

        yield return null;
    }

    private GameObject CreateEnemy() {
        GameObject enemy = Instantiate(ActorModel.Actor);
        enemy.GetComponent<IActorTemplate>().ActorStats(ActorModel);
        enemy.name = ActorModel.ActorName;
        return enemy;
    }
}