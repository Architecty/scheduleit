using UnityEngine;
using System.Collections;

public class MethodMenu : MonoBehaviour {
	
	private string[,] methodSelected = new string[8, 6];
	
	enum assemblies : int {Beam, Column, Eaves, Footing, Sheathing, Shingles, Slab, Truss};
	enum footingActivities: int {Excavate, Form, Reinforce, PlaceConcrete, StripForms, Cure};
	enum slabActivities: int {Excavate, Form, Reinforce, PlaceConcrete, StripForms, CureAndFinish};
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMethod1Clicked()
	{
		UIButton[] allButtons = NGUITools.FindActive<UIButton>(); 
		string currentButtonName = "";
		string selectedAssemblyType = "";
		string selectedActivityName = "";
		
		for (int i = 0; i < allButtons.Length; i++)
		{
			string[] crrButton1 = allButtons[i].name.Split('_');
			
			if (crrButton1[0].Equals("Checkbox"))
			{
				UICheckbox checkbox1 = allButtons[i].GetComponentInChildren<UICheckbox>();	
				if (checkbox1.isChecked)
				{
					string[] currentActivityName = checkbox1.name.Split('_');
					selectedAssemblyType = currentActivityName[1];
					selectedActivityName = currentActivityName[2];
					
					UILabel crrLabel = allButtons[i].GetComponentInChildren<UILabel>();
					string[] tempLabel = crrLabel.text.Split(':');
					crrLabel.text = tempLabel[0] + ": M1";
					crrLabel.color = Color.green;
				}

			}
		}
		
		UIImageButton[] allImageButtons = NGUITools.FindActive<UIImageButton>();
		for (int i = 0; i < allImageButtons.Length; i++)
		{
			UILabel[] labels = allImageButtons[i].GetComponentsInChildren<UILabel>();
			string methodName = "";
			
			for (int labelIndex = 0; labelIndex < labels.Length; labelIndex++)
			{
				if (labels[labelIndex].name.Equals("Label")) methodName = labels[labelIndex].text;	
			}
			
			if (allImageButtons[i].name.Equals("Method1"))
			{
				switch (selectedAssemblyType)
				{
					case "Beam" : methodSelected[(int) assemblies.Beam, 0] = methodName; break;
					case "Column" : methodSelected[(int) assemblies.Column, 0] = methodName; break;
					case "Eaves" : methodSelected[(int) assemblies.Eaves, 0] = methodName; break;
					case "Footing" :
						switch (selectedActivityName)
						{
							case "Excavate": methodSelected[(int) assemblies.Footing, (int) footingActivities.Excavate] = methodName; break;
							case "Form": methodSelected[(int) assemblies.Footing, (int) footingActivities.Form] = methodName; break;
							case "Reinforce": methodSelected[(int) assemblies.Footing, (int) footingActivities.Reinforce] = methodName; break;
							case "Place concrete": methodSelected[(int) assemblies.Footing, (int) footingActivities.PlaceConcrete] = methodName; break;
							case "Strip forms": methodSelected[(int) assemblies.Footing, (int) footingActivities.StripForms] = methodName; break;
							case "Cure": methodSelected[(int) assemblies.Footing, (int) footingActivities.Cure] = methodName; break;
							default: break;
						}
						break;
					case "Sheathing" : methodSelected[(int) assemblies.Sheathing, 0] = methodName; break;
					case "Shingles" : methodSelected[(int) assemblies.Shingles, 0] = methodName; break;
					case "Slab" :
						switch (selectedActivityName)
						{
							case "Excavate": methodSelected[(int) assemblies.Slab, (int) slabActivities.Excavate] = methodName; break;
							case "Form": methodSelected[(int) assemblies.Slab, (int) slabActivities.Form] = methodName; break;
							case "Reinforce": methodSelected[(int) assemblies.Slab, (int) slabActivities.Reinforce] = methodName; break;
							case "Place concrete": methodSelected[(int) assemblies.Slab, (int) slabActivities.PlaceConcrete] = methodName; break;
							case "Strip forms": methodSelected[(int) assemblies.Slab, (int) slabActivities.StripForms] = methodName; break;
							case "Cure and finish": methodSelected[(int) assemblies.Slab, (int) slabActivities.CureAndFinish] = methodName; break;
							default: break;
						}					
						break;
					case "Truss" : methodSelected[(int) assemblies.Truss, 0] = methodName; break;					
					default: break;
				}
			}
		}
	}
	
