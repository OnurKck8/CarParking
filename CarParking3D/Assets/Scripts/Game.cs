using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [HideInInspector] public List<Route> readyRoutes = new();

    private int totalRoutes;
    private int successFullParks;

    public UnityAction<Route> OnCarEntersPark;
    public UnityAction OnCarCollision;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        totalRoutes = transform.GetComponentsInChildren<Route>().Length;
        successFullParks = 0;

        OnCarEntersPark += OnCarEntersParkHandler;
        OnCarCollision += OnCarCollisionHandler;
    }

    private void OnCarEntersParkHandler(Route route)
    {
        route.car.StopDancingAnimation();
        successFullParks++;

        if(successFullParks==totalRoutes)
        {
            Debug.Log("You Win");
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            DOVirtual.DelayedCall(1.3f, () =>
            {
                if (nextLevel < SceneManager.sceneCountInBuildSettings)
                    SceneManager.LoadScene(nextLevel);
                else
                    Debug.LogWarning("No next level to load");
            });
        }
    }

    private void OnCarCollisionHandler()
    {
        Debug.Log("Game Over");

        DOVirtual.DelayedCall(2f, () =>
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentLevel);
        });
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
