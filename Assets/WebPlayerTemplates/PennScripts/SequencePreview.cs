using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class SequencePreview : MonoBehaviour {
	
	private TreeNode rootNode;
	
	// Use this for initialization
	void Start () {
		rootNode = new TreeNode("root", null, "", 0);
	}
	
	public TreeNode RootNode
	{
		get { return rootNode; }
	}
	
	void readSequenceData()
	{
		var reader = new StreamReader(Application.dataPath + "/" + Constants.SequenceFile);
		var fileContents = reader.ReadToEnd();
		reader.Close();
		
		string[] sequenceLines = fileContents.Split("\n"[0]);
		
		for (int i = 0; i < sequenceLines.Length; i++)
		{
			if (sequenceLines[i] != null && sequenceLines[i] != "")
			{
				string[] crrLine = sequenceLines[i].Split(',');
				constructSequenceTree(crrLine[0].Replace(" ", string.Empty), crrLine[1].Replace(" ", string.Empty), crrLine[2].Replace(" ", string.Empty));
			}
		}
	}
	
	void constructSequenceTree(string predecessor, string successor, string relationship)
	{
		//print ("Predecessor: " + predecessor + ", Successor: " + successor + ", Relationship: " + relationship);
		//TreeNode predecessorNode = rootNode.doesAssemblyGroupExist(predecessor);
		//TreeNode successorNode = rootNode.doesAssemblyGroupExist(successor);

		TreeNode predecessorNode = rootNode.getAssemblyGroupWithName(predecessor);
		TreeNode successorNode = rootNode.getAssemblyGroupWithName(successor);

		if( predecessorNode != null && successorNode != null)
		{
			//two assembly groups already exist. no further actions needed
		}
		else if(predecessorNode != null && successorNode == null)
		{
			TreeNode newSuccessorNode = predecessorNode.addSuccessor(successor, relationship);
			print ("predecessor of " + newSuccessorNode.GroupName + " is " + predecessorNode.GroupName);
		}
		else if(predecessorNode == null && successorNode != null)
		{
			TreeNode newPredecessorNode = successorNode.addPredecessor(predecessor, relationship);
			print ("successor of " + newPredecessorNode.GroupName + " is " + successorNode.GroupName);
		}
		else //both assembly grups do not exist in the tree(!rootNode.doesAssemblyGroupExist(predecessor))
		{
			TreeNode newPredecessorNode = rootNode.addSuccessor(predecessor, Constants.StartToStart);
			TreeNode newSuccessorNode = newPredecessorNode.addSuccessor(successor, relationship);
			print ("Predecessor of " + newPredecessorNode.GroupName + " is root");
			print ("Predecessor of " + newSuccessorNode.GroupName + " is " + newPredecessorNode.GroupName);
		}
	}


	/*
	void addRelationshipInSequence (string firstGroup, string secondGroup, string relationship)
	{
		if (assemblySequenceList.Count == 1)
		{
			int startTime = 0;
			switch (relationship) {
				case "StartToStart": startTime = 1; break;
				case "FinishToStart": startTime = 5; break;
				default: break;
			}

			AssemblyGroup firstNewAssemblyGroup = new AssemblyGroup();
			firstNewAssemblyGroup.groupName = firstGroup;
			firstNewAssemblyGroup.successor[0] = secondGroup;
			firstNewAssemblyGroup.relSuccessor[0] = relationship;
			firstNewAssemblyGroup.startRenderingTime = 0;
			assemblySequenceList.Add(firstNewAssemblyGroup);

			AssemblyGroup secondNewAssemblyGroup = new AssemblyGroup();
			secondNewAssemblyGroup.groupName = secondGroup;
			secondNewAssemblyGroup.predecessor = firstGroup;
			secondNewAssemblyGroup.relPredecessor = relationship;
			secondNewAssemblyGroup.startRenderingTime = startTime;
			assemblySequenceList.Add(secondNewAssemblyGroup);
		}

		else 
		{
			bool doesPredessorExist = false;
			bool doesSuccessorExist = false;

			bool doesFirstGroupExist = false;
			int firstGroupIndex = 0;
			bool doesSecondGroupExist = false;
			int secondGroupIndex = 0;

			//check if the current assembly group is already in the list
			for (int i = 0; i < assemblySequenceList.Count; i++)
			{
				AssemblyGroup crrAssemblyGroup = (AssemblyGroup) assemblySequenceList[i];

				if (crrAssemblyGroup.groupName.Equals(firstGroup))
				{
					doesFirstGroupExist = true;
					firstGroupIndex = i;
				}

				if (crrAssemblyGroup.groupName.Equals(secondGroup))
				{
					doesSecondGroupExist = true;
					secondGroupIndex = i;
				}
			}

			if (doesFirstGroupExist && !doesSecondGroupExist)
			{
				AssemblyGroup crrAssemblyGroup = (AssemblyGroup) assemblySequenceList[firstGroupIndex];
				bool doesSeccussorExist = false;
				for (int i = 0; i < crrAssemblyGroup.successor.Count; i++)
				{
					if (crrAssemblyGroup.successor[i].Equals(secondGroup))
					{
						doesSeccussorExist = true;
					}
				}
				if (!doesSeccussorExist)
				{
					crrAssemblyGroup.successor.Add(secondGroup);
					crrAssemblyGroup.relSuccessor.Add(relationship);
				}

				int startTime = 0;
				switch (relationship) {
					case "StartToStart": startTime = crrAssemblyGroup.StartRenderingTime + 1; break;
					case "FinishToStart": startTime = crrAssemblyGroup.StartRenderingTime + 5; break;
					default: break;
				}

				assemblySequenceList.Insert(firstGroupIndex + 1, new AssemblyGroup{groupName = secondGroup, 
																				   predecessor = firstGroup, 
																				   relPredecessor = relationship,
																				   startRenderingTime = startTime});

			}

			else if (doesSecondGroupExist && !doesFirstGroupExist)
			{
				int startTimeIncrease = 0;rootNode = new TreeNode("root", null, "", 0);
				switch (relationship) {
					case "StartToStart": startTimeIncrease = 1; break;
					case "FinishToStart": startTimeIncrease = 5; break;
					default: break;
				}
				AssemblyGroup crrAssemblyGroup = (AssemblyGroup) assemblySequenceList[secondGroupIndex];
				crrAssemblyGroup.predecessor = firstGroup;
				crrAssemblyGroup.startRenderingTime += startTimeIncrease;

				assemblySequenceList.Insert(secondGroupIndex, new AssemblyGroup{groupName = firstGroup, 
																			    predecessor = "",
																			    relPredecessor = relationship,
																				successor.Add(secondGroup),
																				relSuccessor = relationship,
																			    startRenderingTime = startTime});

			}
			else //none of them exists
			{
				int startTime = 0;
				switch (relationship) {
				case "StartToStart":
					startTime = crrAssemblyGroup.StartRenderingTime + 1;
					break;
				case "FinishToStart":
					startTime = crrAssemblyGroup.StartRenderingTime + 5;
					break;
				default:
					break;
				}
				assemblySequenceList.Add(new AssemblyGroup{	groupName = firstGroup, 
															predecessor = "",
															relPredecessor = relationship,
															successor.Add(secondGroup),
															relSuccessor = relationship,
															startRenderingTime = 0});

				assemblySequenceList.Add(new AssemblyGroup{	groupName = secondGroup, 
															predecessor = firstGroup,
															relPredecessor = relationship,
															startRenderingTime = startTime});
			}
		}
	}
	*/


	/* Old Implementation
	void addRelationshipInSequence(string firstGroup, string secondGroup, string relationship)
	//AssemblyGroup newAssemblyGroup = new AssemblyGroup();
		if (assemblySequenceList.Count != 0)
		{
			for (int i = 0; i < assemblySequenceList.Count; i++)
			{
				AssemblyGroup crrAssemblyGroup = (AssemblyGroup) assemblySequenceList[i];
				if (crrAssemblyGroup.GroupName.Equals(firstGroup))
				{
					int startTime = 0;
					switch (relationship) {
						case "StartToStart":
							startTime = crrAssemblyGroup.StartRenderingTime + 1;
							assemblySequenceList.Insert(i+1, new AssemblyGroup{groupName = secondGroup, predecessor = firstGroup, startRenderingTime = startTime});
							break;
						case "FinishToStart":
							startTime = crrAssemblyGroup.StartRenderingTime + 5;
							assemblySequenceList.Insert(i+1, new AssemblyGroup {groupName = secondGroup, predecessor = firstGroup, startRenderingTime = startTime});
							break;
						default:
							break;
					}
				}
			}
		}
		else 
		{
			assemblySequenceList.Add (new AssemblyGroup{groupName = firstGroup, predecessor = "", startRenderingTime = 0});
			assemblySequenceList.Add (new AssemblyGroup{groupName = secondGroup, predecessor = firstGroup, startRenderingTime = 0});
			switch (relationship) {
			case "StartToStart":
				((AssemblyGroup) assemblySequenceList[1]).StartRenderingTime = ((AssemblyGroup) assemblySequenceList[0]).StartRenderingTime + 1;
				break;
			case "FinishToStart":
				((AssemblyGroup) assemblySequenceList[1]).StartRenderingTime = ((AssemblyGroup) assemblySequenceList[0]).StartRenderingTime + 5;
				break;
			default:
				break;
			}
		}

	}
	*/


	// Update is called once per frame
	void Update () {
	
	}

	void buildSequence()
	{
		readSequenceData();
		hidePreviewModel();
		traverseTreeNode(rootNode);
		StartCoroutine(renderSequence(rootNode));
	}

	void traverseTreeNode(TreeNode node)
	{
		string predecessorName = "";
		if (node.Predecessor != null) predecessorName = node.Predecessor.GroupName;
		print (node.GroupName + " predecessor: " + predecessorName);
		for (int i = 0; i < node.countSuccessors(); i++)
			traverseTreeNode(node.getSuccessor(i));
	}


	IEnumerator renderSequence(TreeNode node)
	{
		int waitSeconds = 1;
		yield return new WaitForSeconds(waitSeconds);

		for (int i = 0; i < node.countSuccessors(); i++)
		{
			//print (node.GroupName + " Successor #" + i.ToString() + ": " + node.getSuccessor(i).GroupName);
			if (node.getSuccessor(i).RelPredecessor == Constants.StartToStart)
			{
				string[] previewGroup = FileReader.getGroupElements(node.getSuccessor(i).GroupName);
				for(int j = 3; j < previewGroup.Length - 1; j++)
				{
					// This turns all of the renderer colors into another to show highlights
					//showWindow = true;
					string[] previewAssemblyType = previewGroup[j].Split('_');
					Component[] renderers;
					GameObject[] previewAssemblies = GameObject.FindGameObjectsWithTag(previewAssemblyType[0] + "Preview");

					for (int index = 0; index < previewAssemblies.Length; index++)
					{
						if (previewAssemblies[index].name.Equals(previewGroup[j]))
						{
							renderers = previewAssemblies[index].GetComponentsInChildren(typeof(Renderer));
							foreach (Renderer renderer in renderers)
							{
								renderer.enabled = true;
							}							
						}
						
					}
				}
			}
		}

		yield return new WaitForSeconds(waitSeconds);

		for (int i = 0; i < node.countSuccessors(); i++)
		{
			//print (node.GroupName + " Successor #" + i.ToString() + ": " + node.getSuccessor(i).GroupName);
			if (node.getSuccessor(i).RelPredecessor == Constants.FinishToStart)
			{
				string[] previewGroup = FileReader.getGroupElements(node.getSuccessor(i).GroupName);
				for(int j = 3; j < previewGroup.Length - 1; j++)
				{
					// This turns all of the renderer colors into another to show highlights
					//showWindow = true;
					string[] previewAssemblyType = previewGroup[j].Split('_');
					Component[] renderers;
					GameObject[] previewAssemblies = GameObject.FindGameObjectsWithTag(previewAssemblyType[0] + "Preview");
					
					for (int index = 0; index < previewAssemblies.Length; index++)
					{
						if (previewAssemblies[index].name.Equals(previewGroup[j]))
						{
							renderers = previewAssemblies[index].GetComponentsInChildren(typeof(Renderer));
							foreach (Renderer renderer in renderers)
							{
								renderer.enabled = true;
							}							
						}
						
					}
				}
			}
		}

		for (int i = 0; i < node.countSuccessors(); i++)
		{
			StartCoroutine(renderSequence(node.getSuccessor(i)));
		}
	}

	public static void hidePreviewModel()
	{
		GameObject modelPreview = GameObject.FindWithTag("ModelPreview");
		Component[] allPreviewElements = modelPreview.GetComponentsInChildren(typeof(Renderer));
		foreach (Renderer renderer in allPreviewElements)
		{
			renderer.enabled = false;
		}
		
	}

	void showSequencedGroup(string groupName)
	{
		GameObject modelPreview = GameObject.FindWithTag("ModelPreview");
		Component[] allPreviewElements = modelPreview.GetComponentsInChildren(typeof(Renderer));
		
		string[] groupElements = FileReader.getGroupElements(groupName);

		foreach (Renderer renderer in allPreviewElements)
		{
			for (int i = 0; i < groupElements.Length; i++)
			{
				if (renderer.name.Equals(groupElements[i]))
					renderer.enabled = true;
			}
		}
	}
}




/*
public class SequenceRelationship : MonoBehaviour {
	string firstAssemblyGroup;
	string secondAssemblyGroup;
	string relationship;

	void start () {
		firstAssemblyGroup = "";
		secondAssemblyGroup = "";
		relationship = "";
	}

	public string FirstAssemblyGroup
	{
		get { return firstAssemblyGroup; }
		set { firstAssemblyGroup = value; }
	}

	public string SecondAssemblyGroup
	{
		get { return secondAssemblyGroup; }
		set { secondAssemblyGroup = value; }
	}

	public string Relationship
	{
		get { return relationship; }
		set { relationship = value; }
	}

}
*/