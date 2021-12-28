using System.Collections;
using System.Collections.Generic;
using UnityEditor;




[CanEditMultipleObjects]
public class SchoolMasterTool : Editor {





	public override void OnInspectorGUI(){
		
		//remove point from the inspector

		SchoolMasterMangerScript myTarget = (SchoolMasterMangerScript)target;

		serializedObject.Update ();

		serializedObject.ApplyModifiedProperties ();
	}




}
