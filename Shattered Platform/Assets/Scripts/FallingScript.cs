using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingScript : MonoBehaviour {


	public float _delayMin;
	public float _delayMax;
	public int _minBloodMeatRatio;
	public int _maxBloodMeatRatio;
	public float _gravityAccel;
	public float _maxSpeed;

	private int _boolDirection;
	private bool _isFalling = true;
	private float _delay;
	private float _randInt;
	private bool _isBlood;
	private float _totalTime;
	private float _falligSpeed;
	private Rigidbody2D _rb;
	private ProjectileMove _script;
	private Animator _anim;


	void Awake () {
		_rb = GetComponent<Rigidbody2D> ();
		_script = GetComponent<ProjectileMove> (); 
		_anim = GetComponent<Animator> ();
	}

	void Start(){

		_boolDirection = Random.Range (0, 2);
		if (_boolDirection == 0)
			_boolDirection = -1;
		_script._direction = new Vector2(_boolDirection, 0);
	}



	void Update(){
		if (!_isFalling) {
			if (_delay < Time.time) {
				_script.enabled = true;
				_script.SetSpeedRandomRange (7, 12);
			}
		}
	}

	void FixedUpdate () {
		if (_isFalling == true) {
			SetFallingSpeed ();
		}
	}

	void SetFallingSpeed(){
		_totalTime = Time.fixedDeltaTime + _totalTime;
		_falligSpeed = -(Mathf.Pow(_totalTime, 2) * _gravityAccel);
		if (-_maxSpeed > _falligSpeed)
			_falligSpeed = -_maxSpeed;
		_rb.velocity = new Vector2 (0, _falligSpeed);
	}

	void OnTriggerEnter2D(Collider2D _otherCol){
		if (_otherCol.gameObject.tag == "Ground" && _isBlood) {
			Destroy (transform.gameObject, 0.1f);
		} else if(_otherCol.gameObject.tag == "Ground"){
			_isFalling = false;
			_rb.velocity = new Vector2 (0, 0);
			transform.position = new Vector3 (transform.position.x, _otherCol.transform.position.y + _otherCol.bounds.extents.y, 0);
			_delay = Random.Range (_delayMin, _delayMax) + Time.time;
		}
	}

	public void SetObjectType(int _ratio){
		if (_ratio > 0) {
			_isBlood = true;
		} else {
			_isBlood = false; 
			_anim.Play ("FleshieIdle");
		}
	}
}
