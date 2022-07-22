using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DHARTAPI;
using DHARTAPI.GraphGenerator;
using DHARTAPI.RayTracing;
using DHARTAPI.Geometry;
using DHARTAPI.SpatialStructures;
using DHARTAPI.Pathfinding;

public class view_path : MonoBehaviour
{
    Node closest_node;

    // Start is called before the first frame update
    void Start()
    {
        // Change the path here to the full path to your own OBJ file. 
        string obj_path = @"C:\Users\stong\Downloads\CS\Research Summer 2022\test objs\energy_blob_zup.obj";

        // Load the OBj file from disk.
        MeshInfo PlaneMeshInfo = OBJLoader.LoadOBJ(obj_path);


        // Construct a meshinfo instance for the plane
        EmbreeBVH bvh1 = new EmbreeBVH(PlaneMeshInfo, true);


        Vector3D start_point = new Vector3D(-30, 0, 20);
        Vector3D spacing = new Vector3D(1, 1, 10);
        int max_nodes = 5000;
        float up_step = 5f;
        float down_step = 5f;
        float up_slope = 60f;
        float down_slope = 60f;
        int max_step_connections = 1;
        int cores = -1;

        

        Graph G = GraphGenerator.GenerateGraph(bvh1, start_point, spacing, max_nodes, up_step, up_slope, down_step, down_slope, max_step_connections, cores);

        NodeList nodes = G.getNodes();
        int max_node = nodes.array.Length;
        Debug.Log("Visualize a Path: Graph Generated with " + max_node + " nodes");

        int[,] p_desired = new int[,] { { -30, 0 }, { 30, 0 }};
        

        
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
