using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeNode {
	private string groupName;
	private string relPredecessor;
	private TreeNode predecessor;
	private int startTime;
	private List<TreeNode> successors;

	public TreeNode ()
	{
		groupName = "";
		relPredecessor = "";
		predecessor = null;
		startTime = 0;
		successors = new List<TreeNode>();
	}

	public TreeNode (string newGroupName, TreeNode newPredecessor, string newRelPredecessor, int newStartTime)
	{
		groupName = newGroupName;
		predecessor = newPredecessor;
		relPredecessor = newRelPredecessor;
		startTime = newStartTime;
		successors = new List<TreeNode>();
	}

	public string GroupName
	{
		get { return groupName; }
		set { groupName = value; }
	}

	public TreeNode Predecessor 
	{
		get { return predecessor; }
		set { predecessor = value; } 
	}

	public string RelPredecessor 
	{
		get { return relPredecessor; }
		set { relPredecessor = value; }
	}

	public int StartTime
	{
		get { return startTime; }
		set { startTime = value; }
	}

	public TreeNode getSuccessor(int i)
	{
		return successors[i];
	}

	public int countSuccessors ()
	{
		return successors.Count;
	}

	public int doesAssemblyGroupExist (string name)
	{
		int doesExist = 0;
		if (groupName.Equals(name)) doesExist = 1;
		else {
			foreach (TreeNode successor in successors)
				doesExist += successor.doesAssemblyGroupExist(name);
		}

		return doesExist;
	}

	public TreeNode getAssemblyGroupWithName (string name)
	{
		if (groupName.Equals(name)) return this;
		else
		{
			TreeNode newNode = null;
			foreach (TreeNode successor in successors)
			{
				newNode = successor.getAssemblyGroupWithName(name);
				if ( newNode != null) return newNode;
			}

			return newNode;
		}
	}

	public TreeNode addPredecessor (string newGroupName, string newRelationship)
	{
		int timeInterval = 0;
		switch (newRelationship) {
			case Constants.StartToStart: timeInterval = 1; break;
			case Constants.FinishToStart: timeInterval = 5; break;
			default: break;
		}

		TreeNode root = this;
		while (root != null)
		{
			root = root.predecessor;
			if (root.groupName.Equals("root")) break;
		}

		TreeNode newPredecessor = new TreeNode(newGroupName, root, Constants.StartToStart, 0);
		this.predecessor = newPredecessor;
		this.relPredecessor = newRelationship;
		this.updateStartingTime(timeInterval);

		newPredecessor.successors.Add(this);
		root.removeSuccessor(this);
		root.successors.Add(newPredecessor);

		return newPredecessor;
	}

	public TreeNode addSuccessor(string newGroupName, string newRelationship)
	{
		int timeInterval = 0;
		switch (newRelationship) {
			case Constants.StartToStart: timeInterval = 1; break;
			case Constants.FinishToStart: timeInterval = 5; break;
			default: break;
		}

		TreeNode newSuccessor = new TreeNode(newGroupName, this, newRelationship, startTime + timeInterval);

		successors.Add(newSuccessor);
		return newSuccessor;
	}

	public bool removeSuccessor (TreeNode node)
	{
		return successors.Remove (node);
	}


	public void updateStartingTime (int newTimeInterval)
	{
		startTime += newTimeInterval;
		foreach (var successor in successors)
		{
			successor.updateStartingTime (newTimeInterval);
		}
	}
}
