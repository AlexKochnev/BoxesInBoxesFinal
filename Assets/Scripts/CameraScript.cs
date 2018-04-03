using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public Transform target;
	public float speed = 1f;

	Vector3 delta;

	void Start(){
		delta = target.InverseTransformPoint (transform.position);
	}

	void LateUpdate(){
		Vector3 wishPos = target.TransformPoint (delta);

		transform.position = Vector3.Lerp (transform.position, wishPos, Time.deltaTime * speed);
	}
}
