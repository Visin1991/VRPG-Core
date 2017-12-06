using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace V
{
    public abstract class AnimationCallBack : MonoBehaviour
    {
        [DependencyInjector]
        public EntityCore entityCore;
     
        //This function need to be public and static
        public static void Non() { Debug.LogError("This Function Should Not be Called"); }

        public static void NonFloat(float number) { Debug.LogError("This Function Should Not be Called"); }

        public static void NonBool(bool value) { Debug.LogError("This Function Should Not be Called"); }

        public static void NonVec3(Vector3 vec3) { Debug.LogError("This Function Should Not be Called"); }

        public abstract void InvokeMethod(MethodInfo m, object[] obj);
    }

    public enum AnimationMessageType
    {
        SetBasicMoveMent_ActiveStatu,
        SetAnimation_AttackStatue,
        LookAtTarget,
    }
}