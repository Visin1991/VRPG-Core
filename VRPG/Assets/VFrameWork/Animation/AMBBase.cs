using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Reflection;

namespace V
{
    public class AMBBase : StateMachineBehaviour
    {

        static List<System.Type> animationCallBackTypes;
        static Dictionary<System.Type, List<MethodInfo>> allVoidCallBacksDic = new Dictionary<Type, List<MethodInfo>>();
        static Dictionary<System.Type, List<MethodInfo>> allIntCallBackDic = new Dictionary<Type, List<MethodInfo>>();
        static Dictionary<System.Type, List<MethodInfo>> allBoolCallBackDic = new Dictionary<Type, List<MethodInfo>>();
        static Dictionary<System.Type, List<MethodInfo>> allVec3CallBackDic = new Dictionary<Type, List<MethodInfo>>();

        AnimationSys animatorManager;
        public System.Type animationCallBackType;                    //chich child of the LEAnimationCallback
        public string managerTypeName = "LEAnimationCallback";
        public int managerIndex = 0;

        [SerializeField]
        public List<VoidCallBackObject> voidEnterCallObjects = new List<VoidCallBackObject>();
        [SerializeField]
        public List<VoidCallBackObject> voidExitCallObjects = new List<VoidCallBackObject>();
        List<MethodInfo> instanceVoidEnteres = new List<MethodInfo>();
        List<MethodInfo> instanceVoidExites = new List<MethodInfo>();

        [SerializeField]
        public List<FloatCallBackObject> floatEnterCallObjects = new List<FloatCallBackObject>();
        [SerializeField]
        public List<FloatCallBackObject> floatExitCallObjects = new List<FloatCallBackObject>();
        List<FloatMethodInfo> instanceFloatEnteres = new List<FloatMethodInfo>();
        List<FloatMethodInfo> instanceFloatExites = new List<FloatMethodInfo>();

        [SerializeField]
        public List<BoolCallBackObject> boolEnterCallObjects = new List<BoolCallBackObject>();
        [SerializeField]
        public List<BoolCallBackObject> boolExitCallObjects = new List<BoolCallBackObject>();
        List<BoolMethodInfo> instanceBoolEnteres = new List<BoolMethodInfo>();
        List<BoolMethodInfo> instanceBoolExites = new List<BoolMethodInfo>();

        [SerializeField]
        public List<Vec3CallBackObject> vec3EnterCallBacks = new List<Vec3CallBackObject>();
        [SerializeField]
        public List<Vec3CallBackObject> vec3ExitCallBacks = new List<Vec3CallBackObject>();
        List<Vec3MethodInfo> instanceVec3Enter = new List<Vec3MethodInfo>();
        List<Vec3MethodInfo> instanceVec3Exit = new List<Vec3MethodInfo>();


        [SerializeField]
        public List<BoolObject> boolObjectsEnter = new List<BoolObject>();
        [SerializeField]
        public List<BoolObject> boolObjectsExit = new List<BoolObject>();

        [SerializeField]
        public List<IntObject> intObjectsEnter = new List<IntObject>();
        [SerializeField]
        public List<IntObject> intObjectsExit = new List<IntObject>();


