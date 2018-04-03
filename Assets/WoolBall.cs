using UnityEngine;
using System.Collections;

public class WoolBall : MonoBehaviour {
	Rigidbody rb;
	void Start(){
		rb = GetComponent<Rigidbody> ();
	}
	void GetDamage(){
		var dir = (transform.position - GameManager.inst.player.transform.position).normalized;
		dir += Vector3.up;

		rb.AddForce (dir * 5f, ForceMode.VelocityChange);
	}
}
