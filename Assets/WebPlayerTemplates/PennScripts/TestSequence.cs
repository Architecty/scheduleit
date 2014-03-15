using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class TestSequence : MonoBehaviour {
	bool showWindow;
	private string fullpath;
	
	private Color startColor;
	private Texture startTexture;
	private Texture highlight = new Texture2D(128,128);
	private Ray ray;
	private RaycastHit hit;
	private bool selected = false;
	private GameObject obj;
	private string [] selGroup;
//	private bool popupMenu = false;
	
	// Use this for initialization
	void Start () {
		obj = this.gameObject;
		startColor = renderer.material.color;
		startTexture = renderer.material.mainTexture;
		
	}
	
	// Update is called once per frame
	void Update () {
		

		// Recieve left click
		if(Input.GetMouseButtonDown(0))
		{
			// This detects what object is being clicked as long as it has a collider
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray,out hit))
			{
				if(hit.collider.gameObject == this.collider.gameObject)
				{
					selGroup = FileReader.getGroup(this.name);

					for(int j = 3; j < selGroup.Length - 1; j++)
					{
					
						// This turns all of the renderer colors into another to show highlights
						showWindow = true;
				        Component[] renderers;
	                    renderers = GameObject.Find(selGroup[j]).GetComponentsInChildren(typeof(Renderer));
	                    foreach (Renderer renderer in renderers)
	                    {
	                        for(int i = 0; i< renderer.materials.Length; i++)
							{
								renderer.material.mainTexture = highlight;
								renderer.materials[i].color = Color.yellow;
							}
	                    }
					}
			
					if(Pavillion3.groupChoice[0] == null)
					{
						Pavillion3.groupChoice[0] = selGroup[1];
					}else if(Pavillion3.groupChoice[1] == null && selGroup[1] != Pavillion3.groupChoice[0])
					{
						Pavillion3.groupChoice[1] = selGroup[1];
						presentPopup();
					}
				
					
					// This sets up the arrays to be used in the info window
					FileReader.SelectObject(this.name);
					FileReader.getActivityList(FileReader.objectElements[2]);
		
					
				// If the item is not selected it is turned to its orignal color
				}else{
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
							showWindow = false;
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
							showWindow = false;
						}
					}
					
				}
			}
		}
		
	}
	
	void OnGUI ()
	{
		if(showWindow == true)
		{
	//		GUI.Window(0,(new Rect(Screen.width - 210,Screen.height-210,200,200)),infoWindowFunc,selGroup[1]);
		}
		
	}
	
	// Controls what is put in the infoWindow
	void infoWindowFunc(int id)
	{
		object [] relGroups = FileReader.getRelatedGroup(selGroup[0]);
 		int count = 1;
		for(int i = 0; i < relGroups.Length; i++)
		{
			if(relGroups[i].ToString() != selGroup[1])
			{
				GUI.Label(new Rect(10,((count)*20),500,100),relGroups[i].ToString());
				count++;
			}
		}
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
    
	public void returnColor()
	{
		Component[] renderers;
        renderers = obj.GetComponentsInChildren(typeof(Renderer));
        foreach (Renderer renderer in renderers)
        {
        	for(int i = 0; i< renderer.materials.Length; i++)
			{
				renderer.material.mainTexture = startTexture;
				renderer.materials[i].color = startColor;
			}
        }
	}
	
	//This puts the ObjectInteraction script on every object in the model
	// by matching it with the 3D_Objects table
    public static void AddInteractions()
    {
        GameObject tempObject;

        tempObject = GameObject.FindGameObjectWithTag("Model");

        Component[] models;
        models = tempObject.GetComponentsInChildren(typeof(Renderer));
        foreach (Renderer renderer in models)
        {
            FileReader.SelectObject(renderer.gameObject.name);
            if(FileReader.objectElements[1].ToString() == renderer.gameObject.name)
            {
                renderer.gameObject.AddComponent("TestSequence");
               
            }
            

        }

    }
}