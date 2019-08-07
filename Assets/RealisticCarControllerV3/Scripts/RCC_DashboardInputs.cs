//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/Dashboard Inputs")]
public class RCC_DashboardInputs : MonoBehaviour {

	public RCC_CarControllerV3 currentCarController;

	public GameObject RPMNeedle;
	public GameObject KMHNeedle;
	public GameObject turboGauge;
	public GameObject NOSGauge;
	public GameObject BoostNeedle;
	public GameObject NoSNeedle;

	private float RPMNeedleRotation = 0f;
	private float KMHNeedleRotation = 0f;
	private float BoostNeedleRotation = 0f;
	private float NoSNeedleRotation = 0f;

	public float RPM;
	public float KMH;
	internal int direction = 1;
	internal float Gear;
	internal bool NGear = false;

	internal bool ABS = false;
	internal bool ESP = false;
	internal bool Park = false;
	internal bool Headlights = false;
	internal RCC_CarControllerV3.IndicatorsOn indicators;

    private float dis;
    private double dist;
    public GameObject Car_1;
    public GameObject Car_2;
    public float acceleration_x;
    public float acceleration_z;
    public float lastVelocity;
    public GameObject Acci;
    public Rigidbody Car_rb;

    public Text Accilera_x;
    public Text Accilera_z;
    public Text Dist;

    void Update()
    {

		if(RCC_Settings.Instance.uiType == RCC_Settings.UIType.None){
			gameObject.SetActive(false);
			enabled = false;
			return;
		}

		GetValues();

	}
	
	public void GetVehicle(RCC_CarControllerV3 rcc){

		currentCarController = rcc;
		RCC_UIDashboardButton[] buttons = GameObject.FindObjectsOfType<RCC_UIDashboardButton>();

		foreach(RCC_UIDashboardButton button in buttons)
			button.Check();

	}

	void GetValues()
    {
        /*
		if(!currentCarController)
			return;

		if(!currentCarController.canControl || currentCarController.AIController  )
        {
			return;
		}
        */

		if(NOSGauge){
			if(currentCarController.useNOS){
				if(!NOSGauge.activeSelf)
					NOSGauge.SetActive(true);
			}else{
				if(NOSGauge.activeSelf)
					NOSGauge.SetActive(false);
			}
		}

		if(turboGauge){
			if(currentCarController.useTurbo){
				if(!turboGauge.activeSelf)
					turboGauge.SetActive(true);
			}else{
				if(turboGauge.activeSelf)
					turboGauge.SetActive(false);
			}
		}
		
		RPM = currentCarController.engineRPM;
		KMH = currentCarController.speed ;
		direction = currentCarController.direction;
		Gear = currentCarController.currentGear;

		NGear = currentCarController.changingGear;
		
		ABS = currentCarController.ABSAct;
		ESP = currentCarController.ESPAct;
		Park = currentCarController.handbrakeInput > .1f ? true : false;
		Headlights = currentCarController.lowBeamHeadLightsOn || currentCarController.highBeamHeadLightsOn;
		indicators = currentCarController.indicatorsOn;

		if(RPMNeedle){
			RPMNeedleRotation = (currentCarController.engineRPM / 50f);
			RPMNeedle.transform.eulerAngles = new Vector3(RPMNeedle.transform.eulerAngles.x ,RPMNeedle.transform.eulerAngles.y, -RPMNeedleRotation);
		}
		if(KMHNeedle){
			if(RCC_Settings.Instance.units == RCC_Settings.Units.KMH)
				KMHNeedleRotation = (currentCarController.speed);
			else
				KMHNeedleRotation = (currentCarController.speed * 0.62f);
			KMHNeedle.transform.eulerAngles = new Vector3(KMHNeedle.transform.eulerAngles.x ,KMHNeedle.transform.eulerAngles.y, -KMHNeedleRotation);
		}
		if(BoostNeedle){
			BoostNeedleRotation = (currentCarController.turboBoost / 30f) * 270f;
			BoostNeedle.transform.eulerAngles = new Vector3(BoostNeedle.transform.eulerAngles.x ,BoostNeedle.transform.eulerAngles.y, -BoostNeedleRotation);
		}
		if(NoSNeedle){
			NoSNeedleRotation = (currentCarController.NoS / 100f) * 270f;
			NoSNeedle.transform.eulerAngles = new Vector3(NoSNeedle.transform.eulerAngles.x ,NoSNeedle.transform.eulerAngles.y, -NoSNeedleRotation);
		}

        //calculates the acceleration by dividing the change in speed by change in time
        acceleration_x = Car_rb.velocity.x ;
        // lastVelocity = Car_rb.velocity.x;
        double acc_x = System.Math.Round(acceleration_x, 3);
        Accilera_x.text = acc_x.ToString();

        //acceleration_z = (Car_rb.velocity.z - lastVelocity) / Time.timeScale;
        acceleration_z = Car_rb.velocity.y ;
        double acc_z = System.Math.Round(acceleration_z, 3);
        //lastVelocity = Car_rb.velocity.z;
        Accilera_z.text = acc_z.ToString();

        dis = Vector3.Distance(Car_1.transform.position, Car_2.transform.position) - 5.3f;
        dist = System.Math.Round(dis, 2);
        Dist.text = "Distance: " + dist.ToString();

    }

}



