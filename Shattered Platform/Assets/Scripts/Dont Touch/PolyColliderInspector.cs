	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

[ExecuteInEditMode]
	public class PolyColliderInspector : MonoBehaviour {


	private int derp;
	private PolyColliderSet _col;


	void Awake(){
		_col = GetComponent<PolyColliderSet> ();
		derp = _col._currentFrameNumber;

	}
		
	void Update(){
		derp = _col._currentFrameNumber;
	}
			
	void OnDrawGizmos(){
		Gizmos.color = Color.red;

		if (_col._attackPolyPoints.Length - 1 >= _col._currentFrameNumber && _col._currentFrameNumber >= 0){
				
			for (int i = 1; i < _col._attackPolyPoints [derp].Points.Length; i++) {
				Gizmos.DrawLine (_col._attackPolyPoints [derp].Points [i - 1], _col._attackPolyPoints [derp].Points [i]);
		
			}
			Gizmos.DrawLine (_col._attackPolyPoints [derp].Points [(_col._attackPolyPoints [derp].Points.Length - 1)], _col._attackPolyPoints [derp].Points [0]);
		
			}
		}



}