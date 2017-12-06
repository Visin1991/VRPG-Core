using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
	public class AK_Installer : RTDependencyInstaller {

		[DependencyInjector]
		public AK_47 ak47;

		public override void Install(EntityCore platformer)
		{
			//Check Dependency
			bool success = platformer.ContainsSystemCore<AnimationSys>();
			GunSystem s = platformer.GetSystemCore<GunSystem>();
			success &= (s != null);

			if (success) {
				//Check if the Entity Know how to use the Gun..........
				success &= s.InitSystemWith<AK_Installer>(entityCore);
			}

			if (success) {
                //When platformer left mouse down----> the ak47.shoot.
                platformer.OVERRIDE_L_MOUSE_HOLD(ak47.OnTriggerHold);
                Debug.Log ("Success");
			} else {
				Debug.Log ("Fail");
			}

			
		}

		public override System.Type Type {
			get {
				return typeof(AK_Installer);
			}
		}


		[ContextMenu("RecordDepencency")]
		public void ET_RecordDependency()
		{
			GunSystem gunSys = transform.root.GetComponentInChildren<GunSystem>();
			if (gunSys != null) {		
				gunSys.Recoder_Gun_DependencyInfo(typeof(AK_Installer), transform.localPosition, transform.localRotation);
			}
		}
	}
}
