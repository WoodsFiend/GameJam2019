using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public float FollowSpeed = 2f;
    public Vector3 offset;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 newPosition = player.position;
            newPosition.x = newPosition.x + offset.x;
            newPosition.y = newPosition.y + offset.y;
            newPosition.z = offset.z;
            transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
        }
        catch { Debug.Log("No Player"); }

        

    }
}
