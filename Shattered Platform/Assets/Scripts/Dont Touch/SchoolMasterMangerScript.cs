using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolMasterMangerScript : MonoBehaviour {



	//Health and phases
	public int currentHealth;

	public int perPhaseHealth = 50;
	private Animator _anim;
	private string _SSD;

	//Attacks
	private int phaseNumber = 0;
	public int currentPhase;
	public GameObject[] attacks;


	void Awake(){
		_anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		currentHealth = perPhaseHealth;
		SetPhase (currentPhase - 1);
	}
	
	// Update is called once per frame
	void Update () {
		switch (_SSD) {
		default:

			break;
		}
	}


	//Picks an attack determined by the attackNumber variable and assigns it while also turning off any other attack;
	void SetPhase(int phaseIndex){
		//disable current phase
		attacks[phaseNumber].SetActive(false);

		phaseNumber = phaseIndex;
		currentPhase = phaseNumber + 1;
		//enable new phase
		attacks[phaseIndex].SetActive(true);

	}


	public void Damage(){
		currentHealth = currentHealth - 1;
		if (phaseNumber > attacks.Length && currentHealth <= 0){
			_SSD = "Dead";
			_anim.SetTrigger("Dead");
		}
		else if (currentHealth <= 0) {
			currentHealth = perPhaseHealth;
			SetPhase(phaseNumber);
		}
	}	
}
