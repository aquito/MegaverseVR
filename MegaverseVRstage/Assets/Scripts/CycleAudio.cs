using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleAudio : MonoBehaviour {

	// Use this for initialization

	public AudioClip[] options;

	public string keyboardKey;

	public string quietKey;

	bool isQuiet;

	int _currentOption;

	int _nextOptionIndex;

	AudioClip _nextOption;

	AudioSource audioSource;
	void Start () {

        _nextOptionIndex = 0;
        audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.clip = options[_nextOptionIndex];
		isQuiet = true;

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

            audioSource.clip = _nextOption;

        	ChangeOption(_nextOption);
			Debug.Log("Option Changed on: " + gameObject.name);
			
		}
			

		if(Input.GetKeyDown(quietKey))
		{
			if(audioSource.isPlaying)
			{
				audioSource.Stop();
				isQuiet = true;

			} 
			else
			{
				audioSource.Play();
				isQuiet = false;
			}
			

			
		}
		
	}

    
     void ChangeOption(AudioClip option)
	{
		for(int i =0 ; i < options.Length; i++)
		{
			if(options[i] == option)
			{
                audioSource.PlayOneShot(option);
			}
			//else
			//{
			//	audioSource.Stop();
			//}	
		}
		
	}
     

}
