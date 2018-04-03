using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
	public float levelLength = 12;
	public float levelWidth = 17;
	public string levelVisibleName = "lvlName";
	public Vector3 GetRandomPoint(){
		return new Vector3 (Random.Range(-levelLength, levelLength), 0f, Random.Range(-levelWidth, levelWidth));
	}

	public static GameManager inst{
		get{
			return FindObjectOfType<GameManager> ();
		}
	}

	void Start(){
		Cursor.lockState = CursorLockMode.Locked;
		enemies = FindObjectsOfType<EnemyBotController> ().Length;
		levelStartTime = Time.time;
		player = FindObjectOfType<MainCharController> ();
	}
	public float levelStartTime;

	public int enemies;

	void Update(){
		if (enemies == 0 && Time.time - levelStartTime > 1f) {
			foreach (var box in FindObjectsOfType<CatBox>()) {
				box.Open ();
			}
		}
	}

	public MainCharController player;

	public AudioClip punchAudio;
	public AudioClip missAudio;

	public void LoadFirstLevel(){
		SceneManager.LoadScene ("1");
	}
}
