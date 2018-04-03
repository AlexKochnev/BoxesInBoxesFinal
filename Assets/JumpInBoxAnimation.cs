using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JumpInBoxAnimation : MonoBehaviour {
	[HideInInspector]
	public GameObject box;
	Image fade;
	Vector3 initPos;
	Vector3 initLookAt;
	Vector3 initCameraAngle;
	
	void Start(){
		initPos = transform.position;
		initLookAt = transform.forward;
		initCameraAngle = GetComponentInChildren<Camera> ().transform.localEulerAngles;
		fade = FindObjectOfType<UIScript> ().fade;
	}

	float t = 0;

	void Update () {
		t += Time.deltaTime;
		t = Mathf.Clamp01 (t);
		transform.position = Vector3.Lerp (initPos, box.transform.position, t);
		transform.position += new Vector3 (0f, (1 - t) * (t) * 10f, 0f);

		Vector3 lookAt = Vector3.Lerp (initLookAt, Vector3.down, t);
		transform.LookAt (transform.position + lookAt);
		var cameraTrans = GetComponentInChildren<Camera> ().transform;
		cameraTrans.localEulerAngles = new Vector3 (Mathf.LerpAngle (initCameraAngle.x, 0f, t), 0f, 0f);

		var color = fade.color;
		color.a = t;
		fade.color = color;
	}


}
