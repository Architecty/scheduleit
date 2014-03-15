using UnityEngine;
using System.Collections;


// Work in Progress
public class ChosenMethod : MonoBehaviour {

	Component [] checks;
	
	private int clickedCount;
	
	public GameObject button;
	public GameObject label;
	public UIFont font;
	void Start()
	{
		Component[] panels;
        panels = GetComponentsInChildren(typeof(UIPanel),true);
		foreach (UIPanel panel in panels)
        {
			UILabel [] labels = panel.GetComponentsInChildren<UILabel>();
			FileReader.getMethods(labels[6].text);
			UITable [] temp = panel.GetComponentsInChildren<UITable>();

		//	temp[0].gameObject.AddComponent<UILabel>();
			UILabel labeler = NGUITools.AddChild<UILabel>(temp[0].gameObject);
			
			labeler.GetComponent<UILabel>().font = font;
			labeler.GetComponent<UILabel>().text = FileReader.method1[5];
		/*	temp[0].gameObject.AddComponent<UILabel>().text = FileReader.method1[6];
			temp[0].gameObject.AddComponent<UILabel>().text = FileReader.method1[7];
			temp[0].gameObject.AddComponent<UILabel>().text = FileReader.method1[8];
			
			temp[1].gameObject.AddComponent<UILabel>().text = FileReader.method1[5];
			temp[1].gameObject.AddComponent<UILabel>().text = FileReader.method1[6];
			temp[1].gameObject.AddComponent<UILabel>().text = FileReader.method1[7];
			temp[1].gameObject.AddComponent<UILabel>().text = FileReader.method1[8];
			*/
        }
	//	NGUITools.SetActiveChildren(GameObject.Find("Anchor"), true);
	}
	
	void NextMethod()
	{
		clickedCount = 0;
		checks = GameObject.Find("Anchor").GetComponentsInChildren(typeof(UICheckbox), true);
		for(int i = 0; i < checks.Length; i++)
		{
			UICheckbox box = (UICheckbox)checks[i];
			if(box.isChecked)
			{
				clickedCount++;
			}
		}
		if(clickedCount == (checks.Length/2))
		{
			NGUITools.SetActive(button,true);
		}
		
	}
	
	// Continue is selected
	void ContinueClicked()
	{
		// This activates each panel and checks which method is chosen
		// For each chosen method, it is sent to add to the sequence file 
		// Then loads the resources scene
		NGUITools.SetActiveChildren(GameObject.Find("Anchor"),true);
		checks = GameObject.Find("Anchor").GetComponentsInChildren(typeof(UICheckbox), true);
		for(int i = 0; i < checks.Length; i++)
		{
			UICheckbox box = (UICheckbox)checks[i];
			if(box.isChecked)
			{
				UIPanel temp = NGUITools.FindInParents<UIPanel>(box.gameObject);
				UILabel [] tempLabel = temp.GetComponentsInChildren<UILabel>();

				FileReader.createSequenceFile(box.GetComponentInChildren<UILabel>().text, tempLabel[6].text);
			}
		}
		
		Application.LoadLevel("Resource");
	}
}