        void OnEnable()
        {
            FetchAnimationManagerType();
            animationCallBackType = animationCallBackTypes[managerIndex - 1]; //because in editor model we add a defualt LEUnitAnimatorManager......

            if (allVoidCallBacksDic.ContainsKey(animationCallBackType))
            {
                InitVoidCallBack();
                InitIntCallBack();
                InitBoolCallBack();
                InitVec3CallBack();
            }
            else
            {
                InitAllMethodOfType();
                InitVoidCallBack();
                InitIntCallBack();
                InitBoolCallBack();
                InitVec3CallBack();
            }
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (BoolObject b in boolObjectsEnter) { animator.SetBool(b.boolName, b.value); }
            foreach (IntObject i in intObjectsEnter) { animator.SetInteger(i.intName, i.value); }

            if (animatorManager != null)
            {
                foreach (MethodInfo m in instanceVoidEnteres)
                {
                    animatorManager.InvokeMethod(m, null);
                }
                foreach (FloatMethodInfo m in instanceFloatEnteres)
                {
                    animatorManager.InvokeMethod(m.methodInfo, m.value);
                }
                foreach (BoolMethodInfo m in instanceBoolEnteres)
                {
                    animatorManager.InvokeMethod(m.methodInfo, m.value);
                }
                foreach (Vec3MethodInfo m in instanceVec3Enter)
                {
                    animatorManager.InvokeMethod(m.methodInfo, m.value);
                }
            }
            else
            {
                animatorManager = animator.transform.parent.GetComponent<AnimationSys>();
                //Convert.ChangeType(animatorManager, animatorManagerType);
                if (!animatorManager)
                {
                    Debug.LogError("No AnimationController attach to this animator's GameObject");
                }
                else
                {
                    foreach (MethodInfo m in instanceVoidEnteres)
                    {
                        animatorManager.InvokeMethod(m, null);
                    }
                    foreach (FloatMethodInfo m in instanceFloatEnteres)
                    {
                        animatorManager.InvokeMethod(m.methodInfo, m.value);
                    }
                    foreach (BoolMethodInfo m in instanceBoolEnteres)
                    {
                        animatorManager.InvokeMethod(m.methodInfo, m.value);
                    }
                    foreach (Vec3MethodInfo m in instanceVec3Enter)
                    {
                        animatorManager.InvokeMethod(m.methodInfo, m.value);
                    }
                }
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (BoolObject b in boolObjectsExit) { animator.SetBool(b.boolName, b.value); }
            foreach (IntObject i in intObjectsExit) { animator.SetInteger(i.intName, i.value); }

            if (animatorManager != null)
            {
                foreach (MethodInfo m in instanceVoidExites)
                {
                    animatorManager.InvokeMethod(m, null);
                }
                foreach (FloatMethodInfo m in instanceFloatExites)
                {
                    animatorManager.InvokeMethod(m.methodInfo, m.value);
                }
                foreach (BoolMethodInfo m in instanceBoolExites)
                {
                    animatorManager.InvokeMethod(m.methodInfo, m.value);
                }
                foreach (Vec3MethodInfo m in instanceVec3Exit)
                {
                    animatorManager.InvokeMethod(m.methodInfo, m.value);
                }
            }
            else
            {
                animatorManager = animator.transform.parent.GetComponent<AnimationSys>();
                //Convert.ChangeType(animatorManager, animatorManagerType);
                if (!animatorManager)
                {
                    Debug.LogError("No AnimationController attach to this animator's GameObject");
                }
                else
                {
                    foreach (MethodInfo m in instanceVoidExites)
                    {
                        animatorManager.InvokeMethod(m, null);
                    }
                    foreach (FloatMethodInfo m in instanceFloatExites)
                    {
                        animatorManager.InvokeMethod(m.methodInfo, m.value);
                    }
                    foreach (BoolMethodInfo m in instanceBoolExites)
                    {
                        animatorManager.InvokeMethod(m.methodInfo, m.value);
                    }
                    foreach (Vec3MethodInfo m in instanceVec3Exit)
                    {
                        animatorManager.InvokeMethod(m.methodInfo, m.value);
                    }
                }
            }
        }

        void InitVoidCallBack()
        {
            List<MethodInfo> methodInfoByType = allVoidCallBacksDic[animationCallBackType];
            foreach (VoidCallBackObject call in voidEnterCallObjects)
            {
                instanceVoidEnteres.Add(methodInfoByType[call.methodIndex - 1]);  //In editor Model we add a Non as Default
            }
            foreach (VoidCallBackObject call in voidExitCallObjects)
            {
                instanceVoidExites.Add(methodInfoByType[call.methodIndex - 1]);
            }
        }

        void InitIntCallBack()
        {
            List<MethodInfo> methodInfoByType = allIntCallBackDic[animationCallBackType];
            foreach (FloatCallBackObject call in floatEnterCallObjects)
            {
                instanceFloatEnteres.Add(new FloatMethodInfo(methodInfoByType[call.methodIndex - 1], call.value));  //In editor Model we add a Non as Default
            }
            foreach (FloatCallBackObject call in floatExitCallObjects)
            {
                instanceFloatExites.Add(new FloatMethodInfo(methodInfoByType[call.methodIndex - 1], call.value));
            }
        }

        void InitBoolCallBack()
        {
            List<MethodInfo> methodInfoByType = allBoolCallBackDic[animationCallBackType];
            foreach (BoolCallBackObject call in boolEnterCallObjects)
            {
                instanceBoolEnteres.Add(new BoolMethodInfo(methodInfoByType[call.methodIndex - 1], call.value));  //In editor Model we add a Non as Default
            }
            foreach (BoolCallBackObject call in boolExitCallObjects)
            {
                instanceBoolExites.Add(new BoolMethodInfo(methodInfoByType[call.methodIndex - 1], call.value));  //In editor Model we add a Non as Default
            }
        }

        void InitVec3CallBack()
        {
            List<MethodInfo> methodInfoByType = allVec3CallBackDic[animationCallBackType];
            foreach (Vec3CallBackObject call in vec3EnterCallBacks)
            {
                instanceVec3Enter.Add(new Vec3MethodInfo(methodInfoByType[call.methodIndex - 1], call.value));  //In editor Model we add a Non as Default
            }
            foreach (Vec3CallBackObject call in vec3ExitCallBacks)
            {
                instanceVec3Exit.Add(new Vec3MethodInfo(methodInfoByType[call.methodIndex - 1], call.value));  //In editor Model we add a Non as Default
            }
        }

        static void FetchAnimationManagerType()
        {
            if (animationCallBackTypes != null) { return; }
            animationCallBackTypes = new List<System.Type>();
            IEnumerable<Assembly> scriptAssemblies = System.AppDomain.CurrentDomain.GetAssemblies().Where((Assembly assembly) => assembly.FullName.Contains("Assembly"));
            foreach (Assembly assembly in scriptAssemblies)
            {
                foreach (System.Type type in assembly.GetTypes().Where(T => T.IsClass && !T.IsAbstract && T.IsSubclassOf(typeof(AnimationCallBack))))
                {
                    animationCallBackTypes.Add(type);
                }
            }
        }

        void InitAllMethodOfType()
        {
            List<MethodInfo> voidOfType = new List<MethodInfo>();
            List<MethodInfo> intOfType = new List<MethodInfo>();
            List<MethodInfo> boolOfType = new List<MethodInfo>();
            List<MethodInfo> vec3OfType = new List<MethodInfo>();

            MethodInfo[] ms = animationCallBackType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            foreach (MethodInfo m in ms)
            {
                AMBCallback attr = System.Attribute.GetCustomAttribute(m, typeof(AMBCallback)) as V.AMBCallback;
                if (attr != null)
                {
                    ParameterInfo[] parameterInfos = m.GetParameters();
                    if (parameterInfos.Length == 0)
                    {
                        voidOfType.Add(m);
                    }
                    else if (parameterInfos[0].ParameterType == typeof(float))
                    {
                        intOfType.Add(m);
                    }
                    else if (parameterInfos[0].ParameterType == typeof(bool))
                    {
                        boolOfType.Add(m);
                    }
                    else if (parameterInfos[0].ParameterType == typeof(Vector3))
                    {
                        vec3OfType.Add(m);
                    }
                }
            }
            allVoidCallBacksDic.Add(animationCallBackType, voidOfType);
            allIntCallBackDic.Add(animationCallBackType, intOfType);
            allBoolCallBackDic.Add(animationCallBackType, boolOfType);
            allVec3CallBackDic.Add(animationCallBackType, vec3OfType);
        }

        public void AddNew_Void_EnterCallBack()
        {
            voidEnterCallObjects.Add(new VoidCallBackObject());
        }

        public void AddNew_Void_ExiteCallBack()
        {
            voidExitCallObjects.Add(new VoidCallBackObject());
        }

        public void AddNew_Float_EnterCallBack()
        {
            floatEnterCallObjects.Add(new FloatCallBackObject());
        }

        public void AddNew_Float_ExitCallBack()
        {
            floatExitCallObjects.Add(new FloatCallBackObject());
        }

        public void AddNew_Bool_EnterCallBack()
        {
            boolEnterCallObjects.Add(new BoolCallBackObject());
        }

        public void AddNew_Bool_ExitCallBack()
        {
            boolExitCallObjects.Add(new BoolCallBackObject());
        }

        public void AddNew_Vec3_EnterCallBack()
        {
            vec3EnterCallBacks.Add(new Vec3CallBackObject());
        }

        public void AddNew_Vec3_ExitCallBack()
        {
            vec3ExitCallBacks.Add(new Vec3CallBackObject());
        }
    }


