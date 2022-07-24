using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    private Mesh _mesh;
    [SerializeField] private float fov = 90f;
    [SerializeField] private int rayCount = 15;
    [SerializeField] private float viewDistance = 10f;
    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private Transform parent;
    void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
    }

    private void DrawFieldOfView()
    {
        float angle = GetAngleFromVector(parent.forward) + fov / 2f;
        float angleIncrease = fov / rayCount;
        var origin = parent.position;

        var vertices = new Vector3[rayCount + 1 + 1];
        var uv = new Vector2[vertices.Length];
        var triangles = new int[rayCount * 3];

        vertices[0] = origin;
        var triangleIndex = 0;
        var vertexIndex = 1;

        for (int i = 0; i <= rayCount; i++)
        {
            var vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            RaycastHit rayCastHit;
            var hit = Physics.Raycast(origin, GetVectorFromAngle(angle), out rayCastHit, viewDistance, obstacleLayerMask);
            if (hit)
            {
                vertex = rayCastHit.point;
            }
            vertices[vertexIndex] = vertex;
            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }

        _mesh.vertices = vertices;
        _mesh.uv = uv;
        _mesh.triangles = triangles;

        _mesh.RecalculateBounds();
        _mesh.Optimize();
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;
        return new Vector3(MathF.Cos(angleRad), 0f, MathF.Sin(angleRad));
    }

    private float GetAngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float n = MathF.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360f;
        return n;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        DrawFieldOfView();
    }
}
