using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
	/// <summary>
	/// This is a Dependency Injector..... mainly used for test in Editor Time
	/// </summary>
	[System.AttributeUsage(System.AttributeTargets.Method | System.AttributeTargets.Field,AllowMultiple = false, Inherited = false)]
	public class DependencyInjector : System.Attribute
	{
		public bool optional;
		public DependencyInjector(bool _optional = false)
		{
			optional = _optional;
		}

		public override string ToString()
		{
			return "Attribute of DependencyInjector";
		}
	}
}