using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float speed;
    public float timeWait;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("WaitDestroy");
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }

    IEnumerator WaitDestroy()
    {

        yield return new WaitForSeconds(timeWait);
        Destroy(this.gameObject);
    }
}