	void OnMethod2Clicked()
	{
		UIButton[] allButtons = NGUITools.FindActive<UIButton>(); 
		string currentButtonName = "";
		string selectedAssemblyType = "";
		string selectedActivityName = "";
		
		for (int i = 0; i < allButtons.Length; i++)
		{
			string[] crrButton1 = allButtons[i].name.Split('_');
			
			if (crrButton1[0].Equals("Checkbox"))
			{
				UICheckbox checkbox1 = allButtons[i].GetComponentInChildren<UICheckbox>();	
				if (checkbox1.isChecked)
				{
					string[] currentActivityName = checkbox1.name.Split('_');
					selectedAssemblyType = currentActivityName[1];
					selectedActivityName = currentActivityName[2];
					
					UILabel crrLabel = allButtons[i].GetComponentInChildren<UILabel>();
					string[] tempLabel = crrLabel.text.Split(':');
					crrLabel.text = tempLabel[0] + ": M2";
					crrLabel.color = Color.green;
				}

			}
		}
		
		UIImageButton[] allImageButtons = NGUITools.FindActive<UIImageButton>();
		for (int i = 0; i < allImageButtons.Length; i++)
		{
			UILabel[] labels = allImageButtons[i].GetComponentsInChildren<UILabel>();
			string methodName = "";
			
			for (int labelIndex = 0; labelIndex < labels.Length; labelIndex++)
			{
				if (labels[labelIndex].name.Equals("Label")) methodName = labels[labelIndex].text;	
			}
			
			if (allImageButtons[i].name.Equals("Method2"))
			{
				switch (selectedAssemblyType)
				{
					case "Beam" : methodSelected[(int) assemblies.Beam, 0] = methodName; break;
					case "Column" : methodSelected[(int) assemblies.Column, 0] = methodName; break;
					case "Eaves" : methodSelected[(int) assemblies.Eaves, 0] = methodName; break;
					case "Footing" :
						switch (selectedActivityName)
						{
							case "Excavate": methodSelected[(int) assemblies.Footing, (int) footingActivities.Excavate] = methodName; break;
							case "Form": methodSelected[(int) assemblies.Footing, (int) footingActivities.Form] = methodName; break;
							case "Reinforce": methodSelected[(int) assemblies.Footing, (int) footingActivities.Reinforce] = methodName; break;
							case "Place concrete": methodSelected[(int) assemblies.Footing, (int) footingActivities.PlaceConcrete] = methodName; break;
							case "Strip forms": methodSelected[(int) assemblies.Footing, (int) footingActivities.StripForms] = methodName; break;
							case "Cure": methodSelected[(int) assemblies.Footing, (int) footingActivities.Cure] = methodName; break;
							default: break;
						}
						break;
					case "Sheathing" : methodSelected[(int) assemblies.Sheathing, 0] = methodName; break;
					case "Shingles" : methodSelected[(int) assemblies.Shingles, 0] = methodName; break;
					case "Slab" :
						switch (selectedActivityName)
						{
							case "Excavate": methodSelected[(int) assemblies.Slab, (int) slabActivities.Excavate] = methodName; break;
							case "Form": methodSelected[(int) assemblies.Slab, (int) slabActivities.Form] = methodName; break;
							case "Reinforce": methodSelected[(int) assemblies.Slab, (int) slabActivities.Reinforce] = methodName; break;
							case "Place concrete": methodSelected[(int) assemblies.Slab, (int) slabActivities.PlaceConcrete] = methodName; break;
							case "Strip forms": methodSelected[(int) assemblies.Slab, (int) slabActivities.StripForms] = methodName; break;
							case "Cure and finish": methodSelected[(int) assemblies.Slab, (int) slabActivities.CureAndFinish] = methodName; break;
							default: break;
						}					
						break;
					case "Truss" : methodSelected[(int) assemblies.Truss, 0] = methodName; break;					
					default: break;
				}
			}
		}	}
	
