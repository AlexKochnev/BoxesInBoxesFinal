using UnityEngine;
using System.Collections;

public class SpawnRectObjects : MonoBehaviour {
	public int nX = 1;
	public int nZ = 1;
	public float widthX = 1f;
	public float widthZ = 1f;

	[Range(0f, 1f)]
	public float prop = 1f;

	public GameObject prefab;
	// Use this for initialization
	void Awake () {
		for (int i = 0; i < nX; i++) {
			for (int j = 0; j < nX; j++) {
				if (Random.Range (0f, 1f) < prop) {
					float x = i * widthX;
					float z = j * widthZ;
					var local = new Vector3 (x, 0f, z);
					var go = Instantiate<GameObject> (prefab);
					go.transform.parent = transform;
					go.transform.localScale = Vector3.one;
					go.transform.localPosition = local;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
