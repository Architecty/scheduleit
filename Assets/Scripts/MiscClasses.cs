using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//The object in the game, with the elementID from the FBX stripped out
public class ModelObject {

	public GameObject modelElement;
	public int elementId;
}

//Activities that can occur throughout the process
public class Activity {

	public int activityId;
	public string activityName;
	public float startTime;
	public float endTime;
	public bool criticalPath;

	public List<ModelObject> modelObjects;
}
