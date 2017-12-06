using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
	public abstract class VCamera : Driver{
		[DependencyInjector]
		public CameraSys cameraSys;
		public abstract void LateUpdateCamera();
        public abstract float Yaw { get; }
        public abstract void SetCameraDetal(CameraDelta delta);
    }

	public enum CameraType { FirstPerson, ThridPerson, God }
	public enum CameraRotateModel { Fixed,Free,KeyBoardRestrict}

	public struct CameraDelta
	{
		public float delta_yaw;
		public float delta_pitch;
		public float delta_dstToTarget;

	}
}

