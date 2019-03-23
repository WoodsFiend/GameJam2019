using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    private float startHealth;
    public Image healthBar;
    public GameObject mainCanvas;
    // Start is called before the first frame update
    void Start()
    {
        startHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / startHealth;
        if(health <= 0)
        {
            if(this.gameObject.name == "House")
            {
                GameObject gameOver = mainCanvas.transform.GetChild(6).gameObject;
                //splash screen death
                gameOver.SetActive(true);
                Time.timeScale = 0;

            }
            Destroy(this.gameObject);
        }
    }
}
