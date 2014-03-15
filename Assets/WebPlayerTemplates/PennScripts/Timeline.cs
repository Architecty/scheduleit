using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class Timeline : MonoBehaviour {

	public GameObject timelinePrefab;
	public TreeNode rootNode;
	List<GameObject> icons;
	Vector3 iconOffset;
	int nodez = 0;
	

	// Use this for initialization
	void Start () {
		icons = new List<GameObject>();
	}

	void buildTrigger()
	{

		rootNode = new TreeNode("root", null, "", 0);
		GameObject timeline = GameObject.Find("TimelineBackground");

		for (int i = icons.Count; i > 0 ; i--)
		{
			GameObject.Destroy(icons[i-1]);
			icons.RemoveAt(i-1);
		}
		iconOffset.x = 0;
		readSequenceData();
		buildTimeline(rootNode);

	}

	void buildTimeline(TreeNode node)
	{


		//print(node.countSuccessors().ToString());
		//icon = NGUITools.AddChild(GameObject.Find("TimelinePanel"), timelinePrefab);
		//icon.transform.localPosition.x = timelinePrefab.transform.localPosition.x;
		//UISprite icon2Sprite = icon2.GetComponentInChildren<UISprite>();
		//icon2Sprite.spriteName ="form-slab";
		//iconOffset.x = icon.transform.localScale.x;



		for (int i = 0; i < node.countSuccessors(); i++)
		{
			//string [] previewGroup = getSequenceElements(node.getSuccessor(i).GroupName);


			GameObject icon2;
			icon2 = NGUITools.AddChild(GameObject.Find("TimelinePanel"), timelinePrefab);
			UISprite iconSprite = icon2.GetComponentInChildren<UISprite>();
			icon2.transform.localPosition = iconOffset;
			String crrName = "";
			crrName = node.getSuccessor(i).GroupName;
			string[] groupType = crrName.Split('_');
			print (node.GroupName);
			switch(groupType[i])
			{
			case ("Footing"):
				switch (node.getSuccessor(i).RelPredecessor)
				{
				case (Constants.StartToStart): iconSprite.spriteName = "footing-ss"; break;
				case (Constants.FinishToStart): iconSprite.spriteName = "footing-fs"; break;
				default: break;
				}
				break;
			case ("Slab"):
				switch (node.getSuccessor(i).RelPredecessor)
				{
				case (Constants.StartToStart): iconSprite.spriteName = "slab-ss"; break;
				case (Constants.FinishToStart): iconSprite.spriteName = "slab-fs"; break;
				default: break;
				}
				break;
			case("Column"):
				switch (node.getSuccessor(i).RelPredecessor)
				{
				case (Constants.StartToStart): iconSprite.spriteName = "column-ss"; print("column"); break;
				case (Constants.FinishToStart): iconSprite.spriteName = "column-fs"; print("column"); break;
				default: break;

				}
				break;
			case("Beam"):
				switch (node.getSuccessor(i).RelPredecessor)
				{
				case (Constants.StartToStart): iconSprite.spriteName = "beam-ss"; break;
				case (Constants.FinishToStart): iconSprite.spriteName = "beam-fs"; break;
				default: break;
				}
				break;
			case("Shingles"):
				switch (node.getSuccessor(i).RelPredecessor)
				{
				case (Constants.StartToStart): iconSprite.spriteName = "shingles-ss"; break;
				case (Constants.FinishToStart): iconSprite.spriteName = "shingles-fs"; break;
				default: break;
				}
				break;
			case("Eaves"):
				switch (node.getSuccessor(i).RelPredecessor)
				{
				case (Constants.StartToStart): iconSprite.spriteName = "eave-ss"; break;
				case (Constants.FinishToStart): iconSprite.spriteName = "eave-fs"; break;
				default: break;
				}
				break;
			case("Sheathing"):
				switch (node.getSuccessor(i).RelPredecessor)
				{
				case (Constants.StartToStart): iconSprite.spriteName = "sheating-ss"; break;
				case (Constants.FinishToStart): iconSprite.spriteName = "sheating-fs"; break;
				default: break;
				}
				break;
			case("Truss"):
				switch (node.getSuccessor(i).RelPredecessor)
				{
				case (Constants.StartToStart): iconSprite.spriteName = "truss-ss"; break;
				case (Constants.FinishToStart): iconSprite.spriteName = "truss-fs"; break;
				default: break;
				}
				break;

			default: break;
			}
			iconOffset.x = icon2.transform.localScale.x + icon2.transform.localPosition.x;
			icons.Add(icon2);


		}
		for(int k = 0; k < node.countSuccessors(); k++)
		{
			buildTimeline(node.getSuccessor(k));
		}
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
		TreeNode predecessorNode = rootNode.getAssemblyGroupWithName(predecessor);
		TreeNode successorNode = rootNode.getAssemblyGroupWithName(successor);
		if( predecessorNode != null && successorNode != null)
		{
			//two assembly groups already exist. no further actions needed
		}
		else if(predecessorNode != null && successorNode == null)
		{
			predecessorNode.addSuccessor(successor, relationship);
		}
		else if(predecessorNode == null && successorNode != null)
		{
			successorNode.addPredecessor(predecessor, relationship);
		}
		else //both assembly grups do not exist in the tree(!rootNode.doesAssemblyGroupExist(predecessor))
		{
			TreeNode newPredecessor = rootNode.addSuccessor(predecessor, Constants.StartToStart);
			newPredecessor.addSuccessor(successor, relationship);
		}
	}


	
	// Update is called once per frame
	void Update () {
		
	}
	

	
	public static void traverseTreeNode(TreeNode node)
	{
		print (node.GroupName);
		for (int i = 0; i < node.countSuccessors(); i++)
			traverseTreeNode(node.getSuccessor(i));
	}

	int countnodes(int n)
	{
		for (int i = 0; i < rootNode.countSuccessors(); i++)
		{
			traverseTreeNode(rootNode.getSuccessor(i));
			n++;
		}
		return n;
	}
	
}