using UnityEngine;
using System.Collections;

public class HeadView : MonoBehaviour {
	CardboardHead cardboardhead;
	RaycastHit hit;
	// Use this for initialization
	void Start () {
		cardboardhead = gameObject.GetComponent<CardboardHead> ();
		hit = new RaycastHit ();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		Ray ray = cardboardhead.Gaze;
		if (Physics.Raycast (ray, out hit, 30.0f)) {
			Debug.Log (hit.collider.name);
		}*/
	}
}
