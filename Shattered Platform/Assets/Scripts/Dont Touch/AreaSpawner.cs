using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour {

	public float _delay;
	public Vector2 _spawnPointTopRight;
	public Vector2 _spawnPointBottomLeft;
	public GameObject _prefab;
	public int _minSpawnRatio;
	public int _maxSpawnRatio;


	private float _colliderWidth;
	private float _spawnChannels;
	private int _spawnRatio;
	private float _waitTime;
	private float _randomX;
	private float _randomY;
	private GameObject _clone;

	void Start(){
		_spawnPointTopRight = new Vector2(_spawnPointTopRight.x + transform.position.x, _spawnPointTopRight.y + transform.position.y);
		_spawnPointBottomLeft = new Vector2(_spawnPointBottomLeft.x + transform.position.x, _spawnPointBottomLeft.y + transform.position.y);
		_waitTime = Time.time + _delay;
		_spawnRatio = Random.Range (_minSpawnRatio, _maxSpawnRatio);

		CircleCollider2D _col = _prefab.GetComponent<CircleCollider2D> ();
		_colliderWidth = _col.radius * 2;
		_spawnChannels = (Mathf.Abs(_spawnPointTopRight.x - _spawnPointBottomLeft.x)) / _colliderWidth;
	
	}

	void Update () {
		if (Time.time > _waitTime) {
			CreateObject (_prefab);
			_waitTime = Time.time + _delay;
		}
	}



	void CreateObject(Object _obj){
		// is used to randomly chooses a channel to spawn an object at
		int _randomChannel = Random.Range(0, Mathf.RoundToInt(_spawnChannels));
		_randomX = (_randomChannel * _colliderWidth) + _spawnPointBottomLeft.x;
		_randomY = Random.Range (_spawnPointTopRight.y, _spawnPointBottomLeft.y);



		GameObject _clone = Instantiate (_obj, new Vector2(_randomX,_randomY), new Quaternion(0,0,0,0)) as GameObject;

		//sets blood and meat ratio
		FallingScript _script = _clone.GetComponent<FallingScript> ();
		_script.SetObjectType (_spawnRatio);
		if (_spawnRatio > 0) {
			_spawnRatio--;
		} else {
			_spawnRatio = Random.Range (_minSpawnRatio, _maxSpawnRatio);
		}
	}
}