using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager> {

	// private static StageManager instance; // making the scene manager a singleton - enables access via StageManager.instance.LoadLevel etc


	// public List<Scene> stageScenes;


	public Object[] stages; // NOTE that Objects only works on Unity Editor so if you want to make a build this solution would not work (you'd need to define an order of scenes for the build)

	public GameObject[] SystemPrefabs;
	private List<GameObject> _instancedSystemPrefabs; // instantiated prefabs that the manager object needs to keep tarck of

	GameObject vrtk;
	private string _currentStageName = string.Empty;

	int numberOfScenes; 

	int _nextLevel;

	string _nextLevelName = string.Empty;

	private void Start()
	{
		DontDestroyOnLoad(gameObject);

		_instancedSystemPrefabs = new List<GameObject>();

		numberOfScenes = stages.Length;

		_currentStageName = SceneManager.GetActiveScene().name;

		_nextLevel = 0;

		vrtk = GameObject.Find("VRTK");

		DontDestroyOnLoad(vrtk);

		InstantiateSystemPrefabs();
	}

	
	void Update()
	{
		if(Input.GetKeyDown("space"))
		{
			
			
			for(int i =0 ; i < stages.Length; i++)
			{
				if(stages[i].name == stages[_nextLevel].name && _nextLevel < stages.Length)
				{
					_nextLevelName = stages[_nextLevel].name;
				}
			}
		
			if(_nextLevel == numberOfScenes -1 )
			{
			 	_nextLevel = 0;
			}
			else
			{
			 	_nextLevel += 1;
			}

			LoadLevel(_nextLevelName);
			Debug.Log("Loading " + _nextLevelName);
		}

		if(Input.GetKeyDown("escape"))
		{
			Destroy(gameObject);
			SceneManager.LoadScene("StartStage");
		}
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
		

		AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single); 
		
		if(ao == null)
		{
			Debug.LogError("[StageManager] unable to load level " + levelName);
			return;
		}

		ao.completed += OnLoadOperationComplete;
		_currentStageName = levelName;

		
		

		
	}


	
}
