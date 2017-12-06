using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace V
{
	public class InputSys : SystemCore{

        public override Type Type { get { return typeof(InputSys);}}

        public bool connectSystemInput = false;

		void Start()
		{
			if (connectSystemInput) {
				entityCore.Connect_System_Input ();
			}
		}
		 
		// 4 * 3 * 3 = 36 byte
		//if we have 32 keys -----> 4 * 32 * 3 = 384 bytes
		//if we have 1000 entity in our scene-----> 384000-----> 370.5 kb.
		public Action<EntityCore> L_Mouse_Hold;
		public Action<EntityCore> L_Mouse_Down;
		public Action<EntityCore> L_Mouse_Up;

		/*
		public Action R_Mouse_Hold;
		public Action R_Mouse_Down;
		public Action R_Mouse_Up;

		public Action M_Mouse_Hold;
		public Action M_Mouse_Down;
		public Action M_Mouse_Up;
		*/

		public Action<EntityCore> Key_A_Hold;
		public Action<EntityCore> Key_A_Down;
		public Action<EntityCore> Key_A_Up;

	}
}
