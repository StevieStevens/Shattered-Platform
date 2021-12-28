using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerStationary : MonoBehaviour {
	public float Speed;
	public float _DelayTime = 1;
	public GameObject _PlayerGameObject;
	public Transform Transform1;
	public Transform Transform2;

	//private Animator _anim;
	private bool _delayAdded = false;
	private float _newPosition;
	private int _positionNormalized;
	private float _pointsDistanceX;
	private float _pointsDistanceY;
	private string _SSD = "Moving";
	private Vector3 Point1;
	private Vector3 Point2;
	private float _delay;
	private Rigidbody2D _rb;
	private CircleCollider2D _col;
	private AudioSource _audio;

	private const string Moving = "Moving";
	private const string Delaying = "Delaying";
	private const string AttackDown = "AttackDown";
	private const string AttackUp = "AttackUp";

	void Awake(){
		//_anim = GetComponent<Animator> ();
		Point1 = Transform1.position;
		Point2 = Transform2.position;
		_rb = GetComponent<Rigidbody2D> ();
		_col = GetComponent<CircleCollider2D> ();
		_audio = GetComponent<AudioSource> ();
		transform.position = new Vector2 (transform.position.x, Point1.y);
	}
	// Use this for initialization
	void Start () {
		_pointsDistanceY = Mathf.Abs(Point1.y - Point2.y);
		_pointsDistanceX = Mathf.Abs (Point1.x - Point2.x);
	}

	// Update is called once per frame
	void Update () {
		SSD ();



	}

	void SSD(){
		switch (_SSD) {
		default:
			_SSD = "Moving";
			break;
		case  Moving:
			Move ();
			_SSD = Delaying;
			break;
		case Delaying:
			if (!_delayAdded) {
				_delay = Time.time + _DelayTime;
				_delayAdded = true;
			}
			if (_delay < Time.time) {
				_SSD = AttackDown;
				SSD ();
				_delayAdded = false;
			}
			break;
		case AttackDown:
			if (transform.position.y > Point2.y) {
				//_anim.SetTrigger ("Down");
				_col.enabled = true;
				_rb.velocity = new Vector2 (0, -Speed * 2);
				if (_audio != null) {
					_audio.Play();
				}
			} else {
				_SSD = AttackUp;
//				_anim.SetTrigger ("Up");
				SSD ();
			}
			break;
		case AttackUp:
			if (transform.position.y < Point1.y) {
				_rb.velocity = new Vector2 (0, Speed);
				if (transform.position.y > (Point1.y - (_pointsDistanceY / 1.5f))) {
					_col.enabled = false;
				}
			} else {
				_col.enabled = false;
				_rb.velocity = new Vector2 (0,0);
				_SSD = Moving;
				SSD ();
			}
			break;
		}
	}



	void Move(){

		_positionNormalized = Random.Range (1, 4);
		switch (_positionNormalized) {
		case 1:

		//	_anim.SetTrigger ("Left");
			break;
		case 2:
		//	_anim.SetTrigger ("Middle");
			break;
		case 3:

		//	_anim.SetTrigger ("Right");
			break;

		}
		_newPosition = ((_pointsDistanceX / 4) * _positionNormalized) + Point1.x;
		transform.position = new Vector2 (_newPosition, transform.position.y);

	}
}
