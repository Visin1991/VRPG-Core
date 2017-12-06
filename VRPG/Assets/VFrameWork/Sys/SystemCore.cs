using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace V
{
	//Dependency Injection
	public abstract class SystemCore : ICore {

		[SerializeField]protected EntityCore entityCore;


		//this function will be call at editor time by default
		public void ET_Sys_Init_Default(EntityCore _entity){
			entityCore = _entity;
			entityCore.AddSystemCore(this);
		}

	}
}