using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public List<GameObject> enemies;

    [SerializeField] private GameObject enemyPrefab;
    private GameObject enemy;

    void Awake() {
        Messenger<GameObject>.AddListener(GameEvent.ENEMY_DIED, RemoveEnemy);
    }

    void OnDestroy(){
        Messenger<GameObject>.RemoveListener(GameEvent.ENEMY_DIED, RemoveEnemy);
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SpawnWave(int numEnemies) {
        for (int i = 0; i < numEnemies; i++) {
            enemy = Instantiate(enemyPrefab) as GameObject;
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();

            EnemyState level = (EnemyState)Random.Range(1.0f, 8.99f);
            enemyAI.SetState(level);

            int width = 200;
            int depth = 20;
            int buffer = 100;
            int increase = 7;
            int side = (int)Random.Range(1.0f, 4.99f);
            switch (side) {
                case 1: // Top side
                    enemy.transform.position = new Vector2(Random.Range((-1 * width), width), Random.Range(0, depth + (increase * numEnemies)) + buffer);
                    break;
                case 2: // Right Side
                    enemy.transform.position = new Vector2(Random.Range(0, depth + (increase * numEnemies)) + buffer, Random.Range((-1 * width), width));
                    break;
                case 3: // Bottom side
                    enemy.transform.position = new Vector2(Random.Range((-1 * width), width), Random.Range(-1 * (depth + (increase * numEnemies)), 0) - buffer);
                    break;
                case 4: // Left side
                    enemy.transform.position = new Vector2(Random.Range(-1 * (depth + (increase * numEnemies)), 0) - buffer, Random.Range((-1 * width), width));
                    break;
            }
            enemies.Add(enemy);
        }
    }

    public void RemoveEnemy(GameObject enemy) {
        enemies.Remove(enemy);
        Destroy(enemy);
    }
}
