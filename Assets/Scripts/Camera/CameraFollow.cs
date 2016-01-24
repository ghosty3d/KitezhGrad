using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    void Start()
    {
        if (Target == null)
        {
            Target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update ()
    {
        if (Target != null)
        {
            transform.LookAt(Target.position);
        }
	}
}
