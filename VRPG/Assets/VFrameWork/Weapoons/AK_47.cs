using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
	public class AK_47 : Gun {

		public override System.Type Type {get {return typeof(AK_47);}}

		[DependencyInjector]
		public GunMuzzl muzz;

		public void OnTriggerHold(EntityCore other)
		{
			//Debug.LogFormat ("The {0} is shooting the Gun AK_47", other.name);
			GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			bullet.transform.rotation = muzz.transform.rotation;
			bullet.transform.position = muzz.transform.position;
			Rigidbody rg = bullet.AddComponent<Rigidbody> ();
			rg.AddForce (bullet.transform.forward * 10000);
		}

		public void OnTriggerRelease()
		{

		}

	}
}
