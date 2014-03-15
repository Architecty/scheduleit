using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class MethodSelection : MonoBehaviour {
	
	public static bool grouping = false;
	public GameObject prefab;
	
	private GameObject[] model;
	private GameObject[] modelGroup;
	private string[] methodData;
	// 
	private GameObject[] selectedGroup; // holds all the element gameobjects of the same assembly
	//
	
	string crrAssemblyTypeClicked = "";
	string crrActNameClicked = "";
	
	private string[] tempTags;
	
	//public GameObject button;
	
	private bool showErrorMessage = false;
	
	UITable panel = new UITable();
		
	void Start()
	{
		UITable panel = NGUITools.FindInParents<UITable>(GameObject.Find("Footing"));	
		
		var reader1 = new StreamReader(Application.dataPath + "/" + Constants.ActivityFile);
		var fileContents1 = reader1.ReadToEnd();
		reader1.Close();
		
		var reader2 = new StreamReader(Application.dataPath + "/Methods.txt");//"/" + Constants.MethodFile);
		var fileContents2 = reader2.ReadToEnd();
		reader2.Close();
		
		methodData = fileContents2.Split("\n"[0]);
		
		// Load tables into arrays
		string[] allActivities = fileContents1.Split("\n"[0]);

		//for (int i = 0; i < allActivities.Length; i++)
		for(int i = allActivities.Length - 1; i >= 0; i--)
		{
			if (allActivities[i] != "")
			{
				string[] activity = allActivities[i].Split(',');
				//footing
				if (activity[1] == "Footing")
				{
					GameObject activityGameObject = NGUITools.AddChild(GameObject.Find("Table1"),prefab);
					activityGameObject.GetComponentInChildren<UILabel>().text = activity[2].ToString();
					activityGameObject.name="Checkbox_Footing_" + activity[2].ToString();
					activityGameObject.GetComponentInChildren<UICheckbox>().radioButtonRoot = transform.parent;
					NGUITools.FindInParents<UITable>(GameObject.Find("Checkbox_Footing_" + activity[2].ToString())).sorted = false;
					
				}
				else if (activity[1] == "Slab")
				{
					GameObject activityGameObject = NGUITools.AddChild(GameObject.Find("Table2"),prefab);
					activityGameObject.GetComponentInChildren<UILabel>().text = activity[2].ToString();
					activityGameObject.name="Checkbox_Slab_" + activity[2].ToString();
					activityGameObject.GetComponentInChildren<UICheckbox>().radioButtonRoot = transform.parent;
					NGUITools.FindInParents<UITable>(GameObject.Find("Checkbox_Slab_" + activity[2].ToString())).sorted = false;				
				}
				else if (activity[1] == "Column")
				{
					GameObject activityGameObject = NGUITools.AddChild(GameObject.Find("Table3"),prefab);
					activityGameObject.GetComponentInChildren<UILabel>().text = activity[2].ToString();
					activityGameObject.name="Checkbox_Column_" + activity[2].ToString();
					activityGameObject.GetComponentInChildren<UICheckbox>().radioButtonRoot = transform.parent;
				}
				else if (activity[1] == "Beam")
				{
					GameObject activityGameObject = NGUITools.AddChild(GameObject.Find("Table4"),prefab);
					activityGameObject.GetComponentInChildren<UILabel>().text = activity[2].ToString();
					activityGameObject.name="Checkbox_Beam_" + activity[2].ToString();
					activityGameObject.GetComponentInChildren<UICheckbox>().radioButtonRoot = transform.parent;
				}
				else if (activity[1] == "Shingles")
				{
					GameObject activityGameObject = NGUITools.AddChild(GameObject.Find("Table5"),prefab);
					activityGameObject.GetComponentInChildren<UILabel>().text = activity[2].ToString();
					activityGameObject.name="Checkbox_Shingles_" + activity[2].ToString();
					activityGameObject.GetComponentInChildren<UICheckbox>().radioButtonRoot = transform.parent;
				}
				else if (activity[1] == "Truss")
				{
					GameObject activityGameObject = NGUITools.AddChild(GameObject.Find("Table6"),prefab);
					activityGameObject.GetComponentInChildren<UILabel>().text = activity[2].ToString();
					activityGameObject.name="Checkbox_Truss_" + activity[2].ToString();
					activityGameObject.GetComponentInChildren<UICheckbox>().radioButtonRoot = transform.parent;
				}
				else if (activity[1] == "Sheathing")
				{
					GameObject activityGameObject = NGUITools.AddChild(GameObject.Find("Table7"),prefab);
					activityGameObject.GetComponentInChildren<UILabel>().text = activity[2].ToString();
					activityGameObject.name="Checkbox_Sheathing_" + activity[2].ToString();
					activityGameObject.GetComponentInChildren<UICheckbox>().radioButtonRoot = transform.parent;
				}
				else if (activity[1] == "Eaves")
				{
					GameObject activityGameObject = NGUITools.AddChild(GameObject.Find("Table8"),prefab);
					activityGameObject.GetComponentInChildren<UILabel>().text = activity[2].ToString();
					activityGameObject.name="Checkbox_Eaves_" + activity[2].ToString();
					activityGameObject.GetComponentInChildren<UICheckbox>().radioButtonRoot = transform.parent;
				}
			}
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
		
		panel.Reposition();
		
	}
	
	void OnBeamClicked()
	{
		/*
		GameObject.Find("FootingTween").SetActive(false);
		GameObject.Find("SlabTween").SetActive(false);
		GameObject.Find("ColumnTween").SetActive(false);
		GameObject.Find("BeamTween").SetActive(true);
		GameObject.Find("ShinglesTween").SetActive(false);
		GameObject.Find("SheathingTween").SetActive(false);
		GameObject.Find("TrussTween").SetActive(false);
		GameObject.Find("EavesTween").SetActive(false);
		panel.Reposition();
		*/
	}
	
	void OnColumnClicked()
	{
		/*
		GameObject.Find("FootingTween").SetActive(false);
		GameObject.Find("SlabTween").SetActive(false);
		GameObject.Find("ColumnTween").SetActive(true);
		GameObject.Find("BeamTween").SetActive(false);
		GameObject.Find("ShinglesTween").SetActive(false);
		GameObject.Find("SheathingTween").SetActive(false);
		GameObject.Find("TrussTween").SetActive(false);
		GameObject.Find("EavesTween").SetActive(false);
		panel.Reposition();		
		*/
	}

	
	void OnEavesClicked()
	{
		/*
		GameObject.Find("FootingTween").SetActive(false);
		GameObject.Find("SlabTween").SetActive(false);
		GameObject.Find("ColumnTween").SetActive(false);
		GameObject.Find("BeamTween").SetActive(false);
		GameObject.Find("ShinglesTween").SetActive(false);
		GameObject.Find("SheathingTween").SetActive(false);
		GameObject.Find("TrussTween").SetActive(false);
		GameObject.Find("EavesTween").SetActive(true);
		panel.Reposition();
		*/				
	}
	
	void OnFootingClicked()
	{
		/*
		GameObject.Find("FootingTween").SetActive(true);
		GameObject.Find("SlabTween").SetActive(false);
		GameObject.Find("ColumnTween").SetActive(false);
		GameObject.Find("BeamTween").SetActive(false);
		GameObject.Find("ShinglesTween").SetActive(false);
		GameObject.Find("SheathingTween").SetActive(false);
		GameObject.Find("TrussTween").SetActive(false);
		GameObject.Find("EavesTween").SetActive(false);	
		panel.Reposition();
		*/			
	}
	
	void OnSheathingClicked()
	{
		/*
		GameObject.Find("FootingTween").SetActive(false);
		GameObject.Find("SlabTween").SetActive(false);
		GameObject.Find("ColumnTween").SetActive(false);
		GameObject.Find("BeamTween").SetActive(false);
		GameObject.Find("ShinglesTween").SetActive(false);
		GameObject.Find("SheathingTween").SetActive(true);
		GameObject.Find("TrussTween").SetActive(false);
		GameObject.Find("EavesTween").SetActive(false);	
		panel.Reposition();
		*/			
	}
	
	void OnShinglesClicked()
	{
		/*
		GameObject.Find("FootingTween").SetActive(false);
		GameObject.Find("SlabTween").SetActive(false);
		GameObject.Find("ColumnTween").SetActive(false);
		GameObject.Find("BeamTween").SetActive(false);
		GameObject.Find("ShinglesTween").SetActive(true);
		GameObject.Find("SheathingTween").SetActive(false);
		GameObject.Find("TrussTween").SetActive(false);
		GameObject.Find("EavesTween").SetActive(false);		
		panel.Reposition();
		*/		
	}
	
	void OnSlabClicked()
	{
		/*
		GameObject.Find("FootingTween").SetActive(false);
		GameObject.Find("SlabTween").SetActive(true);
		GameObject.Find("ColumnTween").SetActive(false);
		GameObject.Find("BeamTween").SetActive(false);
		GameObject.Find("ShinglesTween").SetActive(false);
		GameObject.Find("SheathingTween").SetActive(false);
		GameObject.Find("TrussTween").SetActive(false);
		GameObject.Find("EavesTween").SetActive(false);	
		panel.Reposition();
		*/			
	}
	
	void OnTrussClicked()
	{
		/*
		GameObject.Find("FootingTween").SetActive(false);
		GameObject.Find("SlabTween").SetActive(false);
		GameObject.Find("ColumnTween").SetActive(false);
		GameObject.Find("BeamTween").SetActive(false);
		GameObject.Find("ShinglesTween").SetActive(false);
		GameObject.Find("SheathingTween").SetActive(false);
		GameObject.Find("TrussTween").SetActive(true);
		GameObject.Find("EavesTween").SetActive(false);	
		panel.Reposition();
		*/			
	}
	
	void OnActivityClicked()
	{
		UIButton[] allButtons = NGUITools.FindActive<UIButton>(); 
		string currentButtonName = "";
		
		for (int i = 0; i < allButtons.Length; i++)
		{
			string[] crrButton1 = allButtons[i].name.Split('_');
			
			if (crrButton1[0].Equals("Checkbox"))
			{
				UICheckbox checkbox1 = allButtons[i].GetComponentInChildren<UICheckbox>();	
				if (checkbox1.isChecked == true) 
				{
					string[] currentActivityName = checkbox1.name.Split('_');
					crrActNameClicked = currentActivityName[2];
					//print (currentActivityName[2].ToString());
				}
			}
		}
		
		if (crrActNameClicked != "" && crrAssemblyTypeClicked != "")
			loadMethods(crrAssemblyTypeClicked, crrActNameClicked);
	}
	
	void loadMethods(string assemblyType, string actName)
	{


		//UIAnchor methodAnchor = NGUITools.FindInParents<UIAnchor>(GameObject.Find("Method1"));
		UIImageButton[] imageButtons = NGUITools.FindActive<UIImageButton>();
		//UIAtlas MethodAtlas = Resources.Load ("New Atlas", typeof(UIAtlas)) as UIAtlas;
		
		string[] applicableMethods = new string[4];
		for (int i = 0; i < applicableMethods.Length; i++) applicableMethods[i] = "";//{"", "", "", ""};
		applicableMethods[2] = "_empty";
		applicableMethods[3] = "_empty";
		int crrMethodCount = 0;
		
		for (int i = 0; i < methodData.Length; i++)
		{
			string[] crrMethod = methodData[i].Split(',');
			if (crrMethod[1].Equals(assemblyType) && crrMethod[2].Equals(actName))
			{
				applicableMethods[crrMethodCount] = crrMethod[crrMethod.Length - 2];
				crrMethodCount++;
			}
		}
		
		for (int i = 0; i < imageButtons.Length; i++)
		{
			UISprite buttonSprite = imageButtons[i].GetComponentInChildren<UISprite>();
			if (imageButtons[i].name.Equals("Method1"))
			{
				buttonSprite.spriteName = applicableMethods[0];
				imageButtons[i].hoverSprite = applicableMethods[0];
				imageButtons[i].pressedSprite = applicableMethods[0];
				imageButtons[i].disabledSprite = applicableMethods[0];
			}
			else if (imageButtons[i].name.Equals("Method2"))
			{
				buttonSprite.spriteName = applicableMethods[1];
				imageButtons[i].hoverSprite = applicableMethods[1];
				imageButtons[i].pressedSprite = applicableMethods[1];
				imageButtons[i].disabledSprite = applicableMethods[1];
			}
			else if (imageButtons[i].name.Equals("Method3"))
			{
				//imageButtons[i].enabled = false;
				buttonSprite.spriteName = applicableMethods[2];
				imageButtons[i].hoverSprite = applicableMethods[2];
				imageButtons[i].pressedSprite = applicableMethods[2];
				imageButtons[i].disabledSprite = applicableMethods[2];
			}
			else if (imageButtons[i].name.Equals("Method4"))
			{
				//imageButtons[i].enabled = false;
				buttonSprite.spriteName = applicableMethods[3];
				imageButtons[i].hoverSprite = applicableMethods[3];
				imageButtons[i].pressedSprite = applicableMethods[3];
				imageButtons[i].disabledSprite = applicableMethods[3];
				
			}		
		}
	}

	bool checkIfAllActivitiesSelected(string groupName)
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
		GameObject temp = GameObject.Find(assemblyName);
		//print("Game object name = " + temp.name.ToString() + " assembly name = " + assemblyName);
		UIButton[] temp2 = new UIButton[3];
		temp2 = temp.GetComponentsInChildren<UIButton>();
		
		for (int i = 0; i < temp2.Length; i++)
		{
			temp2[i].disabledColor = color;
			temp2[i].isEnabled = false;
		}
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
		Application.LoadLevel("TestMethods");
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
		GUI.Label (new Rect(30, 30, 250,90),
			       "Building Elements of the same type can only be grouped!");
	}
}

