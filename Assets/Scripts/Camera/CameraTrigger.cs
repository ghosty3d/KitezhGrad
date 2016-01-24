using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTrigger : MonoBehaviour
{
    public List<GameObject> CamerasPositions = new List<GameObject>();
    public int CameraPositionIndex = 0;
    public Camera ActiveCamera;

    void Start()
    {
        if (CamerasPositions.Count == 0)
        {
            CamerasPositions.AddRange(GameObject.FindGameObjectsWithTag("CameraPosition"));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (CamerasPositions[CameraPositionIndex] != null)
            {
                ActiveCamera.transform.position = CamerasPositions[CameraPositionIndex].transform.position;
                ActiveCamera.transform.rotation = CamerasPositions[CameraPositionIndex].transform.rotation;  
            }
        }
    }
}
