 using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public LayerMask circleLayerMask;

    private List<Vector3> linePositions = new List<Vector3>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            linePositions.Clear();
            lineRenderer.positionCount = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            linePositions.Add(mousePosition);

            lineRenderer.positionCount = linePositions.Count;
            lineRenderer.SetPositions(linePositions.ToArray());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D[] hits = Physics2D.LinecastAll(linePositions[0], linePositions[linePositions.Count - 1], circleLayerMask);

            foreach (RaycastHit2D hit in hits)
            {
                Destroy(hit.collider.gameObject);
            }

            linePositions.Clear();
            lineRenderer.positionCount = 0;
        }
    }
}
