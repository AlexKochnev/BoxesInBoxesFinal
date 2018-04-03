using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HellTrigger : MonoBehaviour {
	public string level;
	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Player")) {
			print ("HEEEEELLL");
			SceneManager.LoadScene (level);
		}
	}
}
