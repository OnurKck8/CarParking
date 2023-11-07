using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] LinesDrawer lineDrawer;

    [SerializeField] private CanvasGroup availableLineCanvasGroup;
    [SerializeField] private GameObject availableLineHolder;
    [SerializeField] private Image availableLineFill;
    public bool isAvailableLineUIAcitve = false;

    [SerializeField] Image fadePanel;
    [SerializeField] float fadeDuration;

    private Route activeRoute;

    void Start()
    {
        fadePanel.DOFade(0f, fadeDuration).From(1f);

        availableLineCanvasGroup.alpha = 0f;

        lineDrawer.OnBeginDraw  += OnBeginDrawHandler;
        lineDrawer.OnDraw += OnDrawHandler;
        lineDrawer.OnEndDraw += OnEndDrawHandler;
    }

    private void OnBeginDrawHandler(Route route)
    {
        activeRoute = route;
        availableLineFill.color = activeRoute.carColor;
        availableLineFill.fillAmount = 1f;
        availableLineCanvasGroup.DOFade(1f, .3f).From(0f);
        isAvailableLineUIAcitve = true;
    }

    private void OnDrawHandler()
    {
        if(isAvailableLineUIAcitve)
        {
            float maxLineLenght = activeRoute.maxLineLenght;
            float lineLenght = activeRoute.line.length;
            availableLineFill.fillAmount = 1f - (lineLenght / maxLineLenght);
        }
    }

    private void OnEndDrawHandler()
    {
        if (isAvailableLineUIAcitve)
        {
            isAvailableLineUIAcitve = false;
            activeRoute = null;
            availableLineCanvasGroup.DOFade(0f, .3f).From(1f);
        }
    }

}
