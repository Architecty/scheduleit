using UnityEngine;
using System.Collections;

// Work in Progress

// This will create 2 temporary sequence and predecessor arraylists that will be applied to the real ones if submitted
public class PopupControl : MonoBehaviour {
	
	int tempCount;
	private string [] activityChoice;
	private Color defaultColor;
	private Color defaultHover;
	private ArrayList _Sequence;
	private ArrayList _Predecessor;
	private ArrayList constraints;
	private int _Index;
	private int tc;
	private string [] group1;
	private string [] group2;
	private string [] elements;
	
	private bool arguments = false;
	
	void Start()
	{
		constraints = new ArrayList();
		_Sequence = new ArrayList();
		_Predecessor = new ArrayList();
		_Index = Pavillion3.index;
		activityChoice = new string [2];
		defaultColor = GameObject.Find("ContinueButton").GetComponent<UIButton>().defaultColor;
		defaultHover = GameObject.Find("ContinueButton").GetComponent<UIButton>().hover;
		Component [] group1Temp = GameObject.Find("Group1Activity").GetComponentsInChildren(typeof(UIButton));
		Component [] group2Temp = GameObject.Find("Group2Activity").GetComponentsInChildren(typeof(UIButton));
		group1 = new string[group1Temp.Length];
		group2 = new string[group2Temp.Length];
		for(int i = 0; i < group1Temp.Length;i++)
		{
			group1[i] = group1Temp[i].GetComponentInChildren<UILabel>().text;
		}
		for(int i = 0; i < group2Temp.Length;i++)
		{
			group2[i] = group2Temp[i].GetComponentInChildren<UILabel>().text;
		}
	}
	
	// Handles the selection of activities within the first group
	//    By changing the color and by applying it to activityChoice[]
	void OnFirstGroup(GameObject obj)
	{
		obj.GetComponent<UIButton>().defaultColor = Color.red;
		obj.GetComponent<UIButton>().hover = Color.red;
		activityChoice[0] = obj.GetComponentInChildren<UILabel>().text;
		
		Component [] buttons = GameObject.Find("Group1Activity").GetComponentsInChildren(typeof(UIButton));
		for(int i = 0; i < buttons.Length; i++)
		{
			if(buttons[i].GetComponent<UIButton>().defaultColor == Color.red && buttons[i].gameObject != obj)
			{
				buttons[i].GetComponent<UIButton>().defaultColor = defaultColor;
				buttons[i].GetComponent<UIButton>().hover = defaultHover;
				buttons[i].GetComponent<UIButton>().UpdateColor(true,true);
			}
		}
		
		if(activityChoice[1] != null)
		{
			CallPopup();
		}
	}
	
	// Handles the selection of activities within the second group
	//    By changing the color and by applying it to activityChoice[]	
	void OnSecondGroup(GameObject obj)
	{
		obj.GetComponent<UIButton>().defaultColor = Color.red;
		obj.GetComponent<UIButton>().hover = Color.red;
		activityChoice[1] = obj.GetComponentInChildren<UILabel>().text;
		
		Component [] buttons = GameObject.Find("Group2Activity").GetComponentsInChildren(typeof(UIButton));
		for(int i = 0; i < buttons.Length; i++)
		{
			if(buttons[i].GetComponent<UIButton>().defaultColor == Color.red && buttons[i].gameObject != obj)
			{
				buttons[i].GetComponent<UIButton>().defaultColor = defaultColor;
				buttons[i].GetComponent<UIButton>().hover = defaultHover;
				buttons[i].GetComponent<UIButton>().UpdateColor(true,true);
			}
		}
		
		if(activityChoice[0] != null)
		{
			CallPopup();
		}
	}
	
