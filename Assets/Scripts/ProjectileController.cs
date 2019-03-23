using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed=0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            transform.Translate(Vector3.up * speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            try
            {
                collision.gameObject.GetComponent<EnemyAI>().TakeDamage(1);
                Destroy(this.gameObject);
            }
            catch { Debug.Log("Not Damagable"); }
        }
        
    }
}
