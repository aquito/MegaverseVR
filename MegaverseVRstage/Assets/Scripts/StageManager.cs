using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {

	private string _currentLevelName = string.Empty;


	private void Start()
	{
		DontDestroyOnLoad(gameObject);

		LoadLevel("SampleScene");
	}
	
	void OnLoadOperationComplete(AsyncOperation ao)
	{
		Debug.Log("Scene load complete");
	}

	void OnUnloadOperationComplete(AsyncOperation ao)
	{
		Debug.Log("Scene unload complete");
	}

	public void LoadLevel(string levelName)
	{
		AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive); // additive scene load so that this manager remains in memory
		
		if(ao == null)
		{
			Debug.LogError("[StageManager] unable to load level " + levelName);
			return;
		}

		ao.completed += OnLoadOperationComplete;
		_currentLevelName = levelName;
	}


	public void UnloadLevel(string levelName)
	{
		AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
		
		if(ao == null)
		{
			Debug.LogError("[StageManager] unable to unload level " + levelName);
			return;
		}

		ao.completed += OnUnloadOperationComplete;
	}
}
