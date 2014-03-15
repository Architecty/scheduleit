using UnityEngine;
using System.Collections;

// This is a modified method from NGUI
// It was created to be able to handle more than one item
public class CheckboxControlledObject : MonoBehaviour {

	public GameObject[] target;
	public bool inverse = false;
	
	void OnEnable ()
	{
		UICheckbox chk = GetComponent<UICheckbox>();
		if (chk != null) OnActivate(chk.isChecked);
	}

	void OnActivate (bool isActive)
	{
		if (target != null)
		{
			for(int i =0; i < target.Length; i++)
			{
				NGUITools.SetActive(target[i], inverse ? !isActive : isActive);
				UIPanel panel = NGUITools.FindInParents<UIPanel>(target[i]);
				if (panel != null) panel.Refresh();
			}
		}
	}
}