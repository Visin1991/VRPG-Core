using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
	public abstract class RTDependencyInstaller : ICore {
		[SerializeField]protected EntityCore entityCore;
		public abstract void Install (EntityCore e);
	}
}
