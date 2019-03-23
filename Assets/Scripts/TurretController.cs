using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public bool isAttacking;
    public List<GameObject> nearbyEnemies;
    public GameObject target;
    public float targetDistance;
    public bool isShooting;
    public float fireRate;
    bool canChange;

    public GameObject bulletSpawn;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        canChange = true;
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        //checks if enemies are in nearby list
        if (nearbyEnemies.Count >= 1)
        {
            try
            {
                targetDistance = Vector2.Distance(transform.parent.position, target.transform.position);
            }
            catch
            {
            }

            if (canChange)
            {
                StartCoroutine("WaitToChange");
                //loops through list to find the nearest enemy
                foreach (GameObject enemy in nearbyEnemies)
                {
                    float distance = Vector2.Distance(transform.parent.position, enemy.transform.position);
                    //first target enters
                    if (target == null)
                    {
                        target = enemy;
                        //set current target distance
                        targetDistance = Vector2.Distance(transform.parent.position, target.transform.position);
                    }
                    //next enemy in list was closer 
                    else if (distance < targetDistance)
                    {
                        target = enemy;
                        //set current target distance
                        targetDistance = Vector2.Distance(transform.parent.position, target.transform.position);
                    }
                }
            }
            //rotate head towards enemy
            Vector3 targetPosition = new Vector3(0, 0, 0);
            try
            {
                targetPosition = target.transform.position;
            }
            catch
            {
                nearbyEnemies.Remove(target);
            }
            //targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);
            Vector2 direction = new Vector2(
                targetPosition.x - transform.parent.position.x,
                targetPosition.y - transform.parent.position.y
                );
            transform.parent.up = direction;


            //checks if turret is shooting already
            if (isShooting == false)
            {
                //start shooting coroutine
                StartCoroutine(Shoot(target));
            }
        }

       
        //the list of nearby enemies was empty, stop shooting
        else
        {
            target = null;
            targetDistance = 0;
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if enemy is near
        if(collision.gameObject.tag == "Enemy")
        {
            nearbyEnemies.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //checks if enemy moves to far away
        if (collision.gameObject.tag == "Enemy")
        {
            nearbyEnemies.Remove(collision.gameObject);
        }
    }

    IEnumerator Shoot(GameObject target)
    {
        isShooting = true;
        //shoot projectile per second at the target object
        GameObject theBullet = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        yield return new WaitForSeconds(fireRate);
        Destroy(theBullet);
        isShooting = false;
    }
    IEnumerator WaitToChange()
    {
        canChange = false;
        yield return new WaitForSeconds(0.49f);
        canChange = true;
    }
}
