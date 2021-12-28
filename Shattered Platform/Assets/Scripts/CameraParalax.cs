using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParalax : MonoBehaviour {

	public Transform _leftBound;
	public Transform _rightBound;
	public float _foregroundParalaxPercent = 80;
	public float _backgroundParalaxPercent = 40;
	public GameObject _player;
	public GameObject _background;

	private float _middlePointX;
	private float _totalDistance;
	private float _totalForegroundDistance;
	private float _totalBackgroundDistance;
	private float _totalPlayerDistance;
	private float _minPlayerPosX;
	private float _ratio;
	private float _maxCamPosX;
	private float _minCamPosX;
	private float _newCamPosX;
	private Camera _cam;

	void Awake(){ 
		_cam = GetComponent<Camera> ();
		_middlePointX = (Mathf.Abs(_leftBound.position.x - _rightBound.position.x) / 2f) + _leftBound.position.x;
		_totalDistance = Mathf.Abs(_rightBound.position.x - _leftBound.position.x);
		_totalForegroundDistance = _totalDistance - (14.6f);
		_totalPlayerDistance = Mathf.Abs(_rightBound.position.x - _leftBound.position.x) * (_foregroundParalaxPercent / 100);
		_totalBackgroundDistance = _totalForegroundDistance * (_backgroundParalaxPercent / 100);//Mathf.Abs(_rightBound.position.x - _leftBound.position.x) * (_backgroundParalaxPercent / 100);

		_minPlayerPosX = _middlePointX - (_totalPlayerDistance / 2);
		_minCamPosX = _middlePointX - (_totalForegroundDistance / 2);
		_maxCamPosX = _middlePointX + (_totalForegroundDistance / 2);
		SetCameraPostion ();
	}


	// Update is called once per frame
	void Update () {
		SetCameraPostion ();
	}

	void SetCameraPostion(){
		_ratio = (_player.transform.position.x - _minPlayerPosX) / _totalPlayerDistance;
		_newCamPosX = (_ratio * _totalForegroundDistance) + _minCamPosX;
		if(_background != null)
			_background.transform.position = new Vector2((_ratio * _totalBackgroundDistance) + (_middlePointX - (_totalBackgroundDistance / 2)), _background.transform.position.y);
		if (_newCamPosX > _maxCamPosX)
			_newCamPosX = _maxCamPosX;
		if (_newCamPosX < _minCamPosX)
			_newCamPosX = _minCamPosX;
			_cam.transform.position = new Vector3(_newCamPosX, _cam.transform.position.y , _cam.transform.position.z);

	}
}
