using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerMoving : MonoBehaviour {

	public float Speed = 10;
	public int _Passes = 3;
	public float _DelayTime = 1;
	public GameObject _PlayerGameObject;
	public Transform Transform1;
	public Transform Transform2;

	private Animator _anim;
	private float pointsDistance;
	private string _SSD = "Moving";
	private Vector3 Point1;
	private Vector3 Point2;
	private float _Delay;
	private bool _add = false;
	private int _TotalPasses = 0;
	private bool _movingLeft = false;
	private Rigidbody2D _rb;
	private CircleCollider2D _col;

	private const string Moving = "Moving";
	private const string Delaying = "Delaying";
	private const string AttackDown = "AttackDown";
	private const string AttackUp = "AttackUp";

	void Awake(){
		Point1 = Transform1.position;
		Point2 = Transform2.position;
		_rb = GetComponent<Rigidbody2D> ();
		_col = GetComponent<CircleCollider2D> ();
		_anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		pointsDistance = (Point1.y - Point2.y);
		transform.position = new Vector2 (transform.position.x, Point1.y);
	}
	
	// Update is called once per frame
	void Update () {
		HammerMovingAttack ();
		


	}

	void HammerMovingAttack(){
		switch (_SSD) {
		default:
			_SSD = "Moving";
			_anim.SetTrigger ("Moving");
			break;
		case  Moving:
			Move ();
			if (_Passes <= _TotalPasses && transform.position.x - 2 < _PlayerGameObject.transform.position.x && transform.position.x + 2 > _PlayerGameObject.transform.position.x){
				_SSD = Delaying;
				_rb.velocity = new Vector2 (0, 0);
				HammerMovingAttack ();
			}
			break;
		case Delaying:
			_Delay = Time.time + _DelayTime;
			_SSD = AttackDown;
			_anim.SetTrigger ("Down");
			HammerMovingAttack ();
			break;
		case AttackDown:
			if (_Delay < Time.time && transform.position.y > Point2.y) {
				_col.enabled = true;
				_rb.velocity = new Vector2 (0, -Speed * 2);
			} else if (_Delay < Time.time) {
				_SSD = AttackUp;
				_anim.SetTrigger ("Up");
				HammerMovingAttack ();
			}
			break;
		case AttackUp:
			if (_Delay < Time.time && transform.position.y < Point1.y) {
				_rb.velocity = new Vector2 (0, Speed);
				if (transform.position.y > (Point1.y - (pointsDistance / 1.5f))) {
					_col.enabled = false;
				}
			} else {

				transform.position = new Vector2 (transform.position.x, Point1.y);
				_col.enabled = false;
				_rb.velocity = new Vector2 (0,0);
				_TotalPasses = 0;
				_SSD = Moving;
				_anim.SetTrigger ("Moving");
				HammerMovingAttack ();
			}
			break;
		}
	}



	void Move(){
		if (Point1.x < transform.position.x && _movingLeft == true) {
			if (_PlayerGameObject.transform.position.x > transform.position.x && _add == true){
				++_TotalPasses;
				_add = false;
			}
			_rb.velocity = new Vector2 (-Speed, 0);
		}
		else if (Point2.x > transform.position.x && _movingLeft == false) {
			if (_PlayerGameObject.transform.position.x < transform.position.x && _add == true){
				++_TotalPasses;
				_add = false;
			}
			_rb.velocity = new Vector2 (Speed, 0);
		}
		else {
			_movingLeft = !_movingLeft;

			transform.localScale = new Vector2 (transform.localScale.x * -1, transform.localScale.y);
			_add = true;
		}
			


	}
}
