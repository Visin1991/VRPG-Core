using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
	public class RiggidBodyController : TransitionDriver {

		[DependencyInjector]
		public Rigidbody rig;

		public float force;

		public override System.Type Type {
			get {
				return typeof(RiggidBodyController);
			}
		}

		public override void Set_SetTransitionVH(Vector3 transitionVH)
		{
			//Debug.Log(transitionVH);
			transitionVH = transitionVH * force * Time.deltaTime;
			rig.AddForce(transitionVH);
		}

	}
}
	
