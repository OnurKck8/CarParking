using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [HideInInspector] public List<Route> readyRoutes = new();

    private int totalRoutes;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        totalRoutes = transform.GetComponentsInChildren<Route>().Length;
    }

    public void RegisterRoute(Route route)
    {
        readyRoutes.Add(route);

        if(readyRoutes.Count == totalRoutes)
        {
            MoveAllCars();
        }
    }

    private void MoveAllCars()
    {
        foreach (var route in readyRoutes)
        {
            route.car.Move(route.linePoints);
        }
    }
}
