using UnityEngine;
using System.Collections;

public class MainCharController : MonoBehaviour {
	CharacterController cc;
	public int hp = 100;
	AudioSource audioS;
	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
		actionBox = GetComponentInChildren<ActionBox> ();
		anims = GetComponentsInChildren<Animator> ();
		audioS = GetComponent<AudioSource> ();
		initSpeed = speed;
		lastHp = hp;
	}

	Animator[] anims;
	public float speed = 1f;
	public bool jump;
	public float ySpeed = 0f;
	public float jumpInitSpeed = 10f;
	public Transform cameraTrans;
	float lastJumpTime = -100f;

	// Update is called once per frame
	void Update () {
		float dt = Time.deltaTime;
		Vector3 dir = Vector3.zero;
		dir += transform.right * Input.GetAxis ("Horizontal");
		dir += transform.forward * Input.GetAxis ("Vertical");

		cc.Move (Vector3.ClampMagnitude(dir, 1f) * speed * dt);
		cc.Move (new Vector3(0, ySpeed * Time.deltaTime, 0));

		bool grounded = cc.isGrounded;

		ySpeed += dt * Physics.gravity.y;

		if (grounded && (lastJumpTime + 1f < Time.time)) {
			ySpeed = 0;
			temp = true;
		} else {
			temp = false;
		}
		jump = Input.GetKey (KeyCode.Space);

		if (jump && grounded) {
			ySpeed = jumpInitSpeed;
			lastJumpTime = Time.time;
		}


		var mouseUp = Input.GetAxis ("Mouse Y");
		var mouseRight = Input.GetAxis ("Mouse X");

		transform.Rotate (0f, mouseRight * rotateSpeed, 0f);
		cameraTrans.localEulerAngles -= new Vector3 (mouseUp * rotateSpeed, 0f, 0f);

		var cameraX = cameraTrans.localEulerAngles.x;
		if (cameraX > 90)
			cameraX -= 360f;
		cameraX = Mathf.Clamp (cameraX, -80, 80);
		cameraTrans.localEulerAngles = new Vector3 (cameraX, 0f, 0f);

		doAction = Input.GetButtonDown ("Fire1");
		if (doAction) {
			actionBox.DoAction ();
		}
		foreach(var anim in anims)
			anim.SetBool ("Attack", false);

		if (Input.GetButton ("Fire1") && lastAttackTime + attackCD < Time.time) {
			Attack ();
		}

		if (hp <= 0) {
			enabled = false;	
			FindObjectOfType<UIScript> ().deathUI.SetActive (true);
			GameManager.inst.Invoke ("LoadFirstLevel", 1.7f);
		}

		if (hp < lastHp) {
			lastHp = hp;
			FindObjectOfType<Scratch> ().DoScratch ();
		}
	}
	int lastHp;

	float damageCD = 0.25f;
	float attackCD = 0.5f;
	float lastAttackTime = 0.15f;

	public int damage = 25;

	public bool temp;
	public float rotateSpeed = 1f;

	public bool doAction;

	void Attack(){

		foreach(var anim in anims)
			anim.SetBool ("Attack", true);
		damageBox.Invoke ("DoDamage", damageCD);
		Invoke ("PlayPunchSound", damageCD);
		lastAttackTime = Time.time;
	}


	void PlayPunchSound(){
		if (damageBox.objsNum > 0) {

			audioS.clip = GameManager.inst.punchAudio;
			audioS.Play ();
			//audioS.PlayOneShot(
		} else {
			audioS.clip = GameManager.inst.missAudio;
			audioS.Play ();
		}
	}

	ActionBox actionBox;
	public ActionBox damageBox;

	void GetDamage(int damage){
		hp -= damage;
	}

	private float initSpeed;
	IEnumerator SpeedUpCor(){
		float t = 0;
		float endT = 3f;
		var speed1 = speed * 3f;
		while (t < endT) {
			t += Time.deltaTime;
			speed = Mathf.Lerp (speed1, initSpeed, t / endT);
			yield return null;
		}
		speed = initSpeed;
	}

	public void SpeedUp(){
		StartCoroutine (SpeedUpCor ());
	}

	IEnumerator CatNipCor(){
		var eyes = GetComponentsInChildren<UnityStandardAssets.ImageEffects.Fisheye > ();
		float t = 0;
		float f = 1f;
		while (t < 3f) {
			t += Time.deltaTime;
			foreach (var eye in eyes) {
				eye.strengthX = Mathf.Lerp (0f, f, t / 3);
				eye.strengthY = Mathf.Lerp (0f,f, t / 3);
			}
			yield return null;
		}
		t = 0f;
		while (t < 6f) {
			t += Time.deltaTime;
			foreach (var eye in eyes) {
				eye.strengthX = Mathf.Lerp (f, 0f, t / 3);
				eye.strengthY = Mathf.Lerp (f, 0f, t / 3);
			}
			yield return null;
		}
		foreach (var eye in eyes) {
			eye.strengthX = 0f;
			eye.strengthY = 0f;
		}
		yield return null;
	}

	public void CatNip(){
		StartCoroutine (CatNipCor ());
	}
}
