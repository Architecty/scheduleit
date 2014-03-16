using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class SliderBar : MonoBehaviour {

	public float currentValue = 0;

	public GameObject slider;
	public GameObject textObject;

	public DateTime StartDate = new DateTime();
	public DateTime EndDate = new DateTime();

	public TimeSpan schedLength = new TimeSpan();

	public Material doneMat;
	public Material inProcessMat;
	public Material doneMatCrit;
	public Material inProcessMatCrit;
	public Material matCrit;
	public Material matNorm;

	DateTime currentDate;

	ParseSchedule parserObject;

	// Use this for initialization
	void Start () {
		parserObject = GameObject.Find("ScriptObject").GetComponent<ParseSchedule>();
	}
	
	// Update is called once per frame
	void Update () {
		
		currentValue = slider.transform.localPosition.x+.5F;
		Debug.Log(setCurrentDate(currentValue, StartDate, EndDate));

		slideByTrigger();


	}

	void slideByTrigger(){
		Debug.Log ("Axis Is: " + Input.GetAxis("LeftTrigger"));
		if(Input.GetAxis("LeftTrigger") > 0 && slider.transform.localPosition.x >= -.5F){
			slider.transform.localPosition += new Vector3(Input.GetAxis("LeftTrigger") * -.01F, 0F, 0F);
			currentValue = slider.transform.localPosition.x+.5F;
			UpdateModel(parserObject.parsedActivities, setCurrentDate(currentValue, StartDate, EndDate));
		}
		if(Input.GetAxis("RightTrigger") > 0 && slider.transform.localPosition.x <= .5F){
			slider.transform.localPosition += new Vector3(Input.GetAxis("RightTrigger") * .01F, 0F, 0F);
			currentValue = slider.transform.localPosition.x+.5F;
			UpdateModel(parserObject.parsedActivities, setCurrentDate(currentValue, StartDate, EndDate));
		}
	}

	DateTime setCurrentDate(float whatPercent, DateTime startDate, DateTime endDate){
		DateTime currentDate = startDate.AddDays(endDate.Subtract(startDate).TotalDays * whatPercent);
		return currentDate;
	}

	void UpdateModel(Dictionary<string, Activity> activityDict, DateTime currentDate){

		textObject.GetComponent<TextMesh>().text = currentDate.ToShortDateString();

		//Look through each activity. If the time activity state has changed, update the model
		foreach(Activity whichActivity in activityDict.Values){
			float diff = whichActivity.PlannedStart.CompareTo(currentDate) + whichActivity.PlannedEnd.CompareTo(currentDate);

			if(diff <= -2){
				if(whichActivity.state != 0){
					whichActivity.state = 0;
					updateRenderer(whichActivity.modelObjects, whichActivity.state, whichActivity.Float);
				}
			}
			else if(diff < 2){
				if(whichActivity.state != 1){
					whichActivity.state = 1;
					updateRenderer(whichActivity.modelObjects, whichActivity.state, whichActivity.Float);
				}
			}
			else{
				if(whichActivity.state != 2){
					whichActivity.state = 2;
					updateRenderer(whichActivity.modelObjects, whichActivity.state, whichActivity.Float);
				}
			}

		}
	}

	void updateRenderer(List<ModelObject> objectArray, int whichValue, string projFloat){
		float tempFloat = float.Parse(projFloat);

//		foreach(ModelObject obj in objectArray){
//			if(obj.modelElement.renderer.material.name == "blank"){
//				if(tempFloat <= 0){
//					obj.modelElement.renderer.material = matCrit;
//				}
//				else{
//					obj.modelElement.renderer.material = matNorm;
//				}
//			}
//		}

		Material tempDoneMat = (tempFloat <= 0)?doneMatCrit:doneMat;
		Material tempProcessMat = (tempFloat <= 0)?inProcessMatCrit:inProcessMat;
		switch(whichValue){
		case 0:
			foreach(ModelObject obj in objectArray){
//				StartCoroutine(changeTransparency(obj.modelElement.renderer, .5F));
				obj.modelElement.renderer.enabled = true;
				obj.modelElement.renderer.material = tempDoneMat;
			}
			break;
		case 1:
			foreach(ModelObject obj in objectArray){
//				StartCoroutine(changeTransparency(obj.modelElement.renderer, 1F));
				obj.modelElement.renderer.enabled = true;
				obj.modelElement.renderer.material = tempProcessMat;
			}
			break;
		case 2:
			foreach(ModelObject obj in objectArray){
//				StartCoroutine(changeTransparency(obj.modelElement.renderer, 0F));
				obj.modelElement.renderer.enabled = false;
			}
			break;
		}
	}

	IEnumerator changeTransparency(Renderer whichObj, float endState){
		Debug.Log ("Starting Trans");
		float StartTime = Time.time;
		float fadeTime = 1F;
		float startingAlpha = whichObj.material.color.a;
		Color startingColor = whichObj.material.color;
		Color newColor;

		while(Time.time - StartTime < fadeTime){
			Debug.Log (startingColor.a);
			newColor = new Color(startingColor.r, startingColor.g, startingColor.b, Mathf.Lerp (startingAlpha, endState, (Time.time-StartTime)/fadeTime));

			whichObj.material.color = newColor;
			yield return null;
		}
		newColor = new Color(startingColor.r, startingColor.g, startingColor.b, endState);
		whichObj.material.color = newColor;
		yield return null;
	}


}
