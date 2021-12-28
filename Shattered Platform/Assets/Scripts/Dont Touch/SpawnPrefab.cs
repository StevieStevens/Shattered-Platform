using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour {


	public Object Prefab;
	public int SpawnAmountMax;
	public RangeT TimeRange;
	public Vector2 _speedRange;
	private GameObject _Clone;
	private int EnemyGroupSize;
	private CapsuleCollider2D _col;
	private float wait = 1;

	void Awake(){
		wait = Random.Range (TimeRange.RangeMin, TimeRange.RangeMax) + Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (wait < Time.time) {
			int EnemyGroupSize = Random.Range (1,SpawnAmountMax);

			float _RandomSpeed = Random.Range(_speedRange.x, _speedRange.y);

			for(int i = 0; i < EnemyGroupSize; i++){
				GameObject _Clone =  Instantiate (Prefab, transform.position, transform.rotation) as GameObject;
				_col = _Clone.GetComponent<CapsuleCollider2D> ();

				_Clone.transform.position = new Vector2 (transform.position.x + ((_col.bounds.extents.x * 2) * i), transform.position.y);
				_Clone.GetComponent<ProjectileMove>().SetSpeed (_RandomSpeed);
			}
			wait = Random.Range (TimeRange.RangeMin, TimeRange.RangeMax) + Time.time;
		}
	}

	[System.Serializable]
	public class RangeT
	{
		public float RangeMin;
		public float RangeMax;
	}
}
