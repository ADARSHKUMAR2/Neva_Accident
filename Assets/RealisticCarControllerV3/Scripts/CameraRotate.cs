using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RCC_CameraConfig))]
public class CameraRotate : MonoBehaviour
{
    public float speed = 10;
    public RCC_CameraConfig cameraConfig_1;

    private void Update()
    {
        if(RCC_CameraConfig.itsPauseTime==1)
        {
            transform.Rotate(0, speed * Time.deltaTime * 1000, 0);
            Debug.Log("ROtate it man!");
        }
        else
        {
            transform.Rotate(0, speed * Time.deltaTime , 0);
            Debug.Log("SLow Rotate Man!");
        }
    }
}
