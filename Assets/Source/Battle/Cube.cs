using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	/*Cubeの時間を*/
	public float c_time = 0;

	public Vector3 position;
	
	public GameObject damage_area;

	private Color normal_color;
	private Color danger_color;
	//
	public bool danger = false;

	// Use this for initialization
	void Start () {
		normal_color = new Color(255f,255f,255f,0f);
		normal_color.a = 0f;
		
		danger_color = new Color(1f,0f,0f,0f);
		danger_color.a = 0.4f;
	}
	
	// Update is called once per frame
	void Update () {
		if(danger) {
			gameObject.GetComponent<Renderer>().material.color = normal_color;
			Instantiate (damage_area, gameObject.transform.position, gameObject.transform.rotation);
			danger = false;
		}
	}

	public Vector3 cubePosition(){
		return position;
	}
	public void change_danger() {
		danger = true;
	}
	
	IEnumerator changeColor(){
		gameObject.GetComponent<Renderer>().material.color = danger_color;
		yield return new WaitForSeconds (1.0f);
		gameObject.GetComponent<Renderer>().material.color = normal_color;	
	}
}
