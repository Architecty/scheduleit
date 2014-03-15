using UnityEngine;
using System.Collections;

public class SliderBar : MonoBehaviour {

	public float currentValue = 0;

	public GameObject slider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		slideByTrigger();

		currentValue = slider.transform.localPosition.x+.5F;
	}

	void slideByTrigger(){
		Debug.Log ("Axis Is: " + Input.GetAxis("LeftTrigger"));
		if(Input.GetAxis("LeftTrigger") > 0 && slider.transform.localPosition.x >= -.5F){
			slider.transform.localPosition += new Vector3(Input.GetAxis("LeftTrigger") * -.01F, 0F, 0F);
		}
		if(Input.GetAxis("RightTrigger") > 0 && slider.transform.localPosition.x <= .5F){
			slider.transform.localPosition += new Vector3(Input.GetAxis("RightTrigger") * .01F, 0F, 0F);
		}
	}

}
