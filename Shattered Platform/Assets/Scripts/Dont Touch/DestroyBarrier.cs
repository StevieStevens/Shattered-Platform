using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBarrier : MonoBehaviour {




	void OnTriggerEnter2D(Collider2D _col){
			Destroy(_col.gameObject, 0f);
	}
}
