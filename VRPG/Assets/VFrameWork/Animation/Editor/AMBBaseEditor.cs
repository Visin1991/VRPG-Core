using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace V
{
    [CustomEditor(typeof(AMBBase))]
    [CanEditMultipleObjects]
    public class AMBBaseEditor : Editor
    {
        AMBBase ambBase;
        public static List<string> animatorManagerTypesNames = new List<string>();
        public static List<System.Type> animatorManagerTypes = new List<System.Type>();

        List<string> voidCallbacNames = new List<string>();
        List<MethodInfo> voidCallbackMethods = new List<MethodInfo>();

        List<string> floatCallbackNames = new List<string>();
        List<MethodInfo> floatCallbackMethods = new List<MethodInfo>();

        List<string> boolCallBackNames = new List<string>();
        List<MethodInfo> boolCallBackMethods = new List<MethodInfo>();

        List<string> vec3CallBackNames = new List<string>();
        List<MethodInfo> vec3CallBackMethods = new List<MethodInfo>();

        string[] A_ManagerTypeNames;
        string[] void_CallBackNames;
        string[] float_CallBackNames;
        string[] bool_CallBackNames;
        string[] vec3_CallBackNames;

        bool showVoidCallBack = true;
        bool showFloatCallBack = true;
        bool showBoolCallBack = true;
        bool showVec3CallBack = true;

        bool showBoolObj = true;
        bool showIntObj = true;

        void OnEnable()
        {
            ambBase = target as AMBBase;
            //manager Type
            FetchAnimationManagerType();
            ReMatchManagerType();

            //Method
            FetchCallBackMethod();
            ReMatchAllCallBacks();
        }

        void OnDisable()
        {

        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            DrawAnimatorManagerType();
            DrawVoidCallBacks();
            DrawFloatCallBacks();
            DrawBoolCallBacks();
            DrawVec3CallBacks();
            DrawBoolObj();
            DrawIntObj();
        }

        void ReMatchManagerType()
        {
            if (ambBase == null) { return; }

            if (ambBase.managerTypeName != animatorManagerTypesNames[ambBase.managerIndex])
            {
                ambBase.managerIndex = animatorManagerTypesNames.FindIndex(name => name == ambBase.managerTypeName);
                if (ambBase.managerIndex < 0)
                {
                    ambBase.managerIndex = 0;
                    ambBase.managerTypeName = "LEUnitAnimatorManager";
                }
            }
        }

        void ReMatchAllCallBacks()
        {
            if (ambBase == null) { return; }
            ReMatchVoidEnter();
            ReMatchVoidExit();

            ReMatchFloatEnter();
            ReMatchFloatExit();

            ReMatchBoolEnter();
            ReMatchBoolExit();

            ReMatchVec3Enter();
            ReMatchVec3Exit();
        }

        void DrawAnimatorManagerType()
        {
            GUILayout.BeginVertical("Box");
            {
                if (ambBase.managerIndex == 0) { GUI.color = Color.red; }
                EditorGUI.BeginChangeCheck();
                ambBase.managerIndex = EditorGUILayout.Popup("", ambBase.managerIndex, A_ManagerTypeNames, EditorStyles.popup);
                ambBase.managerTypeName = animatorManagerTypes[ambBase.managerIndex].Name;
                if (EditorGUI.EndChangeCheck())
                {
                    voidCallbacNames.Clear();
                    voidCallbackMethods.Clear();
                    FetchCallBackMethod();
                    ReMatchAllCallBacks();
                    void_CallBackNames = voidCallbacNames.ToArray();
                }
                serializedObject.ApplyModifiedProperties();
                SceneView.RepaintAll();
                GUI.color = Color.white;
            }
            GUILayout.EndVertical();
        }

        #region Void CallBack

        void DrawVoidCallBacks()
        {
            GUILayout.BeginVertical("Box");
            {
                showVoidCallBack = EditorGUILayout.Foldout(showVoidCallBack, "Void CallBack");
                if (showVoidCallBack)
                {

                    GUILayout.BeginVertical("Box");
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("CallBack Enter");
                            GUI.color = Color.green;
                            if (GUILayout.Button("Add", GUILayout.Width(100))) { if (ambBase.voidEnterCallObjects.Count < void_CallBackNames.Length) { ambBase.AddNew_Void_EnterCallBack(); } }
                            GUI.color = Color.white;
                        }
                        GUILayout.EndHorizontal();
                        DrawVoidEnterCallBack();
                    }
                    GUILayout.EndVertical();

                    GUILayout.Space(10);

                    GUILayout.BeginVertical("Box");
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("CallBack Exit");
                            GUI.color = Color.green;
                            if (GUILayout.Button("Add", GUILayout.Width(100))) { if (ambBase.voidExitCallObjects.Count < void_CallBackNames.Length) { ambBase.AddNew_Void_ExiteCallBack(); } }
                            GUI.color = Color.white;
                        }
                        GUILayout.EndHorizontal();
                        DrawVoidExitCallBack();
                    }
                    GUILayout.EndVertical();

                    serializedObject.ApplyModifiedProperties();
                    SceneView.RepaintAll();
                }
            }
            GUILayout.EndVertical();
        }

        void DrawVoidEnterCallBack()
        {
            for (int i = 0; i < ambBase.voidEnterCallObjects.Count; i++)
            {
                GUILayout.BeginHorizontal();
                {
                    if (ambBase.voidEnterCallObjects[i].methodIndex == 0) { GUI.color = Color.red; }
                    int index = EditorGUILayout.Popup("", ambBase.voidEnterCallObjects[i].methodIndex, void_CallBackNames, EditorStyles.popup);
                    // ambBase.voidEnterCallObjects[i].methodIndex = index;
                    ambBase.voidEnterCallObjects[i].methodIndex = index;
                    ambBase.voidEnterCallObjects[i].methodName = voidCallbacNames[index];

                    GUI.color = Color.cyan;
                    if (GUILayout.Button("Delete", GUILayout.Width(50))) { if (ambBase.voidEnterCallObjects.Count > 0) { ambBase.voidEnterCallObjects.RemoveAt(i); } }
                    GUI.color = Color.white;
                }
                GUILayout.EndHorizontal();
            }
        }

        void DrawVoidExitCallBack()
        {
            for (int i = 0; i < ambBase.voidExitCallObjects.Count; i++)
            {
                GUILayout.BeginHorizontal();
                {
                    if (ambBase.voidExitCallObjects[i].methodIndex == 0) { GUI.color = Color.red; }
                    int index = EditorGUILayout.Popup("", ambBase.voidExitCallObjects[i].methodIndex, void_CallBackNames, EditorStyles.popup);
                    ambBase.voidExitCallObjects[i].methodIndex = index;
                    ambBase.voidExitCallObjects[i].methodName = voidCallbacNames[index];

                    GUI.color = Color.cyan;
                    if (GUILayout.Button("Delete", GUILayout.Width(50))) { if (ambBase.voidExitCallObjects.Count > 0) { ambBase.voidExitCallObjects.RemoveAt(i); } }
                    GUI.color = Color.white;
                }
                GUILayout.EndHorizontal();
            }
        }

        void ReMatchVoidEnter()
        {
            for (int i = 0; i < ambBase.voidEnterCallObjects.Count; i++)
            {
                if (ambBase.voidEnterCallObjects[i].methodIndex >= voidCallbacNames.Count)
                {
                    ambBase.voidEnterCallObjects[i].methodIndex = 0;
                    ambBase.voidEnterCallObjects[i].methodName = "Non";
                }
                else
                {
                    VoidCallBackObject functionCall = ambBase.voidEnterCallObjects[i];
                    if (functionCall.methodName != voidCallbacNames[functionCall.methodIndex])
                    {
                        ambBase.voidEnterCallObjects[i].methodIndex = voidCallbacNames.FindIndex(name => name == functionCall.methodName);
                        if (ambBase.voidEnterCallObjects[i].methodIndex < 0)
                        {
                            ambBase.voidEnterCallObjects[i].methodIndex = 0;
                            ambBase.voidEnterCallObjects[i].methodName = "Non";
                        }
                    }
                }
            }
        }

        void ReMatchVoidExit()
        {
            foreach (VoidCallBackObject callObj in ambBase.voidExitCallObjects)
            {
                if (callObj.methodIndex >= voidCallbacNames.Count)
                {
                    callObj.methodIndex = 0;
                    callObj.methodName = "Non";
                }
                else
                {
                    if (callObj.methodName != voidCallbacNames[callObj.methodIndex])
                    {
                        callObj.methodIndex = voidCallbacNames.FindIndex(name => name == callObj.methodName);
                        if (callObj.methodIndex < 0)
                        {
                            callObj.methodIndex = 0;
                            callObj.methodName = "Non";
                        }
                    }
                }
            }
        }

        #endregion

        #region Float CallBack

        void DrawFloatCallBacks()
        {
            GUILayout.BeginVertical("Box");
            {
                showFloatCallBack = EditorGUILayout.Foldout(showFloatCallBack, "Float CallBack");
                if (showFloatCallBack)
                {

                    GUILayout.BeginVertical("Box");
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("Float CallBack Enter");
                            GUI.color = Color.green;
                            if (GUILayout.Button("Add", GUILayout.Width(100))) { if (ambBase.floatEnterCallObjects.Count < float_CallBackNames.Length) { ambBase.AddNew_Float_EnterCallBack(); } }
                            GUI.color = Color.white;
                        }
                        GUILayout.EndHorizontal();
                        DrawFloatEnterCallBack();
                    }
                    GUILayout.EndVertical();

                    GUILayout.Space(10);

                    GUILayout.BeginVertical("Box");
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("Float CallBack Exit");
                            GUI.color = Color.green;
                            if (GUILayout.Button("Add", GUILayout.Width(100))) { if (ambBase.floatExitCallObjects.Count < float_CallBackNames.Length) { ambBase.AddNew_Float_ExitCallBack(); } }
                            GUI.color = Color.white;
                        }
                        GUILayout.EndHorizontal();
                        DrawFloatExitCallBack();
                    }
                    GUILayout.EndVertical();

                    serializedObject.ApplyModifiedProperties();
                    SceneView.RepaintAll();
                }
            }
            GUILayout.EndVertical();
        }

        void DrawFloatEnterCallBack()
        {
            for (int i = 0; i < ambBase.floatEnterCallObjects.Count; i++)
            {
                GUILayout.BeginHorizontal();
                {
                    if (ambBase.floatEnterCallObjects[i].methodIndex == 0) { GUI.color = Color.red; }
                    int index = EditorGUILayout.Popup("", ambBase.floatEnterCallObjects[i].methodIndex, float_CallBackNames, EditorStyles.popup);
                    ambBase.floatEnterCallObjects[i].methodIndex = index;
                    ambBase.floatEnterCallObjects[i].methodName = float_CallBackNames[index];
                    ambBase.floatEnterCallObjects[i].value = EditorGUILayout.FloatField(ambBase.floatEnterCallObjects[i].value);

                    GUI.color = Color.cyan;
                    if (GUILayout.Button("Delete", GUILayout.Width(50))) { if (ambBase.floatEnterCallObjects.Count > 0) { ambBase.floatEnterCallObjects.RemoveAt(i); } }
                    GUI.color = Color.white;
                }
                GUILayout.EndHorizontal();
            }
        }

        void DrawFloatExitCallBack()
        {
            for (int i = 0; i < ambBase.floatExitCallObjects.Count; i++)
            {
                GUILayout.BeginHorizontal();
                {
                    if (ambBase.floatExitCallObjects[i].methodIndex == 0) { GUI.color = Color.red; }
                    int index = EditorGUILayout.Popup("", ambBase.floatExitCallObjects[i].methodIndex, float_CallBackNames, EditorStyles.popup);
                    ambBase.floatExitCallObjects[i].methodIndex = index;
                    ambBase.floatExitCallObjects[i].methodName = float_CallBackNames[index];
                    ambBase.floatExitCallObjects[i].value = EditorGUILayout.FloatField(ambBase.floatExitCallObjects[i].value);

                    GUI.color = Color.cyan;
                    if (GUILayout.Button("Delete", GUILayout.Width(50))) { if (ambBase.floatExitCallObjects.Count > 0) { ambBase.floatExitCallObjects.RemoveAt(i); } }
                    GUI.color = Color.white;
                }
                GUILayout.EndHorizontal();
            }
        }

        void ReMatchFloatEnter()
        {
            foreach (FloatCallBackObject callObj in ambBase.floatEnterCallObjects)
            {
                if (callObj.methodIndex >= floatCallbackNames.Count)
                {
                    callObj.methodIndex = 0;
                    callObj.methodName = "NonFloat";
                }

                if (callObj.methodName != floatCallbackNames[callObj.methodIndex])
                {
                    callObj.methodIndex = floatCallbackNames.FindIndex(name => name == callObj.methodName);
                    if (callObj.methodIndex < 0)
                    {
                        callObj.methodIndex = 0;
                        callObj.methodName = "NonFloat";
                    }
                }
            }
        }

        void ReMatchFloatExit()
        {
            foreach (FloatCallBackObject callObj in ambBase.floatExitCallObjects)
            {
                if (callObj.methodIndex >= floatCallbackMethods.Count)
                {
                    callObj.methodIndex = 0;
                    callObj.methodName = "NonFloat";
                }
                if (callObj.methodName != floatCallbackNames[callObj.methodIndex])
                {
                    callObj.methodIndex = floatCallbackNames.FindIndex(name => name == callObj.methodName);
                    if (callObj.methodIndex < 0)
                    {
                        callObj.methodIndex = 0;
                        callObj.methodName = "NonFloat";
                    }
                }
            }
        }

        #endregion

        #region Bool Call

        void DrawBoolCallBacks()
        {
            GUILayout.BeginVertical("Box");
            {
                showBoolCallBack = EditorGUILayout.Foldout(showBoolCallBack, "Bool CallBack");
                if (showBoolCallBack)
                {
                    GUILayout.BeginVertical("Box");
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("Bool CallBack Enter");
                            GUI.color = Color.green;
                            if (GUILayout.Button("Add", GUILayout.Width(100))) { if (ambBase.boolEnterCallObjects.Count < bool_CallBackNames.Length) { ambBase.AddNew_Bool_EnterCallBack(); } }
                            GUI.color = Color.white;
                        }
                        GUILayout.EndHorizontal();
                        DrawBoolEnterCallBack();
                    }
                    GUILayout.EndVertical();

                    GUILayout.Space(10);

                    GUILayout.BeginVertical("Box");
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("Bool CallBack Exit");
                            GUI.color = Color.green;
                            if (GUILayout.Button("Add", GUILayout.Width(100))) { if (ambBase.boolExitCallObjects.Count < bool_CallBackNames.Length) { ambBase.AddNew_Bool_ExitCallBack(); } }
                            GUI.color = Color.white;
                        }
                        GUILayout.EndHorizontal();
                        DrawBoolExitCallBack();
                    }
                    GUILayout.EndVertical();

                    serializedObject.ApplyModifiedProperties();
                    SceneView.RepaintAll();
                }
            }
            GUILayout.EndVertical();
        }

        void DrawBoolEnterCallBack()
        {
            for (int i = 0; i < ambBase.boolEnterCallObjects.Count; i++)
            {
                GUILayout.BeginHorizontal();
                {
                    if (ambBase.boolEnterCallObjects[i].methodIndex == 0) { GUI.color = Color.red; }
                    int index = EditorGUILayout.Popup("", ambBase.boolEnterCallObjects[i].methodIndex, bool_CallBackNames, EditorStyles.popup);
                    ambBase.boolEnterCallObjects[i].methodIndex = index;
                    ambBase.boolEnterCallObjects[i].methodName = bool_CallBackNames[index];
                    ambBase.boolEnterCallObjects[i].value = EditorGUILayout.Toggle(ambBase.boolEnterCallObjects[i].value);

                    GUI.color = Color.cyan;
                    if (GUILayout.Button("Delete", GUILayout.Width(50))) { if (ambBase.boolEnterCallObjects.Count > 0) { ambBase.boolEnterCallObjects.RemoveAt(i); } }
                    GUI.color = Color.white;
                }
                GUILayout.EndHorizontal();
            }
        }

        void DrawBoolExitCallBack()
        {
            for (int i = 0; i < ambBase.boolExitCallObjects.Count; i++)
            {
                GUILayout.BeginHorizontal();
                {
                    if (ambBase.boolExitCallObjects[i].methodIndex == 0) { GUI.color = Color.red; }
                    int index = EditorGUILayout.Popup("", ambBase.boolExitCallObjects[i].methodIndex, bool_CallBackNames, EditorStyles.popup);
                    ambBase.boolExitCallObjects[i].methodIndex = index;
                    ambBase.boolExitCallObjects[i].methodName = bool_CallBackNames[index];
                    ambBase.boolExitCallObjects[i].value = EditorGUILayout.Toggle(ambBase.boolExitCallObjects[i].value);

                    GUI.color = Color.cyan;
                    if (GUILayout.Button("Delete", GUILayout.Width(50))) { if (ambBase.boolExitCallObjects.Count > 0) { ambBase.boolExitCallObjects.RemoveAt(i); } }
                    GUI.color = Color.white;
                }
                GUILayout.EndHorizontal();
            }
        }

        void ReMatchBoolEnter()
        {
            foreach (BoolCallBackObject callObj in ambBase.boolEnterCallObjects)
            {
                if (callObj.methodIndex >= boolCallBackNames.Count)
                {
                    callObj.methodIndex = 0;
                    callObj.methodName = "NonFloat";
                }
                else
                {
                    if (callObj.methodName != boolCallBackNames[callObj.methodIndex])
                    {
                        callObj.methodIndex = boolCallBackNames.FindIndex(name => name == callObj.methodName);
                        if (callObj.methodIndex < 0)
                        {
                            callObj.methodIndex = 0;
                            callObj.methodName = "NonBool";
                        }
                    }
                }
            }
        }

        void ReMatchBoolExit()
        {
            foreach (BoolCallBackObject callObj in ambBase.boolExitCallObjects)
            {
                if (callObj.methodIndex >= boolCallBackNames.Count)
                {
                    callObj.methodIndex = 0;
                    callObj.methodName = "NonFloat";
                }
                else
                {
                    if (callObj.methodName != boolCallBackNames[callObj.methodIndex])
                    {
                        callObj.methodIndex = boolCallBackNames.FindIndex(name => name == callObj.methodName);
                        if (callObj.methodIndex < 0)
                        {
                            callObj.methodIndex = 0;
                            callObj.methodName = "NonBool";
                        }
                    }
                }
            }
        }
        #endregion

        #region Vec3 CallBack

        void DrawVec3CallBacks()
        {
            GUILayout.BeginVertical("Box");
            {
                showVec3CallBack = EditorGUILayout.Foldout(showVec3CallBack, "Vec3 CallBack");
                if (showVec3CallBack)
                {

                    GUILayout.BeginVertical("Box");
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("Vec3 CallBack Enter");
                            GUI.color = Color.green;
                            if (GUILayout.Button("Add", GUILayout.Width(100))) { if (ambBase.vec3EnterCallBacks.Count < vec3_CallBackNames.Length) { ambBase.AddNew_Vec3_EnterCallBack(); } }
                            GUI.color = Color.white;
                        }
                        GUILayout.EndHorizontal();
                        DrawVec3Enter();
                    }
                    GUILayout.EndVertical();

                    GUILayout.Space(10);

                    GUILayout.BeginVertical("Box");
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("Vec3 CallBack Exit");
                            GUI.color = Color.green;
                            if (GUILayout.Button("Add", GUILayout.Width(100))) { if (ambBase.vec3ExitCallBacks.Count < float_CallBackNames.Length) { ambBase.AddNew_Vec3_ExitCallBack(); } }
                            GUI.color = Color.white;
                        }
                        GUILayout.EndHorizontal();
                        DrawVec3Exit();
                    }
                    GUILayout.EndVertical();

                    serializedObject.ApplyModifiedProperties();
                    SceneView.RepaintAll();
                }
            }
            GUILayout.EndVertical();
        }

        void DrawVec3Enter()
        {
            for (int i = 0; i < ambBase.vec3EnterCallBacks.Count; i++)
            {
                if (ambBase.vec3EnterCallBacks[i].methodIndex == 0) { GUI.color = Color.red; }
                int index = EditorGUILayout.Popup(ambBase.vec3EnterCallBacks[i].methodIndex, vec3_CallBackNames, EditorStyles.popup);
                ambBase.vec3EnterCallBacks[i].methodIndex = index;
                ambBase.vec3EnterCallBacks[i].methodName = vec3_CallBackNames[index];
                ambBase.vec3EnterCallBacks[i].value = EditorGUILayout.Vector3Field("Vec3:", ambBase.vec3EnterCallBacks[i].value);

                GUI.color = Color.cyan;
                if (GUILayout.Button("Delete", GUILayout.Width(50))) { if (ambBase.vec3EnterCallBacks.Count > 0) { ambBase.vec3EnterCallBacks.RemoveAt(i); } }
                GUI.color = Color.white;
            }
        }

        void DrawVec3Exit()
        {
            for (int i = 0; i < ambBase.vec3ExitCallBacks.Count; i++)
            {
                if (ambBase.vec3ExitCallBacks[i].methodIndex == 0) { GUI.color = Color.red; }
                int index = EditorGUILayout.Popup(ambBase.vec3ExitCallBacks[i].methodIndex, vec3_CallBackNames, EditorStyles.popup);
                ambBase.vec3ExitCallBacks[i].methodIndex = index;
                ambBase.vec3ExitCallBacks[i].methodName = vec3_CallBackNames[index];
                ambBase.vec3ExitCallBacks[i].value = EditorGUILayout.Vector3Field("Vec3:", ambBase.vec3ExitCallBacks[i].value);

                GUI.color = Color.cyan;
                if (GUILayout.Button("Delete", GUILayout.Width(50))) { if (ambBase.vec3ExitCallBacks.Count > 0) { ambBase.vec3ExitCallBacks.RemoveAt(i); } }
                GUI.color = Color.white;
            }
        }

        void ReMatchVec3Enter()
        {
            foreach (Vec3CallBackObject callObj in ambBase.vec3EnterCallBacks)
            {
                if (callObj.methodIndex >= vec3CallBackNames.Count)
                {
                    callObj.methodIndex = 0;
                    callObj.methodName = "NonFloat";
                }
                else
                {
                    if (callObj.methodName != vec3CallBackNames[callObj.methodIndex])
                    {
                        callObj.methodIndex = vec3CallBackNames.FindIndex(name => name == callObj.methodName);
                        if (callObj.methodIndex < 0)
                        {
                            callObj.methodIndex = 0;
                            callObj.methodName = "NonVec3";
                        }
                    }
                }
            }
        }

        void ReMatchVec3Exit()
        {
            foreach (Vec3CallBackObject callObj in ambBase.vec3ExitCallBacks)
            {
                if (callObj.methodIndex >= vec3CallBackNames.Count)
                {
                    callObj.methodIndex = 0;
                    callObj.methodName = "NonFloat";
                }
                else
                {
                    if (callObj.methodName != vec3CallBackNames[callObj.methodIndex])
                    {
                        callObj.methodIndex = vec3CallBackNames.FindIndex(name => name == callObj.methodName);
                        if (callObj.methodIndex < 0)
                        {
                            callObj.methodIndex = 0;
                            callObj.methodName = "NonVec3";
                        }
                    }
                }
            }
        }

        #endregion

        #region Internal Call

        void DrawBoolObj()
        {
            GUILayout.BeginVertical("Box");
            {
                showBoolObj = EditorGUILayout.Foldout(showBoolObj, "BoolObject");
                if (showBoolObj)
                {
                    DrawBoolEnter();
                    DrawBoolExit();
                }
            }
            GUILayout.EndVertical();
        }

        void DrawBoolEnter()
        {
            GUILayout.BeginVertical("Box");
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Bool Enter");
                    GUI.color = Color.green;
                    if (GUILayout.Button("Add", GUILayout.Width(100)))
                    {
                        BoolObject boolObj = new BoolObject();
                        ambBase.boolObjectsEnter.Add(boolObj);
                    }
                    GUI.color = Color.white;
                }
                GUILayout.EndHorizontal();

                for (int i = 0; i < ambBase.boolObjectsEnter.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Name:");
                        ambBase.boolObjectsEnter[i].boolName = GUILayout.TextField(ambBase.boolObjectsEnter[i].boolName, 25);
                        ambBase.boolObjectsEnter[i].value = GUILayout.Toggle(ambBase.boolObjectsEnter[i].value, "");
                        GUI.color = Color.cyan;
                        if (GUILayout.Button("Delete", GUILayout.Width(50))) { ambBase.boolObjectsEnter.RemoveAt(i); }
                        GUI.color = Color.white;
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndVertical();
        }

        void DrawBoolExit()
        {
            GUILayout.BeginVertical("Box");
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Bool Exit");
                    GUI.color = Color.green;
                    if (GUILayout.Button("Add", GUILayout.Width(100)))
                    {
                        BoolObject boolObj = new BoolObject();
                        ambBase.boolObjectsExit.Add(boolObj);
                    }
                    GUI.color = Color.white;
                }
                GUILayout.EndHorizontal();

                for (int i = 0; i < ambBase.boolObjectsExit.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Name:");
                        ambBase.boolObjectsExit[i].boolName = GUILayout.TextField(ambBase.boolObjectsExit[i].boolName, 25);
                        ambBase.boolObjectsExit[i].value = GUILayout.Toggle(ambBase.boolObjectsExit[i].value, "");
                        GUI.color = Color.cyan;
                        if (GUILayout.Button("Delete", GUILayout.Width(50))) { ambBase.boolObjectsExit.RemoveAt(i); }
                        GUI.color = Color.white;
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndVertical();
        }

        void DrawIntObj()
        {
            GUILayout.BeginVertical("Box");
            {
                showIntObj = EditorGUILayout.Foldout(showIntObj, "IntObj");
                if (showIntObj)
                {
                    DrawIntEnter();
                    DrawIntExit();
                }
            }
            GUILayout.EndVertical();
        }

        void DrawIntEnter()
        {
            //Enter
            GUILayout.BeginVertical("Box");
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Enter");

                    GUI.color = Color.green;
                    if (GUILayout.Button("Add", GUILayout.Width(100)))
                    {
                        IntObject intObj = new IntObject();
                        ambBase.intObjectsEnter.Add(intObj);
                    }
                    GUI.color = Color.white;
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginVertical("Box");
                for (int i = 0; i < ambBase.intObjectsEnter.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Name:");
                        ambBase.intObjectsEnter[i].intName = GUILayout.TextField(ambBase.intObjectsEnter[i].intName, 25);
                        ambBase.intObjectsEnter[i].value = EditorGUILayout.IntField(ambBase.intObjectsEnter[i].value);
                        GUI.color = Color.cyan;
                        if (GUILayout.Button("Delete", GUILayout.Width(50))) { ambBase.intObjectsEnter.RemoveAt(i); }
                        GUI.color = Color.white;
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndVertical();
        }

        void DrawIntExit()
        {
            //Exit
            GUILayout.BeginVertical("Box");
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Exit");

                    GUI.color = Color.green;
                    if (GUILayout.Button("Add", GUILayout.Width(100)))
                    {
                        IntObject intObj = new IntObject();
                        ambBase.intObjectsExit.Add(intObj);
                    }
                    GUI.color = Color.white;
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginVertical("Box");

                for (int i = 0; i < ambBase.intObjectsExit.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Name:");
                        ambBase.intObjectsExit[i].intName = GUILayout.TextField(ambBase.intObjectsExit[i].intName, 25);
                        ambBase.intObjectsExit[i].value = EditorGUILayout.IntField(ambBase.intObjectsExit[i].value);
                        GUI.color = Color.cyan;
                        if (GUILayout.Button("Delete", GUILayout.Width(50))) { ambBase.intObjectsExit.RemoveAt(i); }
                        GUI.color = Color.white;
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndVertical();
        }

        #endregion

        void FetchCallBackMethod()
        {
            if (ambBase == null) { return; }
            //Clean
            voidCallbacNames.Clear();
            voidCallbackMethods.Clear();

            floatCallbackNames.Clear();
            floatCallbackMethods.Clear();

            boolCallBackNames.Clear();
            boolCallBackMethods.Clear();

            vec3CallBackNames.Clear();
            vec3CallBackMethods.Clear();

            //Add and Fetch
            MethodInfo Non = GetNonVoid();
            voidCallbacNames.Add("Non");
            voidCallbackMethods.Add(Non);

            MethodInfo NonFloat = GetNonFloat();
            floatCallbackNames.Add("NonFloat");
            floatCallbackMethods.Add(NonFloat);

            MethodInfo NonBool = GetNonBool();
            boolCallBackNames.Add("NonBool");
            boolCallBackMethods.Add(NonBool);

            MethodInfo NonVec3 = GetNonVec3();
            vec3CallBackNames.Add("NonVec3");
            vec3CallBackMethods.Add(NonVec3);


            MethodInfo[] ms = animatorManagerTypes[ambBase.managerIndex].GetMethods(BindingFlags.Instance | BindingFlags.Public);

            foreach (MethodInfo m in ms)
            {
                V.AMBCallback attr = System.Attribute.GetCustomAttribute(m, typeof(V.AMBCallback)) as V.AMBCallback;
                if (attr != null)
                {
                    ParameterInfo[] parameterInfos = m.GetParameters();
                    if (parameterInfos.Length == 0)                             //if There is no parameter pass to the function
                    {
                        voidCallbacNames.Add(m.Name);
                        voidCallbackMethods.Add(m);
                    }
                    else if (parameterInfos[0].ParameterType == typeof(float))   //if it pass a int as parameter
                    {
                        floatCallbackNames.Add(m.Name);
                        floatCallbackMethods.Add(m);
                    }
                    else if (parameterInfos[0].ParameterType == typeof(bool))
                    {
                        boolCallBackNames.Add(m.Name);
                        boolCallBackMethods.Add(m);
                    }
                    else if (parameterInfos[0].ParameterType == typeof(Vector3))
                    {
                        vec3CallBackNames.Add(m.Name);
                        vec3CallBackMethods.Add(m);
                    }
                }
            }

            //Get Array
            void_CallBackNames = voidCallbacNames.ToArray();
            float_CallBackNames = floatCallbackNames.ToArray();
            bool_CallBackNames = boolCallBackNames.ToArray();
            vec3_CallBackNames = vec3CallBackNames.ToArray();
        }

        internal void FetchAnimationManagerType()
        {
            //Clean first
            animatorManagerTypesNames.Clear();
            animatorManagerTypes.Clear();

            //Add and Fetch
            animatorManagerTypes.Add(typeof(AnimationCallBack));
            animatorManagerTypesNames.Add("AnimationCallBack");

            IEnumerable<Assembly> scriptAssemblies = System.AppDomain.CurrentDomain.GetAssemblies().Where((Assembly assembly) => assembly.FullName.Contains("Assembly"));
            foreach (Assembly assembly in scriptAssemblies)
            {
                foreach (System.Type type in assembly.GetTypes().Where(T => T.IsClass && !T.IsAbstract && T.IsSubclassOf(typeof(AnimationCallBack))))
                {
                    animatorManagerTypes.Add(type);
                    animatorManagerTypesNames.Add(type.Name);
                }
            }

            //Get Array
            A_ManagerTypeNames = animatorManagerTypesNames.ToArray();
        }

        internal static MethodInfo GetNonVoid()
        {
            return typeof(AnimationCallBack).GetMethod("Non", BindingFlags.Static | BindingFlags.Public);
        }

        internal static MethodInfo GetNonFloat()
        {
            return typeof(AnimationCallBack).GetMethod("NonFloat", BindingFlags.Static | BindingFlags.Public);
        }

        internal static MethodInfo GetNonBool()
        {
            return typeof(AnimationCallBack).GetMethod("NonBool", BindingFlags.Static | BindingFlags.Public);
        }

        internal static MethodInfo GetNonVec3()
        {
            return typeof(AnimationCallBack).GetMethod("NonVec3", BindingFlags.Static | BindingFlags.Public);
        }
    }
}