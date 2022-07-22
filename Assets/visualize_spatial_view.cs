using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DHARTAPI;
using DHARTAPI.GraphGenerator;
using DHARTAPI.RayTracing;
using DHARTAPI.Geometry;
using DHARTAPI.ViewAnalysis;
using DHARTAPI.Pathfinding;
using DHARTAPI.SpatialStructures;
using DHARTAPI.NativeUtils.CommonNativeArrays;

public class visualize_spatial_view : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string obj_path = @"C:\Users\stong\Downloads\CS\Research Summer 2022\test objs\VisibilityTestCases.obj";
        MeshInfo PlaneMeshInfo = OBJLoader.LoadOBJ(obj_path);
        EmbreeBVH bvh1 = new EmbreeBVH(PlaneMeshInfo, true);
        Vector3D start_point = new Vector3D(1.1f, 1.1f, 20);
        Vector3D spacing = new Vector3D(1, 1, 5);
        int max_nodes = 10000;
        float up_step = 0.1f; 
        float down_step = 0.1f;
        float up_slope = 1f;
        float down_slope = 1f;
        int max_step_connections = 1;
        int cores = -1;


        Graph G = GraphGenerator.GenerateGraph(bvh1, start_point, spacing, max_nodes, up_step, up_slope, down_step, down_slope, max_step_connections, cores);
        NodeList nodes = G.getNodes();
        Debug.Log("Visualize spatial view: "+nodes.array.Length);

        float height = 1.7f;
        int ray_count = 1500;
        float upward_fov = 20.0f;
        float downward_fov = 20.0f;

        Vector3D[] nodesIE = nodes.ToVector3D();
        ManagedFloatArray results = ViewAnalysis.ViewAnalysisAggregate(bvh1, nodesIE, ray_count, height, upward_fov, downward_fov);
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
