using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
	public class RotationSys : SystemCore {

        [DependencyInjector(true)]
        public CameraSys cameraSys;

        [DependencyInjector]
        public MainBody mainBody;

        public float turnReactionSpped = 0.1f;
        float turnSmoothVelocity;

        public RotationControlType controlType = RotationControlType.Look_To_CameraDir;

        public override System.Type Type {
			get {
				return typeof(RotationSys);
			}
		}

        public void Update()
        {
            if (controlType == RotationControlType.Look_At_MouseDir)
            {
                
            }
            else if (controlType == RotationControlType.Look_To_CameraDir)
            {
                Turn_To_CameraDir_Smooth();
            }
            else
            {
                //.....
            }
        }

        //When player press W the character will move forwad direction of the camera
        //When player press A the character will move left direction of the camera
        protected void Turn_To_CameraDir_Smooth()
        {
            if (SystemInput.Instance.CurrentVH == Vector2.zero) { return; }
            float targetDegree = Mathf.Atan2(SystemInput.Instance.CurrentVH.x, SystemInput.Instance.CurrentVH.y) * Mathf.Rad2Deg + cameraSys.CurrentCamera.Yaw; // cameramanager.CurrentCamera.Transform.eulerAngles.y;
            mainBody.transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(mainBody.transform.eulerAngles.y, targetDegree, ref turnSmoothVelocity, turnReactionSpped);
        }

    }

    public enum RotationControlType
    {
        Look_At_MouseDir,
        Look_To_CameraDir,
        Non_Strafe_And_Back
    }

}
