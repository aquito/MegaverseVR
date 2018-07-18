using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleOptions : MonoBehaviour {

	// Use this for initialization

	public GameObject[] options;

	public string keyboardKey;

	int _currentOption;

	int _nextOptionIndex;

	GameObject _nextOption;
	void Start () {

		
		
	}
	
	// Update is called once per frame
	void Update () {


		if(Input.GetKeyDown(keyboardKey))
		{
			
			
			for(int i =0 ; i < options.Length; i++)
			{
				if(options[i] == options[_nextOptionIndex] && _nextOptionIndex < options.Length)
				{
					_nextOption = options[_nextOptionIndex];
				}
			}
		
			if(_nextOptionIndex == options.Length -1 )
			{
			 	_nextOptionIndex = 0;
			}
			else
			{
			 	_nextOptionIndex += 1;
			}

			ChangeOption(_nextOption);
			Debug.Log("Option Changed on: " + gameObject.name);
		}
		
	}

	void ChangeOption(GameObject option)
	{
		for(int i =0 ; i < options.Length; i++)
		{
			if(options[i] == option)
			{
				option.SetActive(true);
			}
			else{
				options[i].SetActive(false);
			}	
		}
		
	}
}
