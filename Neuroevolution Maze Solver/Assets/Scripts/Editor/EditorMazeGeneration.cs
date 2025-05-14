using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



//Script to edit
[CustomEditor(typeof(ProceduralMazeGenerator))]
public class EditorMazeGeneration : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        EditorGUILayout.Space();
        
        //Script to edit
        ProceduralMazeGenerator procGenMaze = (ProceduralMazeGenerator)target;
        if (GUILayout.Button("Generate Maze"))
        {
            procGenMaze.StartGeneration();
        }
    }
}
