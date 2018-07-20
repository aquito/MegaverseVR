using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleMaterials : MonoBehaviour {

	// Use this for initialization

	public Material[] options;

	public string keyboardKey;

	int _currentOption;

	int _nextOptionIndex;

	Material _nextOption;
	void Start () {

        _nextOptionIndex = 0;
        gameObject.GetComponent<Renderer>().material = options[_nextOptionIndex];

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

            gameObject.GetComponent<Renderer>().material = options[_nextOptionIndex]; // _nextOption;

        //  ChangeOption(_nextOption);
			Debug.Log("Option Changed on: " + gameObject.name);
		}
		
	}

    /*
     void ChangeOption(Material option)
	{
		for(int i =0 ; i < options.Length; i++)
		{
			if(options[i] == option)
			{
                gameObject.GetComponent<Renderer>().material = option; // option.SetActive(true);
			}
			else{
				options[i].SetActive(false);
			}	
		}
		
	}
     */

}
