using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour {

	public Vector2 _direction;
	public float SpeedMin, SpeedMax;

	private Rigidbody2D _rb;

	void Awake(){

		_rb = GetComponent <Rigidbody2D> ();
	}

	public void SetSpeed(float _newSpeed){

		_direction = (_newSpeed * _direction.normalized);

		_rb.velocity = _direction;
	}

	public void SetSpeedRandomRange(float _minSpeed, float _maxSpeed){
		_direction = (Random.Range(_minSpeed,_maxSpeed) * _direction.normalized);

		_rb.velocity = _direction;
	}


}
