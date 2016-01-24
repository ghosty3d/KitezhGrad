using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CameraManager))]
public class CameraManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CameraManager m_CameraManager = (CameraManager)target;

        for(int i = 0; i < m_CameraManager.CameraPositionsList.Count; i++)
        {
            if(GUILayout.Button("Cam Pos: " + (i + 1).ToString(), GUILayout.ExpandWidth(true), GUILayout.Height(32)))
            {
                m_CameraManager.SetCameraAtPosition(i);
            }
        }
    }
}
