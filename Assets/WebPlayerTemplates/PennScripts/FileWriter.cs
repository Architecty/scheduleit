using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class FileWriter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void createNewTextFile(string fileName)
	{
		StreamWriter sw = File.CreateText(Application.dataPath + "/" + fileName);
		sw.Close();
	}

	public static void writeLine(string fileName, string newLine)
	{

	}

	public static void appendLine(string fileName, string newLine)
	{
		File.AppendAllText(Application.dataPath + "/" + fileName, newLine + "\n");
	}
}
