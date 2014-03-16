using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


//The object in the game, with the elementID from the FBX stripped out
public class ModelObject {

	public GameObject modelElement;
	public string elementId;
	public string GUID;
	public string elemName;
}

//Activities that can occur throughout the process
public class Activity {

	public string DisplayId;
	public string Name;
	public DateTime PlannedStart;
	public DateTime PlannedEnd;
	public float MaterialCost;
	public float LaborCost;
	public float EquipmentCost;
	public string Float;

	public int state;

	public List<ModelObject> modelObjects = new List<ModelObject>();
}

