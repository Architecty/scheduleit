using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Grouping : MonoBehaviour {
	
	public static bool grouping = false;
	public GameObject prefab;
	public GameObject groupPrefab;
	
	private GameObject[] model;
	private GameObject[] modelGroup;
	// 
	private GameObject[] selectedGroup; // holds all the element gameobjects of the same assembly
	//
	
	private string[] tempTags;
	
	public GameObject button;
	
	enum assemblies : int {Beam, Column, Eaves, Footing, Sheathing, Shingles, Slab, Truss};
	
	private bool[] isolated = new bool[8]; //beam, column, eaves, footing, sheathing, shingles, slab, truss

	
	private bool showErrorMessage = false;

	void Start()
	{
		File.Create(Application.dataPath + "/" + Constants.GroupFile);

		
		// Each one of these sections are what dynamically adds the checkboxes under the menu items
		GameObject temp;
		UITable panel = NGUITools.FindInParents<UITable>(GameObject.Find("Footing"));		
		model = GameObject.FindGameObjectsWithTag("Footing");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table1"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
		
		model = GameObject.FindGameObjectsWithTag("Slab");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table2"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
		
		model = GameObject.FindGameObjectsWithTag("Column");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table3"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
		
		model = GameObject.FindGameObjectsWithTag("Beam");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table4"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
		
		model = GameObject.FindGameObjectsWithTag("Shingles");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table5"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
		
		model = GameObject.FindGameObjectsWithTag("Truss");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table6"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
		
		model = GameObject.FindGameObjectsWithTag("Sheathing");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table7"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
		
		model = GameObject.FindGameObjectsWithTag("Eaves");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table8"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
	
		// This resets the menus so that they are all closed
		GameObject.Find("FootingTween").SetActive(false);
		GameObject.Find("SlabTween").SetActive(false);
		GameObject.Find("ColumnTween").SetActive(false);
		GameObject.Find("BeamTween").SetActive(false);
		GameObject.Find("ShinglesTween").SetActive(false);
		GameObject.Find("SheathingTween").SetActive(false);
		GameObject.Find("TrussTween").SetActive(false);
		GameObject.Find("EavesTween").SetActive(false);
		
		selectedGroup = new GameObject[Constants.MAXElement]; 
		
		// Set isolated list false by default
		for (int i = 0; i < isolated.Length; i++) isolated[i] = false;
		
		panel.Reposition();

		UIButton startGroupingButton = NGUITools.FindInParents<UIButton>(GameObject.Find("GroupingButton"));
		startGroupingButton.defaultColor = Color.green;
		UISprite buttonSprite = startGroupingButton.GetComponentInChildren<UISprite>();
		buttonSprite.color = Color.green;

		//added on September 6, 2013
		ObjectInteraction.AddInteractions();
	}
	
	void isolateAssemblies()
	{
		model = GameObject.FindGameObjectsWithTag("Model");
		bool noisolation = true;
		
		for(int i = 0; i < model.Length; i++)
		{
			Component[] renderers;
	        renderers = model[i].GetComponentsInChildren(typeof(Renderer),true);
			
			//make all the assemblies invisible
			for (int j = 0; j < isolated.Length; j++)
			{
				if (isolated[j])
				{
					foreach (Renderer renderer in renderers)
					{
						if (renderer.tag != "Ground")
							renderer.gameObject.SetActive(false);
					}
					noisolation = false;
					break;
				}
			}
			
			if (noisolation)
			{
				foreach (Renderer renderer in renderers)
				{
					if (renderer.tag != "Ground")
						renderer.gameObject.SetActive(true);
				}
				
				return;
			}
			
			//make isolated assemblies visible
			foreach (Renderer renderer in renderers)
	        {
				if(renderer.tag == "Beam" && isolated[(int) assemblies.Beam] == true)
				{
						renderer.gameObject.SetActive(true);
				}
				else if(renderer.tag == "Column" && isolated[(int) assemblies.Column] == true)
				{
						renderer.gameObject.SetActive(true);
				}
				else if(renderer.tag == "Eaves" && isolated[(int) assemblies.Eaves] == true)
				{
						renderer.gameObject.SetActive(true);
				}
				else if(renderer.tag == "Footing" && isolated[(int) assemblies.Footing] == true)
				{
						renderer.gameObject.SetActive(true);
				}
				else if(renderer.tag == "Sheathing" && isolated[(int) assemblies.Sheathing] == true)
				{
						renderer.gameObject.SetActive(true);
				}
				else if(renderer.tag == "Shingles" && isolated[(int) assemblies.Shingles] == true)
				{
						renderer.gameObject.SetActive(true);
				}
				else if(renderer.tag == "Slab" && isolated[(int) assemblies.Slab] == true)
				{
						renderer.gameObject.SetActive(true);
				}
				else if(renderer.tag == "Truss" && isolated[(int) assemblies.Truss] == true)
				{
						renderer.gameObject.SetActive(true);
				}
	        }
			
		}
	}
	
	
	
	// Handles for the Footings
	void OnFootingClicked()
	{

		model = GameObject.FindGameObjectsWithTag("Footing");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
	}
	
	void IsolateFooting()
	{
		if (isolated[(int) assemblies.Footing])
			isolated[(int) assemblies.Footing] = false;
		else 
			isolated[(int) assemblies.Footing] = true;
	
		isolateAssemblies();
	}
	
	
	// Handles for the Slab
	void OnSlabClicked()
	{

		model = GameObject.FindGameObjectsWithTag("Slab");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
	}
	
	void IsolateSlab()
	{
		if (isolated[(int) assemblies.Slab])
			isolated[(int) assemblies.Slab] = false;
		else 
			isolated[(int) assemblies.Slab] = true;
	
		isolateAssemblies();
	}
	
	
	// Handles for the columns
	void OnColumnClicked()
	{
		model = GameObject.FindGameObjectsWithTag("Column");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
		
	}
	
	void IsolateColumn()
	{
		if (isolated[(int) assemblies.Column])
			isolated[(int) assemblies.Column] = false;
		else 
			isolated[(int) assemblies.Column] = true;
	
		isolateAssemblies();
	}
	
	
	// Handles for the beams
	void OnBeamClicked()
	{

		model = GameObject.FindGameObjectsWithTag("Beam");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
	}
	
	void IsolateBeam()
	{
		if (isolated[(int) assemblies.Beam])
			isolated[(int) assemblies.Beam] = false;
		else 
			isolated[(int) assemblies.Beam] = true;
		
		isolateAssemblies();
	}
	
	
	// Handles for the Shingles
	void OnShinglesClicked()
	{

		model = GameObject.FindGameObjectsWithTag("Shingles");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
	}
	
	void IsolateShingles()
	{
		if (isolated[(int) assemblies.Shingles])
			isolated[(int) assemblies.Shingles] = false;
		else 
			isolated[(int) assemblies.Shingles] = true;
	
		isolateAssemblies();
	}
	
	
	// Handles for the trusses
	void OnTrussClicked()
	{

		model = GameObject.FindGameObjectsWithTag("Truss");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
	}
	
	void IsolateTruss()
	{
		if (isolated[(int) assemblies.Truss])
			isolated[(int) assemblies.Truss] = false;
		else 
			isolated[(int) assemblies.Truss] = true;
	
		isolateAssemblies();
	}
	
	
	// Handles for the Sheathings
	void OnSheathingClicked()
	{

		model = GameObject.FindGameObjectsWithTag("Sheathing");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
	}
	
	void IsolateSheathing()
	{
		if (isolated[(int) assemblies.Sheathing])
			isolated[(int) assemblies.Sheathing] = false;
		else 
			isolated[(int) assemblies.Sheathing] = true;
	
		isolateAssemblies();
	}
	
	
	// Handles for the Eaves
	void OnEavesClicked()
	{

		model = GameObject.FindGameObjectsWithTag("Eaves");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
	}

	void IsolateEaves()
	{
		if (isolated[(int) assemblies.Eaves])
			isolated[(int) assemblies.Eaves] = false;
		else 
			isolated[(int) assemblies.Eaves] = true;
	
		isolateAssemblies();
	}
	
	
	// This handles if the Start Grouping button is clicked
	void OnGroupingClicked()
	{
		// Checks to see if grouping was clicked and sets the correct color
		/*GameObject temp = GameObject.Find("GroupingButton");
		UIButton buttonClicked = temp.GetComponent<UIButton>();
		UISprite buttonSprite = buttonClicked.GetComponentInChildren<UISprite>();
		buttonSprite.color = Color.green;
		*/
		/*
		if(buttonClicked.defaultColor != buttonClicked.disabledColor)
		{
			buttonClicked.defaultColor = buttonClicked.disabledColor;

		}else{
			buttonClicked.defaultColor = Color.white;
		}
		*/
		UIButton startGroupingButton = NGUITools.FindInParents<UIButton>(GameObject.Find("GroupingButton"));
		UISprite crrSprite = startGroupingButton.GetComponentInChildren<UISprite>();
		UILabel crrLabel = startGroupingButton.GetComponentInChildren<UILabel>();

		// Sets the valued if grouping is clicked
		if(grouping)
		{
			grouping = false;
			crrLabel.text = "Start Grouping";
			//crrSprite.color = Color.green;
			startGroupingButton.defaultColor = Color.green;

		}else{
			grouping = true;
			crrLabel.text = "Stop Grouping";
			startGroupingButton.defaultColor = Color.red;
			//crrSprite.color = Color.red;
		}
	}
	
	// This handles what happens when the Add Group button is clicked
	void OnGroupClicked()
	{
		// Sets values to default
		int count = 0;
		bool sameType = false;
		string groupName = "";
		
		// Checks to see if the StartGroupingButton was selected
		if(grouping)
		{
			// Finds all of the objects that are colored yellow and puts them into an array
			model = GameObject.FindGameObjectsWithTag("Model");
			modelGroup = new GameObject[100];
			tempTags = new string[100];
			for(int j = 0; j < model.Length; j++)
			{
				Component[] renderers;
		        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
		        foreach (Renderer renderer in renderers)
		        {	
					if(renderer.material.color == Color.yellow)
					{
						modelGroup[count] = renderer.gameObject;
						count++;
					}					
		        }
			} 
			
			// This makes sure that the selected elements are the same type
			for(int j = 0; j < count; j++)
			{
				if(modelGroup[0].tag != modelGroup[j].tag)
				{
					//GUI.Label (new Rect (Screen.width / 2, Screen.height / 2, 400, 20), "Building Elements of the same type can only be grouped."); 
					Debug.Log("Building Elements of the same type can only be grouped");
					sameType = false;
					showErrorMessage = true;
					break;
				}else{
					sameType = true;
					showErrorMessage = false;
					groupName = modelGroup[0].tag.ToString();
				}					
			}
			
			// If elements are the same type they will be deleted and added to the group file
			if(sameType)
			{
				for(int j = 0; j < count; j++)
				{
					if(j == 0)
					{
						FileReader.writeGroup(modelGroup[j].tag.ToString());
					}
					FileReader.writeGroup(modelGroup[j].name.ToString());
					modelGroup[j].collider.enabled = false;
					modelGroup[j].renderer.material.color = Color.blue;
				}
				FileReader.writeGroup("/");
				
				// Finds the Correct button class and area for the checkbox
				GameObject temp = GameObject.Find(modelGroup[0].tag.ToString());
				NGUITools.SetActiveChildren(temp, true);
				temp = GameObject.Find(modelGroup[0].tag.ToString() + "Tween");
				
				// Adds a checkbox to the groups
				GameObject temp2 = NGUITools.AddChild(GameObject.Find("GroupTable"),groupPrefab);
				
				// Makes the correct name and the correct label
				temp2.GetComponentInChildren<UILabel>().text = FileReader.lastGroup();
				temp2.name="Checkbox_"+FileReader.lastGroup();
				
				// Sets the objects to interact with the checkbox group
				temp2.GetComponent<CheckboxControlledObject>().target = new GameObject[count];
				
				// removes the old checkbox items
				for(int i = 0; i< count; i++)
				{
					temp2.GetComponent<CheckboxControlledObject>().target[i] = modelGroup[i];
					Destroy(GameObject.Find("Checkbox_"+modelGroup[i].name));
				}
				// if all the assembly instances are grouped, change the button color to blue
				if (checkIfAllAssemblyGrouped(groupName)) disableButtons(groupName, Color.green);
				
				// Repositions all of the tables affected
				temp.GetComponentInChildren<UITable>().Reposition();
				GameObject.Find("GroupTable").GetComponent<UITable>().Reposition();
				
				UITable panel = NGUITools.FindInParents<UITable>(GameObject.Find("Footing"));
				panel.Reposition();

			}

		}else{
			Debug.Log("Must be in grouping mode to begin grouping building elements");
		}
		
	}
	
	bool checkIfAllAssemblyGrouped(string groupName)
	{
		model = GameObject.FindGameObjectsWithTag("Model");

		for(int i = 0; i < model.Length; i++)
		{
			Component[] renderers;
	        renderers = model[i].GetComponentsInChildren(typeof(Renderer),true);
			
			foreach (Renderer renderer in renderers)
			{
				if (renderer.tag.ToLower().Equals(groupName.ToLower()) && 
					renderer.gameObject.collider.enabled == true)
					return false;
			}
		}
		
		return true;
	}
	
	void disableButtons(string assemblyName, Color color)
	{
		GameObject temp = GameObject.Find(assemblyName + "Sprite");
		UIButton crrButton = temp.GetComponentInChildren<UIButton>();
		crrButton.disabledColor = color;
		crrButton.isEnabled = false;

		//print("Game object name = " + temp.name.ToString() + " assembly name = " + assemblyName);
		//UIButton[] temp2 = new UIButton[3];
		//temp2 = temp.GetComponentsInChildren<UIButton>();

		//temp2[1].disabledColor = color;
		//temp2[1].isEnabled = false;
		/*
		for (int i = 0; i < temp2.Length; i++)
		{
			temp2[i].disabledColor = color;
			temp2[i].isEnabled = false;
		}
		*/
	}
	
	// Restart is selected
	void OnRestartClicked()
	{
		Application.LoadLevel("PAVGrouping");
	}
	
	// Continue is selected
	void OnContinueClicked()
	{
		// This section finds any object that wasn't grouped and puts it in its own group
		// by finding collider and seeing if it was made blue by grouping
		model = GameObject.FindGameObjectsWithTag("Model");
		modelGroup = new GameObject[100];
		tempTags = new string[100];
		for(int j = 0; j < model.Length; j++)
		{
			Component[] colliders;
	        colliders = model[j].GetComponentsInChildren(typeof(Collider));
	        foreach (Collider collider in colliders)
	        {	
				if(collider.renderer.material.color != Color.blue)
				{
					FileReader.writeGroup(collider.tag.ToString());
					FileReader.writeGroup(collider.name.ToString());
					FileReader.writeGroup("/");
				}					
	        }
		} 
		Application.LoadLevel("PAVMethods");
	}
	
	void OnGUI ()
	{
		if(showErrorMessage == true)
		{
			GUI.depth = 1;
			GUI.Window(0,(new Rect(Screen.width/2 - 100,Screen.height/2 - 100, 300,100)),
				displayGroupingErrorMessage,
				"Error!");
			
			GUI.depth = 0;
			if(GUI.Button (new Rect(Screen.width/2 + 20, Screen.height/2 - 30, 50,30), "OK"))
			{
				showErrorMessage = false;	
			}

		}
		
	}
	
	// Controls what is put in the infoWindow
	void displayGroupingErrorMessage(int id)
	{
		GUI.Label (new Rect(30, 30, 250,90), "Building Elements of the same type can only be grouped!");
	}
}

