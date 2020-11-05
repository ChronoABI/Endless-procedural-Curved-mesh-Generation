using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class CurveGeneration : MonoBehaviour
{
    //Reference to the mesh we will generate
    Mesh mesh;

    //The points used to create the curve
    Vector3[] curve = new Vector3[4];

    // The vertices and triangles of the mesh
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        //Get a reference to the mesh and clear it
        var filter = GetComponent<MeshFilter>();
        mesh = filter.mesh;
        mesh.Clear();

        //Generate 4 random points for the top
        var xPos = 0f;
        for (int i = 0; i < curve.Length; i++)
        {
            var p = new Vector3(xPos, Random.Range(1f, 2f), 0f);
            curve[i] = p;
            //AddTerrainPoint(p);
            xPos += 0.5f;
        }
        //Number of points to draw, how smooth the curve is
        int resolution = 20;
       /* for (int i = 0; i & lt; resolution; i++)
        {

        }*/
        //Assign the vertices and triangles to the mesh
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
    }

    void AddTerrainPoint(Vector3 point)
    {
        //Create a correspoinding point along the bottom
        vertices.Add(new Vector3(point.x, 0f, 0f));//Then add our top
        vertices.Add(point);

        if (vertices.Count >= 4)
        {
            //Completed a new quad , create 2 triangles
            int start = vertices.Count - 4;
            triangles.Add(start + 0);
            triangles.Add(start + 1);
            triangles.Add(start + 2);
            triangles.Add(start + 1);
            triangles.Add(start + 3);
            triangles.Add(start + 2);
        }  
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0,Vector3 p1,Vector3 p2,Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }
}
