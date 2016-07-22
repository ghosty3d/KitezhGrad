using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{
    void OnSceneGUI()
    {
        PlayerController m_PlayerController = (PlayerController)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(m_PlayerController.transform.position, Vector3.up, Vector3.forward, 360, m_PlayerController.ViewRadus);

        Vector3 ViewAngleA = m_PlayerController.DifFromAngle(-m_PlayerController.ViewAngle / 2, false);
        Vector3 ViewAngleB = m_PlayerController.DifFromAngle(m_PlayerController.ViewAngle / 2, false);

        Handles.DrawLine(m_PlayerController.transform.position, m_PlayerController.transform.position + ViewAngleA * m_PlayerController.ViewRadus);
        Handles.DrawLine(m_PlayerController.transform.position, m_PlayerController.transform.position + ViewAngleB * m_PlayerController.ViewRadus);
    }
}
