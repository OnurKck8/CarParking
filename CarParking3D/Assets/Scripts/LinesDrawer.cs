using System;
using UnityEngine;

public class LinesDrawer : MonoBehaviour
{
    [SerializeField] UserInput userInput;
    [SerializeField] int interactableLayer;

    private Line currentLine;
    private Route currentRoute;

    RaycastDetector raycastDetector = new();

    void Start()
    {
        userInput.OnMouseDown += OnMouseDownHandler;
        userInput.OnMouseMove += OnMouseMoveHandler;
        userInput.OnMouseUp   += OnMouseUpHandler;
    }
    private void OnMouseDownHandler()
    {
        ContactInfo contactInfo = raycastDetector.RayCast(interactableLayer);

        if(contactInfo.contacted)
        {
            bool isCar = contactInfo.collider.TryGetComponent(out Car _car);

            if(isCar && _car.route.isActive)
            {
                currentRoute = _car.route;
                currentLine = currentRoute.line;
                currentLine.Init();
            }
        }
    }

    private void OnMouseMoveHandler()
    {
        if(currentRoute != null)
        {
            ContactInfo contactInfo = raycastDetector.RayCast(interactableLayer);

            if (contactInfo.contacted)
            {
                Vector3 newPoint = contactInfo.point;

                currentLine.AddPoint(newPoint);

                bool isPark = contactInfo.collider.TryGetComponent(out Park _park);

                if (isPark)
                {
                    Route parkRoute = _park.route;
                    if(parkRoute==currentLine)
                    {
                       currentLine.AddPoint(contactInfo.transform.position);
                    }
                    else
                    {
                        currentLine.Clear();
                    }
                    OnMouseUpHandler();
                }
            }
        }
    }

    private void OnMouseUpHandler()
    {
        if (currentRoute != null)
        {
            ContactInfo contactInfo = raycastDetector.RayCast(interactableLayer);

            if(contactInfo.contacted)
            {
                bool isPark = contactInfo.collider.TryGetComponent(out Park _park);

                if(currentLine.pointsCount<3 || !isPark)
                {
                    currentLine.Clear();
                }
                else
                {
                    currentRoute.Disactivate();
                }
            }
            else
            {
                currentLine.Clear();
            }
        }
        ResetDrawer();
    }

    private void ResetDrawer()
    {
        currentRoute = null;
        currentLine = null;
    }
}
