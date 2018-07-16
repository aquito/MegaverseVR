using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMaterial : MonoBehaviour {

	
	Material material;

	Renderer rend;

	Material originalMaterial;
	Material darkMaterial;
	Material lightMaterial;

	public Material[] materials;
	



	bool isOriginalMaterial;
	
	// Use this for initialization
	void Start () {

		rend = GetComponent<Renderer>();
		originalMaterial = materials[0];
		darkMaterial = materials[1];
		lightMaterial = materials[2];
		rend.sharedMaterial = originalMaterial;
		isOriginalMaterial = true;
		
	}
	
	public void ChangeToDark()
	{
		if(materials != null)
		{
			if(isOriginalMaterial)
			{
				rend.sharedMaterial = darkMaterial;
				Debug.Log("Dark material switched");
				isOriginalMaterial = false;
			}
			else
			{
				rend.sharedMaterial = originalMaterial;
				isOriginalMaterial = true;
			}

		}
		
	}

	public void ChangeToLight()
	{
		if(materials != null)
		{
			if(isOriginalMaterial)
			{
				rend.sharedMaterial = lightMaterial;
				isOriginalMaterial = false;
			}
			else
			{
				rend.sharedMaterial = originalMaterial;
				isOriginalMaterial = true;
			}

		}
		
	}
}
