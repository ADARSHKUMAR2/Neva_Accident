//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Camera/Auto Camera Config")]
public class RCC_CameraConfig : MonoBehaviour {

	public bool automatic = true;
	private Bounds combinedBounds;

	public float distance = 10f;
	public float height = 5f;

    public static int itsPauseTime = 0;

    public GameObject objectToActivate;

    
    private void Start()
    {
        StartCoroutine(PauseGame(1f));
    }

    public IEnumerator PauseGame(float pauseTIme)
    {
        yield return new WaitForSeconds(10f);
        objectToActivate.SetActive(true);
        //if(GameObject.FindGameObjectWithTag("TurnCam"))
        //{
        //    Time.timeScale= 1f * 1000f;
        //}
        Time.timeScale = 0.001f;
        float pauseEndTime = Time.realtimeSinceStartup + 10;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            itsPauseTime = 1;
            yield return 0;
        }
        Time.timeScale = 1;
        itsPauseTime = 0;
    }
    

    //private IEnumerator ActivationRoutine()
    //{
    //    yield return new WaitForSeconds(10f);   //after 10 sec
    //    //if(!GameObject.FindGameObjectWithTag("TurnCam"))
    //    Time.timeScale = 0f;
    //    Debug.Log("Pause Now");
    //    //objectToActivate.SetActive(true);

    //    yield return new WaitForSeconds(5f);   //after 25 sec
    //    Time.timeScale = 1f;
    //    Debug.Log("Play Now");
    //    objectToActivate.SetActive(false);

    //    yield return new WaitForSeconds(5f);   // after 30 sec
    //    //Time.timeScale = 0f;
    //    objectToActivate.SetActive(true);
    //}


    void Awake(){

		if(automatic){

			Quaternion orgRotation = transform.rotation;
			transform.rotation = Quaternion.identity;

			distance = MaxBoundsExtent(transform) * 2.5f;
			height = MaxBoundsExtent(transform) * .5f;

			transform.rotation = orgRotation;



		}

	}

	public void SetCameraSettings () {

		RCC_Camera cam = GameObject.FindObjectOfType<RCC_Camera>();
		 
		if(!cam)
			return;
			
		cam.distance = distance;
		cam.height = height;

	}

	public static float MaxBoundsExtent(Transform obj){
		// get the maximum bounds extent of object, including all child renderers,
		// but excluding particles and trails, for FOV zooming effect.

		var renderers = obj.GetComponentsInChildren<Renderer>();

		Bounds bounds = new Bounds();
		bool initBounds = false;
		foreach (Renderer r in renderers)
		{
			if (!((r is TrailRenderer) || (r is ParticleRenderer) || (r is ParticleSystemRenderer)))
			{
				if (!initBounds)
				{
					initBounds = true;
					bounds = r.bounds;
				}
				else
				{
					bounds.Encapsulate(r.bounds);
				}
			}
		}
		float max = Mathf.Max(bounds.extents.x, bounds.extents.y, bounds.extents.z);
		return max;
	}

}
