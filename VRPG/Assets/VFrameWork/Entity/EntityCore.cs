using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;


namespace V
{
	//Define Entity:
	public partial class EntityCore : ICore {
		public override Type Type {
			get {
				return typeof(EntityCore);
			}
		}

		public RTDependencyInstaller runtimeInstaller;

		public List<SystemCore> systems = new List<SystemCore>();

		public void Install_Entity_To_Target(EntityCore e)
		{
			runtimeInstaller.Install(e);
		}

		public T GetSystemCore<T>() where T : SystemCore
		{
			foreach (SystemCore s in systems) {
				if (s.Type == typeof(T)) {
					return s as T;
				}
			}
			#if UNITY_EDITOR
			Debug.LogErrorFormat("The Entity dont contains the Required [**** {0} ****] ",typeof(T));
			#endif
			return null;
		}

		public SystemCore GetSystemCore(System.Type t)
		{
			for(int i = 0;i <systems.Count;i++){
				if (systems[i].Type == t) {
					return systems[i];
				}
			}
			return null;
		}

		//Forsafe Check
		public void AddSystemCore<T>(T t) where T : SystemCore
		{
			for(int i = 0; i < systems.Count; i++)
			{
				if (systems[i] == null) {
					systems.RemoveAt(i);
					continue;
				}

				if (systems[i].Type == t.Type) {
					if (systems[i] != t) {
						Debug.LogErrorFormat ("The {0} is already exist in current entity. It attatck to the game Object of {1}",
							typeof(T).ToString (), systems[i].gameObject.name);
					}
					return;
				}
			}
			systems.Add(t);
		}

		public bool ContainsSystemCore<T>() where T: SystemCore
		{
			foreach (SystemCore s in systems) {
				if (s.Type == typeof(T)) {
					return true;
				}
			}
			#if UNITY_EDITOR
			Debug.LogErrorFormat("The Entity dont contains the Required [**** {0} ****] ",typeof(T));
			#endif
			return false;
		}

