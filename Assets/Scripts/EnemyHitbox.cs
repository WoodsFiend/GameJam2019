using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour {
    private bool targetDetected;
    private EnemyAI parentAI;

    // Start is called before the first frame update
    void Start() {
        parentAI = gameObject.GetComponentInParent<EnemyAI>();
    }

    // Update is called once per frame
    void Update() {
        
    }
    
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Friendly" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Wall" ) {
            //Debug.Log("Friendly in range!");
            if (Time.time > parentAI.nextAttack) {
                parentAI.nextAttack = Time.time + parentAI.attackRate;
                Health targetHP = collision.gameObject.GetComponent<Health>();
                targetHP.health -= parentAI.damage;
                //Debug.Log("Attack! " + "Damage: " + parentAI.damage + " " + Time.time);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log("Something is in range.");
    }

    
    private void OnTriggerExit2D(Collider2D collision) {
        
    }
}
