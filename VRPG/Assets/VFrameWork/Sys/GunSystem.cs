using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
	//
	public class GunSystem : SystemCore {

		[SerializeField]
		public List<GunDependentInfo> gunDependencyInfos = new List<GunDependentInfo>();

		public override System.Type Type {get {return typeof(GunSystem);}}


		public bool InitSystemWith<T>(EntityCore other) where T : RTDependencyInstaller
		{
			for (int i = 0; i < gunDependencyInfos.Count; i++)
			{
				if (gunDependencyInfos [i].typeString == typeof(T).ToString()) {
					other.transform.SetParent(transform);
					other.transform.localPosition = gunDependencyInfos [i].transformOffset;
					other.transform.localRotation = gunDependencyInfos [i].rotationOffset;
					return true;
				}
			}
			Debug.LogErrorFormat("The Current GunSystem Don't know How to Install {0}",typeof(T));
			return false;
		}
			

		public void Recoder_Gun_DependencyInfo(System.Type type,Vector3 tfOffset, Quaternion rotOffset)
		{
			gunDependencyInfos.Add (new GunDependentInfo(type, tfOffset, rotOffset));
		}

	}

	[System.Serializable]
	public class GunDependentInfo
	{
		[SerializeField]
		public string typeString;
		[SerializeField]
		public Vector3 transformOffset;
		[SerializeField]
		public Quaternion rotationOffset;

		public GunDependentInfo(System.Type type,Vector3 tfOffset,Quaternion rotOffset)
		{
			Debug.Log(tfOffset);
			typeString = type.ToString ();
			transformOffset = tfOffset;
			rotationOffset = rotOffset;
		}
	}
}