	void OnMethod3Clicked()
	{
		UIButton[] allButtons = NGUITools.FindActive<UIButton>(); 
		string currentButtonName = "";
		string selectedAssemblyType = "";
		string selectedActivityName = "";
		
		for (int i = 0; i < allButtons.Length; i++)
		{
			string[] crrButton1 = allButtons[i].name.Split('_');
			
			if (crrButton1[0].Equals("Checkbox"))
			{
				UICheckbox checkbox1 = allButtons[i].GetComponentInChildren<UICheckbox>();	
				if (checkbox1.isChecked)
				{
					string[] currentActivityName = checkbox1.name.Split('_');
					selectedAssemblyType = currentActivityName[1];
					selectedActivityName = currentActivityName[2];
					
					UILabel crrLabel = allButtons[i].GetComponentInChildren<UILabel>();
					string[] tempLabel = crrLabel.text.Split(':');
					crrLabel.text = tempLabel[0] + ": M3";
					crrLabel.color = Color.green;
				}

			}
		}
		
		UIImageButton[] allImageButtons = NGUITools.FindActive<UIImageButton>();
		for (int i = 0; i < allImageButtons.Length; i++)
		{
			UILabel[] labels = allImageButtons[i].GetComponentsInChildren<UILabel>();
			string methodName = "";
			
			for (int labelIndex = 0; labelIndex < labels.Length; labelIndex++)
			{
				if (labels[labelIndex].name.Equals("Label")) methodName = labels[labelIndex].text;	
			}
			
			if (allImageButtons[i].name.Equals("Method3"))
			{
				switch (selectedAssemblyType)
				{
					case "Beam" : methodSelected[(int) assemblies.Beam, 0] = methodName; break;
					case "Column" : methodSelected[(int) assemblies.Column, 0] = methodName; break;
					case "Eaves" : methodSelected[(int) assemblies.Eaves, 0] = methodName; break;
					case "Footing" :
						switch (selectedActivityName)
						{
							case "Excavate": methodSelected[(int) assemblies.Footing, (int) footingActivities.Excavate] = methodName; break;
							case "Form": methodSelected[(int) assemblies.Footing, (int) footingActivities.Form] = methodName; break;
							case "Reinforce": methodSelected[(int) assemblies.Footing, (int) footingActivities.Reinforce] = methodName; break;
							case "Place concrete": methodSelected[(int) assemblies.Footing, (int) footingActivities.PlaceConcrete] = methodName; break;
							case "Strip forms": methodSelected[(int) assemblies.Footing, (int) footingActivities.StripForms] = methodName; break;
							case "Cure": methodSelected[(int) assemblies.Footing, (int) footingActivities.Cure] = methodName; break;
							default: break;
						}
						break;
					case "Sheathing" : methodSelected[(int) assemblies.Sheathing, 0] = methodName; break;
					case "Shingles" : methodSelected[(int) assemblies.Shingles, 0] = methodName; break;
					case "Slab" :
						switch (selectedActivityName)
						{
							case "Excavate": methodSelected[(int) assemblies.Slab, (int) slabActivities.Excavate] = methodName; break;
							case "Form": methodSelected[(int) assemblies.Slab, (int) slabActivities.Form] = methodName; break;
							case "Reinforce": methodSelected[(int) assemblies.Slab, (int) slabActivities.Reinforce] = methodName; break;
							case "Place concrete": methodSelected[(int) assemblies.Slab, (int) slabActivities.PlaceConcrete] = methodName; break;
							case "Strip forms": methodSelected[(int) assemblies.Slab, (int) slabActivities.StripForms] = methodName; break;
							case "Cure and finish": methodSelected[(int) assemblies.Slab, (int) slabActivities.CureAndFinish] = methodName; break;
							default: break;
						}					
						break;
					case "Truss" : methodSelected[(int) assemblies.Truss, 0] = methodName; break;					
					default: break;
				}
			}
		}	}
	
