using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EnemyBotAI : MonoBehaviour {

	public float stateChangePeriod;

	[Header("Weights")]
	public float StayToRetreat = 1f;
	public float StayToAttack = 1f;
	public float StayToIgnore = 1f;
	public float AttackToAttack = 1f;
	public float AttackToStay = 1f;
	public float AttackToRetreat = 1f;
	public float RetreatToAttack = 1f;
	public float RetreatToRetreat = 1f;
	public float RetreatToIgnore = 1f;
	public float IgnoreToIgnore = 1f;
	public float IgnoreToStay = 1f;

	[Header("porogi")]

	public float radiusAgression;
	public float hpPorog;

	[Header("deltaWeights")]
	public float bigDistanceMinus;

	public float lowHpMinus;

	private bool bigDistance;
	public EnemyBotController controller;
	void Start(){
		InvokeRepeating ("ChangeState", 0f, stateChangePeriod);
	}

	void ChangeState(){
		currentState = NextState ();
		controller.state = currentState;
	}

	EnemyState currentState = EnemyState.stay;

	EnemyState NextState(){
		if (controller.hp < hpPorog) {
			hpPorog = -100;
			AttackToAttack -= lowHpMinus;
			RetreatToAttack -= lowHpMinus;
			StayToAttack -= lowHpMinus;
			AttackToRetreat += lowHpMinus;
			StayToRetreat += lowHpMinus;
		}

		if (!bigDistance && Vector3.Distance (controller.player.position, controller.transform.position) > radiusAgression) {
			bigDistance = true;
			AttackToAttack -= bigDistanceMinus;
			RetreatToAttack -= bigDistanceMinus;
			StayToAttack -= bigDistanceMinus;

			RetreatToIgnore += bigDistanceMinus;
			AttackToRetreat += bigDistanceMinus;
			IgnoreToIgnore += bigDistanceMinus;
		} else {
			if (bigDistance && Vector3.Distance (controller.player.position, controller.transform.position) < radiusAgression) {
				bigDistance = false;
				AttackToAttack += bigDistanceMinus;
				RetreatToAttack += bigDistanceMinus;
				StayToAttack += bigDistanceMinus;

				RetreatToIgnore -= bigDistanceMinus;
				AttackToRetreat -= bigDistanceMinus;
				IgnoreToIgnore -= bigDistanceMinus;
			}
		}


		float total = 0f;
		if (currentState == EnemyState.stay) {
			total += Mathf.Max(StayToAttack, 0f);
			total += Mathf.Max(StayToIgnore, 0f);
			total += Mathf.Max(StayToRetreat, 0f);

			var attack = Mathf.Max (StayToAttack, 0f) / total;
			var retreat = Mathf.Max (StayToRetreat, 0f) / total + attack;
			var ignore = Mathf.Max (StayToIgnore, 0f) / total + attack + retreat;

			float r = Rand ();
			if (r < attack)
				return EnemyState.attack;
			if (r < retreat)
				return EnemyState.retreat;
			else
				return EnemyState.ignore;
		}

		if (currentState == EnemyState.attack) {
			total += Mathf.Max(AttackToAttack, 0f);
			total += Mathf.Max(AttackToStay, 0f);
			total += Mathf.Max(AttackToRetreat, 0f);

			var attack = Mathf.Max (AttackToAttack, 0f) / total;
			var retreat = Mathf.Max (AttackToRetreat, 0f) / total + attack;
			var stay = Mathf.Max (AttackToStay, 0f) / total + attack + retreat;

			float r = Rand ();
			if (r < attack)
				return EnemyState.attack;
			if (r < retreat)
				return EnemyState.retreat;
			else
				return EnemyState.stay;
		}

		if (currentState == EnemyState.retreat) {
			total += Mathf.Max(RetreatToAttack, 0f);
			total += Mathf.Max(RetreatToRetreat, 0f);
			total += Mathf.Max(RetreatToIgnore, 0f);

			var attack = Mathf.Max (RetreatToAttack, 0f) / total;
			var retreat = Mathf.Max (RetreatToRetreat, 0f) / total + attack;
			var ignore = Mathf.Max (RetreatToIgnore, 0f) / total + attack + retreat;

			float r = Rand ();
			if (r < attack)
				return EnemyState.attack;
			if (r < retreat)
				return EnemyState.retreat;
			else
				return EnemyState.ignore;
		}

		if (currentState == EnemyState.ignore) {
			total += Mathf.Max(IgnoreToIgnore, 0f);
			total += Mathf.Max(IgnoreToStay, 0f);

			var ignore = Mathf.Max (IgnoreToIgnore, 0f) / total;
			var retreat = Mathf.Max (IgnoreToStay, 0f) / total + ignore;

			float r = Rand ();
			if (r < ignore)
				return EnemyState.ignore;
			else
				return EnemyState.retreat;
		}

		return EnemyState.stay;
	}
	


	float Rand(){
		return Random.Range (0f, 1f);
	}

	bool Roll(float weight){
		return Rand () < weight;
	}


	public enum EnemyState{
		stay, attack, ignore, retreat
	}

	void Update(){
		sphere.localScale = Vector3.one * radiusAgression / 0.5f;
	}
	public Transform sphere;
}
