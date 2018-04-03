using UnityEngine;
using System.Collections;

public class DogMeshes : MonoBehaviour {
	public GameObject left, right, stay, attack;

	EnemyBotController contrl;
	// Use this for initialization
	void Start () {
		contrl = GetComponentInParent<EnemyBotController> ();
	}

	EnemyBotAI.EnemyState lastState;
	// Update is called once per frame
	void Update () {
		if (lastState != contrl.state) {
			lastState = contrl.state;
			StopAllCoroutines ();
			if (lastState != EnemyBotAI.EnemyState.stay) {
				StartCoroutine (Walk ());
			} else {
				stay.SetActive (true);
				attack.SetActive (false);
				left.SetActive (false);
				right.SetActive (false);
			}
		}
	}

	IEnumerator Walk(){
		bool a = false;
		while (true) {
			if (contrl.lastAttackTime + 0.3f > Time.time) {
				//print ("attack");
				stay.SetActive (false);
				attack.SetActive (true);
				left.SetActive (false);
				right.SetActive (false);
			}else
			if (a) {
				a = !a;
				stay.SetActive (false);
				attack.SetActive (false);
				left.SetActive (false);
				right.SetActive (true);
			} else {
				a = !a;
				stay.SetActive (false);
				attack.SetActive (false);
				left.SetActive (true);
				right.SetActive (false);
			}
			yield return new WaitForSeconds(0.2f);
		}
	}
}
