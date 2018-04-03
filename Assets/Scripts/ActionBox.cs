using UnityEngine;
using System.Collections.Generic;

public class ActionBox : MonoBehaviour {
	public List<GameObject> actionObjectsStay = new List<GameObject>();
	System.Collections.IEnumerator OnTriggerStay(Collider col){
		
		//yield return new WaitForFixedUpdate ();
		if (
			(col.CompareTag (tagToCheck) || tagToCheck != "Player") && 
			!actionObjectsStay.Contains(col.gameObject) &&
			!col.gameObject.transform.IsChildOf(transform.root)) {
			actionObjectsStay.Add (col.gameObject);
		}
		return null;	
	}
	public string tagToCheck = "";
	public int objsNum;
	private bool doAction;
	void FixedUpdate(){
		objsNum = actionObjectsStay.Count;
		if (doAction) {
			DoActionInner ();
		}
		if (doDamage) {
			DoDamageInner ();
		}

		doAction = false;
		doDamage = false;
		actionObjectsStay.Clear ();
	}

	public void DoAction(){
		doAction = true;
	}

	private void DoActionInner(){
		foreach (GameObject objs in actionObjectsStay) {
			objs.SendMessage ("DoAction", SendMessageOptions.DontRequireReceiver);
		}
	}

	private bool doDamage;

	private void  DoDamageInner(){
		foreach (GameObject objs in actionObjectsStay) {
			objs.SendMessage ("GetDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
	}

	public void DoDamage(){
		doDamage = true;
	}

	MainCharController main;
	EnemyBotController bot;
	void Start(){
		main = GetComponentInParent<MainCharController> ();
		bot = GetComponentInParent<EnemyBotController> ();
		if (main)
			damage = main.damage;
		if (bot)
			damage = bot.damage;

		transform.localScale = Vector3.one * 1.1f;
		
	}

	int damage;
}
