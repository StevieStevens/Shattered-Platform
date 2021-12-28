using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public Vector2 _direction = new Vector2(1,0);
	public float _speed = 8;
	public float _delay = 1;
	public Collider2D _player;

	//private bool _sitting = false;
	private Collider2D _bulletCollider;
	private float _fallingSpeed = -7;
//	private float _newDelay = 1;
	private Rigidbody2D _rb;


	void Awake(){
		_rb = GetComponent<Rigidbody2D> ();
		//_newDelay = _delay + Time.time;
		Move (_speed);
	}

	void Update(){
	//	if (_newDelay < Time.time && !_sitting)
			//Fall ();

	}
		
	public void Move(float _newSpeed){
		_rb.velocity = _direction.normalized * _newSpeed;
	}

	void Fall(){
		_rb.velocity = new Vector2 (0, _fallingSpeed);
	}

	void Sit(){
		_rb.velocity = new Vector2 (0, 0);
		//_sitting = true;
	}

	void OnTriggerStay2D(Collider2D _col){
		/*
		if (_col.tag == "Player" && _sitting) {
			PlayerController2D _player = _col.gameObject.GetComponent<PlayerController2D> ();
			_player.IncrementAmmoCount ();
			Destroy (this.gameObject);
		}
		*/
	}


	void OnTriggerEnter2D(Collider2D _col){
		/*
		switch (_col.gameObject.tag) {
			case "Enemy":
				EnemyController2D derp = _col.gameObject.GetComponent<EnemyController2D> ();
				if (derp != null)
					derp.Damage ();
				break;
			case "Player":
				if(_sitting){
					PlayerController2D _player = _col.gameObject.GetComponent<PlayerController2D> ();
					_player.IncrementAmmoCount ();
					Destroy (this.gameObject);
				}
				break;
			case "Roof":
				Fall ();
				break;
			case "Ground":
			
				_rb.velocity = new Vector2 (0, 0);
				transform.position = new Vector2 (transform.position.x, _col.transform.position.y + _col.bounds.extents.y + _bulletCollider.bounds.extents.y);
				Sit ();
				break;

			case "Left Wall":
				transform.position = new Vector2 (_col.transform.position.x + _col.bounds.extents.x + _bulletCollider.bounds.extents.x, transform.position.y);
				Fall ();
				break;
			case "Right Wall":
				transform.position = new Vector2 (_col.transform.position.x - _col.bounds.extents.x - _bulletCollider.bounds.extents.x, transform.position.y);
				Fall ();
				break;
			}
		*/
			
	}



	public void SetDirection(float _newSpeed){
		_direction = _newSpeed * _direction.normalized;

		_rb.velocity = _direction;
	}
}
