using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CatBox : MonoBehaviour {
	public string nextLevelName;

	public void LoadLevel(){
		SceneManager.LoadScene (nextLevelName);
	}

	void Update(){
	}

	void DoAction(){
		if(opened.activeSelf){
			var player = FindObjectOfType<MainCharController> ();
			player.enabled = false;
			player.GetComponent<JumpInBoxAnimation> ().enabled = true;
			player.GetComponent<JumpInBoxAnimation> ().box = gameObject;
			Invoke ("LoadLevel", 1f);
			//LoadLevel ();
		}
	}

	public GameObject opened;
	public GameObject closed;

	public void Open(){
		opened.SetActive (true);
		closed.SetActive (false);
	}
}
