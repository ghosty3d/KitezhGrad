using UnityEngine;
using System.Collections;

public class DebugObjectMesh : MonoBehaviour
{
	public Mesh l_NewMesh;

	void Start ()
	{
		l_NewMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;

		int[] tri = l_NewMesh.GetTriangles (0);
		Debug.Log ("Triangle:" + l_NewMesh.GetTriangles(0).Length);

		for (int i = 0; i < tri.Length; i++)
		{
			Debug.Log ("Tri: " + tri[i]);
		}
	}
}
