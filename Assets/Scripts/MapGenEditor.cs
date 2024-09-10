using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RockGeneration))]
public class MapGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RockGeneration mapGen = (RockGeneration)target;

        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.GenerateMap();
            }
        }


        if (GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();
        }
    }
}
