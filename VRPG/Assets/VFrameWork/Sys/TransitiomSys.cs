using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityEngine.AI;


namespace V
{
	public class TransitiomSys : SystemCore {

		[DependencyInjector]
		public TransitionDriver driver;

		public TransitionControlType controlType = TransitionControlType.RootMotion;

		[DependencyInjector(true)]
		public CameraSys cameraSys;

		private Vector3 transitionVH;

		public bool freezeY = true;

        public Transform mainBody;

		public override System.Type Type {
			get {
				return typeof(TransitiomSys);
			}
		}
			
		public void Update()
		{
			if (controlType == TransitionControlType.RootMotion) {
				//If we use Root Motion, We just do not do any transition to our Object
			} else if (controlType == TransitionControlType.NavMesh) {
				//Update NavMesh...........
			} else if (controlType == TransitionControlType.Forward_Camera) {
				UpdateTransitionBasedOn_Camera_Forward ();
			} else if (controlType == TransitionControlType.Forward_MainBody) {
				//
			} else if (controlType == TransitionControlType.InputDir) {
				//
			}

		}

        //This function Used Only for the Player
		void UpdateTransitionBasedOn_Camera_Forward()
		{
			if (SystemInput.Instance.CurrentVH != Vector2.zero) {
				Vector3 right = cameraSys.CurrentCamera.transform.right;
				Vector3 forward = Quaternion.AngleAxis(-90,Vector3.up) * right;

				transitionVH = (forward * SystemInput.Instance.CurrentVH.y + right * SystemInput.Instance.CurrentVH.x);
				//Debug.Log (transitionVH);
				if (freezeY) {
					transitionVH.y = 0.0f;
					transitionVH.Normalize ();
				} else {
					transitionVH.Normalize ();
				}
				driver.Set_SetTransitionVH(transitionVH);
			}
		}

        //Only Used for Player
        //Main Body is the transform which contain the animator....
        public void UpdateTransitionBasedOn_MainBody_forward()
        {
            if (SystemInput.Instance.CurrentVH != Vector2.zero)
            {
                Vector3 right = mainBody.transform.right;
                Vector3 forward = Quaternion.AngleAxis(-90, Vector3.up) * right;
             
                
                transitionVH = (forward * SystemInput.Instance.CurrentVH.y + right * SystemInput.Instance.CurrentVH.x);

                if (freezeY) { transitionVH.y = 0.0f; transitionVH.Normalize(); }
                else { transitionVH.Normalize(); }

                driver.Set_SetTransitionVH(transitionVH);
            }
        }


    }
		
	public enum TransitionDriverType
	{
		Non,
		RigidBody,
		CharacterController,
	}

	public enum TransitionControlType
	{
		RootMotion,
		Forward_Camera,
		NavMesh,
		InputDir,
		Forward_MainBody
	}

}
