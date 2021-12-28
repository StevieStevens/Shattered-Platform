using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

	private SchoolMasterMangerScript _SchoolMasterManager;

	void Awake(){
		_SchoolMasterManager = GetComponentInParent<SchoolMasterMangerScript> ();


	}

	void OnTriggerEnter2D(Collider2D _derp){

		if (_derp.tag == "Bullet") {
			_SchoolMasterManager.Damage ();


		}
	}
}
