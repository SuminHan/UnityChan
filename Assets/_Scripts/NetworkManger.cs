using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;

public class NetworkManger : MonoBehaviour {
	private SocketIOComponent socket;
	private GameObject go;
	private GameObject controlobject;
	private GameObject headview;
	private UnityChanControlScriptWithRgidBody unitychancontrol;
	private Animator anim;
	private CardboardHead cardboardhead;
	private RaycastHit hit;
	private GameObject cardboardobject;
	private Cardboard cardboard;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	bool sideSwing;

	private float nextFire;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("return")) {
			cardboard.Recenter ();
		}
			
		if (Input.GetKeyDown ("z") && Time.time > nextFire) {
			//Debug.Log (anim);
			Ray ray = cardboardhead.Gaze;

			if (Physics.Raycast (ray, out hit, 30.0f)) {
				if (!hit.collider.CompareTag ("unitychan")) {
					anim.Play ("JUMP01B", -1, 0.0f);
					nextFire = Time.time + fireRate;
					Vector3 shotorigin = shotSpawn.position + new Vector3 (0.5f, 1.0f, 0.0f);
					GameObject clone = Instantiate (shot, shotorigin, new Quaternion(0.0f,0.0f,0.0f,0.0f)) as GameObject;
					Vector3 vec = (hit.collider.transform.position - shotorigin);
					vec.Normalize ();
					clone.GetComponent<Rigidbody> ().velocity = vec * 5;
				} else {
					anim.Play ("JUMP01B", -1, 0.0f);
					nextFire = Time.time + fireRate;
					Debug.Log (cardboard.HeadPose.Orientation);
					Vector3 shotorigin = shotSpawn.position + new Vector3 (0.5f, 1.0f, 0.0f);
					GameObject clone = Instantiate (shot,shotorigin, new Quaternion(0.0f,0.0f,0.0f,0.0f)) as GameObject;
					clone.GetComponent<Rigidbody> ().velocity = ray.direction * 5;
				}
			} else {
				anim.Play ("JUMP01B", -1, 0.0f);
				nextFire = Time.time + fireRate;
				Debug.Log (cardboard.HeadPose.Orientation);
				Vector3 shotorigin = shotSpawn.position + new Vector3 (0.5f, 1.0f, 0.0f);
				GameObject clone = Instantiate (shot,shotorigin, new Quaternion(0.0f,0.0f,0.0f,0.0f)) as GameObject;
				clone.GetComponent<Rigidbody> ().velocity = ray.direction * 5;
			}
			//Debug.Log (shotSpawn.forward);
		}
	}

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (transform.gameObject);
		go = GameObject.Find ("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		controlobject = GameObject.FindWithTag ("unitychan");
		anim = controlobject.GetComponent<Animator> ();
		unitychancontrol = controlobject.GetComponent<UnityChanControlScriptWithRgidBody> ();

		headview = GameObject.FindWithTag ("head");
		cardboardhead = headview.GetComponent<CardboardHead> ();
		hit = new RaycastHit ();

		cardboardobject = GameObject.FindWithTag ("cardboardmain");
		cardboard = cardboardobject.GetComponent<Cardboard> ();

		Debug.Log (socket);
		if (socket == null) {
			Debug.Log("fuck you!");
		}

		socket.On ("connection", Connection);
		socket.On("toclient", Controler);
	}

	public void Connection(SocketIOEvent e){
		Debug.Log (e.data ["msg"]);
	}

	public void Controler(SocketIOEvent e){
		Debug.Log(string.Format("[name: {0}, data: {1}]", e.name, e.data));
		Debug.Log (e.data ["msg"].ToString().Trim());
		if (e.data ["msg"].ToString ().Trim ('"').Equals ("a")) {
			unitychancontrol.h = -1;
		}
		else if (e.data ["msg"].ToString ().Trim ('"').Equals ("A")) {
			unitychancontrol.h = 0;
		}

		if (e.data ["msg"].ToString ().Trim ('"').Equals ("d")) {
			unitychancontrol.h = 1;
		}
		else if (e.data ["msg"].ToString ().Trim ('"').Equals ("D")) {
			unitychancontrol.h = 0;
		}
			
		if (e.data ["msg"].ToString ().Trim ('"').Equals ("w")) {
			unitychancontrol.v = 1;
		}
		else if (e.data ["msg"].ToString ().Trim ('"').Equals ("W")) {
			unitychancontrol.v = 0;
		}

		if (e.data ["msg"].ToString ().Trim ('"').Equals ("s")) {
			unitychancontrol.v = -1;
		}
		else if (e.data ["msg"].ToString ().Trim ('"').Equals ("S")) {
			unitychancontrol.v = 0;
		}

		if (e.data ["msg"].ToString ().Trim ('"').Equals ("space")) {
			unitychancontrol.space = true;
		}
		else if (e.data ["msg"].ToString ().Trim ('"').Equals ("SPACE")) {
			unitychancontrol.space = false;
		}
	}
}
