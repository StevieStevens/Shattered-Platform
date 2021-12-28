using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	public GameObject _pauseMenu;

	public bool _active;
	public bool _animator;
	public bool _rigidbody;
	public bool _collider;
	public bool _sound;
	public bool _script;


	private bool _enabled = false;

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			PauseSelected ();
		}

	}

	void PauseSelected(){
		if (_enabled) {
			_pauseMenu.SetActive (false);

			Time.timeScale = 1;

			if (_active)
				gameObject.SetActive (true);
			if (_rigidbody) {
				GetComponent<Rigidbody2D> ().simulated = true;
			}
			if (_animator)
				GetComponent<Animator> ().enabled = true;
			if (_sound)
				GetComponent<AudioSource> ().enabled = true;
			if (_script)
				GetComponent<SchoolMasterMangerScript> ().enabled = true;
			if (_collider)
				GetComponent<PolygonCollider2D> ().enabled = true;
			_enabled = false;



		} else {
			_pauseMenu.SetActive (true);

			Time.timeScale = 0;

			if (_active)
				gameObject.SetActive (false);
			if (_rigidbody) 
				GetComponent<Rigidbody2D> ().simulated = false;
			if (_animator)
				GetComponent<Animator> ().enabled = false;
			if (_sound)
				GetComponent<AudioSource> ().enabled = false;
			if (_script)
				GetComponent<SchoolMasterMangerScript> ().enabled = false;
			if (_collider)
				GetComponent<PolygonCollider2D> ().enabled = false;
			_enabled = true;

		}
	}
}
