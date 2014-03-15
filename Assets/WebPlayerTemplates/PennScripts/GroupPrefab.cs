using UnityEngine;
using UnityEditor;
using System;
using System.Collections;


public class GroupPrefab : MonoBehaviour {
	
	private Texture highlight = new Texture2D(128,128);
	private Color startColor;
	private Texture startTexture;	
	private string[] assemblyType;

	// Use this for initialization
	void Start () {
		assemblyType = this.name.Split('_');
		GameObject crrAssembly = GameObject.FindGameObjectWithTag(assemblyType[1]);
		Component[] renderers = crrAssembly.GetComponentsInChildren(typeof(Renderer));
        foreach (Renderer renderer in renderers)
        {
            for(int k = 0; k< renderer.materials.Length; k++)
			{
				startTexture = renderer.material.mainTexture;
				startColor = renderer.materials[k].color;
			}
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCheckmarkClicked()
	{
		/*
		UICheckbox crrCheckbox = this.GetComponentInChildren<UICheckbox>();
		string[] crrGroup = FileReader.getGroupElements(crrCheckbox.GetComponentInChildren<UILabel>().text);

		for(int j = 3; j < crrGroup.Length - 1; j++)
		{
			// This turns all of the renderer colors into another to show highlights
			string[] assemblyType = crrGroup[j].Split('_');
			Component[] renderers;
			GameObject[] assemblies = GameObject.FindGameObjectsWithTag(assemblyType[0]);
			
			for (int index = 0; index < assemblies.Length; index++)
			{
				if (assemblies[index].name.Equals(crrGroup[j]))
				{
					renderers = assemblies[index].GetComponentsInChildren(typeof(Renderer));
					foreach (Renderer renderer in renderers)
					{
						for(int k = 0; k< renderer.materials.Length; k++)
						{
							renderer.material.mainTexture = highlight;
							renderer.materials[k].color = Color.yellow;
						}
					}							
				}
			}
		}
		*/

		UIButton[] allButtons = NGUITools.FindActive<UIButton>(); 
		string[] currentGroupName = new string[Constants.MAXElement];
		
		for (int i = 0; i < allButtons.Length; i++)
		{
			string[] crrButton1 = allButtons[i].name.Split('_');
			
			if (crrButton1[0].Equals("Checkbox"))
			{
				UICheckbox checkbox1 = allButtons[i].GetComponentInChildren<UICheckbox>();	
				if (checkbox1.isChecked == true) 
				{
					currentGroupName[i] = checkbox1.GetComponentInChildren<UILabel>().text;
				}
			}
		}

		resetModel();

		
		//highlight the selected groups
		for (int i = 0; i < Constants.MAXElement - 1; i++)
		{
			if (currentGroupName[i] != null && currentGroupName[i] != "")
			{
				string[] crrGroup = FileReader.getGroupElements(currentGroupName[i]);
				
				for(int j = 3; j < crrGroup.Length - 1; j++)
				{
					// This turns all of the renderer colors into another to show highlights
					string[] assemblyType = crrGroup[j].Split('_');
			        Component[] renderers;
					GameObject[] assemblies = GameObject.FindGameObjectsWithTag(assemblyType[0]);
		      		
					for (int index = 0; index < assemblies.Length; index++)
					{
						if (assemblies[index].name.Equals(crrGroup[j]))
						{
							renderers = assemblies[index].GetComponentsInChildren(typeof(Renderer));
				            foreach (Renderer renderer in renderers)
				            {
				                for(int k = 0; k< renderer.materials.Length; k++)
								{
									renderer.material.mainTexture = highlight;
									renderer.materials[k].color = Color.yellow;
								}
				            }							
						}
					}
				}
			}
		}

		string[] groupName = this.name.Split('_');
		SequencingInteraction.crrSelectedAssemblyGroup = groupName[1] + "_" + groupName[2];

		if (!Sequencing.firstGroupSelected)
		{
			SequencingInteraction.startColor1 = startColor;
			SequencingInteraction.startTexture1 = startTexture;
		}
		else
		{
			SequencingInteraction.startColor2 = startColor;
			SequencingInteraction.startTexture2 = startTexture;
		}
		//updateSequencing(groupName[1] + "_" + groupName[2]);

	}

	void updateSequencing(string selectedGroup)
	{
		if(!Sequencing.getFirstGroupSelected())
		{
			Sequencing.SelectedFirstGroupName = selectedGroup;
			string firstMessage = "First Group: " + selectedGroup + "\n";
			string secondMessage = "Select Second Group: ";
			Sequencing.setMessage(firstMessage + secondMessage);
			Sequencing.setFirstGroupSelected(true);
			Sequencing.FirstGroupTexture = startTexture;
			Sequencing.FirstGroupColor = startColor;
		}
		else if (Sequencing.getFirstGroupSelected() && !Sequencing.getSecondGroupSelected())
		{
			Sequencing.SelectedSecondGroupName = selectedGroup;
			string firstMessage = "First Group: " + Sequencing.SelectedFirstGroupName + "\n";
			string secondMessage = "Second Group: " + selectedGroup;
			Sequencing.setMessage(firstMessage + secondMessage);
			Sequencing.setFirstGroupSelected(false);
			Sequencing.SecondGroupTexture = startTexture;
			Sequencing.SecondGroupColor = startColor;
			Sequencing.enableRelationshipButtons();
		}
	}


	
	void resetModel()
	{
		GameObject[] allElements = GameObject.FindGameObjectsWithTag(assemblyType[1]);
        
		for (int i = 0; i < allElements.Length; i++)
		{
			Component[] renderers = allElements[i].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	            for(int j = 0; j< renderer.materials.Length; j++)
				{
					renderer.materials[j].color = startColor;					
					renderer.material.mainTexture = startTexture;
				}
	        }

		}
	}

}
