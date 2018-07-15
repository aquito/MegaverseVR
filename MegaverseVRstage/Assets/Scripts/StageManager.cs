using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager> {

	// private static StageManager instance; // making the scene manager a singleton - enables access via StageManager.instance.LoadLevel etc

	public GameObject[] SystemPrefabs; // array that can be accessed via inspector; list of prefabs that are meant to be created

	private List<GameObject> _instancedSystemPrefabs; // instantiated prefabs that the manager object needs to keep tarck of
	private string _currentLevelName = string.Empty;


/*	private void Awake() // inherited from singleton class therefore commented out
	{
		if(instance == null)
		{
			instance = this; // if there yet isn't a single instance, set it to this
		}
		else
		{
			Destroy(gameObject);
			Debug.LogError("StageManager instantiated multiple times!");
		}
	}
*/
	private void Start()
	{
		DontDestroyOnLoad(gameObject);

		_instancedSystemPrefabs = new List<GameObject>();

		InstantiateSystemPrefabs();

		// LoadLevel("SampleScene");
	}

	void InstantiateSystemPrefabs()
	{
		GameObject prefabInstance;

		for(int i = 0; i < SystemPrefabs.Length; i++)
		{
			prefabInstance = Instantiate(SystemPrefabs[i]);
			_instancedSystemPrefabs.Add(prefabInstance);
		}
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

	protected override void OnDestroy()
	{
		base.OnDestroy();

		for(int i = 0; i < _instancedSystemPrefabs.Count; i++)
		{
			Destroy(_instancedSystemPrefabs[i]);		
		}

		_instancedSystemPrefabs.Clear();
	}
}
