using UnityEngine;
using System.Collections;

public class TestButton : MonoBehaviour {
	
	// Simply to test if we could open MS Project in Unity
	// Found out you can call to open the program and which file to open
	void OnClick()
	{
		print (Application.dataPath);
		System.Diagnostics.Process.Start("WINPROJ.exe", Application.dataPath + "/Sequence.txt");
		
	}
}