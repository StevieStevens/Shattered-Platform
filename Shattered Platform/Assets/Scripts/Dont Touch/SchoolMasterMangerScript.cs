using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolMasterMangerScript : MonoBehaviour {



	//Health and phases
	public int _health;
	public int _currentPhase = 2;

	private int _newHealth;
	private Animator _anim;
	private string _SSD;

	//Attacks
	public int attackNumber;
	public GameObject attack1;
	public GameObject attack2;
	public GameObject attack3;



	void Awake(){
		_anim = GetComponent<Animator> ();
		_newHealth = _health;
	}

	// Use this for initialization
	void Start () {
		NewAttack ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (_SSD) {
		default:

			break;
		}
	}


	//Picks an attack determined by the attackNumber variable and assigns it while also turning off any other attack;
	void NewAttack(){
		switch(attackNumber){

		case 1:
			attack1.SetActive(true);
			attack2.SetActive(false);
			attack3.SetActive(false);
			break;
		case 2:
			attack1.SetActive(false);
			attack2.SetActive(false);
			attack3.SetActive(true);
			break;
		case 3:
			attack1.SetActive(false);
			attack2.SetActive(true);
			attack3.SetActive(false);
			break;
		}
		attackNumber++;
	}


	public void Damage(){
		_health = _health - 1;
		if (_currentPhase == 0 && _health == 0){
			_SSD = "Dead";
			_anim.SetTrigger("Dead");
		}
		else if (_health == 0) {
			_currentPhase--;
			_health = _newHealth;
			NewAttack ();
		}
	}	
}