    //Because string is a reference type, it size is unknowed.
    //So we need to tell unity to serialize the string value.

    [Serializable]
    public class VoidCallBackObject
    {
        public int methodIndex;
        [SerializeField]
        public string methodName;
        //Because MethodInfo is a unknow size of reference type. so we get the index ID, and retch it at the run time......
        //Another way is to tell Unity to serialize the MethodInfo. it may workes. may not
    }

    [System.Serializable]
    public class FloatCallBackObject
    {
        public int methodIndex;
        [SerializeField]
        public string methodName;
        public float value;
        //Because MethodInfo is a unknow size of reference type. so we get the index ID, and retch it at the run time......
        //Another way is to tell Unity to serialize the MethodInfo. it may workes. may not
    }

    public struct FloatMethodInfo
    {
        public MethodInfo methodInfo;
        public object[] value;
        public FloatMethodInfo(MethodInfo _m, float v)
        {
            methodInfo = _m;
            value = new object[1];
            value[0] = v;
        }
    }

    [System.Serializable]
    public class BoolCallBackObject
    {
        public int methodIndex;
        [SerializeField]
        public string methodName;
        public bool value;
        //Because MethodInfo is a unknow size of reference type. so we get the index ID, and retch it at the run time......
        //Another way is to tell Unity to serialize the MethodInfo. it may workes. may not
    }

