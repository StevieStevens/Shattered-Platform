using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PolyColliderSet : MonoBehaviour {


	public int _currentFrameNumber;
	public Vector2DArray[] _attackPolyPoints;
	private SpriteRenderer _SR;
	private PolygonCollider2D _polyCol;
	private string _frameCount;
	private char[] _spriteNumberEnding;


	[System.Serializable]
	public class Vector2DArray  {
		public Vector2[] Points;
	}

	// Use this for initialization
	void Awake () {
		_SR = GetComponent<SpriteRenderer> ();
		_polyCol = GetComponent<PolygonCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void SetPolyCollider(){
		_frameCount = _SR.sprite.name;
		_frameCount = _frameCount.Substring (_frameCount.Length - 2, 1);
		int.TryParse (_frameCount, out _currentFrameNumber);
		if (_polyCol.pathCount > _currentFrameNumber && _attackPolyPoints.Length > 0) {
			_polyCol.points = _attackPolyPoints [_currentFrameNumber].Points;
		}
	}


}
