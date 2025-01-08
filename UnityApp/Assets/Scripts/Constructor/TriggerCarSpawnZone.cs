using UnityEngine;

public class TriggerCarSpawnZone : MonoBehaviour
{
    public delegate void CarZoneEventHandler();
    public event CarZoneEventHandler OnCarEnterZone;
    public event CarZoneEventHandler OnCarExitZone;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Вы в зоне!");
        if (other.gameObject.tag == TagObjectNamesTypes.CAR_IMG)
        {

            // Вызываем событие при входе в зону триггера
            OnCarEnterZone?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Вы снаружи зоне!");
        if (other.gameObject.tag == TagObjectNamesTypes.CAR_IMG)
        {

            // Вызываем событие при выходе из зоны триггера
            OnCarExitZone?.Invoke();
        }
    }
}