    public struct BoolMethodInfo
    {
        public MethodInfo methodInfo;
        public object[] value;
        public BoolMethodInfo(MethodInfo _m, bool v)
        {
            methodInfo = _m;
            value = new object[1];
            value[0] = v;
        }
    }

    [System.Serializable]
    public class Vec3CallBackObject
    {
        public int methodIndex;
        [SerializeField]
        public string methodName;
        [SerializeField]
        public Vector3 value;
        //Because MethodInfo is a unknow size of reference type. so we get the index ID, and retch it at the run time......
        //Another way is to tell Unity to serialize the MethodInfo. it may workes. may not
    }

    public struct Vec3MethodInfo
    {
        public MethodInfo methodInfo;
        public object[] value;
        public Vec3MethodInfo(MethodInfo _m, Vector3 v3)
        {
            methodInfo = _m;
            value = new object[1];
            value[0] = v3;
        }
    }

    [System.Serializable]
    public class BoolObject
    {
        [SerializeField]
        public string boolName;
        public bool value;
        public BoolObject(string name = "Non", bool _value = false)
        {
            boolName = name;
            value = _value;
        }
    }

    [System.Serializable]
    public class IntObject
    {
        [SerializeField]
        public string intName;
        public int value;
        public IntObject(string name = "Non", int _value = 0)
        {
            intName = name;
            value = _value;
        }
    }
}
