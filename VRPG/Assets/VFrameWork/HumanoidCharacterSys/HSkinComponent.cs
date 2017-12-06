using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HSkinComponent : MonoBehaviour {

    SkinnedMeshRenderer skinMeshRender;
    public SkinnedMeshRenderer SkinMeshRenderer
    {
        get
        {
            if (skinMeshRender == null)
            {
                skinMeshRender = GetComponent<SkinnedMeshRenderer>();
            };
            return skinMeshRender;
        }
    }
    public bool Active
    {
        get
        {
            return SkinMeshRenderer.enabled;
        }
        set
        {
            SkinMeshRenderer.enabled = value;
        }
    }

    public abstract System.Type GetSkinType();
}
