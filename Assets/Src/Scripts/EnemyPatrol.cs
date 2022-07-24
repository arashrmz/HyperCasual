using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float patrolRadius = 10f;
    [SerializeField] private float scale = 2f;

    private Vector3[] _patrolPoints;

    void Start()
    {
        _patrolPoints = PointsInCircle(transform.position, patrolRadius, scale).ToArray();
    }

    bool InCircle(Vector2 point, Vector2 circlePoint, float radius)
    {
        return (point - circlePoint).sqrMagnitude <= radius * radius;
    }

    private bool CanSetDestination(Vector3 targetDestination)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetDestination, out hit, 1f, NavMesh.AllAreas))
        {
            return true;
        }
        return false;
    }
    IEnumerable<Vector3> PointsInCircle(Vector3 circlePos, float radius, float scale)
    {
        var minX = circlePos.x - radius;
        var maxX = circlePos.x + radius;

        var minY = circlePos.z - radius;
        var maxY = circlePos.z + radius;

        for (var y = minY; y <= maxY; y += scale)
        {
            for (var x = minX; x <= maxX; x += scale)
            {
                if (InCircle(new Vector3(x, 1f, y), circlePos, radius)
                && CanSetDestination(new Vector3(x, 1f, y)))
                {
                    yield return new Vector3(x, 1f, y);
                }
            }
        }
    }

    public Vector3 RandomPatrolPoint
    {
        get
        {
            var index = Random.Range(0, _patrolPoints.Length);
            return _patrolPoints[index];
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
        Gizmos.color = Color.blue;
        if (_patrolPoints != null)
        {
            foreach (var point in _patrolPoints)
            {
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
