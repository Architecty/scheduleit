using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class SequencingInteraction : MonoBehaviour {

	private Ray ray;
	private RaycastHit hit;

	public static Color startColor1;
	public static Texture startTexture1;
	public static Color startColor2;
	public static Texture startTexture2;

	private Texture highlight = new Texture2D(128,128);
	private string[] assemblyType;
	private String oldAssemblyGroup;
	private bool selected = false;
	public static ArrayList selectedGroups = new ArrayList();
	private GameObject obj;

	public static string crrSelectedAssemblyGroup;

	private bool showErrorMessage = false;
	private string errorMessage = "";

	// Use this for initialization
	void Start () {
		obj = this.gameObject;
		//startColor = renderer.material.color;
		//startTexture = renderer.material.mainTexture;	
		crrSelectedAssemblyGroup = "";
		oldAssemblyGroup = "";
		/*
		assemblyType = this.name.Split('_');
		GameObject crrAssembly = GameObject.FindGameObjectWithTag(assemblyType[0]);
		Component[] renderers = crrAssembly.GetComponentsInChildren(typeof(Renderer));
		foreach (Renderer renderer in renderers)
		{
			for(int k = 0; k< renderer.materials.Length; k++)
			{
				startTexture = renderer.material.mainTexture;
				startColor = renderer.materials[k].color;
			}
		}
		*/
	}

	public string getCrrSelectedAssemblyGroup()
	{
		return crrSelectedAssemblyGroup;
	}



	void OnMouseDown()
	{
	
		// This detects what object is being clicked as long as it has a collider
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(UICamera.hoveredObject == null){
			if(Physics.Raycast(ray, out hit))
			{
				if(hit.collider.gameObject == this.collider.gameObject)
				{
					string[] selectedGroup = FileReader.getGroup(this.name);
					
					crrSelectedAssemblyGroup = selectedGroup[1];

					if (selectedGroups.Count != 0 && selectedGroups[0].Equals(selectedGroup[1]))
					{
						errorMessage = "You must select different assembly group";
						showErrorMessage = true;
						return;
					}

					if (selectedGroups.Count >= 2)
					{
						errorMessage = "You can select only two groups to dictate a relationship";
						showErrorMessage = true;
						return;
					}

					selectedGroups.Add(crrSelectedAssemblyGroup);
					//print (selectedGroups[1]);
					for(int j = 3; j < selectedGroup.Length - 1; j++)
					{
						// This turns all of the renderer colors into another to show highlights
						//showWindow = true;
						string[] assemblyType = selectedGroup[j].Split('_');
						Component[] renderers;
						GameObject[] assemblies = GameObject.FindGameObjectsWithTag(assemblyType[0]);
						
						for (int index = 0; index < assemblies.Length; index++)
						{
							if (assemblies[index].name.Equals(selectedGroup[j]))
							{
								renderers = assemblies[index].GetComponentsInChildren(typeof(Renderer));
								foreach (Renderer renderer in renderers)
								{
									for(int k = 0; k< renderer.materials.Length; k++)
									{
										if (Sequencing.firstGroupSelected != true)
										{
											startColor1 = renderer.materials[k].color;
											startTexture1 = renderer.material.mainTexture;
										}
										else
										{
											startColor2 = renderer.materials[k].color;
											startTexture2 = renderer.material.mainTexture;
										}
										renderer.material.mainTexture = highlight;
										renderer.materials[k].color = Color.yellow;
									}
								}							
							}
							
						}
					}
					
					if (Sequencing.firstGroupSelected != true)
					{
						Sequencing.Group1ButtonClicked();
					}

					else if(Sequencing.firstGroupSelected == true && (selectedGroups[0] != selectedGroups[1]))
					{
						Sequencing.Group2ButtonClicked();
					}

					UICheckbox groupCheckbox = NGUITools.FindInParents<UICheckbox>(GameObject.Find("Checkbox_" + crrSelectedAssemblyGroup));
					if (groupCheckbox != null) groupCheckbox.isChecked = true;
					

					return;
					
					//do not know what the codes below do
					if(Pavillion3.groupChoice[0] == null)
					{
						Pavillion3.groupChoice[0] = selectedGroup[1];
					}else if(Pavillion3.groupChoice[1] == null && selectedGroup[1] != Pavillion3.groupChoice[0])
					{
						Pavillion3.groupChoice[1] = selectedGroup[1];
						presentPopup();
					}
					
					
					// This sets up the arrays to be used in the info window
					FileReader.SelectObject(this.name);
					FileReader.getActivityList(FileReader.objectElements[2]);
					
					// If the item is not selected it is turned to its orignal color
				}else{
					return;
					string [] selGroup = FileReader.getGroup(this.name);
					if(!Pavillion3.sequence.Contains(selGroup[1]))
					{
						for(int j = 3; j < selGroup.Length - 1; j++)
						{
							Component[] renderers;
							renderers = this.GetComponentsInChildren(typeof(Renderer));
							foreach (Renderer renderer in renderers)
							{
								for(int i = 0; i< renderer.materials.Length; i++)
								{
									//renderer.material.mainTexture = startTexture;
									//renderer.materials[i].color = startColor;
								}
							}
							//showWindow = false;
						}
					}else{
						for(int j = 3; j < selGroup.Length - 1; j++)
						{
							Component[] renderers;
							renderers = this.GetComponentsInChildren(typeof(Renderer));
							foreach (Renderer renderer in renderers)
							{
								for(int i = 0; i< renderer.materials.Length; i++)
								{
									renderer.materials[i].color = Color.blue;
								}
							}
							//showWindow = false;
						}
						
					}
					
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		// Recieve left click
	
		/*if(Input.GetMouseButtonDown(0))
		{
			Sequencing.Group1ButtonClicked();
			if(Sequencing.firstGroupSelected == true && (getCrrSelectedAssemblyGroup()!=getCrrSelectedAssemblyGroup()))
			{
				Sequencing.Group2ButtonClicked();
			}
			// This detects what object is being clicked as long as it has a collider
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit))
			{
				if(hit.collider.gameObject == this.collider.gameObject)
				{
					string[] selectedGroup = FileReader.getGroup(this.name);

					crrSelectedAssemblyGroup = selectedGroup[1];
					storeOldAssemblyGroup(crrSelectedAssemblyGroup);

						for(int j = 3; j < selectedGroup.Length - 1; j++)
						{
							// This turns all of the renderer colors into another to show highlights
							//showWindow = true;
							string[] assemblyType = selectedGroup[j].Split('_');
							Component[] renderers;
							GameObject[] assemblies = GameObject.FindGameObjectsWithTag(assemblyType[0]);
							
							for (int index = 0; index < assemblies.Length; index++)
							{
								if (assemblies[index].name.Equals(selectedGroup[j]))
								{
									renderers = assemblies[index].GetComponentsInChildren(typeof(Renderer));
									foreach (Renderer renderer in renderers)
									{
										for(int k = 0; k< renderer.materials.Length; k++)
										{
											startColor = renderer.materials[k].color;
											startTexture = renderer.material.mainTexture;
											renderer.material.mainTexture = highlight;
											renderer.materials[k].color = Color.yellow;
										}
									}							
								}

							}
						}


					UICheckbox groupCheckbox = NGUITools.FindInParents<UICheckbox>(GameObject.Find("Checkbox_" + crrSelectedAssemblyGroup));
					if (groupCheckbox != null) groupCheckbox.isChecked = true;

					Sequencing.Group1ButtonClicked();
					if(Sequencing.firstGroupSelected == true)
					{
						Sequencing.Group2ButtonClicked();
					}
					return;

					//do not know what the codes below do
					if(Pavillion3.groupChoice[0] == null)
					{
						Pavillion3.groupChoice[0] = selectedGroup[1];
					}else if(Pavillion3.groupChoice[1] == null && selectedGroup[1] != Pavillion3.groupChoice[0])
					{
						Pavillion3.groupChoice[1] = selectedGroup[1];
						presentPopup();
					}
					
					
					// This sets up the arrays to be used in the info window
					FileReader.SelectObject(this.name);
					FileReader.getActivityList(FileReader.objectElements[2]);

				// If the item is not selected it is turned to its orignal color
				}else{
					return;
					string [] selGroup = FileReader.getGroup(this.name);
					if(!Pavillion3.sequence.Contains(selGroup[1]))
					{
						for(int j = 3; j < selGroup.Length - 1; j++)
						{
							Component[] renderers;
							renderers = this.GetComponentsInChildren(typeof(Renderer));
							foreach (Renderer renderer in renderers)
							{
								for(int i = 0; i< renderer.materials.Length; i++)
								{
									renderer.material.mainTexture = startTexture;
									renderer.materials[i].color = startColor;
								}
							}
							//showWindow = false;
						}
					}else{
						for(int j = 3; j < selGroup.Length - 1; j++)
						{
							Component[] renderers;
							renderers = this.GetComponentsInChildren(typeof(Renderer));
							foreach (Renderer renderer in renderers)
							{
								for(int i = 0; i< renderer.materials.Length; i++)
								{
									renderer.materials[i].color = Color.blue;
								}
							}
							//showWindow = false;
						}

					}

				}
			}
		
		

		
		}*/

		/*if(Input.GetKeyDown(KeyCode.Escape))
		{
			string [] selGroup = FileReader.getGroup(this.name);
				for(int j = 3; j < selGroup.Length - 1; j++)
				{
					Component[] renderers;
					renderers = this.GetComponentsInChildren(typeof(Renderer));
					foreach (Renderer renderer in renderers)
					{
						for(int i = 0; i< renderer.materials.Length; i++)
						{
							renderer.material.mainTexture = startTexture;
							renderer.materials[i].color = startColor;
						}
					}
					//showWindow = false;
				}
		}*/
	}



	//This puts the ObjectInteraction script on every object in the model
	// by matching it with the 3D_Objects table
	public static void AddInteractions()
	{
		GameObject modelObject;
		modelObject = GameObject.FindGameObjectWithTag("Model");
		
		Component[] models;
		models = modelObject.GetComponentsInChildren(typeof(Renderer));
		
		// Retrieve 3D_Objects table
		var reader = new StreamReader(Application.dataPath + "/" + Constants.ObjectFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		string[] allBuildingAssemblies = fileContents.Split("\n"[0]);
		
		foreach (Renderer renderer in models)
		{
			for (int i = 0; i < allBuildingAssemblies.Length - 1; i++)
			{
				string[] currentAssembly = allBuildingAssemblies[i].Split(',');
				
				if(currentAssembly[1] == renderer.gameObject.name)
				{
					renderer.gameObject.AddComponent("SequencingInteraction");
					
				}
			}
			
		}
		
	}

	void OnGUI ()
	{
		if(showErrorMessage == true)
		{
			GUI.depth = 1;
			GUI.Window(0,(new Rect(Screen.width/2 - 120,Screen.height/2 + 100, 300,100)),
				displayGroupingErrorMessage,
				"Error!");
			
			GUI.depth = 0;
			if(GUI.Button (new Rect(Screen.width/2, Screen.height/2 + 160, 50,30), "OK"))
			{
				showErrorMessage = false;	
			}

		}
		
	}
	
	// Controls what is put in the infoWindow
	void displayGroupingErrorMessage(int id)
	{
		GUI.Label (new Rect(30, 30, 250,90), errorMessage);
	}

	public void presentPopup()
	{
		/*Component [] tempGroups = GameObject.Find("Choose").GetComponentsInChildren<UICheckbox>();
		for(int i = 0; i< tempGroups.Length; i++)
		{
			Destroy(tempGroups[i].gameObject);
		}*/
		
		
		GameObject temp = NGUITools.AddChild(GameObject.Find("EmptyPanel"),(GameObject)(Resources.Load("PopupMenu - Copy")));
		GameObject.Find("Title").GetComponent<UILabel>().text = Pavillion3.groupChoice[0] + " to " + Pavillion3.groupChoice[1];
		FileReader.getActivityList(FileReader.getGroup(Pavillion3.groupChoice[0])[0]);
		string [] activities = FileReader.groupedActivity.Split("\n"[0]);
		for(int i = 0; i < activities.Length -1; i++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Group1Activity"),(GameObject)(Resources.Load("ActivityButton")));
			temp.GetComponentInChildren<UILabel>().text = activities[i];
			temp.transform.localScale = new Vector3(0.9f, 0.75f, 0.9f);
			temp.GetComponentInChildren<UIButtonMessage>().target = GameObject.Find("PopupMenu - Copy(Clone)");
			temp.GetComponentInChildren<UIButtonMessage>().functionName = "OnFirstGroup";
		}
		
		FileReader.getActivityList(FileReader.getGroup(Pavillion3.groupChoice[1])[0]);
		activities = FileReader.groupedActivity.Split("\n"[0]);
		for(int i = 0; i < activities.Length -1; i++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Group2Activity"),(GameObject)(Resources.Load("ActivityButton")));
			temp.GetComponentInChildren<UILabel>().text = activities[i];
			temp.transform.localScale = new Vector3(0.9f, 0.75f, 0.9f);
			temp.GetComponentInChildren<UIButtonMessage>().target = GameObject.Find("PopupMenu - Copy(Clone)");
			temp.GetComponentInChildren<UIButtonMessage>().functionName = "OnSecondGroup";
		}
		/*
		GameObject temp = NGUITools.AddChild(GameObject.Find("Choose"),(GameObject)(Resources.Load("Checkbox"))); 
		temp.GetComponentInChildren<UILabel>().text = Pavillion3.groupChoice[0];
		GameObject temp2 = NGUITools.AddChild(GameObject.Find("Choose"),(GameObject)(Resources.Load("Checkbox - Copy"))); 
		temp2.GetComponentInChildren<UILabel>().text = Pavillion3.groupChoice[1];
		
		GameObject.Find("Choose").GetComponent<UITable>().Reposition();
		*/
	}


}
