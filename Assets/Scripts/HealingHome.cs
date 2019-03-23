using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingHome : MonoBehaviour
{
    public int healRate;
    bool canHeal;
    // Start is called before the first frame update
    void Start()
    {
        canHeal = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canHeal)
        {
            StartCoroutine(HealOverTime(collision.gameObject));

        }
    }
    IEnumerator HealOverTime(GameObject player)
    {
        canHeal = false;

        try
        {
            if (player.gameObject.GetComponent<Health>().health < 100) {
                player.gameObject.GetComponent<Health>().health += healRate;
            }
        }
        catch { }

        yield return new WaitForSeconds(1);
        
        canHeal = true;
    }
}
