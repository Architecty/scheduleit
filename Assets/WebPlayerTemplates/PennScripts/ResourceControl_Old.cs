using UnityEngine;
using System.Collections;

public class ResourceControl : MonoBehaviour {
	
	// Starts next level
	// Can be modified to collect resource information
	void OnClick()
	{
		Application.LoadLevel("Pavilion3");
	}
}
