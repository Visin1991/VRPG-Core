using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraBase : Component {
	
	public abstract System.Type CameraType{ get;}
	public Transform Transform{get { return transform;}}

	public abstract void Init_Camera();
	public abstract void Update_Camera();
	public abstract void LateUpdate_Camera();
	public abstract void SetCameraDelta();

	public float yaw;
	public float pitch;

}
