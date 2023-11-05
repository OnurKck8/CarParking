using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    public Route route;
    public Transform bottomTransform;
    public Transform bodyTransform;

    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] ParticleSystem smokeFx;
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent(out Car otherCar))
        {
            StopDancingAnimation();
            rb.DOKill(false);

            //add explosion:
            Vector3 hitPoint = collision.contacts[0].point;
            AddExplosionForce(hitPoint);
            smokeFx.Play();

            Game.Instance.OnCarCollision?.Invoke();
        }
    }

    private void AddExplosionForce(Vector3 point)
    {
        rb.AddExplosionForce(400f, point, 3f);
        rb.AddForceAtPosition(Vector3.up * 2f, point, ForceMode.Impulse);
        rb.AddTorque(new Vector3(GetRandomAngle(), GetRandomAngle(), GetRandomAngle()));
    }
    private float GetRandomAngle()
    {
        float angle = 10f;
        float rand = Random.value;
        return rand > .5f ? angle : -angle;
    }

    public void Move(Vector3[] path)
    {
        rb.DOLocalPath(path, 2f * durationMultiplier * path.Length)
            .SetLookAt(.01f, false)
            .SetEase(Ease.Linear);
    }
    public void StopDancingAnimation()
    {
        bodyTransform.DOKill(true);
    }

    public void SetColor(Color color)
    {
        meshRenderer.sharedMaterials[0].color = color;
    }
}
