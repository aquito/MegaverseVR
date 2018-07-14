using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MenuToggle : MonoBehaviour {

	public VRTK_ControllerEvents controllerEvents;
	public GameObject menu;

	bool menuState = false;
	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{

		controllerEvents.ButtonTwoReleased += ControllerEvents_ButtonTwoReleased;

	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		controllerEvents.ButtonTwoReleased -= ControllerEvents_ButtonTwoReleased;
	}

	private void ControllerEvents_ButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
	{
		 menuState = !menuState;
		 menu.SetActive(menuState);
	}

}
