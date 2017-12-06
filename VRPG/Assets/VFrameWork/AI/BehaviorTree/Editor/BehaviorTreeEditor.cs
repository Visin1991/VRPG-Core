using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using V.AI;

public class BehaviorTreeEditor : EditorWindow {

    [SerializeField]
    public BehaviorTree bhTree;

    [MenuItem("Tools/Behavior Tree")]
    static void ShowBehaviorTreeWindow()
    {
        EditorWindow.GetWindow<BehaviorTreeEditor>();
    }

    private void OnEnable()
    {
        bhTree = Selection.activeGameObject.GetComponent<BehaviorTree>();
    }

    private void OnGUI()
    {
        if (bhTree != null)
        {
            if (GUILayout.Button("PrintData"))
            {
                Debug.Log(bhTree.message);
            }
        }
       
    }


}
