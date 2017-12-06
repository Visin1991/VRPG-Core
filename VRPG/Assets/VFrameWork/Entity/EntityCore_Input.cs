using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace V
{
	public partial class EntityCore : ICore {

		public InputSys input;

		//============================== Override =======================================

		public void OVERRIDE_L_MOUSE_HOLD(Action<EntityCore> a){if (input == null) return; input.L_Mouse_Hold = null; input.L_Mouse_Hold += a;}
		public void OVERRIDE_L_MOUSE_DOWN(Action<EntityCore> a){if (input == null) return; input.L_Mouse_Down = null; input.L_Mouse_Down += a;}
		public void OVERRIDE_L_MOUSE_UP(Action<EntityCore> a){if (input == null) return; input.L_Mouse_Up = null; input.L_Mouse_Up += a;}



		public void OVERRIDE_Key_A_Hold(Action<EntityCore> a){if (input == null) return; input.Key_A_Hold = null; input.Key_A_Hold += a;}
		public void OVERRIDE_Key_A_Down(Action<EntityCore> a){if (input == null) return; input.Key_A_Down = null; input.Key_A_Down += a;}
		public void OVERRIDE_Key_A_Up(Action<EntityCore> a){if (input == null) return; input.Key_A_Up = null; input.Key_A_Up += a;}

		//============================== Invoke =======================================
		//Left Mouse
		public void Invoke_L_Mouse_Hold(){if (input == null) return; if (input.L_Mouse_Hold != null) {input.L_Mouse_Hold (this);}}
		public void Invoke_L_Mouse_Down(){if (input == null) return; if (input.L_Mouse_Down != null) {input.L_Mouse_Down (this);}}
		public void Invoke_L_Mouse_Up(){if (input == null) return; if(input.L_Mouse_Up!=null){input.L_Mouse_Up(this);}}



		public void Invoke_Key_A_Hold(){if (input == null) return; if (input.Key_A_Hold != null) {input.Key_A_Hold(this);}}
		public void Invoke_Key_A_Down(){if (input == null) return; if (input.Key_A_Down != null) {input.Key_A_Down(this);}}
		public void Invoke_Key_A_Up(){if (input == null) return; if (input.Key_A_Up != null) {input.Key_A_Up(this);}}

		//============================== Connect System ==============================
		/// <summary>
		/// Bind this Entity to the system Input event
		/// </summary>
		public void Connect_System_Input(){
			//Debug.Log("Connect System Input");
			SystemInput.Instance.OVERRIDE_SYSTEM_INPUT (this);
		}


	}
}
