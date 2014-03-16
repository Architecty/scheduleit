using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

public class ParseSchedule : MonoBehaviour {

	public Dictionary<string, Activity> parsedActivities = new Dictionary<string, Activity>();
	public TextAsset scheduleXML;
	public float test;

	public Dictionary<string, ModelObject> parsedElements = new Dictionary<string, ModelObject>();

	SliderBar SliderScript;


	// Use this for initialization
	void Start () {
		SliderScript = GameObject.Find ("Slider").transform.GetComponent<SliderBar>();

		parsedElements = CreateModelElementList(scheduleXML);
		parsedActivities = CreateScheduleOfActivities(scheduleXML, parsedElements);

		SliderScript.StartDate = findStartDate(parsedActivities);
		SliderScript.EndDate = findEndDate(parsedActivities);

		SliderScript.schedLength = SliderScript.EndDate.Subtract(SliderScript.StartDate);
	}



	//Create a list of Model elements
	Dictionary<string, ModelObject> CreateModelElementList(TextAsset scheduleText){

		Dictionary<string, ModelObject> tempModelList = new Dictionary<string, ModelObject>();

		GameObject[] parsableElements = GameObject.FindGameObjectsWithTag("parsableModel");
		
		foreach(GameObject element in parsableElements){
			ModelObject tempElement = new ModelObject();
			
			if(element.name.ToString().Length-9 > 0){

				tempElement.elementId = element.name.Substring(element.name.ToString().Length-7,6);
				tempElement.modelElement = element;
				tempModelList.Add(tempElement.elementId, tempElement);

			}
		}
		
		XmlDocument scheduleXML = new XmlDocument();
		scheduleXML.LoadXml(scheduleText.text);


		//Parse through the XML looking for model elements. Add to keys that exist
		foreach(XmlNode node in scheduleXML["NewDataSet"]){
			if(node.Name == "dtModelElements"){


				if(tempModelList.ContainsKey(node["Element.Id"].InnerXml)){
					tempModelList[node["Element.Id"].InnerXml].GUID = node["Item.GUID"].InnerXml;
					tempModelList[node["Element.Id"].InnerXml].elemName = node["Item.Type"].InnerXml;
				}
				else{
					Debug.Log("You're missing element: " + node["Element.Id"].InnerXml);
				}
			}
		}
		return tempModelList;



	}





	//Create an array of Activities
	Dictionary<string, Activity> CreateScheduleOfActivities(TextAsset scheduleText, Dictionary<string, ModelObject> elementThing){
		Debug.Log ("THere are " + elementThing.Count);
		Dictionary<string, Activity> tempDictionary = new Dictionary<string, Activity>();
		XmlDocument scheduleXML = new XmlDocument();
		scheduleXML.LoadXml(scheduleText.text);

		foreach(XmlNode node in scheduleXML["NewDataSet"]){
			Activity tempActivity = new Activity();

			if(node.Name == "dtActivities" && node["Activity.DisplayId"].InnerXml != "0"){
				
				tempActivity.DisplayId = node["Activity.DisplayId"].InnerXml;
				tempActivity.Name = node["Activity.Name"].InnerXml;
				tempActivity.PlannedStart = DateTime.Parse (node["Activity.PlannedStart"].InnerXml);
				tempActivity.PlannedEnd = DateTime.Parse (node["Activity.PlannedEnd"].InnerXml);
				tempActivity.MaterialCost = float.Parse (node["Activity.MaterialCost"].InnerXml);
				tempActivity.LaborCost = float.Parse (node["Activity.LaborCost"].InnerXml);
				tempActivity.EquipmentCost = float.Parse (node["Activity.EquipmentCost"].InnerXml);
				tempActivity.Float = node["Activity.Float"].InnerXml;

				tempDictionary.Add(tempActivity.DisplayId, tempActivity);
			}
		}
		foreach(XmlNode node in scheduleXML["NewDataSet"]){
			
			if(node.Name == "dtActivitiesElements"){


				if(tempDictionary.ContainsKey(node["Activity.DisplayId"].InnerXml) && elementThing.ContainsKey(node["Element.Id"].InnerXml)){
					Debug.Log ("Contained by Activities and Elements Key Exists: "+ elementThing[node["Element.Id"].InnerXml]);
					ModelObject tempObject = elementThing[node["Element.Id"].InnerXml];
					tempDictionary[node["Activity.DisplayId"].InnerXml].modelObjects.Add(tempObject);
					Debug.Log (tempDictionary[node["Activity.DisplayId"].InnerXml].modelObjects.Count);
				}

			}
		}

		Debug.Log("Number of Activities: " + tempDictionary.Count);
		return tempDictionary;
	}

	//Find the first date that an activity happens
	DateTime findStartDate(Dictionary<string, Activity> activityDict){

		DateTime tempDate = new DateTime(5000,1,1);

		foreach(Activity tempActivity in activityDict.Values){
			if(tempActivity.PlannedStart.CompareTo(tempDate) < 0 ){
				tempDate = tempActivity.PlannedStart;
			}
		}
		return tempDate;

	}
	
	//Find the last date that an activity happens
	DateTime findEndDate(Dictionary<string, Activity> activityDict){
		
		DateTime tempDate = new DateTime(1000,1,1);

		foreach(Activity tempActivity in activityDict.Values){
			if(tempActivity.PlannedEnd.CompareTo(tempDate) > 0 ){
				tempDate = tempActivity.PlannedEnd;
			}
		}
		return tempDate;
		
	}






}
