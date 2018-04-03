using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scratch : MonoBehaviour {
	Image image;

	public Sprite[] texs;
	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		a = 0f;
	}

	float a = 1f;
	// Update is called once per frame
	void Update () {
		a -= Time.deltaTime;
		if (a < 0) {
			a = 0f;
		}
		Color c = new Color (1f, 1f, 1f, a);
		image.color = c;
	}

	public void DoScratch(){
		transform.localPosition = new Vector3 (Random.Range (-130, 130), Random.Range (-60, 60), 0f);
		int i = Random.Range (0, texs.Length);
		transform.localEulerAngles = new Vector3 (0f, 0f, Random.Range (0, 360f));
		image.sprite = texs [i];
		a = 1f;
	}
}
