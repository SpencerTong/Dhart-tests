using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using DHARTAPI;
using DHARTAPI.GraphGenerator;
using DHARTAPI.RayTracing;
using DHARTAPI.Geometry;
using DHARTAPI.ViewAnalysis;
using DHARTAPI.Pathfinding;
using DHARTAPI.SpatialStructures;
using DHARTAPI.VisibilityGraph;
using DHARTAPI.NativeUtils.CommonNativeArrays;


public class VA_Graph_Connections : MonoBehaviour
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
        string path = "VA_Graph_Connections_List.csv";


        Graph G = GraphGenerator.GenerateGraph(bvh1, start_point, spacing, max_nodes, up_step, up_slope, down_step, down_slope, max_step_connections, cores);
        CSRInfo csr = G.CompressToCSR();
        NodeList nodes = G.getNodes();
        Debug.Log("visualize a visiblity graph: "+ nodes.array.Length);
        float height = 1.7f;
        Vector3D[] nodesIE = nodes.ToVector3D();
        Graph VG = VisibilityGraph.GenerateAllToAll(bvh1, nodesIE, height);
        CSRInfo visibility_graph = VG.CompressToCSR();
        ManagedFloatArray scores = VG.AggregateEdgeCosts(GraphEdgeAggregation.SUM, true);

        
        bool new_file = true;
        int count = 0;
        foreach (Vector3D v in nodesIE)
        {
            if (new_file)
            {
                File.WriteAllText(path, v.x + "," + v.y + "," + v.z + "," + scores.array[count] + Environment.NewLine);
                count++;
                new_file = false;
            }
            else
            {
                File.AppendAllText(path, v.x + "," + v.y + "," + v.z + "," + scores.array[count] + Environment.NewLine);
                count++;
            }
        }


        //x,y,z, node value (indices correspond)
        //pos = nodes and color = scores



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
