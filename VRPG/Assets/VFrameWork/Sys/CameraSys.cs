using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{

	public class CameraSys : SystemCore{

		[DependencyInjector]
		public VCamera CurrentCamera;

        public bool Freeze_Yaw;

        public bool Freeze_Pitch;

        public override System.Type Type {get {return typeof(CameraSys);}}

        public float Yaw { get { return CurrentCamera.Yaw; } }

        public void LateUpdate()
        {
            if (SystemInput.Instance.MouseScroll != 0 || SystemInput.Instance.MouseHorizontal != 0 || SystemInput.Instance.MouseVertical != 0)
            {
                CameraDelta delta = new CameraDelta();
                delta.delta_pitch = SystemInput.Instance.MouseVertical;
                delta.delta_yaw = SystemInput.Instance.MouseHorizontal;
                delta.delta_dstToTarget = SystemInput.Instance.MouseScroll;
                SetCameraDelta(delta);
            }
            CurrentCamera.LateUpdateCamera();
        }

        public void SetCameraDelta(CameraDelta delta)
        {
            if (Freeze_Yaw) { delta.delta_yaw = 0.0f; }
            if (Freeze_Pitch) { delta.delta_pitch = 0.0f; }
            CurrentCamera.SetCameraDetal(delta);
        }


    }
}
