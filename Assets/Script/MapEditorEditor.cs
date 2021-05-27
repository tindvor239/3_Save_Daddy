using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(MapEditor))]
public class MapScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MapEditor scriptEditor = (MapEditor)target;


        if (GUILayout.Button("Save"))
        {
            scriptEditor.currentMap.Save();
        }
        if (GUILayout.Button("Load"))
        {
            scriptEditor.Load();
        }
        if (GUILayout.Button("Clear"))
        {
            scriptEditor.Clear();
        }
    }
}
#endif

