using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    [SerializeField] float minPointDistance;

    [HideInInspector]
    public List<Vector3> points = new();
    [HideInInspector]
    public int pointsCount = 0;
    [HideInInspector] 
    public float length=0f;

    private float pointsFixedYAxis;
    private Vector3 prevPoint;

    private void Start()
    {
        pointsFixedYAxis = lineRenderer.GetPosition(0).y;
        Clear();
    }

    public void Init()
    {
        gameObject.SetActive(true);
    }

    public void Clear()
    {
        gameObject.SetActive(false);
        lineRenderer.positionCount = 0;
        pointsCount = 0;
        points.Clear();
    }

    public void AddPoint(Vector3 newPoint)
    {
        newPoint.y = pointsFixedYAxis;

        if (pointsCount >= 1 && Vector3.Distance(newPoint, GetLastPoint()) < minPointDistance)
            return;

        //else:
        if (pointsCount == 0)
            prevPoint = newPoint;

        points.Add(newPoint);
        pointsCount++;

        length += Vector3.Distance(prevPoint, newPoint);
        prevPoint = newPoint;

        //lineRenderer update:
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);
    }

    private Vector3 GetLastPoint()
    {
        return lineRenderer.GetPosition(pointsCount - 1);
    }

    public void SetColor(Color color)
    {
        lineRenderer.sharedMaterials[0].color = color;
    }
}
