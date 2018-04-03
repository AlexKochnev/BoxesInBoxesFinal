using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {
	public Text hpText;
	public Text timeText;
	public Text levelText;
	public Image fade;
	public GameObject deathUI;

	float levelStartTime;

	MainCharController player;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<MainCharController> ();
		levelStartTime = Time.time;

		levelText.text = GameManager.inst.levelVisibleName;
	}
	
	// Update is called once per frame
	void Update () {
		hpText.text = "♥" + player.hp + "";
		timeText.text = (int)(Time.time - levelStartTime) + "";
	}
}
