using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed = 25f;
    public GameObject attack;
    public bool canAttack;
    public float attackCooldwn;
    float moveH;
    float moveV;

    
    void Start()
    {
        canAttack = true;
        moveH = 0;
        moveV = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        //Controls Player Movement
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveH * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, moveV * maxSpeed);

        //player turns towards mouse
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );
        transform.up = direction;

        //attack
        if(canAttack && (Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("Fire1")))
        {
            
            Instantiate(attack, this.transform);
            StartCoroutine("AttackCooldown");
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldwn);
        canAttack = true;
    }

}
