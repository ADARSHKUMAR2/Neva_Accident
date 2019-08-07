using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public Vector3 NorthDirection;
    public Transform player;
    public Quaternion MissionDirection;

    public RectTransform NorthLayer;
    public RectTransform MissionLayer;

    public Transform missionPlace;

    private void Update()
    {
        ChangeNorthDirection();
        ChangeMissionDirection();
    }

    public void ChangeNorthDirection()
    {
        NorthDirection.z = player.eulerAngles.y;
        NorthLayer.localEulerAngles = NorthDirection;
        
    }

    public void ChangeMissionDirection()
    {
        Vector3 dir = transform.position - missionPlace.position;

        MissionDirection = Quaternion.LookRotation(dir);

        MissionDirection.z = -MissionDirection.y;
        MissionDirection.y = 0;
        MissionDirection.x = 0;

        MissionLayer.localRotation = MissionDirection * Quaternion.Euler(NorthDirection);
    }
}
