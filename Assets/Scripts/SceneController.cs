using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {
    EnemySpawner enemySpawner;
    public GameObject player;
    private int wave;
    private bool nextWave;
    private bool canRespawn;
    public Text enemyCount;
    public Text waveNum;

    // Start is called before the first frame update
    void Start() {
        wave = StartData.startWave;
        Debug.Log(wave);
        enemySpawner = gameObject.GetComponent<EnemySpawner>();
        nextWave = true;
        canRespawn = true;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update() {
        
        Respawn();
        UpdateHUD();
        if (enemySpawner.enemies.Count < 1) {
            Debug.Log("no enemies");
            
            if (nextWave) {
                StartCoroutine("WaveDelay");
            }
        }
    }
    void Respawn()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            if (canRespawn)
            {
                StartCoroutine("WaitToRespawn");

            }
        }
    }
    void UpdateHUD()
    {
        waveNum.text ="Wave: " + (wave-1).ToString();
        enemyCount.text = "Enemies: " + (enemySpawner.enemies.Count).ToString();

    }
    IEnumerator WaitToRespawn()
    {
        canRespawn = false;
        yield return new WaitForSeconds(5);
        GameObject thePlayer = Instantiate(player);
        thePlayer.GetComponent<Health>().healthBar = GameObject.Find("PlayerHealthBar").GetComponent<Image>();
        thePlayer.GetComponent<AngleCurrencyManager>().angleText = GameObject.Find("PlayerAngles").GetComponent<Text>();
        canRespawn = true;

    }

    IEnumerator WaveDelay() {
        nextWave = false;
        
        Debug.Log("Start " + Time.time);
        yield return new WaitForSeconds(10.0f);
        enemySpawner.SpawnWave(wave);
        if(wave < 100)
        {
            wave++;
        }
        else
        {
            wave = 100;
        }
        
        nextWave = true;
    }

   
}
