using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuilding : MonoBehaviour
{
    //where the building is placed
    public Transform buildSpawn;
    //array of buildings from prefab
    public GameObject[] buildings;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //create button press for each item in the array
        if (Input.GetKeyDown("1"))
        {
            if (GetComponent<AngleCurrencyManager>().CanBuild("Wall"))
            {
                GameObject wall = Instantiate(buildings[0], buildSpawn.position, buildSpawn.rotation);
            }
        }
        if (Input.GetKeyDown("2"))
        {
            if (GetComponent<AngleCurrencyManager>().CanBuild("Turret"))
            {
                GameObject turret = Instantiate(buildings[1], buildSpawn.position, buildSpawn.rotation);
            }
        }
        if (Input.GetKeyDown("3"))
        {
            if (GetComponent<AngleCurrencyManager>().CanBuild("Mine"))
            {
                GameObject mine = Instantiate(buildings[2], buildSpawn.position, buildSpawn.rotation);
            }
        }
    }
}
