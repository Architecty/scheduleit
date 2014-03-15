using UnityEngine;
using System.Collections;

public class LoadingTime: MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(LoadingTimer());	
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	IEnumerator LoadingTimer() {

		for(int i = 0; i <= 100; i += 10)
		{
		   
			int displayInt = i;
		   GameObject.Find("Percent").GetComponentInChildren<UILabel>().text = displayInt.ToString() + " %";
		   yield return new WaitForSeconds(1);
		   
		}
		/*
		 Debug.Log("Before Waiting 2 seconds");
		 yield return new 
		 Debug.Log("After Waiting 2 Seconds");
		 */
	}
	
	
	
}
