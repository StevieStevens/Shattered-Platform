using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour {

	public int _vertical = 0;
	public int _horizontal = 0;
	public bool _jumping = false;
	public bool _shooting = false;
	public bool _damaged = false;

	public GameObject _damagedFX;
	public GameObject _projectile;
	public float _speed = 5;
	public float _projectileSpeed = 4;
	public float _fireRate = 1;
	public int _maxAmmo = 3;
	public float _playerShrinkScale;

	public float _playerHeight;
	public float _playerCrounchHeight;

	public LayerMask _groundLayer;
	//
	//
	//
	//
	//
	//
	// Player Variables
	private Collider2D[] _groundColliders;
	private bool _grounded;
	private float _bulletDirection = 1;
	private float _oldVertical = 0;
	private Animator _anim;
	private float _directionFacing = 1;
	private int _ammoLeftover;
	private GameObject _clone;
	private float _newTime = 0;
	private string _SSD = _constIdle;
	private float DamageBoostDelay;
	private int _Health = 3;
	private CapsuleCollider2D _Cap;
	private Rigidbody2D _rb;
	private Collision2D _col;
	//
	//
	//
	//
	//Jump Variables
	public float _jumpSpeed = 15;
	public float _jumpHeight = 15;
	public float _preJumpTime = 0.05f;
	//public AudioSource _jumpAudio;

	private float _frames;
	private float _parabolaPosition;
	private float _newTransformY;
	private float _originalHeight;
	//
	//
	//
	//
	//
	const string _constLeft = "Left";
	const string _constRight = "Right";
	const string _constJumping = "Jumping";
	const string _constFalling = "Falling";
	const string _constDead = "Dead";
	const string _constMoving = "Moving";
	const string _constIdle = "Idle";
	const string _constLanding = "Landing";

	void Awake () {
		_rb = GetComponent<Rigidbody2D> ();
		_anim = GetComponent<Animator> ();
		_Cap = GetComponent<CapsuleCollider2D> ();

	}

	void Start(){
		_ammoLeftover = _maxAmmo;


	}

	void Update () {
		Move ();
		Shoot ();
		Jump ();
		SetAnim ();


		if (_damaged && DamageBoostDelay < Time.time) {
				_damaged = false;
		}




	}


	private void Move(){
		_horizontal = Mathf.RoundToInt( Input.GetAxisRaw ("Horizontal"));
		_vertical = Mathf.RoundToInt (Input.GetAxisRaw ("Vertical"));
		if (_horizontal != 0)
			_directionFacing = _horizontal;

		//set capsule height and grounds the player after the change in height
		Crouch();

		_rb.velocity = new Vector2 (_horizontal * _speed,  _rb.velocity.y);
		if (_rb.velocity.x > 0 || _rb.velocity.x < 0 && _SSD != _constJumping) {
			_SSD = _constMoving;
			if (_directionFacing > 0) {
				transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x),transform.localScale.y);
				_bulletDirection = 1;
			} else if (_directionFacing < 0) {
				transform.localScale = new Vector2( Mathf.Abs( transform.localScale.x) * -1,transform.localScale.y);
				_bulletDirection = -1;

			}
		} 
		else if (_rb.velocity.y >= 0 && _SSD == _constFalling) {
			_SSD = _constIdle;
		}


	}

	void Crouch(){
		
		if(_oldVertical != _vertical && _vertical != 1){
			if (Input.GetAxisRaw("Vertical") == -1) {
				_Cap.size = new Vector2 (_Cap.size.x, _playerCrounchHeight);
				transform.position = new Vector2 (transform.position.x, transform.position.y - (_playerHeight - _playerCrounchHeight) / 5);
			}
			else {
			_Cap.size = new Vector2 (_Cap.size.x, _playerHeight);
				transform.position = new Vector2 (transform.position.x, transform.position.y + (_playerHeight - _playerCrounchHeight) / 5);
			}
			_oldVertical = _vertical;
		}

	}

	void SetBulletReference(Bullet _b){
		_b._player = GetComponent<CapsuleCollider2D> ();
	}

	public void Damage(){
		_damaged = true;
		_Health = _Health - 1;
		Instantiate (_damagedFX, transform.position, new Quaternion (0, 0, 0, 0));
		DamageBoostDelay = 2 + Time.time;
		if(_Health <= 0){
			gameObject.SetActive (false);
		}
	}

	void OnTriggerStay2D(Collider2D _col){
		
		if (!_damaged) {		
			if (_col.gameObject.layer == 13){
				Damage ();
				_damaged = true;
				DamageBoostDelay = 2 + Time.time;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D _col){

		if (!_damaged) {		
			if (_col.tag == "Enemy"){
				Damage ();

			}
		}
	}

	void OnCollisionEnter2D(Collision2D _col){
		if (_col.gameObject.tag == "Ground"){
			_jumping = false;
			_anim.SetBool ("Jumping", _jumping);
		}
	}

	public void IncrementAmmoCount(){
		_ammoLeftover++;
		transform.localScale = new Vector3 (transform.localScale.x + _playerShrinkScale, transform.localScale.y + _playerShrinkScale, 1);
	}
		

	void Shoot(){
		if(_ammoLeftover > 0 && Input.GetButton("Fire1") && _newTime < Time.time) {
				_clone = Instantiate (_projectile, transform.position, new Quaternion (0, 0, 0, 0)) as GameObject;
				Bullet _bulletScript = _clone.GetComponent <Bullet> ();
			if (Input.GetAxisRaw ("Vertical") == 1 && Input.GetAxisRaw ("Horizontal") == 0) {
				_bulletScript._direction = new Vector2 (0, Input.GetAxisRaw ("Vertical"));
			}
			else if(Input.GetAxisRaw ("Vertical") == -1)
				_bulletScript._direction = new Vector2 (_bulletDirection, 0);
			else {
				_bulletScript._direction = new Vector2 (_bulletDirection, Input.GetAxisRaw ("Vertical"));
			}
				_bulletScript.SetDirection (_projectileSpeed);
				_newTime = Time.time + (1 / _fireRate);
				_ammoLeftover--;
				transform.localScale = new Vector3 (transform.localScale.x - _playerShrinkScale, transform.localScale.y - _playerShrinkScale, 1);
				_shooting = true;
			}
		else {
				_shooting = false;
		}
	}


	void Jump(){
		if (Input.GetButtonDown("Jump") && _jumping == false) {
			_jumping = true;
			_originalHeight = transform.position.y;
			_parabolaPosition = Mathf.Sqrt (_jumpHeight * Mathf.Abs (_jumpSpeed)) / _jumpSpeed;
			_parabolaPosition = Time.deltaTime + _parabolaPosition;
			_newTransformY = _jumpSpeed * Mathf.Pow (_parabolaPosition, 2) + _jumpHeight + _originalHeight;
			transform.position = new Vector2(transform.position.x, _newTransformY);

		}else if(_jumping == true){
			_parabolaPosition = Time.deltaTime + _parabolaPosition;
			_newTransformY = _jumpSpeed * Mathf.Pow (_parabolaPosition, 2) + _jumpHeight + _originalHeight;
			transform.position = new Vector2(transform.position.x, _newTransformY);
		}
	}



	void SetAnim(){
		_anim.SetBool("Jumping",_jumping);
		_anim.SetInteger ("Vertical", _vertical);
		_anim.SetInteger ("Horizontal", _horizontal);
		_anim.SetBool ("Damaged", _damaged);
		_anim.SetBool ("Shooting", _shooting);
	}

}
