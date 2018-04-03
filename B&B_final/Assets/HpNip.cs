using UnityEngine;
using System.Collections;


public class HpNip : MonoBehaviour {
	public int plusHp = 50;
	void OnTriggerEnter(Collider col){
		var main = col.GetComponent<MainCharController> ();
		if (main && !dead) {
			main.hp += plusHp;
			main.CatNip ();
			StartCoroutine (GoToFullZero());
		}
	}

	bool dead = false;

	IEnumerator GoToFullZero(){
		dead = true;
		float t = 0f;
		var sc = transform.localScale;
		while(t < 0.5f){
			t += Time.deltaTime;
			transform.localScale = (sc) * (0.5f - t);
			yield return null;
		}
		Destroy (gameObject);
	}
}
