using UnityEngine;

public abstract class CarPointHandler : MonoBehaviour
{
    [SerializeField]
    private float radiusZone = 1.5f;

    protected virtual void OnEnable()
    {
        SubscribeToEvents();
    }

    protected virtual void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    protected abstract void SubscribeToEvents();
    protected abstract void UnsubscribeFromEvents();

    protected GameObject CarInSpawnPoint()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radiusZone);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Car"))
            {
                return collider.gameObject;
            }
        }
        return null;
    }
}
