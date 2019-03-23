using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleCurrencyManager : MonoBehaviour
{
    public int angles;
    public Text angleText;
    bool canWall;
    bool canTurret;
    bool canMine;

    // Start is called before the first frame update
    void Start()
    {
        canWall = true;
        canTurret = true;
        canMine = true;
        angles = StartData.angles;
    }
    private void OnDestroy()
    {
        StartData.angles = angles;
    }

    // Update is called once per frame
    void Update()
    {
        angleText.text = "Angles: " + angles.ToString();
    }

    //checks if you have enough angles to build and starts timer.  make calls to check if you are allowed to build
    public bool CanBuild(string buildable)
    {
        
        if(canWall && buildable == "Wall" && angles >= 10)
        {
            StartCoroutine(Timer(buildable,2));
            angles -= 10;
            return true;
        }
        else if(canTurret && buildable == "Turret" && angles >= 25)
        {
            StartCoroutine(Timer(buildable, 3));
            angles -= 25;
            return true;
        }
        else if (canMine && buildable == "Mine" && angles >= 50)
        {
            StartCoroutine(Timer(buildable, 5));
            angles -= 50;
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator Timer(string buildable,float time)
    {
        if(buildable == "Wall")
        {
            canWall = false;
        }
        if (buildable == "Turret")
        {
            canTurret = false;
        }
        if (buildable == "Mine")
        {
            canMine = false;
        }
        yield return new WaitForSeconds(time);

        if (buildable == "Wall")
        {
            canWall = true;
        }
        if (buildable == "Turret")
        {
            canTurret = true;
        }
        if (buildable == "Mine")
        {
            canMine = true;
        }
    }
}
