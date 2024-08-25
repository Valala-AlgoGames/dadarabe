using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ShaderManager : MonoBehaviour
{
    [Range(0f,0.025f)]
    public float curve;
    void Awake() {
       if(Application.isPlaying){
        Shader.DisableKeyword("_INEDITOR");
       }
       else{
        Shader.EnableKeyword("_INEDITOR");
       }
    }

    // Update is called once per frame
    void Update()
    {
         if(Application.isPlaying){
            Shader.SetGlobalFloat("_curve",curve);
        }
    }
}
