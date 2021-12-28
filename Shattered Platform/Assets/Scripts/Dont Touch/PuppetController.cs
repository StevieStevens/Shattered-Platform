using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetController : MonoBehaviour {

	public Transform _player;
	public float _acceleration;
	public float _speed;
	public int _countTilTangled;


	private int _currentTilTangled;
	private string _SSD;
	private float _currentSpeed;
	private int Direction;
	private Rigidbody2D _rb;

	const string _Grounded = "Grounded";
	const string _Tangled = "Tangled";
	const string _Bouncing = "Bouncing";
	const string _Untangled = "Untangled";



	public float _bounceSpeed;
	public float _randomDecimal;

	private float _hDistance;
	private float _totalDistance;
	private float _vDistance;
	private float _verticalMovement = 2.5f;
	private float _horizontalMovement = 2.5f;



	void Start () {
		_rb = GetComponent<Rigidbody2D> ();

		Grounded ();

	}



	void Update () {
		switch (_SSD) {
		default:
			_SSD = _Grounded;
			Grounded ();
			break;

		case _Grounded:
			Grounded ();
			break;
		case _Tangled:
			Tangled ();
			break;
		case _Bouncing:
			Bouncing ();
			break;


		}


	}


	void Grounded(){
		if (_player.position.x > transform.position.x && Direction != 1) {
			Direction = 1;
			_currentTilTangled++;
		} else if(_player.position.x < transform.position.x && Direction != -1){
			Direction = -1;
			_currentTilTangled++;
		}
		_currentSpeed = (Direction * Mathf.Abs (_acceleration * Time.deltaTime)) + _currentSpeed;
		if (_currentSpeed > _speed) {
				_currentSpeed = _speed;
		} else if (_currentSpeed < -_speed){
			_currentSpeed = -_speed;
		}
		_rb.velocity = new Vector2 (_currentSpeed, 0);
		if (_currentTilTangled >= _countTilTangled) {
			_currentTilTangled = 0;
			_SSD = _Tangled;
		}
	}


	void Tangled(){

		_currentSpeed = (Direction * Mathf.Abs (_acceleration * Time.deltaTime)) + _currentSpeed;
		if (_currentSpeed > _speed) {
			_currentSpeed = _speed;
		} else if (_currentSpeed < -_speed){
			_currentSpeed = -_speed;
		}
		_rb.velocity = new Vector2 (_currentSpeed, 0);


	}
	void Bouncing(){



	}


	void OnTriggerEnter2D(Collider2D _col){
		switch (_SSD) {
		case _Tangled:
			if (_col.tag == "Left Wall" && _SSD == _Tangled) {
				Debug.Log ("fdsa");
				_SSD = _Bouncing;
				_rb.velocity = new Vector2 (4.5f, 3.5f);
			}

			if (_col.tag == "Right Wall" && _SSD == _Tangled) {
				_SSD = _Bouncing;
				_rb.velocity = new Vector2 (-4.5f, 3.5f);
			}


			break;
		case _Bouncing:
			NewDirection (_col.tag);


			break;
		}

	}


	void NewDirection(string _tag){

		float _randVariance = Random.Range (-_randomDecimal, _randomDecimal);


		switch (_tag) {
		case "Ground":
			_vDistance = -_rb.velocity.y * (1 + _randVariance);
			_totalDistance = Mathf.Sqrt (Mathf.Pow (_vDistance, 2) + Mathf.Pow (_rb.velocity.x, 2));
			if (_verticalMovement < 2.5f)
				_verticalMovement = 2.5f;
			if (_verticalMovement > 4.3f)
				_verticalMovement = 4.3f;
			_verticalMovement = _vDistance * (_bounceSpeed / _totalDistance);
			if (_horizontalMovement < 2.5f)
				_horizontalMovement = 2.5f;
			if (_horizontalMovement > 4.3f)
				_horizontalMovement = 4.3f;
			_horizontalMovement = _rb.velocity.x * (_bounceSpeed / _totalDistance);
			break;
		case "Roof":
			_vDistance = -_rb.velocity.y * (1 + _randVariance);
			_totalDistance = Mathf.Sqrt(Mathf.Pow(_vDistance, 2) + Mathf.Pow(_rb.velocity.x, 2));
			if (_verticalMovement < 2.5f)
				_verticalMovement = 2.5f;
			if (_verticalMovement > 3.3f)
				_verticalMovement = 3.3f;
			_verticalMovement = _vDistance * (_bounceSpeed / _totalDistance);
			if (_horizontalMovement < 2.5f)
				_horizontalMovement = 2.5f;
			if (_horizontalMovement > 3.3f)
				_horizontalMovement = 3.3f;
			_horizontalMovement = _rb.velocity.x * (_bounceSpeed / _totalDistance);
			break;
		case "Left Wall":
			_hDistance = -_rb.velocity.x * (1 + _randVariance);
			_totalDistance = Mathf.Sqrt(Mathf.Pow(_hDistance, 2) + Mathf.Pow(_rb.velocity.y, 2));
			if (_horizontalMovement < 2.5f)
				_horizontalMovement = 2.5f;
			if (_horizontalMovement > 3.3f)
				_horizontalMovement = 3.3f;
			_horizontalMovement = _hDistance * (_bounceSpeed / _totalDistance);
			if (_verticalMovement < 2.5f)
				_verticalMovement = 2.5f;
			if (_verticalMovement > 3.3f)
				_verticalMovement = 3.3f;
			_verticalMovement = _rb.velocity.y * (_bounceSpeed / _totalDistance);
			break;
		case "Right Wall":
			_hDistance = -_rb.velocity.x * (1 + _randVariance);
			_totalDistance = Mathf.Sqrt(Mathf.Pow(_hDistance, 2) + Mathf.Pow(_rb.velocity.y, 2));
			if (_horizontalMovement < 2.5f)
				_horizontalMovement = 2.5f;
			if (_horizontalMovement > 3.3f)
				_horizontalMovement = 3.3f;
			_horizontalMovement = _hDistance * (_bounceSpeed / _totalDistance);
			if (_verticalMovement < 2.5f)
				_verticalMovement = 2.5f;
			if (_verticalMovement > 3.3f)
				_verticalMovement = 3.3f;
			_verticalMovement = _rb.velocity.y * (_bounceSpeed / _totalDistance);
			break;
		case "Start":
			_vDistance = (_player.position.y - transform.position.y);
			_hDistance = (_player.position.x - transform.position.x);
			_totalDistance = Mathf.Sqrt (Mathf.Pow (_vDistance, 2) + Mathf.Pow (_hDistance, 2));
			_horizontalMovement = _hDistance * (_bounceSpeed / _totalDistance);
			_verticalMovement = _vDistance * (_bounceSpeed / _totalDistance);
			break;

		}


		_rb.velocity = new Vector2 (_horizontalMovement, _verticalMovement);

	}
}
