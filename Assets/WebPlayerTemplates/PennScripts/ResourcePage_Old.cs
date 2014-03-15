using UnityEngine;
using System.Collections;

// This handles the dynamically adding of objects to the resources page
// It finds the groups and methods selected and presents them for the user to assign resources
public class ResourcePage : MonoBehaviour {

	public UIFont font;
	// Use this for initialization
	void Start () {

		string [] sequence;
		
		for(int i = 0; i < FileReader.getIndex(); i++)
		{
			sequence = FileReader.getSequence(i);
			var temp = NGUITools.AddChild(GameObject.Find("Index"),(GameObject)(Resources.Load("Label")));
			temp.GetComponent<UILabel>().text = sequence[0];
			temp.transform.localScale = new Vector3(28,28,1);
			temp = NGUITools.AddChild(GameObject.Find("Activity"),(GameObject)(Resources.Load("Label")));
			temp.GetComponent<UILabel>().text = sequence[1];
			temp.transform.localScale = new Vector3(28,28,1);
		//	temp = NGUITools.AddChild(GameObject.Find("Hours"),(GameObject)(Resources.Load("Label")));
		//	temp.GetComponent<UILabel>().text = sequence[2];
			temp = NGUITools.AddChild(GameObject.Find("Resource"),(GameObject)(Resources.Load("Input")));
			temp.transform.localScale = new Vector3(0.175f,1,1);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
