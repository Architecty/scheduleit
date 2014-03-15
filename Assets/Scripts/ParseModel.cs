using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParseModel : MonoBehaviour {

	public List<ModelObject> parsedElements = new List<ModelObject>();


	void Start(){
		GameObject[] parsableElements = GameObject.FindGameObjectsWithTag("parsableModel");

		foreach(GameObject element in parsableElements){
			ModelObject tempElement = new ModelObject();
			int tempVar = 0;

			if(element.name.ToString().Length-9 > 0){
				if(int.TryParse(element.name.Substring(element.name.ToString().Length-7,6), out tempVar)){

					tempElement.elementId = tempVar;
					tempElement.modelElement = element;
					parsedElements.Add(tempElement);
				}
			}
		}

		foreach( ModelObject temp in parsedElements){
			Debug.Log ("Object Number is: " + temp.elementId);
		}
	}





}
