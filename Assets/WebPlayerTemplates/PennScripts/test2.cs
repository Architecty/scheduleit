using UnityEngine;
using System.Collections;

public class test2 : MonoBehaviour {

	// Public variables
	public GameObject prefab;
	public GameObject groupPrefab;
	public GameObject button;
	
	// Private variables
	private GameObject[] model;
	private GameObject[] modelGroup;
	private string[] tempTags;

	
	void Start()
	{
		// This adds all of the checkboxes to the main menu
		GameObject temp;
		UITable panel = NGUITools.FindInParents<UITable>(GameObject.Find("Footing"));
		
		model = GameObject.FindGameObjectsWithTag("Footing");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table1"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
		model = GameObject.FindGameObjectsWithTag("Slab");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table2"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
		model = GameObject.FindGameObjectsWithTag("Column");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table3"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
		model = GameObject.FindGameObjectsWithTag("Beam");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table4"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
		model = GameObject.FindGameObjectsWithTag("Shingles");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table5"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
		model = GameObject.FindGameObjectsWithTag("Truss");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table6"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
		model = GameObject.FindGameObjectsWithTag("Sheathing");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table7"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
		model = GameObject.FindGameObjectsWithTag("Eaves");
		for(int j = 0; j < model.Length; j++)
		{
			temp = NGUITools.AddChild(GameObject.Find("Table8"),prefab);
			temp.GetComponent<UICheckboxControlledObject>().target = model[j];
			temp.GetComponentInChildren<UILabel>().text = model[j].name;
			temp.name="Checkbox_" + model[j].name;
		}
		
		// This resets the menus and positions them properly then closes each menu to start the scene
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
	
	// Handles the Footing buttons
	void OnFootingClicked()
	{
	
		
		model = GameObject.FindGameObjectsWithTag("Footing");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
	}
	
	void IsolateFooting()
	{
		model = GameObject.FindGameObjectsWithTag("Model");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer),true);
			foreach (Renderer renderer in renderers)
	        {
				if(renderer.tag != "Footing")
				{
					//make object visible
					if(renderer.gameObject.activeSelf)
					{
						renderer.gameObject.SetActive(false);
					}else{
						renderer.gameObject.SetActive(true);
					}

				}
	        }
		}
	}
	
	
	// Handles the slab buttons
	void OnSlabClicked()
	{
	
		model = GameObject.FindGameObjectsWithTag("Slab");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
	}
	
	void IsolateSlab()
	{
		model = GameObject.FindGameObjectsWithTag("Model");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer),true);
			foreach (Renderer renderer in renderers)
	        {
				if(renderer.tag != "Slab")
				{
					//make object visible
					if(renderer.gameObject.activeSelf)
					{
						renderer.gameObject.SetActive(false);
					}else{
						renderer.gameObject.SetActive(true);
					}

				}
	        }
		}
	}
	
	
	// Handles the columns buttons
	void OnColumnClicked()
	{
	
		model = GameObject.FindGameObjectsWithTag("Column");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
	}
	
	void IsolateColumn()
	{
		model = GameObject.FindGameObjectsWithTag("Model");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer),true);
			foreach (Renderer renderer in renderers)
	        {
				if(renderer.tag != "Column")
				{
					//make object visible
					if(renderer.gameObject.activeSelf)
					{
						renderer.gameObject.SetActive(false);
					}else{
						renderer.gameObject.SetActive(true);
					}

				}
	        }
		}
	}
	
	
	// Handles the beam buttons
	void OnBeamClicked()
	{
	
		model = GameObject.FindGameObjectsWithTag("Beam");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
	}
	
	void IsolateBeam()
	{
		model = GameObject.FindGameObjectsWithTag("Model");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer),true);
			foreach (Renderer renderer in renderers)
	        {
				if(renderer.tag != "Beam")
				{
					//make object visible
					if(renderer.gameObject.activeSelf)
					{
						renderer.gameObject.SetActive(false);
					}else{
						renderer.gameObject.SetActive(true);
					}

				}
	        }
		}
	}
	
	
	// Handles the Shingles buttons
	void OnShinglesClicked()
	{
	
		model = GameObject.FindGameObjectsWithTag("Shingles");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
	}
	
	void IsolateShingles()
	{
		model = GameObject.FindGameObjectsWithTag("Model");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer),true);
			foreach (Renderer renderer in renderers)
	        {
				if(renderer.tag != "Shingles")
				{
					//make object visible
					if(renderer.gameObject.activeSelf)
					{
						renderer.gameObject.SetActive(false);
					}else{
						renderer.gameObject.SetActive(true);
					}

				}
	        }
		}
	}
	
	
	// Handles the Truss buttons
	void OnTrussClicked()
	{
	
		model = GameObject.FindGameObjectsWithTag("Truss");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
	}
	
	void IsolateTruss()
	{
		model = GameObject.FindGameObjectsWithTag("Model");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer),true);
			foreach (Renderer renderer in renderers)
	        {
				if(renderer.tag != "Truss")
				{
					//make object visible
					if(renderer.gameObject.activeSelf)
					{
						renderer.gameObject.SetActive(false);
					}else{
						renderer.gameObject.SetActive(true);
					}

				}
	        }
		}
	}
	
	
	// Handles the Sheathing buttons
	void OnSheathingClicked()
	{
	
		model = GameObject.FindGameObjectsWithTag("Sheathing");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
	}
	
	void IsolateSheathing()
	{
		model = GameObject.FindGameObjectsWithTag("Model");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer),true);
			foreach (Renderer renderer in renderers)
	        {
				if(renderer.tag != "Sheathing")
				{
					//make object visible
					if(renderer.gameObject.activeSelf)
					{
						renderer.gameObject.SetActive(false);
					}else{
						renderer.gameObject.SetActive(true);
					}

				}
	        }
		}
	}
	
	
	// Handles the Eaves buttons
	void OnEavesClicked()
	{
	
		model = GameObject.FindGameObjectsWithTag("Eaves");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer));
	        foreach (Renderer renderer in renderers)
	        {
	        	for(int i = 0; i< renderers.Length; i++)
				{
					//make object visible
					if(model[j].renderer.enabled == true)
					{
						model[j].renderer.enabled = false;
						model[j].collider.enabled = false;
					}else{
						model[j].renderer.enabled = true;
						model[j].collider.enabled = true;
					}
				}
	        }
		}
	}

	void IsolateEaves()
	{
		model = GameObject.FindGameObjectsWithTag("Model");
		for(int j = 0; j < model.Length; j++)
		{
			Component[] renderers;
	        renderers = model[j].GetComponentsInChildren(typeof(Renderer),true);
			foreach (Renderer renderer in renderers)
	        {
				if(renderer.tag != "Eaves")
				{
					//make object visible
					if(renderer.gameObject.activeSelf)
					{
						renderer.gameObject.SetActive(false);
					}else{
						renderer.gameObject.SetActive(true);
					}

				}
	        }
		}
	}
	
	// Clears the group selection
	void OnClear()
	{
		Pavillion3.groupChoice = new string [2];
	}
	
	// Restarts Pavilion3
	void OnRestartClicked()
	{
		Pavillion3.sequence = new ArrayList();
		Pavillion3.predecessor = new ArrayList();
		Pavillion3.groupChoice = new string[2];
		Pavillion3.count = 1;
		Pavillion3.index = 1;
		Application.LoadLevel("Pavilion3");
	}
	
	// Handles Continue
	void OnContinueClicked()
	{
		Application.LoadLevel("Simulation");
	}
}
