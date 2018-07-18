using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager> {

	// private static StageManager instance; // making the scene manager a singleton - enables access via StageManager.instance.LoadLevel etc

	public GameObject[] SystemPrefabs; // array that can be accessed via inspector; list of prefabs that are meant to be created

	private List<GameObject> _instancedSystemPrefabs; // instantiated prefabs that the manager object needs to keep tarck of

	private List<GameObject> sceneLights;
	private List<GameObject> scenePerformers;

	private List<GameObject> sceneAssets;

	private List<GameObject> sceneAudio;

	private List<GameObject> _instancedScenePrefabs;

	GameObject sceneObject;

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

		_instancedScenePrefabs = new List<GameObject>();

		sceneLights = new List<GameObject>();

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

		ClearStage(); 
		
		BuildLights();
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

	public void BuildLights() // called after new scene loaded
	{

		GameObject sceneLightInstance;

		GameObject sceneObject = GameObject.FindGameObjectWithTag("SceneObjects");

		if(sceneLights != null) // checking if there are any lights in the scene
		{
			
			foreach(GameObject light in GameObject.FindGameObjectsWithTag("Light"))
			{
				sceneLightInstance = Instantiate(light);
				sceneLightInstance.transform.SetParent(sceneObject.transform);
				Debug.Log("Set " + light.name + " as child of " + sceneObject);
				sceneLights.Add(sceneLightInstance);	
				_instancedScenePrefabs.Add(sceneLightInstance);
			}

			Debug.Log(sceneLights.Count + " lights built for the stage");

		}

		

	}

	
	public void BuildPerformers() // called after new scene loaded
	{
		GameObject scenePerformerInstance;

		if(scenePerformers != null) // checking if there are any lights in the scene
		{

			for(int i = 0; i < scenePerformers.Count; i++)
			{
				scenePerformerInstance = Instantiate(scenePerformers[i]);
				_instancedScenePrefabs.Add(scenePerformerInstance);
			}
		}

	}

	public void BuildAssets() // called after new scene loaded
	{
		GameObject sceneAssetInstance;

		if(sceneAssets != null) // checking if there are any lights in the scene
		{
			for(int i = 0; i < sceneAssets.Count; i++)
			{
				sceneAssetInstance = Instantiate(sceneAudio[i]);
				_instancedScenePrefabs.Add(sceneAssetInstance);
			}
		}

	}

	public void BuildAudio() // called after new scene loaded
	{
		GameObject sceneAudioInstance;

		if(sceneAudio != null) // checking if there are any lights in the scene
		{
			for(int i = 0; i < sceneAudio.Count; i++)
			{
				sceneAudioInstance = Instantiate(sceneAssets[i]);
				_instancedScenePrefabs.Add(sceneAudioInstance);
			}
		}

	}

	public void ClearStage() // clear all the scene prefabs before loading new scene
	{
		// if(sceneObjects != null)
		//{ 
			foreach(GameObject sceneObject in GameObject.FindGameObjectsWithTag("SceneObjects"))
			{
				if(sceneObject.transform.childCount > 0)
				{
					Debug.Log("Destroyed " + sceneObject.name + " on level ");
					Destroy(sceneObject);
				}
				
			}
			
				
			
			
			
		//} 
		// if(_instancedScenePrefabs != null)
		// {
			for(int i = 0; i < _instancedScenePrefabs.Count; i++)
			{
				Destroy(_instancedScenePrefabs[i]);		
			}
		//}
		

		_instancedScenePrefabs.Clear();
	}
}
