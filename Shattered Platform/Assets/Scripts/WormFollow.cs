using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormFollow : MonoBehaviour {

	public Transform _player;
	public Transform _attackTransform;
	public float _totalTime;

	public float _chaseTime;
	public float _delayAttack;

	private float _newX;
	private float _newY;
	private float _attackPoint;
	private float _currentTime;
	private float _startPoint;

	void Start(){
		_startPoint = transform.position.x;
		_newX = _startPoint;
		_attackPoint = _attackTransform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (_chaseTime >= 0) {
			Vertical ();
			_chaseTime = _chaseTime - Time.deltaTime;
		}
		else if(_delayAttack > 0){
			_delayAttack = _delayAttack - Time.deltaTime;
		}
		else {
			Horizontal();

		}

			transform.position = new Vector2(_newX, _newY);
	}



	void Horizontal(){

		_currentTime = _currentTime + Time.deltaTime;
		if (_currentTime >= _totalTime) {
			_newX = _startPoint;
			_chaseTime = 3f;
			_delayAttack = 1f;
			_currentTime = 0;
		} 
		else {
			
			_newX = Mathf.Abs(_startPoint - _attackPoint) * (_currentTime / _totalTime);
		}
	}

	void Vertical(){
			_newY = transform.position.y + ((_player.position.y - transform.position.y) / 16);
	

	}
}
