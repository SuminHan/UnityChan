using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {
	public float lifetime;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);
	}
		
	void OnTriggerEnter(Collider other){
		Debug.Log (other.name);
		if (!other.CompareTag ("unitychan")) {
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
