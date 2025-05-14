using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



//Script to edit
[CustomEditor(typeof(RandomTexture))]
public class EditorMazeGeneration : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        EditorGUILayout.Space();
        
        //Script to edit
        RandomTexture randomTexture = (RandomTexture)target;
        if (GUILayout.Button("Generate Maze"))
        {
            randomTexture.GenerateTexture();
        }
    }
}
