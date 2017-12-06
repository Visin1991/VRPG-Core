using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
	public class InventorySys : SystemCore {

		public AnimationSys animationSys;

		public EntityCore item;

		public override System.Type Type {
			get {
				return typeof(InventorySys);
			}
		}

		void Start()
		{
			entityCore.OVERRIDE_Key_A_Down(Key_A_DOWN);
		}

		public void Key_A_DOWN(EntityCore entity)
		{
			Debug.Log("Key A Down");
			if (item != null) {
				EquipItem (item);
			}
		}



		[DependencyInjector]
		public void GetAnimationSys(AnimationSys s)
		{
			animationSys = s;
		}

		public void EquipItem(EntityCore item)
		{
			//Before we need to use the item, We need to Install Driver on the Entity......
			item.Install_Entity_To_Target(entityCore);
		}
	}

}
