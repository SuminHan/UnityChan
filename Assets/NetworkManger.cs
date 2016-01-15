using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;

public class NetworkManger : MonoBehaviour {
	private SocketIOComponent socket;
	private GameObject go;
	private GameObject controlobject;
	private UnityChanControlScriptWithRgidBody unitychancontrol;

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (transform.gameObject);
		go = GameObject.Find ("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

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
		controlobject = GameObject.FindWithTag ("unitychan");
		unitychancontrol = controlobject.GetComponent<UnityChanControlScriptWithRgidBody> ();
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
		/*
		using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
			{
				jo.Call("initActivity", e.data.ToString().Trim());
			}
		}
		*/
	}
}
