using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public List<GameObject> inRange;
    public SpriteRenderer button;
    bool canBlink;
    // Start is called before the first frame update
    void Start()
    {
        canBlink = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canBlink)
        {
            StartCoroutine("ButtonBlink");
        }
    }
    
    IEnumerator ButtonBlink()
    {
        canBlink = false;
        button.color = Color.grey;
        yield return new WaitForSeconds(0.5f);
        button.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        canBlink = true;
    }
    //enemy in range of explosion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            inRange.Add(collision.gameObject);
        }
        
    }
    //enemy leaves range of explosion
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            inRange.Remove(collision.gameObject);
        }

    }
    //enemy walked into mine
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if(collision.gameObject.tag == "Enemy")
        {   
            //deal damage to each enemy here
            foreach (GameObject enemy in inRange)
            {
                try
                {
                    EnemyAI target = enemy.gameObject.GetComponent<EnemyAI>();
                    target.TakeDamage(2);
                }
                catch { Debug.Log("Not an Enemy"); }
            }
            Destroy(this.gameObject);
        }
        
    }
}
