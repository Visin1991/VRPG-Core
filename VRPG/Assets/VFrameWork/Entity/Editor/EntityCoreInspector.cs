using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace V{
	[CustomEditor(typeof(EntityCore))]
	public class EntityCoreInspector : Editor {
		

		public override void OnInspectorGUI ()
		{
			EntityCore entityCore = (EntityCore)target;
			base.OnInspectorGUI ();

			if (GUILayout.Button ("Editor Time Checking")) {
				entityCore.EditorTime_Invalid_Checking ();
			}
		}

	}
}