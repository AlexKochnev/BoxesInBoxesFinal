using UnityEngine;
using System.Collections;

public class AutoEnterBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Player")) {
			var player = FindObjectOfType<MainCharController> ();
			player.enabled = false;
			player.GetComponent<JumpInBoxAnimation> ().enabled = true;
			player.GetComponent<JumpInBoxAnimation> ().box = gameObject;
			Invoke ("LoadLevel", 1f);
			//LoadLevel ();
		}
	}

	void LoadLevel(){
		GetComponentInParent<CatBox>().LoadLevel ();
	}
}