	void OnMethod4Clicked()
	{
		UIButton[] allButtons = NGUITools.FindActive<UIButton>(); 
		string currentButtonName = "";
		string selectedAssemblyType = "";
		string selectedActivityName = "";
		
		for (int i = 0; i < allButtons.Length; i++)
		{
			string[] crrButton1 = allButtons[i].name.Split('_');
			
			if (crrButton1[0].Equals("Checkbox"))
			{
				UICheckbox checkbox1 = allButtons[i].GetComponentInChildren<UICheckbox>();	
				if (checkbox1.isChecked)
				{
					string[] currentActivityName = checkbox1.name.Split('_');
					selectedAssemblyType = currentActivityName[1];
					selectedActivityName = currentActivityName[2];
					
					UILabel crrLabel = allButtons[i].GetComponentInChildren<UILabel>();
					string[] tempLabel = crrLabel.text.Split(':');
					crrLabel.text = tempLabel[0] + ": M4";
					crrLabel.color = Color.green;
				}

			}
		}
		
		UIImageButton[] allImageButtons = NGUITools.FindActive<UIImageButton>();
		for (int i = 0; i < allImageButtons.Length; i++)
		{
			UILabel[] labels = allImageButtons[i].GetComponentsInChildren<UILabel>();
			string methodName = "";
			
			for (int labelIndex = 0; labelIndex < labels.Length; labelIndex++)
			{
				if (labels[labelIndex].name.Equals("Label")) methodName = labels[labelIndex].text;	
			}
			
			if (allImageButtons[i].name.Equals("Method4"))
			{
				switch (selectedAssemblyType)
				{
					case "Beam" : methodSelected[(int) assemblies.Beam, 0] = methodName; break;
					case "Column" : methodSelected[(int) assemblies.Column, 0] = methodName; break;
					case "Eaves" : methodSelected[(int) assemblies.Eaves, 0] = methodName; break;
					case "Footing" :
						switch (selectedActivityName)
						{
							case "Excavate": methodSelected[(int) assemblies.Footing, (int) footingActivities.Excavate] = methodName; break;
							case "Form": methodSelected[(int) assemblies.Footing, (int) footingActivities.Form] = methodName; break;
							case "Reinforce": methodSelected[(int) assemblies.Footing, (int) footingActivities.Reinforce] = methodName; break;
							case "Place concrete": methodSelected[(int) assemblies.Footing, (int) footingActivities.PlaceConcrete] = methodName; break;
							case "Strip forms": methodSelected[(int) assemblies.Footing, (int) footingActivities.StripForms] = methodName; break;
							case "Cure": methodSelected[(int) assemblies.Footing, (int) footingActivities.Cure] = methodName; break;
							default: break;
						}
						break;
					case "Sheathing" : methodSelected[(int) assemblies.Sheathing, 0] = methodName; break;
					case "Shingles" : methodSelected[(int) assemblies.Shingles, 0] = methodName; break;
					case "Slab" :
						switch (selectedActivityName)
						{
							case "Excavate": methodSelected[(int) assemblies.Slab, (int) slabActivities.Excavate] = methodName; break;
							case "Form": methodSelected[(int) assemblies.Slab, (int) slabActivities.Form] = methodName; break;
							case "Reinforce": methodSelected[(int) assemblies.Slab, (int) slabActivities.Reinforce] = methodName; break;
							case "Place concrete": methodSelected[(int) assemblies.Slab, (int) slabActivities.PlaceConcrete] = methodName; break;
							case "Strip forms": methodSelected[(int) assemblies.Slab, (int) slabActivities.StripForms] = methodName; break;
							case "Cure and finish": methodSelected[(int) assemblies.Slab, (int) slabActivities.CureAndFinish] = methodName; break;
							default: break;
						}					
						break;
					case "Truss" : methodSelected[(int) assemblies.Truss, 0] = methodName; break;					
					default: break;
				}
			}
		}	}

	void OnContinueButtonClicked()
	{
		FileReader.writeActivityMethod(methodSelected);
		
		Application.LoadLevel("PAVSequencing");
	}
	
}
