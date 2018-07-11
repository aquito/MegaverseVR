using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

	[SerializeField] bool flickering = true;
	[SerializeField] [Range (0f, 2f)] float brightness = 1f;
	[SerializeField] [Range (0f, 0.2f)] float speed = 0.035f;
	[SerializeField] [Range (0f, 0.2f)] float amplitude = 0.35f;

	float currentBrightness;
	Material mat;

	// Use this for initialization
	void Start () {
		mat = this.GetComponent<MeshRenderer> ().materials[0];

		reset ();
	}

	// Update is called once per frame
	void Update () {
		if (!flickering) {
			return;
		}

		// flickering behaviour
		currentBrightness += Random.Range (-speed, speed);
		currentBrightness = Mathf.Clamp (currentBrightness, Mathf.Clamp(brightness - amplitude, 0f, brightness), brightness + amplitude);

		// updating the material with the current flickering
		mat.SetColor ("_EmissionColor", Color.white * currentBrightness);
	}

	void reset(){
		currentBrightness = brightness;

		if (!flickering) {
			mat.SetColor ("_EmissionColor", Color.white * brightness);
		}
	}

	// for access from other scripts
	public void overrideSettings(bool _flickering, float _brightness, float _speed, float _amplitude){
		flickering = _flickering;
		brightness = _brightness;
		speed = _speed;
		amplitude = _amplitude;

		reset ();
	}
}
