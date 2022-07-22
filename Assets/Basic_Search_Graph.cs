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
using DHARTAPI.VisibilityGraph;

public class Basic_Search_Graph : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string obj_path = @"C:\Users\stong\Downloads\CS\Research Summer 2022\test objs\plane.obj";
        MeshInfo PlaneMeshInfo = OBJLoader.LoadOBJ(obj_path);
        PlaneMeshInfo.RotateMesh(CommonRotations.Yup_To_Zup);
        EmbreeBVH bvh1 = new EmbreeBVH(PlaneMeshInfo, true);
        Vector3D start_point = new Vector3D(0, 0, 1);
        Vector3D spacing = new Vector3D(1, 1, 1);
        int max_nodes = 100000;

        Graph G = GraphGenerator.GenerateGraph(bvh1,
                start_point,
                spacing,
                max_nodes
            );
        G.CompressToCSR();
        CostAlgorithms.CalculateAndStoreEnergyExpenditure(G);


        NodeList nodes = G.getNodes();
        Debug.Log("Basic search: Graph generated with " + nodes.array.Length + " nodes");

        var start_node = nodes[0].ToVector3D();
        var end_node = nodes[100].ToVector3D();

        Debug.Log("Start: " + start_node);
        Debug.Log("End: " + end_node);

        Path distance_path = ShortestPath.DijkstraShortestPath(G, 0, 100);

        Debug.Log("distance path" + distance_path);

        var path_ids = new int[distance_path.size];

        for (int i =0; i< distance_path.size; i++)
        {
            path_ids[i] = distance_path[i].id;     
         }

        foreach (int x in path_ids)
        {
            Debug.Log("path id: " + x);

        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //        System.IO.Directory.CreateDirectory(path_string1);
    //        path1 = System.IO.Path.Combine(path_string1, categories[1]+"_Participant_Order_Horizontal_cw.csv");
    //        File.WriteAllText(path1, y + "," + y +",Forward Order,"+ curr_landmark_order.ToString() + Environment.NewLine);
    //        File.AppendAllText(path1, y + "," + y +",Forward Order,"+ curr_landmark_order.ToString() + Environment.NewLine);



}
