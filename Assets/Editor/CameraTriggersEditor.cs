using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class CameraTriggersEditor : EditorWindow
{
    public List<GameObject> CameraTriggersList = new List<GameObject>();
    public GameObject ActiveCameraTrigger;

    public List<Transform> VerteciesMarkers = new List<Transform>();

    public int SelectedPreset = 0;
    public string CameraTriggerName;

    private MeshFilter newCameraTriggerMeshFilter;
    private MeshRenderer newCameraTriggerMeshRenderer;
    private MeshCollider newCameraTriggerCollider;

    [MenuItem("Kitezhgrad/Editor/CameraTriggers")]
    static void Init ()
    {
        // Get existing open window or if none, make a new one:
        CameraTriggersEditor window = (CameraTriggersEditor)EditorWindow.GetWindow (typeof (CameraTriggersEditor));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Space(16);

        GUILayout.Label(string.Format("You have {0} Camera Triggers in the List.", CameraTriggersList.Count), EditorStyles.boldLabel);

        GUILayout.Space(16);

        string[] l_ButtonLabels = { "1x1", "2x1", "4x1", "6x1", "8x2" };

        SelectedPreset = GUILayout.Toolbar(SelectedPreset, l_ButtonLabels, GUILayout.ExpandWidth(true), GUILayout.Height(32));

        if(GUILayout.Button("Create New Camera Trigger", GUILayout.ExpandWidth(true), GUILayout.Height(32)))
        {
            VerteciesMarkers.Clear();
            CameraTriggerName = "Camera Trigger " + (CameraTriggersList.Count + 1).ToString();
            GameObject newCameraTrigger = new GameObject(CameraTriggerName);

            //Mesh Filter
            newCameraTriggerMeshFilter = newCameraTrigger.AddComponent<MeshFilter>();
            newCameraTriggerMeshFilter.sharedMesh = GenerateMeshByPreset(SelectedPreset, true);

            //Mesh Renderer
            newCameraTriggerMeshRenderer = newCameraTrigger.AddComponent<MeshRenderer>();
            newCameraTriggerMeshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            newCameraTriggerMeshRenderer.receiveShadows = false;
            newCameraTriggerMeshRenderer.sharedMaterial = SetDefaultMaterial();

            //Mesh Collider
            newCameraTriggerCollider = newCameraTrigger.AddComponent<MeshCollider>();
            newCameraTriggerCollider.sharedMesh = newCameraTriggerMeshFilter.sharedMesh;
            newCameraTriggerCollider.convex = true;
            newCameraTriggerCollider.isTrigger = true;

            //Camera Trigger
            newCameraTrigger.AddComponent<CameraTrigger>();

            CameraTriggersList.Add(newCameraTrigger);

            ActiveCameraTrigger = newCameraTrigger;

            //Create Vertecies Markers
            for(int i = 0; i < newCameraTriggerMeshFilter.sharedMesh.vertexCount; i++)
            {
                CreateVertexMarker(newCameraTriggerMeshFilter.sharedMesh.vertices[i]);
            }
        }

        if(GUILayout.Button("Destroy Active Trigger", GUILayout.ExpandWidth(true), GUILayout.Height(32)))
        {
            if (ActiveCameraTrigger != null)
            {
                CameraTriggersList.Remove(ActiveCameraTrigger);
                DestroyImmediate(ActiveCameraTrigger);

                if (CameraTriggersList.Count > 0)
                {
                    ActiveCameraTrigger = CameraTriggersList[CameraTriggersList.Count - 1];
                }
            }
        }

        if(GUILayout.Button("Clear List", GUILayout.ExpandWidth(true), GUILayout.Height(32)))
        {
            if (CameraTriggersList.Count > 0)
            {
                CameraTriggersList.Clear();
            }
        }

        //Active Camera Trigger
        GUILayout.Space(16);

        if (ActiveCameraTrigger != null)
        {
            GUILayout.Label(string.Format("Active Camera Trigger is: {0}", ActiveCameraTrigger.name), EditorStyles.boldLabel);
        }

        GUILayout.Space(16);

        if (ActiveCameraTrigger != null)
        {
            if(GUILayout.Button("Update Mesh by Markers", GUILayout.ExpandWidth(true), GUILayout.Height(32)))
            {
                ActiveCameraTrigger.GetComponent<MeshFilter>().sharedMesh = GenerateMeshByPreset(SelectedPreset, false);
                UpdateCollider();
            }
        }
    }

    public Mesh GenerateMeshByPreset(int a_Preset, bool a_CreateFirstTime)
    {
        Mesh cameraTriggerMesh = new Mesh();
        cameraTriggerMesh.name = CameraTriggerName;
        cameraTriggerMesh.Clear();
        cameraTriggerMesh.MarkDynamic();

        //Create Vertices List
        List<Vector3> meshVertecies = new List<Vector3>();
        meshVertecies.Clear();

        //Create Triangles List
        List<int> meshTriangles = new List<int>();
        meshTriangles.Clear();

            switch (a_Preset)
            {
                case 0:
                    if (a_CreateFirstTime)
                    {    
                        meshVertecies.Add(new Vector3(-0.5f, 0f, 0.5f));
                        meshVertecies.Add(new Vector3(0.5f, 0f, 0.5f));
                        meshVertecies.Add(new Vector3(-0.5f, 1f, 0.5f));
                        meshVertecies.Add(new Vector3(0.5f, 1f, 0.5f));
                        meshVertecies.Add(new Vector3(-0.5f, 1f, -0.5f));
                        meshVertecies.Add(new Vector3(0.5f, 1f, -0.5f));
                        meshVertecies.Add(new Vector3(-0.5f, 0f, -0.5f));
                        meshVertecies.Add(new Vector3(0.5f, 0f, -0.5f));
                    }
                    else
                    {
                        for (int i = 0; i < VerteciesMarkers.Count; i++)
                        {
                            meshVertecies.Add(VerteciesMarkers[i].position);
                        }
                    }

                    meshTriangles.Add(0);
                    meshTriangles.Add(1);
                    meshTriangles.Add(2);

                    meshTriangles.Add(2);
                    meshTriangles.Add(1);
                    meshTriangles.Add(3);

                    meshTriangles.Add(2);
                    meshTriangles.Add(3);
                    meshTriangles.Add(4);

                    meshTriangles.Add(4);
                    meshTriangles.Add(3);
                    meshTriangles.Add(5);

                    meshTriangles.Add(4);
                    meshTriangles.Add(5);
                    meshTriangles.Add(6);

                    meshTriangles.Add(6);
                    meshTriangles.Add(5);
                    meshTriangles.Add(7);

                    meshTriangles.Add(6);
                    meshTriangles.Add(7);
                    meshTriangles.Add(0);

                    meshTriangles.Add(0);
                    meshTriangles.Add(7);
                    meshTriangles.Add(1);

                    meshTriangles.Add(1);
                    meshTriangles.Add(7);
                    meshTriangles.Add(3);

                    meshTriangles.Add(3);
                    meshTriangles.Add(7);
                    meshTriangles.Add(5);

                    meshTriangles.Add(6);
                    meshTriangles.Add(0);
                    meshTriangles.Add(4);

                    meshTriangles.Add(4);
                    meshTriangles.Add(0);
                    meshTriangles.Add(2);

                    break;

            case 1:
                if (a_CreateFirstTime)
                {    
                    meshVertecies.Add(new Vector3(-0.5f, 0f, 0.5f));



                }
                else
                {
                    for (int i = 0; i < VerteciesMarkers.Count; i++)
                    {
                        meshVertecies.Add(VerteciesMarkers[i].position);
                    }
                }
                break;

                default:
                    break;
            }


        cameraTriggerMesh.SetVertices(meshVertecies);
        cameraTriggerMesh.SetTriangles(meshTriangles, 0);
        cameraTriggerMesh.RecalculateNormals();
        cameraTriggerMesh.RecalculateBounds();
        cameraTriggerMesh.UploadMeshData(true);

        return cameraTriggerMesh;
    }

    public Material SetDefaultMaterial()
    {
        Material cameraTriggerMaterial = new Material(Shader.Find("Standard"));
        cameraTriggerMaterial.SetColor("_EmissionColor", Color.red);

        cameraTriggerMaterial.color = new Color(1f, 0, 0, 0.35f);

        cameraTriggerMaterial.SetInt("_Mode", 3);
        cameraTriggerMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        cameraTriggerMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        cameraTriggerMaterial.SetInt("_ZWrite", 0);
        cameraTriggerMaterial.DisableKeyword("_ALPHATEST_ON");
        cameraTriggerMaterial.EnableKeyword("_ALPHABLEND_ON");
        cameraTriggerMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        cameraTriggerMaterial.renderQueue = 3000;

        return cameraTriggerMaterial;
    }

    public void CreateVertexMarker(Vector3 a_MeshVertex)
    {
        if (ActiveCameraTrigger != null)
        {
            GameObject l_VertexMarker = GameObject.CreatePrimitive(PrimitiveType.Cube);
            l_VertexMarker.name = ActiveCameraTrigger.name + "-VertexMarker-" + VerteciesMarkers.Count.ToString();
            l_VertexMarker.transform.position = a_MeshVertex;
            l_VertexMarker.transform.localScale = Vector3.one * 0.15f;
            l_VertexMarker.transform.SetParent(ActiveCameraTrigger.transform);
            DestroyImmediate(l_VertexMarker.GetComponent<BoxCollider>());
            VerteciesMarkers.Add(l_VertexMarker.transform);

        }
    }

    private void UpdateCollider()
    {
        DestroyImmediate(newCameraTriggerCollider);
        newCameraTriggerCollider = ActiveCameraTrigger.AddComponent<MeshCollider>();
        newCameraTriggerCollider.convex = true;
        newCameraTriggerCollider.isTrigger = true;
    }
}