	// Start to Start Relationship
	void OnSS()
	{
		// make Pavillion3.groupChoice[0] start when Pavillion3.groupChoice[1] starts
		// reset groupChoice to new string[]
		// close window
		// apply new sequence to sequence table as well as file
		
		// Finds if a sequence is already started

		
/*******************************************************************************/
		
		// This sections is to check the constraints before allowing the user to 
		//  assign the activities to the sequence
		// It would be best to have this in its on bool method eventually
		// But is still a work in progress
		elements = FileReader.getGroup(Pavillion3.groupChoice[1]);
		ArrayList tempList;
		for(int i = 3; i < elements.Length; i++)
		{
			tempList = FileReader.checkConstraint(elements[i]/* + "/" + activityChoice[1].ToString()*/);
			for(int j = 0; j < tempList.Count - 1; j++)
			{
				string [] temp = tempList[j].ToString().Split(","[0]);
				string [] quickSearch = FileReader.getGroup(temp[3]);
				constraints.Add(quickSearch[1] + "/" + temp[4]);
				
			}
		}

	//	constraints = FileReader.checkConstraint(Pavillion3.groupChoice[1].ToString() + "/" + activityChoice[1].ToString());
		for(int i = 0; i< constraints.Count; i++)
		{
			//print (constraints[i] + " : " + Pavillion3.sequence[i]);
			
			for(int j = 0; j < Pavillion3.sequence.Count; j++)
			{
				print (constraints[i].ToString() +" : " +Pavillion3.sequence[j].ToString());
				if(constraints[i].ToString().Equals(Pavillion3.sequence[j].ToString()))//Pavillion3.sequence.IndexOf(constraints[i]) ==null)//.BinarySearch(constraints[i]) != null)//Pavillion3.sequence.Contains(constraints[i].ToString())/* || _Sequence.Contains(constraints[i].ToString())*/)
				{
					// Print error message
					print ("verify");
					break;
					
				}else{
					
					//print ("ok");
					// set bool to true
					arguments = true;
				}
			}
		}
/******************************************************************************/	
	
		if(Pavillion3.sequence.Contains(Pavillion3.groupChoice[0] + "/" + activityChoice[0]))
		{
			Component [] buttons = GameObject.Find("Group1Activity").GetComponentsInChildren(typeof(UIButton));
			for(int i = 0; i < buttons.Length; i++)
			{
							
				if(buttons[i].GetComponentInChildren<UILabel>().text.Equals(activityChoice[0]))
				{
					Destroy(buttons[i].gameObject);
					break;
				}else{
					Destroy(buttons[i].gameObject);
				}
				
			}
			
			buttons = GameObject.Find("Group2Activity").GetComponentsInChildren(typeof(UIButton));
			for(int i = 0; i < buttons.Length; i++)
			{
				if(i == 0)
				{	
					if(buttons[i].GetComponentInChildren<UILabel>().text.Equals(activityChoice[1]))
					{
						_Sequence.Add(Pavillion3.groupChoice[1] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						_Predecessor.Add(Pavillion3.predecessor[Pavillion3.sequence.IndexOf(Pavillion3.groupChoice[0] + "/" + activityChoice[0])]);
						Destroy(buttons[i].gameObject);
						break;
					}else{
						_Sequence.Add(Pavillion3.groupChoice[1] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						_Predecessor.Add(Pavillion3.sequence.Count-1);
						Destroy(buttons[i].gameObject);
					}
				}else{			
					if(buttons[i].GetComponentInChildren<UILabel>().text.Equals(activityChoice[1]))
					{
						_Sequence.Add(Pavillion3.groupChoice[1] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						_Predecessor.Add(Pavillion3.predecessor[Pavillion3.sequence.IndexOf(Pavillion3.groupChoice[0] + "/" + activityChoice[0])]);
						Destroy(buttons[i].gameObject);
						break;
					}else{
						_Sequence.Add(Pavillion3.groupChoice[1] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						_Predecessor.Add(_Sequence.IndexOf(Pavillion3.groupChoice[1]+ "/" + buttons[i-1].GetComponentInChildren<UILabel>().text));
						Destroy(buttons[i].gameObject);
					}
				}
			}
			
		}else if(Pavillion3.sequence.Contains(Pavillion3.groupChoice[1] + "/" + activityChoice[1]))
		{
			Component [] buttons = GameObject.Find("Group2Activity").GetComponentsInChildren(typeof(UIButton));
			for(int i = 0; i < buttons.Length; i++)
			{
							
				if(buttons[i].GetComponentInChildren<UILabel>().text.Equals(activityChoice[1]))
				{
					Destroy(buttons[i].gameObject);
					break;
				}else{
					Destroy(buttons[i].gameObject);
				}
				
			}
			
			buttons = GameObject.Find("Group1Activity").GetComponentsInChildren(typeof(UIButton));
			for(int i = 0; i < buttons.Length; i++)
			{
				if(i == 0)
				{	
					if(buttons[i].GetComponentInChildren<UILabel>().text.Equals(activityChoice[1]))
					{
						_Sequence.Add(Pavillion3.groupChoice[0] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						_Predecessor.Add(Pavillion3.predecessor[Pavillion3.sequence.IndexOf(Pavillion3.groupChoice[1] + "/" + activityChoice[1])]);
						Destroy(buttons[i].gameObject);
						break;
					}else{
						_Sequence.Add(Pavillion3.groupChoice[0] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						_Predecessor.Add(Pavillion3.sequence.Count-1);
						Destroy(buttons[i].gameObject);
					}
				}else{			
					if(buttons[i].GetComponentInChildren<UILabel>().text.Equals(activityChoice[1]))
					{
						_Sequence.Add(Pavillion3.groupChoice[0] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						_Predecessor.Add(Pavillion3.predecessor[Pavillion3.sequence.IndexOf(Pavillion3.groupChoice[1] + "/" + activityChoice[1])]);
						Destroy(buttons[i].gameObject);
						break;
					}else{
						_Sequence.Add(Pavillion3.groupChoice[0] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						_Predecessor.Add(_Sequence.IndexOf(Pavillion3.groupChoice[0]+ "/" + buttons[i-1].GetComponentInChildren<UILabel>().text));
						Destroy(buttons[i].gameObject);
					}
				}
			}
		}else{
/*			Pavillion3.count = 0;
			
			GameObject temp = NGUITools.AddChild(GameObject.Find("SequenceTable"),(GameObject)Resources.Load("Label"));
			
			if(Pavillion3.index < 10)
			{
				temp.GetComponent<UILabel>().text = "0" + Pavillion3.index.ToString() + ". " + Pavillion3.groupChoice[0];
				temp.name = "0" + Pavillion3.index.ToString() + "." + Pavillion3.count+ " " + Pavillion3.groupChoice[0];
			}else{
				temp.GetComponent<UILabel>().text = Pavillion3.index.ToString() + ". " + Pavillion3.groupChoice[0];
				temp.name = Pavillion3.index.ToString() + "." + Pavillion3.count+ " " + Pavillion3.groupChoice[0];
			}

			temp.transform.localScale = new Vector3(28,28,1);
			
			Pavillion3.count++;
			
			GameObject temp2 = NGUITools.AddChild(GameObject.Find("SequenceTable"),(GameObject)Resources.Load("Label"));
			
			if(Pavillion3.index < 10)
			{
				temp2.GetComponent<UILabel>().text = "0" + Pavillion3.index.ToString() + ". " + Pavillion3.groupChoice[1];
				temp2.name = "0" + Pavillion3.index.ToString() + "." + Pavillion3.count+ " " + Pavillion3.groupChoice[1];
			}else{
				temp2.GetComponent<UILabel>().text = Pavillion3.index.ToString() + ". " + Pavillion3.groupChoice[1];
				temp2.name = Pavillion3.index.ToString() + "." + Pavillion3.count+ " " + Pavillion3.groupChoice[1];
			}
			
			temp2.transform.localScale = new Vector3(28,28,1);
			
			Pavillion3.index++;
			Pavillion3.count++;
		
			for(int i = 0; i < 2; i++)
			{
				
				Pavillion3.sequence.Add(Pavillion3.groupChoice[i]);
				Pavillion3.predecessor.Add(null);
				ColorGroup(Pavillion3.groupChoice[i]);
				
			}	
			
*/

			Component [] buttons = GameObject.Find("Group1Activity").GetComponentsInChildren(typeof(UIButton));
			for(int i = 0; i < buttons.Length; i++)
			{
				if(i == 0)
				{
					if(_Sequence.Count < 1 && Pavillion3.sequence.Count < 1)
					{
						_Predecessor.Add(null);
					}else{
						_Predecessor.Add(_Sequence.Count - 2);
						tc = _Sequence.Count - 1;
					}
					
					if(buttons[i].GetComponentInChildren<UILabel>().text.Equals(activityChoice[0]))
					{
						_Sequence.Add(Pavillion3.groupChoice[0] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						Destroy(buttons[i].gameObject);
						break;
					}else{
						_Sequence.Add(Pavillion3.groupChoice[0] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						Destroy(buttons[i].gameObject);
					}
				}else{			
					if(buttons[i].GetComponentInChildren<UILabel>().text.Equals(activityChoice[0]))
					{
						_Sequence.Add(Pavillion3.groupChoice[0] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						_Predecessor.Add(_Sequence.IndexOf(Pavillion3.groupChoice[0]+ "/" + buttons[i-1].GetComponentInChildren<UILabel>().text));
						Destroy(buttons[i].gameObject);
						break;
					}else{
						_Sequence.Add(Pavillion3.groupChoice[0] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						_Predecessor.Add(_Sequence.IndexOf(Pavillion3.groupChoice[0]+ "/" + buttons[i-1].GetComponentInChildren<UILabel>().text));
						Destroy(buttons[i].gameObject);
					}
				}
			}
			
			buttons = GameObject.Find("Group2Activity").GetComponentsInChildren(typeof(UIButton));
			for(int i = 0; i < buttons.Length; i++)
			{
				if(i == 0)
				{
					
					if(buttons[i].GetComponentInChildren<UILabel>().text.Equals(activityChoice[1]))
					{
						_Sequence.Add(Pavillion3.groupChoice[1] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						Destroy(buttons[i].gameObject);
						_Predecessor.Add(_Predecessor[_Sequence.IndexOf(Pavillion3.groupChoice[0]+ "/" + activityChoice[0])]);
						break;
					}else{
						_Sequence.Add(Pavillion3.groupChoice[1] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						Destroy(buttons[i].gameObject);
						if(_Sequence.Count < 1 && Pavillion3.sequence.Count < 1)
						{
							_Predecessor.Add(null);
						}else{
							_Predecessor.Add(tc);
						}
					}
				}else{			
					if(buttons[i].GetComponentInChildren<UILabel>().text.Equals(activityChoice[1]))
					{
						_Sequence.Add(Pavillion3.groupChoice[1] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						_Predecessor.Add(_Predecessor[_Sequence.IndexOf(Pavillion3.groupChoice[0]+ "/" + activityChoice[0])]);
						Destroy(buttons[i].gameObject);
						break;
					}else{
						_Sequence.Add(Pavillion3.groupChoice[1] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
						_Predecessor.Add(_Sequence.IndexOf(Pavillion3.groupChoice[1]+ "/" + buttons[i-1].GetComponentInChildren<UILabel>().text));
						Destroy(buttons[i].gameObject);
					}
				}
			}
			
		}
/*		
		Pavillion3.groupChoice[0] = Pavillion3.groupChoice[1];
		Pavillion3.groupChoice[1] = null;
		Destroy(GameObject.Find("PopupMenu(Clone)"));
		GameObject.Find("SequenceTable").GetComponent<UITable>().Reposition();
		
		for(int i = 0; i < Pavillion3.sequence.Count; i++)
		{
			if(Pavillion3.predecessor[i] != null)
			{
				print(i.ToString() + Pavillion3.sequence[i].ToString() + " : " + Pavillion3.predecessor[i].ToString());
			}
	
		}
*/
		OnPopupCancel();
	}
	
	
	//********************************************************
	// Finish to Start Relationship
	void OnFS()
	{
		// make Pavillion3.groupChoice[0] is a predecessor to Pavillion3.groupChoice[1]
		// reset groupChoice to new string[]
		// close window
		// apply new sequence to sequence table as well as file
		
			
		if(Pavillion3.sequence.Contains(Pavillion3.groupChoice[0] +"/" + activityChoice[0]))
		{
			Component [] buttons = GameObject.Find("Group2Activity").GetComponentsInChildren(typeof(UIButton));
		
			if(buttons[0].GetComponentInChildren<UILabel>().text.Equals(activityChoice[1]))
			{
				_Sequence.Add(Pavillion3.groupChoice[1] + "/" + buttons[0].GetComponentInChildren<UILabel>().text);
				_Predecessor.Add(_Sequence.IndexOf(Pavillion3.groupChoice[0] + "/" + activityChoice[0]));
				Destroy(buttons[0].gameObject);
				
				buttons = GameObject.Find("Group1Activity").GetComponentsInChildren(typeof(UIButton));
				for(int i = 0; i< buttons.Length; i++)
				{
					if(buttons[i].GetComponentInChildren<UILabel>().text.Equals(activityChoice[0]))
					{
						Destroy(buttons[i].gameObject);
						break;
					}
					Destroy(buttons[i].gameObject);
				}
				
			}else{
				print ("Group 2 Error: Cannot choose an activity that has predecessors unspecified");
			}
		}else if(Pavillion3.sequence.Contains(Pavillion3.groupChoice[1] +"/" + activityChoice[1]))
		{
			print ("Error: The first group should already be within the sequence before continuing");
		}else{
	
			Component [] buttons = GameObject.Find("Group2Activity").GetComponentsInChildren(typeof(UIButton));
			
			if(buttons[0].GetComponentInChildren<UILabel>().text.Equals(activityChoice[1]))
			{
				GameObject selButton = buttons[0].gameObject;
				
				buttons = GameObject.Find("Group1Activity").GetComponentsInChildren(typeof(UIButton));
				for(int i = 0; i < buttons.Length; i++)
				{
					if(i == 0)
					{
						if(_Sequence.Count < 1 && Pavillion3.sequence.Count < 1)
						{
							_Predecessor.Add(null);
						}else{
							_Predecessor.Add(_Sequence.Count - 2);
						}
						
						if(buttons[i].GetComponentInChildren<UILabel>().text.Equals(activityChoice[0]))
						{
							_Sequence.Add(Pavillion3.groupChoice[0] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
							Destroy(buttons[i].gameObject);
							break;
						}else{
							_Sequence.Add(Pavillion3.groupChoice[0] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
							Destroy(buttons[i].gameObject);
						}
					}else{			
						if(buttons[i].GetComponentInChildren<UILabel>().text.Equals(activityChoice[0]))
						{
							_Sequence.Add(Pavillion3.groupChoice[0] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
							_Predecessor.Add(_Sequence.IndexOf(Pavillion3.groupChoice[0]+ "/" + buttons[i-1].GetComponentInChildren<UILabel>().text));
							Destroy(buttons[i].gameObject);
							break;
						}else{
							_Sequence.Add(Pavillion3.groupChoice[0] + "/" + buttons[i].GetComponentInChildren<UILabel>().text);
							_Predecessor.Add(_Sequence.IndexOf(Pavillion3.groupChoice[0]+ "/" + buttons[i-1].GetComponentInChildren<UILabel>().text));
							Destroy(buttons[i].gameObject);
						}
					}
				}
	
				_Sequence.Add(Pavillion3.groupChoice[1] + "/" + selButton.GetComponentInChildren<UILabel>().text);
				_Predecessor.Add(_Sequence.IndexOf(Pavillion3.groupChoice[0]+ "/" + activityChoice[0]));
				Destroy(selButton.gameObject);
			}else{
				print ("Group 2 Error: Cannot choose an activity that has predecessors unspecified");
			}
				

		}
		
		
		OnPopupCancel();
	}
	
	//**************************************************
	// This controls the color change for groups if it is made part of the sequence
	public void ColorGroup(string groupChoice)
	{
		string [] tempGroup = FileReader.getGroup(groupChoice);
		for(int i = 3; i < tempGroup.Length-1; i++)
		{
			GameObject.Find(tempGroup[i]).renderer.material.color = Color.blue;
		}
	}
	
	
	// After two activities are chosen a second popup is created to select finish to start or start to start
	public void CallPopup()
	{
		GameObject temp = NGUITools.AddChild(GameObject.Find("EmptyPanel"),(GameObject)(Resources.Load("PopupMenu")));
		GameObject.Find("PopupTitle").GetComponent<UILabel>().text = activityChoice[0] + " to " + activityChoice[1];
		temp.transform.localPosition = new Vector3(0,210,0);
		Component [] buttons = temp.GetComponentsInChildren<UIButton>();
		for(int i = 0; i < 3; i++)
		{
			buttons[i].GetComponent<UIButtonMessage>().target = GameObject.Find("PopupMenu - Copy(Clone)");
		}
	}
	
	// Cancel button on popup menu: erases last selection and deletes popup
	void OnCancel()
	{
		Pavillion3.groupChoice[1] = null;
		Destroy(GameObject.Find("PopupMenu - Copy(Clone)"));
	}
	
	// cancel button for the second popup: it will unselect the activities and close the second popup
	void OnPopupCancel()
	{
		activityChoice = new string [2];
		Component [] buttons = GameObject.Find("PopupMenu - Copy(Clone)").GetComponentsInChildren(typeof(UIButton));
		for(int i = 0; i < buttons.Length; i++)
		{
			if(buttons[i].GetComponent<UIButton>().defaultColor == Color.red)
			{
				buttons[i].GetComponent<UIButton>().defaultColor = defaultColor;
				buttons[i].GetComponent<UIButton>().hover = defaultHover;
				buttons[i].GetComponent<UIButton>().UpdateColor(true,true);
			}
		}
		Destroy(GameObject.Find("PopupMenu(Clone)"));
	}
	
	// This will first add any unselected activities to the sequence and predecessor file by finish to start relationships
	// within their own respective groups. Then it will apply the entire temporary sequence and predecessor lists
	// to the real lists to eventually be saved to the sequence file. Switches the  groupChoices
	// so that the last chosen is now the first group and then closes out the popup
	void OnSubmit()
	{
		Component [] buttons = GameObject.Find("Group1Activity").GetComponentsInChildren(typeof(UIButton));

		for(int i = 0; i < group1.Length; i++)
		{
			if(buttons[0].GetComponentInChildren<UILabel>().text == group1[i].ToString())
			{
				_Sequence.Add(Pavillion3.groupChoice[0] + "/" + buttons[0].GetComponentInChildren<UILabel>().text);
				_Predecessor.Add(_Sequence.IndexOf(Pavillion3.groupChoice[0] + "/" + group1[i-1].ToString()));
				for(int j = 1; j < buttons.Length; j++)
				{
					_Sequence.Add(Pavillion3.groupChoice[0] + "/" + buttons[j].GetComponentInChildren<UILabel>().text);
					_Predecessor.Add(_Sequence.IndexOf(Pavillion3.groupChoice[0]+ "/" + buttons[j-1].GetComponentInChildren<UILabel>().text));
				}
				break;
			}
		}
		buttons = GameObject.Find("Group2Activity").GetComponentsInChildren(typeof(UIButton));

		for(int i = 0; i < group1.Length; i++)
		{
			if(buttons[0].GetComponentInChildren<UILabel>().text == group1[i].ToString())
			{
				_Sequence.Add(Pavillion3.groupChoice[1] + "/" + buttons[0].GetComponentInChildren<UILabel>().text);
				_Predecessor.Add(_Sequence.IndexOf(Pavillion3.groupChoice[1] + "/" + group2[i-1].ToString()));
				for(int j = 1; j < buttons.Length; j++)
				{
					_Sequence.Add(Pavillion3.groupChoice[1] + "/" + buttons[j].GetComponentInChildren<UILabel>().text);
					_Predecessor.Add(_Sequence.IndexOf(Pavillion3.groupChoice[1]+ "/" + buttons[j-1].GetComponentInChildren<UILabel>().text));
				}
				break;
			}
		}
		
		GameObject temp;
		for(int i = 0; i < _Sequence.Count; i++)
		{
			Pavillion3.sequence.Add(_Sequence[i]);
			Pavillion3.predecessor.Add(_Predecessor[i]);
			
		}
		
		for(int i = 0; i< _Sequence.Count; i++)
		{
			temp = NGUITools.AddChild(GameObject.Find("SequenceTable"),(GameObject)Resources.Load("Label"));
	/*		if( i != Pavillion3.predecessor.IndexOf(Pavillion3.predecessor[i]))
			{
				tempCount = Pavillion3.predecessor.IndexOf(Pavillion3.predecessor[i]);
			}else{
				tempCount = Pavillion3.index;
				Pavillion3.index++;
			}
		*/	
	
			int pred;
			if(Pavillion3.predecessor[i] == null)
			{
				pred = 0;
			}else{
				pred = int.Parse(Pavillion3.predecessor[i].ToString()) + 1;
			}
			
			if(Pavillion3.index < 10)
			{
				temp.GetComponent<UILabel>().text = "0" + Pavillion3.index.ToString() + ". " + Pavillion3.sequence[i] + " \t = " + pred;
			}else{
				temp.GetComponent<UILabel>().text = Pavillion3.index.ToString() + ". " + Pavillion3.sequence[i]+ " \t = " + pred;
			}
			temp.transform.localScale = new Vector3(28,28,1);
			temp.name = temp.GetComponent<UILabel>().text;
			
			Pavillion3.index++;
		}
		
		
	// Used this to make sure the sequence was coming out right	
/*		for(int i = 0; i < Pavillion3.sequence.Count; i++)
		{
			if(Pavillion3.predecessor[i] != null)
			{
				print(i.ToString() + Pavillion3.sequence[i].ToString() + " : " + Pavillion3.predecessor[i].ToString());
			}
	
		}
*/		
		GameObject.Find("SequenceTable").GetComponent<UITable>().Reposition();
		ColorGroup(Pavillion3.groupChoice[0]);
		ColorGroup(Pavillion3.groupChoice[1]);
		Pavillion3.groupChoice[0] = Pavillion3.groupChoice[1];
		OnCancel();
	}
}
