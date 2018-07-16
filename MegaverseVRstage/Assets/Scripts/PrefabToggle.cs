using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabToggle : MonoBehaviour {

	// Use this for initialization
	
	public void CheckIfActive()
	{
		if(gameObject.activeSelf)
		{
			gameObject.SetActive(false);
			
		}
		else
		{
			gameObject.SetActive(true);
		}
	}
		
}
