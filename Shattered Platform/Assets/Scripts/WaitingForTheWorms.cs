using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingForTheWorms : MonoBehaviour {

	public float _totalLength;
	public float _speed;
	public float _delayTime = 1;

	private Animator _anim;
	private float _newDelay;
	private float _beginningTimeLength;
	private float _currentTime;
	private CapsuleCollider2D _capsuleCollider;
	private float _wormLength;

	// Use this for initialization
	void Awake(){
		_anim = GetComponent<Animator> ();
		_beginningTimeLength = Mathf.Sqrt(_totalLength * Mathf.Abs(_speed)) / _speed;
		_currentTime = _beginningTimeLength;
		SetDelay ();
	}

	void Start () {
		_capsuleCollider = GetComponent<CapsuleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(_newDelay < Time.time){
			_anim.SetTrigger ("Attack");
		SetWormCollider ();
		}


	}



	void SetWormCollider(){
		_currentTime = Time.deltaTime + _currentTime;
		_wormLength = _speed * Mathf.Pow((_currentTime), 2) + _totalLength;
		_capsuleCollider.size = new Vector2(_wormLength, _capsuleCollider.size.y);
		_capsuleCollider.offset = _capsuleCollider.size / 2;
		if (_wormLength < 0) {
			_currentTime = _beginningTimeLength;
			gameObject.SetActive (false);
		}
	}

	public void SetDelay(){
		_newDelay = _delayTime + Time.time;

	}

}
