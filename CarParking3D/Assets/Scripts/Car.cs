using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    public Route route;

    public Transform bottomTransform;
    public Transform bodyTransform;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Rigidbody rb;
    [SerializeField] float danceValue;//ne kadar yükselecek
    [SerializeField] float durationMultiplier;

    private void Start()
    {
        //calisiyormus gibi yapmak icin
        bodyTransform.DOLocalMoveY(danceValue, 0.1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }

    public void Move(Vector3[] path)
    {
        rb.DOLocalPath(path, 2f * durationMultiplier * path.Length)
            .SetLookAt(.01f, false)
            .SetEase(Ease.Linear);
    }

    public void SetColor(Color color)
    {
        meshRenderer.sharedMaterials[0].color = color;
    }
}
