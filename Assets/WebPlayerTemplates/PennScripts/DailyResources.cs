using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class DailyResources : MonoBehaviour {
	public GameObject table1LinePrefab;
	public GameObject table2LinePrefab;
	private List<GameObject> activityDataList;
	private List<GameObject> laborList;
	private List<GameObject> equipmentList;

	private Ray ray;

	GameObject dailyResourcesWindow;
	// Use this for initialization
	void Start () {
		activityDataList = new List<GameObject>();
		laborList = new List<GameObject>();
		equipmentList = new List<GameObject>();

		dailyResourcesWindow = GameObject.Find("DailyResourcesPanel");
		loadActivityTable();
		loadLaborTable();
		loadEquipmentTable();
		loadLookAheadTable();
		NGUITools.SetActive(dailyResourcesWindow,false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void hideDailyResources(){
		NGUITools.SetActive(dailyResourcesWindow,false);
	}

	void showDailyResources(){
		//dailyResourcesWindow = GameObject.Find("DailyResourcesPanel");
		NGUITools.SetActive(dailyResourcesWindow,true);
		//GameObject simulationPanel = GameObject.Find("Camera");
		//GameObject dailyResourcesPanel;
		//dailyResourcesPanel = NGUITools.AddChild(simulationPanel, resourceTable1Prefab);
	}

	void loadActivityTable()
	{
		GameObject table1 = GameObject.Find("Table1");
		GameObject resource;
		resource = NGUITools.AddChild(table1, table1LinePrefab);
		resource = NGUITools.AddChild(table1, table1LinePrefab);
		resource = NGUITools.AddChild(table1, table1LinePrefab);
		resource = NGUITools.AddChild(table1, table1LinePrefab);
		resource = NGUITools.AddChild(table1, table1LinePrefab);
		resource = NGUITools.AddChild(table1, table1LinePrefab);
		resource = NGUITools.AddChild(table1, table1LinePrefab);
		UIGrid panel = NGUITools.FindInParents<UIGrid>(GameObject.Find("Table1"));
		panel.Reposition();
	}

	void loadLaborTable()
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.LaborResourcesFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();

		string[] laborList = fileContents.Split("\n"[0]);

		GameObject laborTable = GameObject.Find("Table2");
		
		for (int i = 0; i < laborList.Length; i++)
		{
			if (laborList[i] != null && laborList[i] != "")
			{
				GameObject crrLabor;
				crrLabor = NGUITools.AddChild(laborTable, table2LinePrefab);
				UILabel laborLabel = crrLabor.GetComponentInChildren<UILabel>();
				laborLabel.text = laborList[i].Trim();
				UICheckbox laborCheckbox = crrLabor.GetComponentInChildren<UICheckbox>();
				laborCheckbox.isChecked = false;
			}
		}

		UIGrid panel = NGUITools.FindInParents<UIGrid>(laborTable);
		panel.Reposition();
	}

	void loadEquipmentTable()
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.EquipmentResourcesFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		string[] equipmentList = fileContents.Split("\n"[0]);
		
		GameObject equipmentTable = GameObject.Find("Table3");
		
		for (int i = 0; i < equipmentList.Length; i++)
		{
			if (equipmentList[i] != null && equipmentList[i] != "")
			{
				GameObject crrEquipment;
				crrEquipment = NGUITools.AddChild(equipmentTable, table2LinePrefab);
				UILabel equipmentLabel = crrEquipment.GetComponentInChildren<UILabel>();
				equipmentLabel.text = equipmentList[i].Trim();
				UICheckbox equipmentCheckbox = crrEquipment.GetComponentInChildren<UICheckbox>();
				equipmentCheckbox.isChecked = false;
			}
		}
		
		UIGrid panel = NGUITools.FindInParents<UIGrid>(equipmentTable);
		panel.Reposition();
	}

	void loadLookAheadTable()
	{
		
		GameObject table4 = GameObject.Find("Table4");
		GameObject resource3;
		resource3 = NGUITools.AddChild(table4, table1LinePrefab);
		resource3 = NGUITools.AddChild(table4, table1LinePrefab);
		resource3 = NGUITools.AddChild(table4, table1LinePrefab);
		resource3 = NGUITools.AddChild(table4, table1LinePrefab);
		resource3 = NGUITools.AddChild(table4, table1LinePrefab);

		UIGrid panel3 = NGUITools.FindInParents<UIGrid>(GameObject.Find("Table4"));
		panel3.Reposition();
	}

}
