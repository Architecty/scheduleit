using UnityEngine;
using System.Collections;

public class Pavillion3 : MonoBehaviour {
	
	public static string [] groupChoice;
	public static ArrayList sequence;
	public static ArrayList predecessor;
	public static int index = 1;
	public static int count = 1;

	// Use this for initialization
	void Start () {
		sequence = new ArrayList();
		predecessor = new ArrayList();
		groupChoice = new string [2];
		TestSequence.AddInteractions();
		PlayerControls.UseExistingOrCreateNewMainCamera();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(groupChoice != null)
		{
			GameObject.Find("FirstGroup").GetComponent<UILabel>().text = "First Group: " + groupChoice[0];
		}
	}
}
