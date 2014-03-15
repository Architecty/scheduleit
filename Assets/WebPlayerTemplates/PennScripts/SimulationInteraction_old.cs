using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

// Work in Progress

public class SimulationInteraction : MonoBehaviour {
	
	bool showWindow;
	private String fullpath;
	
	private Color startColor;
	private Shader startTexture;
	public Shader trans;
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
		startTexture = renderer.material.shader;
		renderer.material.shader = trans;
		startColor.a = 0f;
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
					startColor.a = (float)(.01 * Time.deltaTime);
					renderer.materials[i].color = startColor;
				}
            }
		}
		
	//	renderer.material.color.a += (.01 * Time.deltaTime);

/*		// Recieve left click
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
			
					presentPopup();
					// This sets up the arrays to be used in the info window
					FileReader.SelectObject(this.name);
					FileReader.getActivityList(FileReader.objectElements[2]);
		
					
				// If the item is not selected it is turned to its orignal color
				}else{
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
						showWindow = false;
					}

					
				}
			}
		}
	*/	
	}
	
	void OnGUI ()
	{
		if(showWindow == true)
		{
			GUI.Window(0,(new Rect(Screen.width - 210,Screen.height-210,200,200)),infoWindowFunc,selGroup[1]);
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
		Component [] tempGroups = GameObject.Find("SequenceTable").GetComponentsInChildren<UICheckbox>();
		for(int i = 0; i< tempGroups.Length; i++)
		{
			Destroy(tempGroups[i].gameObject);
		}
	//	GameObject.Find("PopupMenu").layer = GameObject.Find("Pavilion").layer;
		object [] relGroups = FileReader.getRelatedGroup(selGroup[0]);

		for(int i = 0; i < relGroups.Length; i++)
		{
			if(relGroups[i].ToString() != selGroup[1])
			{
				
				GameObject temp = NGUITools.AddChild(GameObject.Find("SequenceTable"),(GameObject)(Resources.Load("temp"))); 
				temp.GetComponentInChildren<UILabel>().text = relGroups[i].ToString();
					
			}
		}
		GameObject.Find("SequenceTable").GetComponent<UITable>().Reposition();
	}
    
/*	public void returnColor()
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
*/	
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
                renderer.gameObject.AddComponent("SimulationInteraction");
               
            }
            

        }

    }
}
