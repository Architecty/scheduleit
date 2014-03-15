using UnityEngine;
using System.Collections;

public class Sleep : MonoBehaviour {

	// Use this for initialization
	void Start () {

	
	}


	public static IEnumerator sleep (int seconds)
	{
		yield return new WaitForSeconds(seconds);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
