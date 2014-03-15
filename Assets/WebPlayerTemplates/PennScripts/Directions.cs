using UnityEngine;
using System.Collections;

public class Directions : MonoBehaviour {
	
	
	// This just handles what the tooltip information shows when the mouse hovers the button
	void OnTooltip(bool show)
	{
		if(show)
		{
			UITooltip.ShowText("Rotate the model by holding in the Right Mouse Button. \n" +
				"Zoom using the Mouse Wheel. \n" +
				"Select an object by clicking the Left Mouse Button. \n\n" +
				"Using the menu to the left you may hide and isolate single objects or their respective groups. " +
				"To begin grouping simply press Start Grouping, select the objects you would like to group, and press Add Group. " +
				"Restart will restart the grouping phase and Continue will begin the method selection.");
		}else{
			UITooltip.ShowText(null);
		}
	}
}
