using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.IO;

// This controls the interactions of every object that is interactive in the model

public class ObjectInteraction : MonoBehaviour {
	//
	public static int count = 0; // count the number of objects of the same assembly
	// yifan
	
	bool showWindow;
	private String fullpath;
	
	private Color startColor;
	private Texture startTexture;
	private Texture highlight = new Texture2D(128,128);
	private Ray ray;
	private RaycastHit hit;
	private bool selected = false;
	private GameObject obj;
	
	//
	private GameObject[] modelGroup;
	//yifan
	
//	private bool popupMenu = false;
	
	// Use this for initialization
	void Start () {
		obj = this.gameObject;
		startColor = renderer.material.color;
		startTexture = renderer.material.mainTexture;
		//
		modelGroup = new GameObject[50];
		//yifan
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!Grouping.grouping)
		{
			// Receive left click
			if(Input.GetMouseButtonDown(0))
			{
				// This detects what object is being clicked as long as it has a collider
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(UICamera.hoveredObject == null){
					
					if(Physics.Raycast(ray,out hit))
					{
						/*	
						if(count == 0){
							modelGroup[count]= hit;
							count++;
						}
						else{
							if(modelGroup[count - 1 ].tag == hit.tag){
								modelGroup[count] = hit;
								count ++;
							}
						}
						*/
						
						if(hit.collider.gameObject == this.collider.gameObject)
						{
							// This turns all of the renderer colors into another to show highlights
							showWindow = true;
		                    Component[] renderers;
		                    renderers = this.GetComponentsInChildren(typeof(Renderer));
		                    foreach (Renderer renderer in renderers)
		                    {
		                        for(int i = 0; i< renderer.materials.Length; i++)
								{
									renderer.material.mainTexture = highlight;
									renderer.materials[i].color = Color.yellow;
								}
		                    }
							
					
							// This sets up the arrays to be used in the info window
							FileReader.SelectObject(this.name);
							FileReader.getActivityList(FileReader.objectElements[2]);
				
							
						// If the item is not selected it is turned to its orignal color
						}else{
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

		}else{
			// Receive left click
			if(Input.GetMouseButtonDown(0))
			{
				// This detects what object is being clicked as long as it has a collider
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(Physics.Raycast(ray,out hit))
				{
					if(hit.collider.gameObject == this.collider.gameObject)
					{
						if(!selected)
						{
							// This turns all of the renderer colors into another to show highlights
	                    	Component[] renderers;
	                    	renderers = this.GetComponentsInChildren(typeof(Renderer));
	                    	foreach (Renderer renderer in renderers)
	                    	{
	                        	for(int i = 0; i< renderer.materials.Length; i++)
								{
									renderer.material.mainTexture = highlight;
									renderer.materials[i].color = Color.yellow;
								}
	                    	}
	                    	selected = true;
						}else{
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
	                    	selected = false;
						}
					}
				}
			}
		}
	}
	
	// Handles the info window
	void OnGUI ()
	{
		if(showWindow == true)
		{
			GUI.Window(0,(new Rect(Screen.width - 180,Screen.height/2 - 100, 175,200)),infoWindowFunc,this.name);

		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			showWindow = false;
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
		}
	}
	
	// Controls what is put in the infoWindow
	void infoWindowFunc(int id)
	{
  		String name = FileReader.objectElements[1].ToString();
        String type = FileReader.objectElements[2].ToString();
        String objectMaterial = FileReader.objectElements[4].ToString();
        String size = FileReader.objectElements[5].ToString();
		
		GUI.Label(new Rect(10,20,500,100),("Type: " + type));
		GUI.Label(new Rect(10,40,500,100),("Material: " + objectMaterial));
		GUI.Label(new Rect(10,60,500,100),("Size: " + size));
		
		GUI.Label(new Rect(70,85,500,100),"Activities");
		GUI.Label (new Rect(10,100,500,120),FileReader.groupedActivity);
	}
	

    
	// This makes the object its original color that it started out with
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
            if(isBuildingElement(renderer.gameObject.name))
            {
                renderer.gameObject.AddComponent("ObjectInteraction");
               
           }
       	}
    }

	private static bool isBuildingElement(string objName)
	{
		string[] objects;

		var reader = new StreamReader(Application.dataPath + "/" + Constants.ObjectFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();

		objects = fileContents.Split("\n"[0]);
		for (int i = 0; i < (objects.Length-1); i++)
		{
			string[] objectElements = objects[i].Split(","[0]);
			if (objectElements[1] == objName)
			{
				return true;
			}
		}

		return false;
	}
}
