using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
	public class CameraTarget : TFTarget  {
		public override System.Type Type {
			get {
				return typeof(CameraType);
			}
		}
	}
}
