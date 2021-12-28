using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformOnOff : MonoBehaviour {

	public GameObject _player;
	public int _waitFrames;
	public int _frames;

	private CapsuleCollider2D _playerCol;
	private BoxCollider2D _box;

	// Use this for initialization
	void Start () {
		_box = GetComponent<BoxCollider2D> ();
		_playerCol = _player.GetComponent<CapsuleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_player.transform.position.y > transform.position.y + _box.bounds.extents.y + _playerCol.bounds.extents.y) {
			_box.enabled = true;
			_frames = 0;
		} else {
			_frames++;
			if (_waitFrames == _frames) {
				_box.enabled = false;
			}
		}
	}
}
