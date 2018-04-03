using UnityEngine;
using System.Collections;

public class EnemyBotController : MonoBehaviour {
	[HideInInspector]
	public Transform player;
	CharacterController cc;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<MainCharController> ().transform;
		cc = GetComponent<CharacterController> ();
		InvokeRepeating ("Attack", 0f, attackCD);
	}
	public float speed = 5f;
	// Update is called once per frame
	void Update () {
		float dt = Time.deltaTime;


		//GoFromPlayer (dt);
		if (state == EnemyBotAI.EnemyState.ignore)
			IgnoringState (dt);

		if (state == EnemyBotAI.EnemyState.attack) {
			AttackingState (dt);
		}

		if (state == EnemyBotAI.EnemyState.stay) {
			StayingState (dt);
		}

		if (state == EnemyBotAI.EnemyState.retreat) {
			RetreatingState (dt);
		}

		ApplyYMovement (dt);


		if (hp <= 0) {
			Destroy (gameObject);
			GameManager.inst.enemies--;
		}
	}

	public float lastAttackTime;
	void AttackingState(float dt){
		GoToPlayer (dt);
	}

	void IgnoringState(float dt){
		Ignore (dt);
	}

	void StayingState(float dt){

	}

	void RetreatingState(float dt){
		GoFromPlayer (dt);
	}

	

	public int hp = 100;

	void GetDamage(int damage){
		hp -= damage;
		StopAllCoroutines ();
		StartCoroutine (TakeDamageBlink ());
	}

	void GoToPlayer(float dt){
		GoTo (player.position, dt);
	}

	void GoTo(Vector3 pos, float dt){
		var dir = Vector3.Scale(pos - transform.position, new Vector3(1f, 0f, 1f));
		cc.Move (Vector3.ClampMagnitude(dir, 1f) * speed * dt);

		var eulerOld = transform.localEulerAngles.y;
		transform.LookAt (transform.position + dir);
		transform.localEulerAngles = new Vector3 (0f, Mathf.LerpAngle(eulerOld, transform.localEulerAngles.y, dt * 5f), 0f);

	}

	void GoFromPlayer(float dt){
		var dir = Vector3.Scale(-player.position + transform.position, new Vector3(1f, 0f, 1f));
		GoTo (transform.position + dir, dt);
	}

	Vector3 pointToGo = Vector3.zero;
	void Ignore(float dt){
		Vector3 dir = pointToGo - transform.position;
		if (dir.magnitude < 1f || pointToGo == Vector3.zero) {
			pointToGo = GameManager.inst.GetRandomPoint ();
		}
		GoTo (pointToGo, dt);
	}


	float ySpeed;

	void ApplyYMovement(float dt){
		bool grounded = cc.isGrounded;
		ySpeed += dt * Physics.gravity.y;
		if (grounded)
			ySpeed = 0;
		cc.Move (new Vector3(0, ySpeed * dt, 0));

	}

	void Attack(){
		if (Time.time > lastAttackTime + attackCD) {
			lastAttackTime = Time.time;
			damageBox.Invoke ("DoDamage", damageCD);
		}
	}

	public ActionBox damageBox;
	public float attackCD = 1f;
	float damageCD = 0.0f;
	public int damage = 25;

	public EnemyBotAI.EnemyState state;

	
	IEnumerator TakeDamageBlink(){
		float t = 0;
		float time = 0.5f;
		var dir = -(player.position - transform.position).normalized;
		while (t < time) {
			if (Mathf.Repeat (t * 5f, 1f) > 0.5f) {
				meshRoot.SetActive (false);
			} else {
				meshRoot.SetActive (true);
			}
			t += Time.deltaTime;
			cc.Move(Time.deltaTime * dir * (1f - t / time) * 50f);

			yield return null;
		}
		meshRoot.SetActive(true);

	}
	public GameObject meshRoot;
}

//attack follow seeking retreat
