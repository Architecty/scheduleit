using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class ActivityPreFab : MonoBehaviour {
	
	string crrActNameClicked = "";
	string crrAssemblyTypeClicked = "";
	
	private string[] methodData;
	
	

	// Use this for initialization
	void Start () {
		var reader2 = new StreamReader(Application.dataPath + "/Methods2.txt");
		var fileContents2 = reader2.ReadToEnd();
		reader2.Close();
		
		methodData = fileContents2.Split("\n"[0]);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	
	void OnCheckmarkClicked()
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
					crrAssemblyTypeClicked = currentActivityName[1];
					crrActNameClicked = currentActivityName[2];
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
		string[] applicableMethodNames = new string[4];
		string[] crew = new string[4];
		string[] dailyOutput = new string[4];
		string[] unit = new string[4];
		string[] cost = new string[4];
		
		for (int i = 0; i < applicableMethods.Length; i++) applicableMethods[i] = "";

		int crrMethodCount = 0;
		
		for (int i = 0; i < methodData.Length; i++)
		{
			string[] crrMethod = methodData[i].Split(',');
			if (crrMethod[1].Equals(assemblyType) && crrMethod[2].Equals(actName))
			{
				applicableMethods[crrMethodCount] = crrMethod[crrMethod.Length - 2];
				applicableMethodNames[crrMethodCount] = crrMethod[3];
				crew[crrMethodCount] = crrMethod[4].ToString();
				dailyOutput[crrMethodCount] = crrMethod[5].ToString();
				unit[crrMethodCount] = crrMethod[6].ToString();
				cost[crrMethodCount] = crrMethod[7].ToString();
				crrMethodCount++;
			}
		}
		
		for (int i = crrMethodCount; i < imageButtons.Length; i++)
		{
			applicableMethods[i] = "_empty";
			applicableMethodNames[i] = "N/A";
			imageButtons[i].enabled = false;
			crew[i] = "N/A";
			dailyOutput[i] = "N/A";
			unit[i] = "N/A";
			cost[i] = "N/A";
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
				
				UILabel[] labels = imageButtons[i].GetComponentsInChildren<UILabel>();
				for (int labelIndex = 0; labelIndex < labels.Length; labelIndex++)
				{
					switch (labels[labelIndex].name)
					{
						case "Cost": labels[labelIndex].text = "Cost: " + cost[0]; break;
						case "Crew": labels[labelIndex].text = "Crew: " + crew[0]; break;
						case "Daily Output": labels[labelIndex].text = "Daily Output: " + dailyOutput[0]; break;
						case "Label": labels[labelIndex].text = applicableMethodNames[0]; break;
						case "Unit": labels[labelIndex].text = "Unit: " + unit[0]; break;
						default: break;
					}
						
				}
				//imageButtons[i].GetComponentInChildren<UILabel>().text = applicableMethodNames[0];
			}
			else if (imageButtons[i].name.Equals("Method2"))
			{
				buttonSprite.spriteName = applicableMethods[1];
				imageButtons[i].hoverSprite = applicableMethods[1];
				imageButtons[i].pressedSprite = applicableMethods[1];
				imageButtons[i].disabledSprite = applicableMethods[1];
				
				UILabel[] labels = imageButtons[i].GetComponentsInChildren<UILabel>();
				for (int labelIndex = 0; labelIndex < labels.Length; labelIndex++)
				{
					switch (labels[labelIndex].name)
					{
						case "Cost": labels[labelIndex].text = "Cost: " + cost[1]; break;
						case "Crew": labels[labelIndex].text = "Crew: " + crew[1]; break;
						case "Daily Output": labels[labelIndex].text = "Daily Output: " + dailyOutput[1]; break;
						case "Label": labels[labelIndex].text = applicableMethodNames[1]; break;
						case "Unit": labels[labelIndex].text = "Unit: " + unit[1]; break;
						default: break;
					}
						
				}				
				
				//imageButtons[i].GetComponentInChildren<UILabel>().text = applicableMethodNames[1];
			}
			else if (imageButtons[i].name.Equals("Method3"))
			{
				//imageButtons[i].enabled = false;
				buttonSprite.spriteName = applicableMethods[2];
				imageButtons[i].hoverSprite = applicableMethods[2];
				imageButtons[i].pressedSprite = applicableMethods[2];
				imageButtons[i].disabledSprite = applicableMethods[2];

				UILabel[] labels = imageButtons[i].GetComponentsInChildren<UILabel>();
				for (int labelIndex = 0; labelIndex < labels.Length; labelIndex++)
				{
					switch (labels[labelIndex].name)
					{
						case "Cost": labels[labelIndex].text = "Cost: " + cost[2]; break;
						case "Crew": labels[labelIndex].text = "Crew: " + crew[2]; break;
						case "Daily Output": labels[labelIndex].text = "Daily Output: " + dailyOutput[2]; break;
						case "Label": labels[labelIndex].text = applicableMethodNames[2]; break;
						case "Unit": labels[labelIndex].text = "Unit: " + unit[2]; break;
						default: break;
					}
						
				}
				
				//imageButtons[i].GetComponentInChildren<UILabel>().text = applicableMethodNames[2];
			}
			else if (imageButtons[i].name.Equals("Method4"))
			{
				//imageButtons[i].enabled = false;
				buttonSprite.spriteName = applicableMethods[3];
				imageButtons[i].hoverSprite = applicableMethods[3];
				imageButtons[i].pressedSprite = applicableMethods[3];
				imageButtons[i].disabledSprite = applicableMethods[3];
				
				UILabel[] labels = imageButtons[i].GetComponentsInChildren<UILabel>();
				for (int labelIndex = 0; labelIndex < labels.Length; labelIndex++)
				{
					switch (labels[labelIndex].name)
					{
						case "Cost": labels[labelIndex].text = "Cost: " + cost[3]; break;
						case "Crew": labels[labelIndex].text = "Crew: " + crew[3]; break;
						case "Daily Output": labels[labelIndex].text = "Daily Output: " + dailyOutput[3]; break;
						case "Label": labels[labelIndex].text = applicableMethodNames[3]; break;
						case "Unit": labels[labelIndex].text = "Unit: " + unit[3]; break;
						default: break;
					}
						
				}
				
				//imageButtons[i].GetComponentInChildren<UILabel>().text = applicableMethodNames[3];
				
			}		
		}
	}
}
