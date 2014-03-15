using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class Resource : MonoBehaviour {

	public GameObject resourcePrefab;
	private TreeNode rootNode;
	private string[] activityData;
	private string[,] footingActivityData = new string[6,7]; //six activities, one method, crew, unit production, unit, unit cost, unit work quantity
	private string[,] slabActivityData = new string[6,7];
	private string[] columnActivityData = new string[7];
	private string[] beamActivityData = new string[7];
	private string[] trussActivityData = new string[7];
	private string[] sheathingActivityData = new string[7];
	private string[] shingleActivityData = new string[7];
	private string[] eaveActivityData = new string[7];

	//private List<VCSActivity> vcsActivityList;
	private List<GameObject> resourcePrefabList;

	enum assemblies : int {Beam, Column, Eaves, Footing, Sheathing, Shingles, Slab, Truss};
	enum footingActivities: int {Excavate, Form, Reinforce, PlaceConcrete, StripForms, Cure};
	enum slabActivities: int {Excavate, Form, Reinforce, PlaceConcrete, StripForms, CureAndFinish};

	// Use this for initialization
	void Start () {
		rootNode = new TreeNode("root", null, "", 0);
		resourcePrefabList = new List<GameObject>();

		readActivityData();
		readMethodData();

		readSequenceData();
		loadAssemblyGroups(rootNode);

		updateTotals();

		UIGrid panel = NGUITools.FindInParents<UIGrid>(GameObject.Find("ResourcesGrid"));
		panel.Reposition();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
		{
			updateCost();
			updateDuration();
		}
	}

	void updateTotals()
	{
		updateCost();
		updateDuration();
		updateTotalCost();
		updateTotalDuration();
	}

	void updateTotalDuration()
	{
		UILabel totalDurationLabel = NGUITools.FindInParents<UILabel>(GameObject.Find("TotalTimeLabel"));
		
		double crrTotalDuration = 0.0;
		GameObject[] durationLabels = GameObject.FindGameObjectsWithTag("TimeLabelTag");
		for (int i = 0; i < durationLabels.Length; i++)
		{
			UILabel crrLabel = NGUITools.FindInParents<UILabel>(durationLabels[i]);
			if (crrLabel.text != "Time") crrTotalDuration += double.Parse(crrLabel.text);
		}
		
		totalDurationLabel.text = "Total Duration: \n" + crrTotalDuration.ToString("F02") + " Hour";
	}

	void updateDuration()
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.GroupFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		string[] assemblyGroupData = fileContents.Split("\n"[0]);

		for (int i = 0; i < resourcePrefabList.Count; i++)
		{
			GameObject crrActivityGameObject = resourcePrefabList[i];
			if (crrActivityGameObject != null)
			{
				UILabel[] labels = crrActivityGameObject.GetComponentsInChildren<UILabel>();
				int crewSize = 0;
				int unitProduction = 0;
				double unitWorkQuantity = 0.0;
				string groupName = "";
				string activityName = "";
				string methodName = "";
				for (int j = 0; j < labels.Length; j++)
				{
					if (labels[j].name == "GroupNameLabel") groupName = labels[j].text;
					else if (labels[j].name == "ActivityLabel") activityName = labels[j].text;
					else if (labels[j].name == "MethodLabel") methodName = labels[j].text;
					else if (labels[j].name == "CrewInputLabel") crewSize = int.Parse(labels[j].text.TrimEnd('|'));
				}

				string[] groupType = groupName.Split('_');
				if (groupType[0] == "Footing")
				{
					for (int j = 0; j < 6; j++)
					{
						if (footingActivityData[j, 0] == activityName) 
						{
							unitProduction = int.Parse(footingActivityData[j, 3]);
							unitWorkQuantity = double.Parse(footingActivityData[j, 6]);
							break;
						}
					}
				}
				else if (groupType[0] == "Slab")
				{
					for (int j = 0; j < 6; j++)
					{
						if (slabActivityData[j, 0] == activityName)
						{
							unitProduction = int.Parse(slabActivityData[j, 3]);
							unitWorkQuantity = double.Parse(slabActivityData[j, 6]);
							break;
						}
					}
				}
				else if (groupType[0] == "Column") 
				{
					unitProduction = int.Parse(columnActivityData[3]);
					unitWorkQuantity = double.Parse(columnActivityData[6]);
				}
				else if (groupType[0] == "Beam") 
				{
					unitProduction = int.Parse(beamActivityData[3]);
					unitWorkQuantity = double.Parse(beamActivityData[6]);
				}
				else if (groupType[0] == "Truss") 
				{
					unitProduction = int.Parse(trussActivityData[3]);
					unitWorkQuantity = double.Parse(trussActivityData[6]);
				}
				else if (groupType[0] == "Sheathing") 
				{
					unitProduction = int.Parse(sheathingActivityData[3]);
					unitWorkQuantity = double.Parse(sheathingActivityData[6]);
				}
				else if (groupType[0] == "Shingles") 
				{
					unitProduction = int.Parse(shingleActivityData[3]);
					unitWorkQuantity = double.Parse(shingleActivityData[6]);
				}
				else if (groupType[0] == "Eaves")
				{
					unitProduction = int.Parse(eaveActivityData[3]); 
					unitWorkQuantity = double.Parse(eaveActivityData[6]);
				}
				else print ("Error: no building assembly type matches - " + groupType);

				int totalProduction = crewSize * unitProduction;

				int numberofAssemblyInGroup = 0;
				double totalWorkQuantity = 0.0;

				for (int j = 0; j < assemblyGroupData.Length; j++)
				{
					if (assemblyGroupData[j] != "")
					{
						string[] crrAssemblyGroup = assemblyGroupData[j].Split(',');
						if (crrAssemblyGroup[1] == groupName)
						{
							numberofAssemblyInGroup = crrAssemblyGroup.Length - 4;
							break;
						}
					}
				}

				totalWorkQuantity = (double) numberofAssemblyInGroup * unitWorkQuantity;

				double durationHour = (totalWorkQuantity / totalProduction) * 8.0;

				for (int j = 0; j < labels.Length; j++)
				{
					if (labels[j].name == "TimeLabel")
					{
						labels[j].text = durationHour.ToString("F02");
						break;
					}
				}
			}
		}

		updateTotalDuration();
	}

	void updateCost()
	{
		for (int i = 0; i < resourcePrefabList.Count; i++)
		{
			GameObject crrActivityGameObject = resourcePrefabList[i];

			if (crrActivityGameObject != null)
			{
				UILabel[] labels = crrActivityGameObject.GetComponentsInChildren<UILabel>();
				int crewSize = 0;
				int unitCost = 0;
				string groupType = "";
				string activityName = "";
				string methodName = "";
				for (int j = 0; j < labels.Length; j++)
				{
					if (labels[j].name == "GroupNameLabel" && labels[j].text != "")
					{
						string[] crrGroupName = labels[j].text.Split('_');
						groupType = crrGroupName[0];
					}
					else if (labels[j].name == "ActivityLabel" && labels[j].text != "")
					{
						activityName = labels[j].text;
					}
					else if (labels[j].name == "CrewInputLabel" && labels[j].text != "")
					{
						crewSize = int.Parse(labels[j].text.TrimEnd('|'));
					}
					else if (labels[j].name == "MethodLabel")
					{
						methodName = labels[j].text;
					}
				}

				if (groupType == "Footing")
				{
					for (int j = 0; j < 6; j++)
					{
						if (footingActivityData[j, 0] == activityName) 
						{
							unitCost = int.Parse(footingActivityData[j, 5]);
							break;
						}
					}
				}
				else if (groupType == "Slab")
				{
					for (int j = 0; j < 6; j++)
					{
						if (slabActivityData[j, 0] == activityName)
						{
							unitCost = int.Parse(slabActivityData[j, 5]);
							break;
						}
					}
				}
				else if (groupType == "Column") unitCost = int.Parse(columnActivityData[5]);
				else if (groupType == "Beam") unitCost = int.Parse(beamActivityData[5]);
				else if (groupType == "Truss") unitCost = int.Parse(trussActivityData[5]);
				else if (groupType == "Sheathing") unitCost = int.Parse(sheathingActivityData[5]);
				else if (groupType == "Shingles") unitCost = int.Parse(shingleActivityData[5]);
				else if (groupType == "Eaves") unitCost = int.Parse(eaveActivityData[5]); //eaves
				else print ("Error: no building assembly type matches - " + groupType);

				for (int j = 0; j < labels.Length; j++)
				{
					if (labels[j].name == "CostLabel") labels[j].text = (crewSize * unitCost).ToString();
				}
			}
			else print ("ResourcePrefab Game Object " + i.ToString() + " does not exist!");
		}

		updateTotalCost();
	}

	void updateTotalCost()
	{
		UILabel totalCostLabel = NGUITools.FindInParents<UILabel>(GameObject.Find("TotalCostLabel"));

		int crrTotalCost = 0;
		GameObject[] costLabels = GameObject.FindGameObjectsWithTag("CostLabelTag");
		for (int i = 0; i < costLabels.Length; i++)
		{
			//UILabel[] labels = assemblyPrefabs
			//print (label.text);
			UILabel crrLabel = NGUITools.FindInParents<UILabel>(costLabels[i]);
			if (crrLabel.text != "Cost") crrTotalCost += int.Parse(crrLabel.text);
		}

		totalCostLabel.text = "Total Cost: \n" + "$ " + crrTotalCost.ToString();

	}

	void continueButtonClicked()
	{
		//print ("continue button clicked");
		FileWriter.createNewTextFile(Constants.ResourceFile);

		for (int i = 0; i < resourcePrefabList.Count; i++)
		{
			string crrLine = "";

			GameObject crrActivityGameObject = resourcePrefabList[i];
			
			if (crrActivityGameObject != null)
			{
				UILabel[] labels = crrActivityGameObject.GetComponentsInChildren<UILabel>();
				for (int j = 0; j < labels.Length; j++)
				{
					if (labels[j].name == "GroupNameLabel")
					{
						crrLine += labels[j].text + ",";
					}
				}
				for (int j = 0; j < labels.Length; j++)
				{
					if (labels[j].name == "ActivityLabel")
					{
						crrLine += labels[j].text + ",";
					}
				}
				for (int j = 0; j < labels.Length; j++)
				{
					if (labels[j].name == "MethodLabel")
					{
						crrLine += labels[j].text + ",";
					}
				}
				for (int j = 0; j < labels.Length; j++)
				{
					if (labels[j].name == "CrewInputLabel")
					{
						crrLine += labels[j].text.TrimEnd('|') + ",";
					}
				}
				for (int j = 0; j < labels.Length; j++)
				{
					if (labels[j].name == "UnitProductionLabel")
					{
						crrLine += labels[j].text + ",";
					}
				}
				for (int j = 0; j < labels.Length; j++)
				{
					if (labels[j].name == "TimeLabel")
					{
						crrLine += labels[j].text + ",";
					}
				}
				for (int j = 0; j < labels.Length; j++)
				{
					if (labels[j].name == "CostLabel")
					{
						crrLine += labels[j].text + ",";
					}
				}

			}

			FileWriter.appendLine(Constants.ResourceFile, crrLine);
		}


			Application.LoadLevel("PAVSimulation");


	}

	void createActivityList()
	{

	}
	
	void readActivityData()
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.ActivityFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		string[] activityData = fileContents.Split("\n"[0]);

		for (int i = 0; i < activityData.Length; i++)
		{
			if (activityData[i] != "")
			{
				string[] crrActivityData = activityData[i].Split(',');
				if (crrActivityData[1] == "Footing") 
				{
					if (crrActivityData[2] == "Excavate") 
					{
						footingActivityData[(int)footingActivities.Excavate, 0] = crrActivityData[2];
						footingActivityData[(int)footingActivities.Excavate, 6] = crrActivityData[crrActivityData.Length - 2];
					}
					else if (crrActivityData[2] == "Form" )
					{
						footingActivityData[(int)footingActivities.Form, 0] = crrActivityData[2];
						footingActivityData[(int)footingActivities.Form, 6] = crrActivityData[crrActivityData.Length - 2];
					}
					else if (crrActivityData[2] == "Reinforce") 
					{
						footingActivityData[(int)footingActivities.Reinforce, 0] = crrActivityData[2];
						footingActivityData[(int)footingActivities.Reinforce, 6] = crrActivityData[crrActivityData.Length - 2];
					}
					else if (crrActivityData[2] == "Place concrete") 
					{
						footingActivityData[(int)footingActivities.PlaceConcrete, 0] = crrActivityData[2];
						footingActivityData[(int)footingActivities.PlaceConcrete, 6] = crrActivityData[crrActivityData.Length - 2];
					}
					else if (crrActivityData[2] == "Strip forms") 
					{
						footingActivityData[(int)footingActivities.StripForms,0] = crrActivityData[2];
						footingActivityData[(int)footingActivities.StripForms,6] = crrActivityData[crrActivityData.Length - 2];
					}
					else /* cure */ 
					{
						footingActivityData[(int)footingActivities.Cure, 0] = crrActivityData[2];
						footingActivityData[(int)footingActivities.Cure, 6] = crrActivityData[crrActivityData.Length - 2];
					}
				}
				else if (crrActivityData[1] == "Slab")
				{
					if (crrActivityData[2] == "Excavate") 
					{
						slabActivityData[(int)slabActivities.Excavate, 0] = crrActivityData[2];
						slabActivityData[(int)slabActivities.Excavate, 6] = crrActivityData[crrActivityData.Length - 2];
					}
					else if (crrActivityData[2] == "Form") 
					{
						slabActivityData[(int)slabActivities.Form, 0] = crrActivityData[2];
						slabActivityData[(int)slabActivities.Form, 6] = crrActivityData[crrActivityData.Length - 2];
					}
					else if (crrActivityData[2] == "Reinforce") 
					{
						slabActivityData[(int)slabActivities.Reinforce, 0] = crrActivityData[2];
						slabActivityData[(int)slabActivities.Reinforce, 6] = crrActivityData[crrActivityData.Length - 2];
					}
					else if (crrActivityData[2] == "Place concrete") 
					{
						slabActivityData[(int)slabActivities.PlaceConcrete, 0] = crrActivityData[2];
						slabActivityData[(int)slabActivities.PlaceConcrete, 6] = crrActivityData[crrActivityData.Length - 2];
					}
					else if (crrActivityData[2] == "Strip forms") 
					{
						slabActivityData[(int)slabActivities.StripForms, 0] = crrActivityData[2];
						slabActivityData[(int)slabActivities.StripForms, 6] = crrActivityData[crrActivityData.Length - 2];
					}
					else /* cure */ 
					{
						slabActivityData[(int)slabActivities.CureAndFinish, 0] = crrActivityData[2];
						slabActivityData[(int)slabActivities.CureAndFinish, 6] = crrActivityData[crrActivityData.Length - 2];
					}
				}
				else if (crrActivityData[1] == "Column") 
				{
					columnActivityData[0] = crrActivityData[2];
					columnActivityData[6] = crrActivityData[crrActivityData.Length - 2];
				}
				else if (crrActivityData[1] == "Beam") 
				{
					beamActivityData[0] = crrActivityData[2];
					beamActivityData[6] = crrActivityData[crrActivityData.Length - 2];
				}
				else if (crrActivityData[1] == "Truss") 
				{
					trussActivityData[0] = crrActivityData[2];
					trussActivityData[6] = crrActivityData[crrActivityData.Length - 2];
				}
				else if (crrActivityData[1] == "Sheathing") 
				{
					sheathingActivityData[0] = crrActivityData[2];
					sheathingActivityData[6] = crrActivityData[crrActivityData.Length - 2];
				}
				else if (crrActivityData[1] == "Shingles") 
				{
					shingleActivityData[0] = crrActivityData[2];
					shingleActivityData[6] = crrActivityData[crrActivityData.Length - 2];
				}
				else //eaves 
				{
					eaveActivityData[0] = crrActivityData[2]; //eaves
					eaveActivityData[6] = crrActivityData[crrActivityData.Length - 2]; //eaves
				}
			}
		}
	}

	void readMethodData()
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.SelectedActivityMethodFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();

		var methodDataReader = new StreamReader(Application.dataPath + "/" + Constants.MethodFile);
		var methodDataFileContents = methodDataReader.ReadToEnd();
		methodDataReader.Close();
		
		string[] activityMethodData = fileContents.Split("\n"[0]);	
		string[] methodDetailData = methodDataFileContents.Split("\n"[0]);

		for (int i = 0; i < activityMethodData.Length; i++)
		{
			if (activityMethodData[i] != "")
			{
				string[] crrActivityMethodData = activityMethodData[i].Split(',');
				if (crrActivityMethodData[0] == "Footing") 
				{
					int firstColumn = 0;
					if (crrActivityMethodData[1] == "Excavate") firstColumn = (int)footingActivities.Excavate;
					else if (crrActivityMethodData[1] == "Form")firstColumn = (int)footingActivities.Form;
					else if (crrActivityMethodData[1] == "Reinforce") firstColumn = (int)footingActivities.Reinforce;
					else if (crrActivityMethodData[1] == "Place concrete") firstColumn = (int)footingActivities.PlaceConcrete;
					else if (crrActivityMethodData[1] == "Strip forms") firstColumn = (int)footingActivities.StripForms;
					else /* cure */ firstColumn = (int)footingActivities.Cure;

					for (int j = 0; j < methodDetailData.Length; j++)
					{
						if (methodDetailData[j] != "")
						{
							string[] crrMethodDetailData = methodDetailData[j].Split(',');
							if (crrMethodDetailData[1] == "Footing" && crrMethodDetailData[2] == crrActivityMethodData[1] && crrMethodDetailData[3] == crrActivityMethodData[2])
							{
								footingActivityData[firstColumn, 0] = crrMethodDetailData[2]; //activity
								footingActivityData[firstColumn, 1] = crrMethodDetailData[3]; //method
								footingActivityData[firstColumn, 2] = crrMethodDetailData[4]; //crew
								footingActivityData[firstColumn, 3] = crrMethodDetailData[5]; //unit production
								footingActivityData[firstColumn, 4] = crrMethodDetailData[6]; //unit
								footingActivityData[firstColumn, 5] = crrMethodDetailData[7]; //unit cost
								break;
							}
						}
					}
				}
				else if (crrActivityMethodData[0] == "Slab")
				{
					int firstColumn = 0;
					if (crrActivityMethodData[1] == "Excavate") firstColumn = (int)slabActivities.Excavate;
					else if (crrActivityMethodData[1] == "Form") firstColumn = (int)slabActivities.Form;
					else if (crrActivityMethodData[1] == "Reinforce") firstColumn = (int)slabActivities.Reinforce;
					else if (crrActivityMethodData[1] == "Place concrete") firstColumn = (int)slabActivities.PlaceConcrete;
					else if (crrActivityMethodData[1] == "Strip forms") firstColumn = (int)slabActivities.StripForms;
					else /* cure */ firstColumn = (int)slabActivities.CureAndFinish;

					for (int j = 0; j < methodDetailData.Length; j++)
					{
						if (methodDetailData[j] != "")
						{
							string[] crrMethodDetailData = methodDetailData[j].Split(',');
							if (crrMethodDetailData[1] == "Slab" && crrMethodDetailData[2] == crrActivityMethodData[1] && crrMethodDetailData[3] == crrActivityMethodData[2])
							{
								slabActivityData[firstColumn, 0] = crrMethodDetailData[2]; //activity
								slabActivityData[firstColumn, 1] = crrMethodDetailData[3]; //method
								slabActivityData[firstColumn, 2] = crrMethodDetailData[4]; //crew
								slabActivityData[firstColumn, 3] = crrMethodDetailData[5]; //unit production
								slabActivityData[firstColumn, 4] = crrMethodDetailData[6]; //unit
								slabActivityData[firstColumn, 5] = crrMethodDetailData[7]; //unit cost
								break;
							}
						}
					}
				}
				else if (crrActivityMethodData[0] == "Column") 
				{
					for (int j = 0; j < methodDetailData.Length; j++)
					{
						if (methodDetailData[j] != "")
						{
							string[] crrMethodDetailData = methodDetailData[j].Split(',');
							if (crrMethodDetailData[1] == "Column" && crrMethodDetailData[2] == crrActivityMethodData[1] && crrMethodDetailData[3] == crrActivityMethodData[2])
							{
								for (int k = 0; k < 6; k++)
									columnActivityData[k] = crrMethodDetailData[k+2];
							}
						}
					}

				}
				else if (crrActivityMethodData[0] == "Beam") 
				{
					for (int j = 0; j < methodDetailData.Length; j++)
					{
						if (methodDetailData[j] != "")
						{
							string[] crrMethodDetailData = methodDetailData[j].Split(',');
							if (crrMethodDetailData[1] == "Beam" && crrMethodDetailData[2] == crrActivityMethodData[1] && crrMethodDetailData[3] == crrActivityMethodData[2])
							{
								for (int k = 0; k < 6; k++)
									beamActivityData[k] = crrMethodDetailData[k+2];
							}
						}
					}
				}
				else if (crrActivityMethodData[0] == "Truss") 
				{
					for (int j = 0; j < methodDetailData.Length; j++)
					{
						if (methodDetailData[j] != "")
						{
							string[] crrMethodDetailData = methodDetailData[j].Split(',');
							if (crrMethodDetailData[1] == "Truss" && crrMethodDetailData[2] == crrActivityMethodData[1] && crrMethodDetailData[3] == crrActivityMethodData[2])
							{
								for (int k = 0; k < 6; k++)
									trussActivityData[k] = crrMethodDetailData[k+2];
							}
						}
					}
				}
				else if (crrActivityMethodData[0] == "Sheathing") 
				{
					for (int j = 0; j < methodDetailData.Length; j++)
					{
						if (methodDetailData[j] != "")
						{
							string[] crrMethodDetailData = methodDetailData[j].Split(',');
							if (crrMethodDetailData[1] == "Sheathing" && crrMethodDetailData[2] == crrActivityMethodData[1] && crrMethodDetailData[3] == crrActivityMethodData[2])
							{
								for (int k = 0; k < 6; k++)
									sheathingActivityData[k] = crrMethodDetailData[k+2];
							}
						}
					}
				}
				else if (crrActivityMethodData[0] == "Shingles") 
				{
					for (int j = 0; j < methodDetailData.Length; j++)
					{
						if (methodDetailData[j] != "")
						{
							string[] crrMethodDetailData = methodDetailData[j].Split(',');
							if (crrMethodDetailData[1] == "Shingles" && crrMethodDetailData[2] == crrActivityMethodData[1] && crrMethodDetailData[3] == crrActivityMethodData[2])
							{
								for (int k = 0; k < 6; k++)
									shingleActivityData[k] = crrMethodDetailData[k+2];
							}
						}
					}
				}
				else if (crrActivityMethodData[0] == "Eaves")
				{
					for (int j = 0; j < methodDetailData.Length; j++)
					{
						if (methodDetailData[j] != "")
						{
							string[] crrMethodDetailData = methodDetailData[j].Split(',');
							if (crrMethodDetailData[1] == "Eaves" && crrMethodDetailData[2] == crrActivityMethodData[1] && crrMethodDetailData[3] == crrActivityMethodData[2])
							{
								for (int k = 0; k < 6; k++)
									eaveActivityData[k] = crrMethodDetailData[k+2];
							}
						}
					}
				}
				else 
					print ("Error: No building assemblies match: " + crrActivityMethodData[0]);
			}
		}
	}

	double getUnitDuration (string groupName, string activityName, string methodName)
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.GroupFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		string[] assemblyGroupData = fileContents.Split("\n"[0]);

		int crewSize = 1;
		int unitProduction = 0;
		double unitWorkQuantity = 0.0;
		string[] groupType = groupName.Split('_');
		if (groupType[0] == "Footing")
		{
			for (int j = 0; j < 6; j++)
			{
				if (footingActivityData[j, 0] == activityName) 
				{
					unitProduction = int.Parse(footingActivityData[j, 3]);
					unitWorkQuantity = double.Parse(footingActivityData[j, 6]);
					break;
				}
			}
		}
		else if (groupType[0] == "Slab")
		{
			for (int j = 0; j < 6; j++)
			{
				if (slabActivityData[j, 0] == activityName)
				{
					unitProduction = int.Parse(slabActivityData[j, 3]);
					unitWorkQuantity = double.Parse(slabActivityData[j, 6]);
					break;
				}
			}
		}
		else if (groupType[0] == "Column") 
		{
			unitProduction = int.Parse(columnActivityData[3]);
			unitWorkQuantity = double.Parse(columnActivityData[6]);
		}
		else if (groupType[0] == "Beam") 
		{
			unitProduction = int.Parse(beamActivityData[3]);
			unitWorkQuantity = double.Parse(beamActivityData[6]);
		}
		else if (groupType[0] == "Truss") 
		{
			unitProduction = int.Parse(trussActivityData[3]);
			unitWorkQuantity = double.Parse(trussActivityData[6]);
		}
		else if (groupType[0] == "Sheathing") 
		{
			unitProduction = int.Parse(sheathingActivityData[3]);
			unitWorkQuantity = double.Parse(sheathingActivityData[6]);
		}
		else if (groupType[0] == "Shingles") 
		{
			unitProduction = int.Parse(shingleActivityData[3]);
			unitWorkQuantity = double.Parse(shingleActivityData[6]);
		}
		else if (groupType[0] == "Eaves")
		{
			unitProduction = int.Parse(eaveActivityData[3]); 
			unitWorkQuantity = double.Parse(eaveActivityData[6]);
		}
		else print ("Error: no building assembly type matches - " + groupType);
				
		int totalProduction = crewSize * unitProduction;
				
		int numberofAssemblyInGroup = 0;
		double totalWorkQuantity = 0.0;

		for (int j = 0; j < assemblyGroupData.Length; j++)
		{
			if (assemblyGroupData[j] != "")
			{
				string[] crrAssemblyGroup = assemblyGroupData[j].Split(',');
				if (crrAssemblyGroup[1] == groupName)
				{
					numberofAssemblyInGroup = crrAssemblyGroup.Length - 4;
					break;
				}
			}
		}

		totalWorkQuantity = (double) numberofAssemblyInGroup * unitWorkQuantity;
		//print ("No.: " + numberofAssemblyInGroup.ToString() + " Unit Quantity: " + unitWorkQuantity.ToString());
		//print (totalWorkQuantity.ToString() + " / " + totalProduction.ToString() + " = " + (totalWorkQuantity / totalProduction).ToString());

		return (totalWorkQuantity / totalProduction) * 8.0; //8 hour per day
	}

	string getUnitProduction(string assemblyName, string activityName, string methodName)
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.MethodFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();

		string[] contents = fileContents.Split("\n"[0]);
		for (int i = 0; i < contents.Length; i++)
		{
			string[] crrContent = contents[i].Split(',');
			if (crrContent[1].Equals(assemblyName) && crrContent[2].Equals(activityName) && crrContent[3].Equals(methodName))
				return crrContent[5] + " " + crrContent[6];
		}
		return "";
	}

	int getUnitCost(string assemblyName, string activityName, string methodName)
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.MethodFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		string[] contents = fileContents.Split("\n"[0]);
		for (int i = 0; i < contents.Length; i++)
		{
			string[] crrContent = contents[i].Split(',');
			if (crrContent[1].Equals(assemblyName) && crrContent[2].Equals(activityName) && crrContent[3].Equals(methodName))
				return Int32.Parse(crrContent[7]);
		}
		return 0;
	}


	void loadAssemblyGroups(TreeNode node)
	{
		for (int i = 0; i < node.countSuccessors(); i++)
		{
			TreeNode crrNode = node.getSuccessor(i);
			string[] crrGroupName = crrNode.GroupName.Split('_');

			if (crrGroupName[0] == "Footing")
			{
				for (int j = 0; j < 6 ; j++)
				{
					GameObject assemblyGroup;
					assemblyGroup = NGUITools.AddChild(GameObject.Find("ResourcesGrid"), resourcePrefab);
					UILabel[] labelList = assemblyGroup.GetComponentsInChildren<UILabel>();
					for (int k = 0; k < labelList.Length; k++)
					{
						if (labelList[k].name == "GroupNameLabel") labelList[k].text = crrNode.GroupName;
						else if (labelList[k].name == "ActivityLabel") labelList[k].text = footingActivityData[j,0];
						else if (labelList[k].name == "MethodLabel") labelList[k].text = footingActivityData[j,1];
						else if (labelList[k].name == "CrewInputLabel") labelList[k].text = "1";
						else if (labelList[k].name == "UnitProductionLabel") labelList[k].text = getUnitProduction("Footing", footingActivityData[j,0], footingActivityData[j,1]);
						else if (labelList[k].name == "TimeLabel") labelList[k].text = getUnitDuration(crrNode.GroupName, footingActivityData[j,0], footingActivityData[j,1]).ToString("F03");
						else if (labelList[k].name == "CostLabel") labelList[k].text = getUnitCost("Footing", footingActivityData[j,0], footingActivityData[j,1]).ToString();
						else labelList[k].text = "";

						labelList[k].color = new Color32(224, 224, 224, 255);
					}
				
					resourcePrefabList.Add(assemblyGroup);
				}
			}
			else if (crrGroupName[0] == "Slab")
			{
				for (int j = 0; j < 6 ; j++)
				{
					GameObject assemblyGroup;
					assemblyGroup = NGUITools.AddChild(GameObject.Find("ResourcesGrid"), resourcePrefab);
					UILabel[] labelList = assemblyGroup.GetComponentsInChildren<UILabel>();
					for (int k = 0; k < labelList.Length; k++)
					{
						if (labelList[k].name == "GroupNameLabel") labelList[k].text = crrNode.GroupName;
						else if (labelList[k].name == "ActivityLabel") labelList[k].text = slabActivityData[j,0];
						else if (labelList[k].name == "MethodLabel") labelList[k].text = slabActivityData[j,1];
						else if (labelList[k].name == "CrewInputLabel") labelList[k].text = "1";
						else if (labelList[k].name == "UnitProductionLabel") labelList[k].text = getUnitProduction("Slab", slabActivityData[j,0], slabActivityData[j,1]);
						else if (labelList[k].name == "TimeLabel") labelList[k].text = getUnitDuration(crrNode.GroupName, slabActivityData[j,0], slabActivityData[j,1]).ToString("F02");
						else if (labelList[k].name == "CostLabel") labelList[k].text = getUnitCost("Slab", slabActivityData[j,0], slabActivityData[j,1]).ToString();
						else labelList[k].text = "";

						labelList[k].color = new Color32(106, 255, 181, 255);
					}

					resourcePrefabList.Add(assemblyGroup);

				}
			}
			else if (crrGroupName[0] == "Column")
			{
				GameObject assemblyGroup;
				assemblyGroup = NGUITools.AddChild(GameObject.Find("ResourcesGrid"), resourcePrefab);
				UILabel[] labelList = assemblyGroup.GetComponentsInChildren<UILabel>();
				for (int j = 0; j < labelList.Length; j++)
				{
					if (labelList[j].name == "GroupNameLabel") labelList[j].text = crrNode.GroupName;
					else if (labelList[j].name == "ActivityLabel") labelList[j].text = columnActivityData[0];
					else if (labelList[j].name == "MethodLabel") labelList[j].text = columnActivityData[1];
					else if (labelList[j].name == "CrewInputLabel") labelList[j].text = "1";
					else if (labelList[j].name == "UnitProductionLabel") labelList[j].text = getUnitProduction("Column", columnActivityData[0], columnActivityData[1]);
					else if (labelList[j].name == "TimeLabel") labelList[j].text = getUnitDuration(crrNode.GroupName, columnActivityData[0], columnActivityData[1]).ToString("F02");
					else if (labelList[j].name == "CostLabel") labelList[j].text = getUnitCost("Column", columnActivityData[0], columnActivityData[1]).ToString();
					else labelList[j].text = "";

					labelList[j].color = new Color32(255, 113, 113, 255);
				}

				resourcePrefabList.Add(assemblyGroup);

			}
			else if (crrGroupName[0] == "Beam")
			{
				GameObject assemblyGroup;
				assemblyGroup = NGUITools.AddChild(GameObject.Find("ResourcesGrid"), resourcePrefab);
				UILabel[] labelList = assemblyGroup.GetComponentsInChildren<UILabel>();
				for (int j = 0; j < labelList.Length; j++)
				{
					if (labelList[j].name == "GroupNameLabel") labelList[j].text = crrNode.GroupName;
					else if (labelList[j].name == "ActivityLabel") labelList[j].text = beamActivityData[0];
					else if (labelList[j].name == "MethodLabel") labelList[j].text = beamActivityData[1];
					else if (labelList[j].name == "CrewInputLabel") labelList[j].text = "1";
					else if (labelList[j].name == "UnitProductionLabel") labelList[j].text = getUnitProduction("Beam", beamActivityData[0], beamActivityData[1]);
					else if (labelList[j].name == "TimeLabel") labelList[j].text = getUnitDuration(crrNode.GroupName, beamActivityData[0], beamActivityData[1]).ToString("F02");
					else if (labelList[j].name == "CostLabel") labelList[j].text = getUnitCost("Beam", beamActivityData[0], beamActivityData[1]).ToString();
					else labelList[j].text = "";
					labelList[j].color = new Color32(255, 154, 53, 255);
				}

				resourcePrefabList.Add(assemblyGroup);

			}
			else if (crrGroupName[0] == "Truss")
			{
				GameObject assemblyGroup;
				assemblyGroup = NGUITools.AddChild(GameObject.Find("ResourcesGrid"), resourcePrefab);
				UILabel[] labelList = assemblyGroup.GetComponentsInChildren<UILabel>();
				for (int j = 0; j < labelList.Length; j++)
				{
					if (labelList[j].name == "GroupNameLabel") labelList[j].text = crrNode.GroupName;
					else if (labelList[j].name == "ActivityLabel") labelList[j].text = trussActivityData[0];
					else if (labelList[j].name == "MethodLabel") labelList[j].text = trussActivityData[1];
					else if (labelList[j].name == "CrewInputLabel") labelList[j].text = "1";
					else if (labelList[j].name == "UnitProductionLabel") labelList[j].text = getUnitProduction("Truss", trussActivityData[0], trussActivityData[1]);
					else if (labelList[j].name == "TimeLabel") labelList[j].text = getUnitDuration(crrNode.GroupName, trussActivityData[0], trussActivityData[1]).ToString("F02");
					else if (labelList[j].name == "CostLabel") labelList[j].text = getUnitCost("Truss", trussActivityData[0], trussActivityData[1]).ToString();
					else labelList[j].text = "";

					labelList[j].color = Color.yellow;
				}

				resourcePrefabList.Add(assemblyGroup);

			}
			else if (crrGroupName[0] == "Sheathing")
			{
				GameObject assemblyGroup;
				assemblyGroup = NGUITools.AddChild(GameObject.Find("ResourcesGrid"), resourcePrefab);
				UILabel[] labelList = assemblyGroup.GetComponentsInChildren<UILabel>();
				for (int j = 0; j < labelList.Length; j++)
				{
					if (labelList[j].name == "GroupNameLabel") labelList[j].text = crrNode.GroupName;
					else if (labelList[j].name == "ActivityLabel") labelList[j].text = sheathingActivityData[0];
					else if (labelList[j].name == "MethodLabel") labelList[j].text = sheathingActivityData[1];
					else if (labelList[j].name == "CrewInputLabel") labelList[j].text = "1";
					else if (labelList[j].name == "UnitProductionLabel") labelList[j].text = getUnitProduction("Sheathing", sheathingActivityData[0], sheathingActivityData[1]);
					else if (labelList[j].name == "TimeLabel") labelList[j].text = getUnitDuration(crrNode.GroupName, sheathingActivityData[0], sheathingActivityData[1]).ToString("F02");
					else if (labelList[j].name == "CostLabel") labelList[j].text = getUnitCost("Sheathing", sheathingActivityData[0], sheathingActivityData[1]).ToString();
					else labelList[j].text = "";
					labelList[j].color = new Color32(138, 197, 255, 255);

				}

				resourcePrefabList.Add(assemblyGroup);

			}
			else if (crrGroupName[0] == "Shingles")
			{
				GameObject assemblyGroup;
				assemblyGroup = NGUITools.AddChild(GameObject.Find("ResourcesGrid"), resourcePrefab);
				UILabel[] labelList = assemblyGroup.GetComponentsInChildren<UILabel>();
				for (int j = 0; j < labelList.Length; j++)
				{
					if (labelList[j].name == "GroupNameLabel") labelList[j].text = crrNode.GroupName;
					else if (labelList[j].name == "ActivityLabel") labelList[j].text = shingleActivityData[0];
					else if (labelList[j].name == "MethodLabel") labelList[j].text = shingleActivityData[1];
					else if (labelList[j].name == "CrewInputLabel") labelList[j].text = "1";
					else if (labelList[j].name == "UnitProductionLabel") labelList[j].text = getUnitProduction("Shingles", shingleActivityData[0], shingleActivityData[1]);
					else if (labelList[j].name == "TimeLabel") labelList[j].text = getUnitDuration(crrNode.GroupName, shingleActivityData[0], shingleActivityData[1]).ToString("F02");
					else if (labelList[j].name == "CostLabel") labelList[j].text = getUnitCost("Shingles", shingleActivityData[0], shingleActivityData[1]).ToString();
					else labelList[j].text = "";
					labelList[j].color = new Color32(216, 176, 255, 255);
				}

				resourcePrefabList.Add(assemblyGroup);

			}
			else if (crrGroupName[0] == "Eaves")
			{
				GameObject assemblyGroup;
				assemblyGroup = NGUITools.AddChild(GameObject.Find("ResourcesGrid"), resourcePrefab);
				UILabel[] labelList = assemblyGroup.GetComponentsInChildren<UILabel>();
				for (int j = 0; j < labelList.Length; j++)
				{
					if (labelList[j].name == "GroupNameLabel") labelList[j].text = crrNode.GroupName;
					else if (labelList[j].name == "ActivityLabel") labelList[j].text = eaveActivityData[0];
					else if (labelList[j].name == "MethodLabel") labelList[j].text = eaveActivityData[1];
					else if (labelList[j].name == "CrewInputLabel") labelList[j].text = "1";
					else if (labelList[j].name == "UnitProductionLabel") labelList[j].text = getUnitProduction("Eaves", eaveActivityData[0], eaveActivityData[1]);
					else if (labelList[j].name == "TimeLabel") labelList[j].text = getUnitDuration(crrNode.GroupName, eaveActivityData[0], eaveActivityData[1]).ToString("F02");
					else if (labelList[j].name == "CostLabel") labelList[j].text = getUnitCost("Eaves", eaveActivityData[0], eaveActivityData[1]).ToString();
					else labelList[j].text = "";
					labelList[j].color = new Color32(255, 255, 255, 255);
				}

				resourcePrefabList.Add(assemblyGroup);
			}

		}

		for (int i = 0; i < node.countSuccessors(); i++)
		{
			loadAssemblyGroups(node.getSuccessor(i));
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
			//print ("predecessor of " + newSuccessorNode.GroupName + " is " + predecessorNode.GroupName);
		}
		else if(predecessorNode == null && successorNode != null)
		{
			TreeNode newPredecessorNode = successorNode.addPredecessor(predecessor, relationship);
			//print ("successor of " + newPredecessorNode.GroupName + " is " + successorNode.GroupName);
		}
		else //both assembly grups do not exist in the tree(!rootNode.doesAssemblyGroupExist(predecessor))
		{
			TreeNode newPredecessorNode = rootNode.addSuccessor(predecessor, Constants.StartToStart);
			TreeNode newSuccessorNode = newPredecessorNode.addSuccessor(successor, relationship);
			//print ("Predecessor of " + newPredecessorNode.GroupName + " is root");
			//print ("Predecessor of " + newSuccessorNode.GroupName + " is " + newPredecessorNode.GroupName);
		}
	}
}

