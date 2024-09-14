using UnityEngine;

public abstract class RuleChecker : MonoBehaviour
{
    protected TicketData ticketData; // Данные билета
    protected RoadUserData[] roadUserDatas;

    public virtual void Initialize(TicketData ticketData)
    {
        this.ticketData = ticketData;
        roadUserDatas = ticketData.RoadUsersArr;
        // Инициализация правил
    }

    public virtual void Initialize()
    {
        // this.ticketData = ticketData;
        // Инициализация правил
    }

    public abstract bool IsMovementAllowed(RoadUserData roadUserData);
    // Возвращает true, если движение разрешено, false - иначе

    public abstract bool IsViolation(RoadUserData roadUserData);
    // Возвращает true, если движение является нарушением правил, false - иначе
}

