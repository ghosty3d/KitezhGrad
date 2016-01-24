using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class CameraManager : MonoBehaviour
{
    public List<Transform> CameraPositionsList = new List<Transform>();

    public void SetCameraAtPosition(int index)
    {
        if (CameraPositionsList.Contains(CameraPositionsList[index]))
        {
            Camera.main.transform.position = CameraPositionsList[index].position;
            Camera.main.transform.rotation = CameraPositionsList[index].rotation;
        }
    }
}
