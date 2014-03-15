using UnityEngine;
using System.Collections;

// Work in Progress

public class SimulationOld: MonoBehaviour {
	
	public Shader transparency;
	
	private GameObject model;
	private GameObject modelParts;
	float trans;
	Material [] mats;
	// Use this for initialization
	void Start () {
		SimulationInteraction.AddInteractions();
/*		model = GameObject.Find("Pavillion");
		mats = model.GetComponentsInChildren<Material>();
		modelParts = model.GetComponentsInChildren<GameObject>();
		for(int i = 0; i < mats.Length; i++)
		{
			modelParts[i].gameObject.renderer.material.shader = transparency;
		}
	//	model.transform.renderer.material.color.a = 0;
		trans = 0;
*/	}
	
	// Update is called once per frame
	void Update () {
	
//		for(int i = 0; i< modelParts.Length; i++)
//		{
//			modelParts[i].gameObject.renderer.material.color.a += (.01 * Time.deltaTime);
//		}
//		trans += (float)(.01 * Time.deltaTime);
	//	model.transform.renderer.material.color.a = trans;
	}
}