		[ContextMenu("Init Entity")]
		public void EditorTime_Invalid_Checking()
		{
			EntityCore[] entities = GetComponentsInChildren<EntityCore>();

			if (entities.Length > 1) {
				Debug.LogError ("There are more than One EntittCore Attach to the Object at Editor Time");
				return;
			}

			InputSys inputSys = GetComponentInChildren<InputSys>();
			if (inputSys != null) {input = inputSys;}

			systems.Clear ();

			//Find All ICore Object
			ICore[] cores = GetComponentsInChildren<ICore>();

			//Find All System
			foreach (ICore s in cores)
			{
				SystemCore sys = s as SystemCore;
				if (sys != null) {
					sys.ET_Sys_Init_Default(this);
				}
			}
				
		
			foreach (ICore c in cores) {

				//Check==========Field Dependency Injector=================

				FieldInfo[] fieldInfos = c.Type.GetFields (BindingFlags.Public | BindingFlags.Instance);
				for (int i = 0; i < fieldInfos.Length; i++)
				{
					object[] attrs = fieldInfos [i].GetCustomAttributes (typeof(DependencyInjector), false);
					if (attrs.Length > 0) {
						//Debug.LogFormat("From the Class of {0} ,The Member name is {1} , has Attribute of {2}",s.Type.FullName,fieldInfos[i].Name,"DependencyInjector");
						Type t = fieldInfos [i].FieldType;
						//Debug.Log (t);
						if (t.IsSubclassOf (typeof(SystemCore))) {
							SystemCore core = GetSystemCore (t);
							if (core != null) {
								//Debug.Log("What ???");
								fieldInfos [i].SetValue (c, core);
							} else {
								if (((DependencyInjector)attrs [0]).optional == true) {
									Debug.LogWarningFormat ("from Scene Object of{1}------(Sys::{0}--field::{3})-------Cannot get--> ({2}) from the Entity", c.Type.FullName, ReversePath (c.transform), t, fieldInfos [i].FieldType + " :: " + fieldInfos [i].Name);
								} else {
									Debug.LogErrorFormat ("from Scene Object of{1}------(Sys::{0}--field::{3})-------Cannot get--> ({2}) from the Entity", c.Type.FullName, ReversePath (c.transform), t, fieldInfos [i].FieldType + " :: " + fieldInfos [i].Name);
								}
							}
						} 
						else if (t == (typeof(EntityCore))) {
							fieldInfos [i].SetValue (c, this);
						} 
						else if (t.IsSubclassOf(typeof(TFTarget))) {
							TFTarget target = c.transform.root.GetComponentInChildren(t) as TFTarget;
							if (target != null) {
								fieldInfos [i].SetValue (c, target);
							} else {
								if (((DependencyInjector)attrs [0]).optional == true) {
									Debug.LogWarningFormat ("from Scene Object of{1}------(Sys::{0}--field::{3})-------Cannot get--> ({2}) from the Entity", c.Type.FullName, ReversePath (c.transform), t, fieldInfos [i].FieldType + " :: " + fieldInfos [i].Name);
								} else {
									Debug.LogErrorFormat ("from Scene Object of{1}------(Sys::{0}--field::{3})-------Cannot get--> ({2}) from the Entity", c.Type.FullName, ReversePath (c.transform), t, fieldInfos [i].FieldType + " :: " + fieldInfos [i].Name);
								}
							}
						}
						else if (t.IsSubclassOf (typeof(Component))) {
							Component com = c.gameObject.GetComponent (t);
							if (com != null) {
								fieldInfos [i].SetValue (c, com);
							} else {
								if (((DependencyInjector)attrs [0]).optional == true) {
									Debug.LogWarningFormat ("from Scene Object of{1}------(Sys::{0}--field::{3})-------Cannot get--> ({2}) from the Entity", c.Type.FullName, ReversePath (c.transform), t, fieldInfos [i].FieldType + " :: " + fieldInfos [i].Name);
								} else {
									Debug.LogErrorFormat ("from Scene Object of{1}------(Sys::{0}--field::{3})-------Cannot get--> ({2}) from the Entity", c.Type.FullName, ReversePath (c.transform), t, fieldInfos [i].FieldType + " :: " + fieldInfos [i].Name);
								}
							}
						} else {
							Debug.LogErrorFormat ("from Scene Object of{1}------(Sys::{0}--field::{3})-----> ({2}) has a @@@invalid Attribute@@@", c.Type.FullName, ReversePath (c.transform), t, fieldInfos [i].FieldType + " :: " + fieldInfos [i].Name);
						}
					}
				}

				//Check==========Method Dependency Injector=================

				//1. Get all Public Method
				MethodInfo[] infos = c.Type.GetMethods (BindingFlags.Public | BindingFlags.Instance); //We need eigther to choose it is a Instance function or static function 

				foreach (MethodInfo m in infos) {
				
					//2. If The Method has the Dependency Injector Attribute
					if (m.GetCustomAttributes (typeof(DependencyInjector), false).Length > 0) {

						//Debug.Log(m);

						//3. Get All Parapeters of the Method
						ParameterInfo[] pInfos = m.GetParameters ();
						object[] objs = new object[pInfos.Length];
						bool erro = false;

						for (int i = 0; i < pInfos.Length; i++) {
						
							//4. Check if Parameter are Subclass of SystemCore
							if (pInfos [i].ParameterType.IsSubclassOf (typeof(SystemCore))) {
							
								Type t = pInfos [i].ParameterType;
								//5. Check if we can Get the System from Our Entity
								SystemCore core = GetSystemCore (t);

								if (core != null) {
									objs [i] = core;
								} else {
									erro = true;
									Debug.LogErrorFormat ("The [ {0} ]from the {1} Cannot get {2} from the Entity", c.Type.FullName, ReversePath (c.transform), t);
									break;
								}
							}
							//Check if the Parameter is a subclass of a component
							else if (pInfos [i].ParameterType.IsSubclassOf (typeof(Component))) {
								Type t = pInfos [i].ParameterType;
								Component com = c.gameObject.GetComponent(t);
								if (com != null) {
									objs [i] = com;
								}else {
									erro = true;
									Debug.LogErrorFormat ("The [ {0} ]from the {1} Cannot get {2} from the Entity", c.Type.FullName, ReversePath (c.transform), t);
									break;
								}
							}
							//The Parameter is a invalid Input
							else {
								erro = true;
								Debug.LogErrorFormat ("The [ {0} :: {2}] from the {1} Contains some invalid Parameters", c.Type.FullName, ReversePath (c.transform), m.Name);
								//if anyparameter is not a child of SystemCore, We break
								break;
							}
						}

						if (!erro) {
							m.Invoke (c, objs);
						}
					}
				}
			}
		}

		string ReversePath(Transform tf)
		{
			string path = "(";
			for(;;){
				path = path + "-->" + tf.name;
				if (tf.parent == null) {
					path += ")";
					break;
				}
				tf = tf.parent;
			}
			return path;
		}

	}
}
