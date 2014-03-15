using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class Sequencing : MonoBehaviour {
	
	public GameObject prefab;
	string[] groups;

	private TreeNode rootNode;
	private List<VCSActivity> activitylist;

	public static bool firstGroupSelected = false;
	public static bool secondGroupSelected = false;
	static string firstGroupName = "";
	static string secondGroupName = "";

	static Color startColor1;
	static Texture startTexture1;
	static Color startColor2;
	static Texture startTexture2;

	static string message = null;
	bool showMessage = false;

	//event handler variables
	private bool[] isolated = new bool[8]; //beam, column, eaves, footing, sheathing, shingles, slab, truss
	enum assemblies : int {Beam, Column, Eaves, Footing, Sheathing, Shingles, Slab, Truss};
	private GameObject[] model;

	// Use this for initialization
	void Start () {
		rootNode = new TreeNode("root", null, "", 0);
		activitylist = new List<VCSActivity>();

		var reader = new StreamReader(Application.dataPath + "/" + Constants.GroupFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		groups = fileContents.Split("\n"[0]);
		
		UITable panel = NGUITools.FindInParents<UITable>(GameObject.Find("Footing"));		
		GameObject crrGroup;
		for (int i = 0; i < groups.Length; i++)
		{
			if (groups[i] != "")
			{
				string[] groupData = groups[i].Split(','); 
				switch (groupData[0])
				{
					case "Beam": 
						crrGroup = NGUITools.AddChild(GameObject.Find("Table4"), prefab);
						crrGroup.GetComponentInChildren<UILabel>().text = groupData[1];
						crrGroup.name = "Checkbox_" + groupData[1];
						break;
					case "Column":
						crrGroup = NGUITools.AddChild(GameObject.Find("Table3"), prefab);
						crrGroup.GetComponentInChildren<UILabel>().text = groupData[1];
						crrGroup.name = "Checkbox_" + groupData[1];
						break;
					case "Eaves":
						crrGroup = NGUITools.AddChild(GameObject.Find("Table8"), prefab);
						crrGroup.GetComponentInChildren<UILabel>().text = groupData[1];
						crrGroup.name = "Checkbox_" + groupData[1];
						break;
					case "Footing":
						crrGroup = NGUITools.AddChild(GameObject.Find("Table1"), prefab);
						crrGroup.GetComponentInChildren<UILabel>().text = groupData[1];
						crrGroup.name = "Checkbox_" + groupData[1];
						break;
					case "Sheathing":
						crrGroup = NGUITools.AddChild(GameObject.Find("Table7"), prefab);
						crrGroup.GetComponentInChildren<UILabel>().text = groupData[1];
						crrGroup.name = "Checkbox_" + groupData[1];
						break;
					case "Shingles":
						crrGroup = NGUITools.AddChild(GameObject.Find("Table5"), prefab);
						crrGroup.GetComponentInChildren<UILabel>().text = groupData[1];
						crrGroup.name = "Checkbox_" + groupData[1];
						break;				
					case "Slab":
						crrGroup = NGUITools.AddChild(GameObject.Find("Table2"), prefab);
						crrGroup.GetComponentInChildren<UILabel>().text = groupData[1];
						crrGroup.name = "Checkbox_" + groupData[1];
						break;				
					case "Truss":
						crrGroup = NGUITools.AddChild(GameObject.Find("Table6"), prefab);
						crrGroup.GetComponentInChildren<UILabel>().text = groupData[1];
						crrGroup.name = "Checkbox_" + groupData[1];
						break;				
					default:
						break;
				}
			}
		}

		// Set isolated list false by default
		for (int i = 0; i < isolated.Length; i++)
			isolated[i] = false;
		
		// This resets the menus so that they are all closed
		GameObject.Find("FootingTween").SetActive(false);
		GameObject.Find("SlabTween").SetActive(false);
		GameObject.Find("ColumnTween").SetActive(false);
		GameObject.Find("BeamTween").SetActive(false);
		GameObject.Find("ShinglesTween").SetActive(false);
		GameObject.Find("SheathingTween").SetActive(false);
		GameObject.Find("TrussTween").SetActive(false);
		GameObject.Find("EavesTween").SetActive(false);

		panel.Reposition();

		UIButton StoSButton = NGUITools.FindInParents<UIButton>(GameObject.Find("Start2Start"));
		UIButton FtoSButton = NGUITools.FindInParents<UIButton>(GameObject.Find("Finish2Start"));
		//StoSButton.enabled = false;
		//FtoSButton.enabled = false;
		setButtonDisabled(StoSButton);
		setButtonDisabled(FtoSButton);
	
		UILabel label1 = NGUITools.FindInParents<UILabel>(GameObject.Find("Message1"));
		UILabel label2 = NGUITools.FindInParents<UILabel>(GameObject.Find("Message2"));
		label1.text = "Select First Group:";
		label2.text = "";

		//remove any old sequence data in sequence.txt
		FileWriter.createNewTextFile(Constants.SequenceFile);

		hidePreviewModel();
		SequencingInteraction.AddInteractions();
	}

	// Update is called once per frame
	void Update () {

		
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


	public static void hidePreviewModel()
	{
		GameObject modelPreview = GameObject.FindWithTag("ModelPreview");
		Component[] allPreviewElements = modelPreview.GetComponentsInChildren(typeof(Renderer));
		foreach (Renderer renderer in allPreviewElements)
		{
			renderer.enabled = false;
			/*
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
			*/
		}

	}


	void showSequencedGroups()
	{
		GameObject modelPreview = GameObject.FindWithTag("ModelPreview");
		Component[] allPreviewElements = modelPreview.GetComponentsInChildren(typeof(Renderer));

		string[] firstGroupElements = FileReader.getGroupElements(firstGroupName);
		string[] secondGroupElements = FileReader.getGroupElements(secondGroupName);

		foreach (Renderer renderer in allPreviewElements)
		{
			for (int i = 0; i < firstGroupElements.Length; i++)
			{
				if (renderer.name.Equals(firstGroupElements[i]))
					renderer.enabled = true;
			}

			for (int i = 0; i < secondGroupElements.Length; i++)
			{
				if (renderer.name.Equals(secondGroupElements[i]))
					renderer.enabled = true;
			}
		}
	}

	void OnStoSButtonClicked()
	{
		if (firstGroupName != null && firstGroupName != "")
		{
			if (secondGroupName != null && secondGroupName != "")
			{
				writeSequence(firstGroupName, secondGroupName, Constants.StartToStart);
				showSequencedGroups();
				clearSequencingData();
				SequencingInteraction.selectedGroups.Clear();
			}
			else
			{
				message = "Please select the SECOND group!";
				showMessage = true;
			}
		}
		else 
		{
			message = "Please select the FIRST group!";
			showMessage = true;
		}
	}

	void OnFtoSButtonClicked()
	{
		if (firstGroupName != null && firstGroupName != "")
		{
			if (secondGroupName != null && secondGroupName != "")
			{
				writeSequence(firstGroupName, secondGroupName, Constants.FinishToStart);
				showSequencedGroups();
				clearSequencingData();
				SequencingInteraction.selectedGroups.Clear();
			}
			else
			{
				message = "Please select the SECOND group!";
				showMessage = true;
			}
		}
		else 
		{
			message = "Please select the FIRST group!";
			showMessage = true;
		}
	}

	public static void Group1ButtonClicked()
	{
		string newFirstMessage = "First Group: " + SequencingInteraction.selectedGroups[0] + "\n";
		string newSecondMessage = "";
		UILabel label2 = NGUITools.FindInParents<UILabel>(GameObject.Find("Message2"));
		string[] labelText = label2.text.Split(':');
		if (labelText[0] != "Second Group")
		{
			newSecondMessage = "Select Second Group: ";
		}
		else
		{
			newSecondMessage = label2.text;
		}
		firstGroupName = SequencingInteraction.selectedGroups[0].ToString();
		FirstGroupColor = SequencingInteraction.startColor1;
		FirstGroupTexture = SequencingInteraction.startTexture1;
		firstGroupSelected = true;

		//UICheckbox groupCheckbox = NGUITools.FindInParents<UICheckbox>(GameObject.Find("Checkbox_" + firstGroupName));
		//if (groupCheckbox != null) groupCheckbox.isChecked = true;

		if (firstGroupSelected && secondGroupSelected) enableRelationshipButtons();
		setMessage(newFirstMessage + newSecondMessage);
	}
	
	public static void Group2ButtonClicked()
	{
		UILabel label1 = NGUITools.FindInParents<UILabel>(GameObject.Find("Message1"));
		string newFirstMessage = label1.text + "\n";
		string newSecondMessage = "Second Group: " + SequencingInteraction.selectedGroups[1];
		secondGroupName = SequencingInteraction.selectedGroups[1].ToString();
		SecondGroupColor = SequencingInteraction.startColor2;
		SecondGroupTexture = SequencingInteraction.startTexture2;
		setMessage(newFirstMessage + newSecondMessage);
		secondGroupSelected = true;

		//UICheckbox groupCheckbox = NGUITools.FindInParents<UICheckbox>(GameObject.Find("Checkbox_" + secondGroupName));
		//if (groupCheckbox != null) groupCheckbox.isChecked = true;

		if (firstGroupSelected && secondGroupSelected) enableRelationshipButtons();
	}

	void writeSequence(string group1, string group2, string relationship)
	{
		string newLine = group1 + ", " + group2 + ", " + relationship;
		FileWriter.appendLine(Constants.SequenceFile, newLine);
	}

	void clearSequencingData()
	{
		resetModel(firstGroupName, secondGroupName);

		UILabel label1 = NGUITools.FindInParents<UILabel>(GameObject.Find("Message1"));
		UILabel label2 = NGUITools.FindInParents<UILabel>(GameObject.Find("Message2"));
		label1.text = "Select First Group:";
		label2.text = "";

		UICheckbox group1Checkbox = NGUITools.FindInParents<UICheckbox>(GameObject.Find("Checkbox_" + firstGroupName));
		UICheckbox group2Checkbox = NGUITools.FindInParents<UICheckbox>(GameObject.Find("Checkbox_" + secondGroupName));
		if (group1Checkbox != null) group1Checkbox.isChecked = false;
		if (group2Checkbox != null) group2Checkbox.isChecked = false;

		firstGroupName = "";
		secondGroupName = "";
		firstGroupSelected = false;
		secondGroupSelected = false;
		SequencingInteraction.selectedGroups.Clear();
	}

	void resetModel(string firstGroup, string secondGroup)
	{
		string[] firstGroupType = firstGroup.Split('_');
		GameObject[] firstGroupElements = GameObject.FindGameObjectsWithTag(firstGroupType[0]);
		
		for (int i = 0; i < firstGroupElements.Length; i++)
		{
			Component[] renderers = firstGroupElements[i].GetComponentsInChildren(typeof(Renderer));
			foreach (Renderer renderer in renderers)
			{
				for(int j = 0; j< renderer.materials.Length; j++)
				{
					renderer.materials[j].color = startColor1;					
					renderer.material.mainTexture = startTexture1;
				}
			}

		}

		string[] secondGroupType = secondGroup.Split('_');
		GameObject[] secondGroupElements = GameObject.FindGameObjectsWithTag(secondGroupType[0]);
		
		for (int i = 0; i < secondGroupElements.Length; i++)
		{
			Component[] renderers = secondGroupElements[i].GetComponentsInChildren(typeof(Renderer));
			foreach (Renderer renderer in renderers)
			{
				for(int j = 0; j< renderer.materials.Length; j++)
				{
					renderer.materials[j].color = startColor2;					
					renderer.material.mainTexture = startTexture2;
				}
			}
			
		}

	}


	void setButtonDisabled(UIButton crrButton)
	{
		UISprite crrSprite = crrButton.GetComponentInChildren<UISprite>();
		crrSprite.color = Color.red;
		crrButton.enabled = false;
	}


	public static void enableRelationshipButtons()
	{
		UIButton StoSButton = NGUITools.FindInParents<UIButton>(GameObject.Find("Start2Start"));
		UIButton FtoSButton = NGUITools.FindInParents<UIButton>(GameObject.Find("Finish2Start"));
		StoSButton.enabled = true;
		FtoSButton.enabled = true;

		UISprite crrSprite1 = StoSButton.GetComponentInChildren<UISprite>();
		crrSprite1.color = new Color32(75, 137, 223, 255);

		UISprite crrSprite2 = FtoSButton.GetComponentInChildren<UISprite>();
		crrSprite2.color = new Color32(75, 137, 223, 255);
	}

	void OnGUI()
	{
		if(showMessage == true)
		{
			GUI.depth = 1;
			GUI.Window(0,(new Rect(Screen.width/2 - 100,Screen.height/2 + 100, 300,100)),
			           displayMessage,
			           "Message!");
			
			GUI.depth = 0;
			if(GUI.Button (new Rect(Screen.width/2 + 20, Screen.height/2 + 165, 50,30), "OK"))
			{
				showMessage = false;	
			}
			
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			resetModel(firstGroupName, secondGroupName);
		}	
	}

	void displayMessage(int id)
	{
		GUI.Label (new Rect(30, 30, 250,90), message);
	}


	public static void setMessage(string newMessage)
	{
		string[] lines = newMessage.Split('\n');

		UILabel label1 = NGUITools.FindInParents<UILabel>(GameObject.Find("Message1"));
		UILabel label2 = NGUITools.FindInParents<UILabel>(GameObject.Find("Message2"));
		label1.text = lines[0];
		label2.text = lines[1];
		//message = newMessage;
	}

	public static bool getFirstGroupSelected()
	{
		return firstGroupSelected;
	}

	public static string SelectedFirstGroupName
	{
		get { return firstGroupName; }
		set { firstGroupName = value; }
	}

	public static string SelectedSecondGroupName
	{
		get { return secondGroupName; }
		set { secondGroupName = value; }
	}

	public static Texture FirstGroupTexture
	{
		get { return startTexture1; }
		set { startTexture1 = value; }
	}

	public static Color FirstGroupColor
	{
		get { return startColor1; }
		set { startColor1 = value; }
	}

	public static Texture SecondGroupTexture
	{
		get { return startTexture2; }
		set { startTexture2 = value; }
	}

	public static Color SecondGroupColor
	{
		get { return startColor2; }
		set { startColor2 = value; }
	}

	public static void setFirstGroupSelected( bool newValue)
	{
		firstGroupSelected = newValue;
	}

	public static void setSecondGroupSelected(bool newValue)
	{
		secondGroupSelected = newValue;
	}

	public static bool getSecondGroupSelected()
	{
		return secondGroupSelected;
	}
	
	void OnRestartClicked()
	{
		Application.LoadLevel("PAVSequencing");	
		SequencingInteraction.selectedGroups.Clear();
	}
	
	void OnContinueClicked()
	{
		readSequenceData();
		createActivityWithSchedule(rootNode);
		saveActivitySequences();
		Application.LoadLevel("PAVResource");	
	}

	void createActivityWithSchedule(TreeNode node)
	{

		for (int i = 0; i < node.countSuccessors(); i++)
		{
			TreeNode crrNode = node.getSuccessor(i);
			string[] groupNameData = crrNode.GroupName.Split('_');
			if (groupNameData[0] == "Footing")
			{
				//activitylist.Add(createNewActivity("Excavate", "Method", crrNode.GroupName, groupNameData[0], "crew", 1, 1.0, 1.0, 1, 1, , null));

			}
			else if (groupNameData[0] == "Slab")
			{

			}
			else if (groupNameData[0] == "Column")
			{

			}
			else if (groupNameData[0] == "Beam")
			{

			}
			else if (groupNameData[0] == "Truss")
			{

			}
			else if (groupNameData[0] == "Sheathing")
			{

			}
			else if (groupNameData[0] == "Shingles")
			{

			}
			else if (groupNameData[0] == "Eaves")
			{
			}
			else 
			{
				print ("Error: No assembly type identified");
			}
		}
		for (int i = 0; i < node.countSuccessors(); i++)
		{
			createActivityWithSchedule(node.getSuccessor(i));
		}
	}

	private VCSActivity createNewActivity(string name, string method, string groupName, string groupType, 
	                                      string crew, int crewSize, double unitDuration, double crrDuration, 
	                                      int unitCost, int crrCost, DateTime asPlannedStart, DateTime asBuiltStart)
	{
		VCSActivity newActivity = new VCSActivity();
		newActivity.Name = name;
		newActivity.Method = method;
		newActivity.AssemblyGroupName = groupName;
		newActivity.AssemblyGroupType = groupType;
		newActivity.Crew = crew;
		newActivity.AsPlannedCrewSize = crewSize;
		newActivity.AsBuiltCrewSize = crewSize;
		newActivity.UnitDuration = unitDuration;
		newActivity.CurrentDuration = crrDuration;
		newActivity.UnitCost = unitCost;
		newActivity.CurrentCost = crrCost;
		newActivity.AsPlannedStartDateTime = asPlannedStart;
		newActivity.AsBuiltStartDateTime = asBuiltStart;
		newActivity.CurrentConstructionStatus = Constants.ConstructionStatus.NotStarted;
		return newActivity;
	}

	void saveActivitySequences()
	{
		FileWriter.createNewTextFile(Constants.ActivityScheduleFile);

		for (int i = 0; i < activitylist.Count; i++)
		{
			string crrLine = "";
			VCSActivity crrActivity = activitylist[i];
			crrLine = crrActivity.Name + "," + crrActivity.Method + "," + crrActivity.AssemblyGroupName + "," + 
				      crrActivity.Crew + "," + crrActivity.AsPlannedCrewSize.ToString() + "," + crrActivity.CurrentDuration + "," +
					  crrActivity.AsPlannedStartDateTime.ToString();
			FileWriter.appendLine(Constants.ActivityScheduleFile, crrLine);
		}
	}

	void readSequenceData()
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.SequenceFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		string[] sequenceLines = fileContents.Split("\n"[0]);
		
		for (int i = 0; i < sequenceLines.Length; i++)
		{
			if (sequenceLines[i] != null && sequenceLines[i] != "")
			{
				string[] crrLine = sequenceLines[i].Split(',');
				constructSequenceTree(crrLine[0].Replace(" ", string.Empty), crrLine[1].Replace(" ", string.Empty), crrLine[2].Replace(" ", string.Empty));
			}
		}
	}
	
	void constructSequenceTree(string predecessor, string successor, string relationship)
	{
		TreeNode predecessorNode = rootNode.getAssemblyGroupWithName(predecessor);
		TreeNode successorNode = rootNode.getAssemblyGroupWithName(successor);
		
		if( predecessorNode != null && successorNode != null)
		{
			//two assembly groups already exist. no further actions needed
		}
		else if(predecessorNode != null && successorNode == null)
		{
			TreeNode newSuccessorNode = predecessorNode.addSuccessor(successor, relationship);
		}
		else if(predecessorNode == null && successorNode != null)
		{
			TreeNode newPredecessorNode = successorNode.addPredecessor(predecessor, relationship);
		}
		else //both assembly grups do not exist in the tree(!rootNode.doesAssemblyGroupExist(predecessor))
		{
			TreeNode newPredecessorNode = rootNode.addSuccessor(predecessor, Constants.StartToStart);
			TreeNode newSuccessorNode = newPredecessorNode.addSuccessor(successor, relationship);
		}
	}

	// Handles for the Footings
	void HideFooting()
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
	void HideSlab()
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
	void HideColumn()
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
	void HideBeam()
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
	void HideShingles()
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
	void HideTruss()
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
	void HideSheathing()
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
	void HideEaves()
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
	

}
