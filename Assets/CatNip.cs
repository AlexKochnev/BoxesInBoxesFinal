using UnityEngine;
using System.Collections;

public class CatNip : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		var main = col.GetComponent<MainCharController> ();
		if (main) {
			main.SpeedUp ();
			main.CatNip ();
			Destroy (gameObject);
		}
	}

	void Update(){
		transform.eulerAngles += new Vector3 (0, Time.deltaTime * 360f/ 3, 0f);
	}
}
