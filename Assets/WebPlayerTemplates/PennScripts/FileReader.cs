using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class FileReader : MonoBehaviour {
	// Global variables
	public static string[] objects;
	public static string[] objectElements;
	public static string[] activities;
	public static string[] undoGroup;
	public static string groupedActivity;
	public static string [] method1;
	public static string [] method2;
	
	// Private variables
	private static string[] activityList;
	private static string[] rewriter;
	private static string [] singleMethod;
	private static int index = 0;
	
	// File names
	string fileName = "3D_Objects.csv";
	string activityFile = "Activities.txt";
	static string methodFile = "Methods.txt";
	//static string groupFile = "Groups.txt";
	static string sequenceFile = "Sequence.txt";
	static string constraintFile = "Constraints.txt";
	static bool first = true;

	enum assemblies : int {Beam, Column, Eaves, Footing, Sheathing, Shingles, Slab, Truss};
	enum footingActivities: int {Excavate, Form, Reinforce, PlaceConcrete, StripForms, Cure};
	enum slabActivities: int {Excavate, Form, Reinforce, PlaceConcrete, StripForms, CureAndFinish};
	
	
	// Use this for initialization
	void Start () {
		// Creates a new file to store groups created by user
		//File.Create(Application.dataPath + "/" + groupFile);
		//File.Create(Application.dataPath + "/" + sequenceFile);
		
		// Retrieve 3D_Objects table
		var reader = new StreamReader(Application.dataPath + "/" + fileName);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		// Retrieve Activities table
		var reader1 = new StreamReader(Application.dataPath + "/" + activityFile);
		var fileContents1 = reader1.ReadToEnd();
		reader1.Close();
		
		// Load tables into arrays
		objects = fileContents.Split("\n"[0]);
		activities = fileContents1.Split("\n"[0]);
		
		// Call other classes to begin application
		PlayerControls.UseExistingOrCreateNewMainCamera();
        //ObjectInteraction.AddInteractions();
	
	}


	// This method breaks the objects array into a specific object to be used in the info window
	public static string getElementName(string objName)
	{
        for (int i = 0; i < (objects.Length-1); i++)
        {
            objectElements = objects[i].Split(","[0]);
            if (objectElements[1] == objName)
            {
				return objectElements[1];
                //break;
            }
        }

		return null;

	}

	public static void SelectObject(string objName)
	{
		for (int i = 0; i < (objects.Length-1); i++)
		{
			objectElements = objects[i].Split(","[0]);
			if (objectElements[1] == objName)
			{
				print (objName);
				break;
			}
		}
		
	}

	// This method gathers all of the activities that are associated with an object
	public static void getActivityList(string type)
	{
		groupedActivity = null;
		for(int i = 0; i < (activities.Length-1); i++)
		{
			activityList = activities[i].Split(","[0]);
			if(activityList[1] == type)
			{
				groupedActivity += activityList[2] + "\n";
			}
		}
		
	}
	
	public static string getActivityListbyType(string type)
	{
		groupedActivity = null;
		for(int i = 0; i < (activities.Length-1); i++)
		{
			activityList = activities[i].Split(","[0]);
			if(activityList[1] == type)
			{
				groupedActivity += activityList[2] + "\n";
			}
		}
		return groupedActivity;
	}
	
	// Method writes the groups created by the user to a new groups file
	public static void writeGroup(string groups)
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.GroupFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();

		string [] temp = fileContents.Split("\n"[0]);
		string [] temp2;
		int count = 1;
		// Adds a new line to the file
		if(groups == "/")
		{
			File.AppendAllText(Application.dataPath + "/" + Constants.GroupFile, Environment.NewLine);
			first = true;
		}else{
			// This will check to see if it is a new line so it can name the group
			if(first)
			{
				for(int i = 0; i<temp.Length; i++)
				{
					temp2 = temp[i].Split(","[0]);
					if(temp2[0] == groups)
					{
						count++;
					}
				}
				File.AppendAllText(Application.dataPath + "/" + Constants.GroupFile, groups + ",");
				File.AppendAllText(Application.dataPath + "/" + Constants.GroupFile, groups + "_Group" + count + ",");
				first = false;
			}
			File.AppendAllText(Application.dataPath + "/" + Constants.GroupFile, groups + ",");
		}
	}
	
	public static void writeActivityMethod(string [,] methods)
	{
		string crrAssembly = "";
		string crrActivity = "";
		string crrMethod = "";
		StreamWriter writer = File.CreateText (Application.dataPath + "/" + Constants.SelectedActivityMethodFile);
		
		for (int assemblyIndex = 0; assemblyIndex < 8; assemblyIndex++)
		{
			for (int activityIndex = 0; activityIndex < 6; activityIndex++)
			{
				if (methods[assemblyIndex, activityIndex] != null)
				{
					crrMethod = methods[assemblyIndex, activityIndex];
					switch (assemblyIndex)
					{
						case (int) assemblies.Beam: 	crrAssembly = "Beam"; 	crrActivity = "Install"; break;
						case (int) assemblies.Column: 	crrAssembly = "Column"; crrActivity = "Install"; break;
						case (int) assemblies.Eaves: 	crrAssembly = "Eaves"; 	crrActivity = "Install"; break;
						case (int) assemblies.Footing:
							crrAssembly = "Footing";
							switch (activityIndex)
							{
								case (int) footingActivities.Excavate: 		crrActivity = "Excavate"; 		break;
								case (int) footingActivities.Form: 			crrActivity = "Form"; 			break;
								case (int) footingActivities.Reinforce: 	crrActivity = "Reinforce"; 		break;
								case (int) footingActivities.PlaceConcrete: crrActivity = "Place concrete"; break;
								case (int) footingActivities.StripForms: 	crrActivity = "Strip forms"; 	break;
								case (int) footingActivities.Cure: 			crrActivity = "Cure"; 			break;
								default: break;
							}
							break;
						case (int) assemblies.Sheathing: crrAssembly = "Sheathing"; crrActivity = "Install"; break;
						case (int) assemblies.Shingles: crrAssembly = "Shingles"; crrActivity = "Install"; break;
						case (int) assemblies.Slab:
							crrAssembly = "Slab";
							switch (activityIndex)
							{
								case (int) slabActivities.Excavate: crrActivity = "Excavate"; break;
								case (int) slabActivities.Form: crrActivity = "Form"; break;
								case (int) slabActivities.Reinforce: crrActivity = "Reinforce"; break;
								case (int) slabActivities.PlaceConcrete: crrActivity = "Place concrete"; break;
								case (int) slabActivities.StripForms: crrActivity = "Strip forms"; break;
								case (int) slabActivities.CureAndFinish: crrActivity = "Cure and finish"; break;
								default: break;
							}					
							break;
						case (int) assemblies.Truss: crrAssembly = "Truss"; crrActivity = "Install"; break;
						default: break;
					}
					string newline = crrAssembly + "," + crrActivity + "," + crrMethod + ",";
					writer.WriteLine (newline);
				}		
			}
		}
		
		writer.Close();
	}
	
	// This will find the last group added to the group file - main use is to create a new checkbox
	public static string lastGroup()
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.GroupFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		string [] temp = fileContents.Split("\n"[0]);
		string [] temp2 = temp[temp.Length - 2].Split(","[0]);
		
		return temp2[1];
		
	}
	
	// Uses a given element to find the entire group it is related to
	public static string [] getGroup(string element)
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.GroupFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		string [] temp = fileContents.Split("\n"[0]);
		string [] temp2;
		
		for(int i = 0; i< temp.Length; i++)
		{
			temp2 = temp[i].Split(","[0]);
			for(int j = 0; j< temp2.Length; j++)
			{
				if(temp2[j].Equals(element))
				{
					return temp2;
					break;
				}
			}
		}
		return null;
	}
	
	public static string [] getGroupElements (string groupName)
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.GroupFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		string [] temp = fileContents.Split("\n"[0]);
		string [] temp2;
		
		for(int i = 0; i< temp.Length; i++)
		{
			string currentLine = temp[i].Trim();
			temp2 = currentLine.Split(","[0]);
			if(temp2[1].Equals(groupName))
			{
				return temp2;
				break;
			}
		}
		return null;
	}


	// Uses the type of element to find groups of the same type of element
	public static object [] getRelatedGroup(string gpElement)
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.GroupFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		string [] temp = fileContents.Split("\n"[0]);
		string [] temp2;
		ArrayList retGroup = new ArrayList();
		
		for(int i = 0; i< temp.Length; i++)
		{
			temp2 = temp[i].Split(","[0]);
			if(temp2[0].Equals(gpElement))
			{
				retGroup.Add(temp2[1]);
			}
		}
		
		return retGroup.ToArray();
	}
	
	// This removes a group from the group file - it is called when the ungroup button is clicked
	// First it finds the information and saves it to memory and overwrites the whole file,
	// but leaves out the group that is being deleted
	public static void unGroup(string groups)
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.GroupFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		string [] temp = fileContents.Split("\n"[0]);
		string [] temp2;
		first = true;

		for(int i = 0; i<temp.Length - 1; i++)
		{
			temp2 = temp[i].Split(","[0]);
			
			if(temp.Length <=2)
			{
				File.Create(Application.dataPath + "/" + Constants.GroupFile);
			}
			
			if(temp2[1] != groups)
			{
				if(first)
				{
					File.WriteAllText(Application.dataPath + "/" + Constants.GroupFile, temp[i]);
					File.AppendAllText(Application.dataPath + "/" + Constants.GroupFile, Environment.NewLine);
					first = false;
				}else{
					File.AppendAllText(Application.dataPath + "/" + Constants.GroupFile, temp[i]);
					File.AppendAllText(Application.dataPath + "/" + Constants.GroupFile, Environment.NewLine);
				}
			}
		}
		
	}
	
	// Retrieves the methods associated to the activity
	public static void getMethods(string activity)
	{
		var reader = new StreamReader(Application.dataPath + "/" + methodFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		string [] temp = fileContents.Split("\n"[0]);
		string [] temp2;
		
		for(int i = 0; i < (temp.Length-1); i++)
		{
			temp2 = temp[i].Split(","[0]);
			if(temp2[2] == activity)
			{
				if(first)
				{
					method1 = temp2;
					first = false;
				}else{
					method2 = temp2;
					first = true;
				}
			}
		}
		
		if(!first)
		{
			method2 = method1;
			first = true;
		}
	}
	
	// This creates the file that will be used for sequencing by combining groups and methods
	public static void createSequenceFile(string method, string title)
	{
		// Groups
		var reader = new StreamReader(Application.dataPath + "/" + Constants.GroupFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		// Methods
		var reader1 = new StreamReader(Application.dataPath + "/" + methodFile);
		var fileContents1 = reader1.ReadToEnd();
		reader1.Close();
		
		string [] groupArr = fileContents.Split("\n"[0]);
		string [] methodsArr = fileContents1.Split("\n"[0]);
		string [] singleGroup;
		
		for(int i = 0; i< (methodsArr.Length-1); i++)
		{
			// Matches the method to the method file
			singleMethod = methodsArr[i].Split(","[0]);
			if(singleMethod[3] == method)
			{
				// second check to make sure it is the correct object
				if(singleMethod[2] == title)
				{
					for(int j = 0; j< (groupArr.Length-1); j++)
					{
						// Makes sure the method pertains to that specific group
						singleGroup = groupArr[j].Split(","[0]);
						if(singleGroup[0] == singleMethod[1])
						{
							File.AppendAllText(Application.dataPath + "/" + sequenceFile, index+","+singleGroup[1]+": "+method + "," + Environment.NewLine);
							index++;
						}
					}
				}
			}
		}
		
	}
	
	// Work in progress
	// This is meant to return every constraint related to one object
	public static ArrayList checkConstraint(string constraint)
	{
		var reader = new StreamReader(Application.dataPath + "/" + constraintFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		string [] temp = fileContents.Split("\n"[0]);
		string [] temp2;
		ArrayList retConst = new ArrayList();

		
		for(int i = 0; i < (temp.Length-1); i++)
		{
			temp2 = temp[i].Split(","[0]);
			if((temp2[1]/* + "/" + temp2[2]*/) == constraint)
			{
				retConst.Add(temp[i]/*(temp2[3] + "/" + temp2[4])*/);
			}
		}
		return retConst;
	}
	
	// Returns index of sequence file
	public static int getIndex()
	{
		return index;
	}
	
	public static string [] getSequence(int num)
	{
		var reader = new StreamReader(Application.dataPath + "/" + sequenceFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		string [] temp = fileContents.Split("\n"[0]);
		string [] temp2 = temp[num].Split(","[0]);
		
		return temp2;
	}
}
