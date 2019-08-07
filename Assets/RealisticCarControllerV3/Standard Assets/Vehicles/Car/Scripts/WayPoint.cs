using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public GameObject[] waypoint;
    public Transform target;
    int current = 0;
    public float speed = 10;
    float wpRadius = 1;
    void Update ()
    {
	    if(Vector3.Distance(waypoint[current].transform.position,transform.position)<wpRadius)
        {
            current++;
            //if(current>=waypoint.Length)
            //{
            //    current = 0;
            //}
        }
        Quaternion rotation = Quaternion.LookRotation(waypoint[current].transform.position, Vector3.up);
        transform.rotation = rotation;
        transform.position = Vector3.MoveTowards(transform.position, waypoint[current].transform.position, Time.deltaTime*speed);
	}
}
