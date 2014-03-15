using UnityEngine;
using System.Collections;

public class UngroupTest : MonoBehaviour {
	
	public GameObject ungroupPrefab;
	//public GameObject secondPrefab;
	//private GameObject[] model;
	
	void Start()
	{

	}
	
	// This is used when the option to ungroup a group is selected
	// First it finds the elements of the group and returns them to their original form
	// Next it removes them from the group file
	// and then it removes the group from the list in the scene
	void OnActivate()
	{
		if(GetComponent<UICheckbox>().isChecked == true)
		{
			GameObject.Find(NGUITools.FindInParents<CheckboxControlledObject>(this.gameObject).name).GetComponent<UICheckbox>().isChecked = false;
			int size = NGUITools.FindInParents<CheckboxControlledObject>(this.gameObject).target.Length;
			for(int i = 0; i < size;i++)
			{
				GameObject.Find(NGUITools.FindInParents<CheckboxControlledObject>(this.gameObject).target[i].name).collider.enabled = true;
				GameObject.Find(NGUITools.FindInParents<CheckboxControlledObject>(this.gameObject).target[i].name).GetComponent<ObjectInteraction>().returnColor();
			}
			
			string ungroupName = GameObject.Find(NGUITools.FindInParents<CheckboxControlledObject>(this.gameObject).name).GetComponentInChildren<UILabel>().text;
			
			//added to set the menu button enabled by SL on 09/16/13
			string name = NGUITools.FindInParents<CheckboxControlledObject>(this.gameObject).name;
			string[] splittedName = name.Split('_');
			enableMenuButton(splittedName[1]);
			addUngroupedAssemblyInstances(splittedName[1], ungroupName);
			//end of addition
			
			FileReader.unGroup(ungroupName);
			NGUITools.Destroy(GameObject.Find(NGUITools.FindInParents<CheckboxControlledObject>(this.gameObject).name));
		}
	}
	
	void enableMenuButton(string assemblyName)
	{
		GameObject temp = GameObject.Find(assemblyName);
		UIButton[] temp2 = new UIButton[3];
		temp2 = temp.GetComponentsInChildren<UIButton>();
		for (int i = 0; i < 3; i++)
		{
			temp2[i].isEnabled = true;
		}
	}
	
	void addUngroupedAssemblyInstances (string assemblyName, string ungroupedAssemblies)
	{
		GameObject temp;
		GameObject[] model;
		
		//prefab = new GameObject();
		
		model = GameObject.FindGameObjectsWithTag(assemblyName);
	
		UITable panel = NGUITools.FindInParents<UITable>(GameObject.Find(assemblyName));
		
		string tableName = "";
		
		if (assemblyName == "Footing") tableName = "Table1";
		else if (assemblyName == "Slab") tableName = "Table2"; 
		else if (assemblyName == "Column") tableName = "Table3";
		else if (assemblyName == "Beam") tableName = "Table4"; 
		else if (assemblyName == "Shingles") tableName = "Table5"; 
		else if (assemblyName == "Truss") tableName = "Table6"; 
		else if (assemblyName == "Sheathing") tableName = "Table7";
		else if (assemblyName == "Eaves") tableName = "Table8"; 
		
		string[] ungroupedElements = FileReader.getGroupElements(ungroupedAssemblies);

		for(int j = 3; j < ungroupedElements.Length; j++)
		{
			try {
				if (ungroupedElements[j] != "")
				{
					int index = 0;
					for (int k = 0; k < model.Length; k++)
					{
						if (ungroupedElements[j].Equals(model[k].name)) 
						{
							index = k;
							break;
						}
					}
					string[] nameConvention = ungroupedElements[j].Split('_');
					temp = NGUITools.AddChild(GameObject.Find(tableName), ungroupPrefab);
					temp.GetComponent<UICheckboxControlledObject>().target = model[index];
					temp.GetComponentInChildren<UILabel>().text = model[index].name;
					temp.name="Checkbox_" + model[index].name;
					
				}
			}
			catch (System.Exception ex)
			{
				print (ex.ToString());	
			}
			
		}
		
		//reposition
		temp = GameObject.Find(assemblyName + "Tween");
		temp.GetComponentInChildren<UITable>().Reposition();
		GameObject.Find("GroupTable").GetComponent<UITable>().Reposition();
		
		panel.Reposition();
	}
}
