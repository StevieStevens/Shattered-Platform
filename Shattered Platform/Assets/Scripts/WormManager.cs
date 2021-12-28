using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormManager : MonoBehaviour {

	public float _delayTime = 2;
	public GameObject _1_worm;
	public GameObject _2_worm;
	public GameObject _3_worm;

	private float _newDelay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(_newDelay < Time.time)
		ChooseWorm ();
	}

	void ChooseWorm(){

		switch (Random.Range(1, 4)){
		case 1:
			_1_worm.SetActive (true);
			_1_worm.GetComponent<WaitingForTheWorms> ().SetDelay ();
			break;
		case 2:
			_2_worm.SetActive (true);
			_2_worm.GetComponent<WaitingForTheWorms> ().SetDelay ();
			break;
		case 3:
			_3_worm.SetActive (true);
			_3_worm.GetComponent<WaitingForTheWorms> ().SetDelay ();
			break;
		}
		_newDelay = Time.time + _delayTime;
	}
}
