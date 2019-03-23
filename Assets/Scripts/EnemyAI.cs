using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { dead = 0, triangle, square, pentagon, hexagon, heptagon, octagon, nonagon, decagon };

public class EnemyAI : MonoBehaviour {
    // Global variables
    public float attackRate = 3f;
    public float nextAttack = 0.0f;
    public Sprite[] shapes = new Sprite[8];
    public List<GameObject> targets;
    public GameObject angle;

    // Private variables
    private EnemyState state;
    public int damage;
    private int health;
    private int rotationSpeed;
    private float enemySpeed;

    private GameObject aggroTarget;
    private SpriteRenderer myRenderer;
    
    // Start is called before the first frame update
    void Start() {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        rotationSpeed = -2;
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(0, 0, rotationSpeed);

        // Update the shape & colour based on the state
        switch (state) { // Should be health????
            case EnemyState.dead:
                Messenger<GameObject>.Broadcast(GameEvent.ENEMY_DIED, gameObject);
                break;
            case EnemyState.triangle:
                myRenderer.sprite = shapes[0];
                myRenderer.color = new Color32(255, 0, 0, 255);
                break;
            case EnemyState.square:
                myRenderer.sprite = shapes[1];
                myRenderer.color = new Color32(255, 153, 51, 255);
                break;
            case EnemyState.pentagon:
                myRenderer.sprite = shapes[2];
                myRenderer.color = new Color32(255, 204, 102, 255);
                break;
            case EnemyState.hexagon:
                myRenderer.sprite = shapes[3];
                myRenderer.color = new Color32(255, 255, 0, 255);
                break;
            case EnemyState.heptagon:
                myRenderer.sprite = shapes[4];
                myRenderer.color = new Color32(204, 255, 153, 255);
                break;
            case EnemyState.octagon:
                myRenderer.sprite = shapes[5];
                myRenderer.color = new Color32(0, 255, 0, 255);
                break;
            case EnemyState.nonagon:
                myRenderer.sprite = shapes[6];
                myRenderer.color = new Color32(0, 255, 255, 255);
                break;
            case EnemyState.decagon:
                myRenderer.sprite = shapes[7];
                myRenderer.color = new Color32(0, 153, 255, 255);
                break;
        }

        
        // Find the nearest target
        foreach (GameObject target in targets) {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (aggroTarget == null || distance < Vector2.Distance(transform.position, aggroTarget.transform.position)) {
                aggroTarget = target;
            }
        }

        float currSpeed = enemySpeed;
        // Move towards a target
        if (aggroTarget != null) {
            transform.position = Vector2.MoveTowards(transform.position, aggroTarget.transform.position, currSpeed * Time.deltaTime);
        } else {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0.0f, 0.0f), (currSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log("I've found something!");
        if (collision.gameObject.tag == "Friendly" || collision.gameObject.tag == "Player") {
            //Debug.Log("It's a target, get them!");
            targets.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Friendly" || collision.gameObject.tag == "Player") {
            targets.Remove(collision.gameObject);
        }
    }

    public void SetState(EnemyState newState) {
        state = newState;
        health = (int)newState;
        damage = 9 - (int)newState;
        enemySpeed = 2.0f + ((int)newState * 2);
        //Debug.Log("New State | State: " + state + " Health: " + health + " Damage: " + damage + " Speed: " + enemySpeed);
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health < 0) {
            health = 0;
        }
        for(int i = 0; i < damage; i++)
        {
            Instantiate(angle,transform.position, transform.rotation);
        }
        SetState((EnemyState)health);
    }
}
