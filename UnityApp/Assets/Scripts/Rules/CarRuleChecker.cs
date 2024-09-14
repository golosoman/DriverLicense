using UnityEngine;
using System.Collections.Generic;

public class CarRuleChecker : RuleChecker
{
    private RoadUserManager roadUserManager;

    public override void Initialize(TicketData ticketData)
    {
        this.ticketData = ticketData;
        // Получаем RoadUserManager
        roadUserManager = FindObjectOfType<RoadUserManager>();
    }

    public override bool IsMovementAllowed(RoadUserData roadUserData)
    {
        // Проверка разрешения движения для машины
        // 1. Проверка светофора
        // if (CheckTrafficLight(roadUserData)) {
        //     return true;
        // }
        // 2. Проверка знаков
        // if (CheckSigns(roadUserData)) {
        //     return true;
        // }
        // 3. Проверка помехи справа
        if (CheckRightOfWay(roadUserData)) {
            return true;
        }

        // 4. Проверка свободного пути (простое условие, можно расширить)
        // if (CheckFreePath(roadUserData)) {
        //     return true;
        // }

        // Если ни одно условие не выполнено, движение запрещено
        return false;
    }

    private bool CheckTrafficLight(RoadUserData roadUserData)
    {
        // 1. Проверка наличия светофора
        if (ticketData.TrafficLightsArr.Length == 0) {
            return true; // Светофора нет
        }

        // 2. Проверка состояния светофора
        //   Ищем светофор на той стороне перекрестка, где находится машина
        foreach (TrafficLightData trafficLight in ticketData.TrafficLightsArr)
        {
            if (trafficLight.SidePosition == roadUserData.SidePosition)
            {
                //  TODO: Логика проверки состояния светофора (красный, зеленый, желтый)
                //  Пример:
                if (trafficLight.ModelName == "green") {
                    return true; // Движение разрешено
                }
            }
        }

        return false; // Светофор есть, но движение запрещено
    }

    private bool CheckSigns(RoadUserData roadUserData)
    {
        // 1. Проверка наличия знаков
        if (ticketData.SignsArr.Length == 0) {
            return true; // Знаков нет
        }

        // 2. Проверка знаков на той стороне перекрестка, где находится машина
        foreach (SignData sign in ticketData.SignsArr)
        {
            if (sign.SidePosition == roadUserData.SidePosition)
            {
                // TODO: Логика проверки знаков (например, "Уступи дорогу")
                //  Пример:
                if (sign.ModelName == "YieldSign") {
                    return false; // Движение запрещено из-за знака
                }
            }
        }

        return true; // Знаки есть, но движение разрешено
    }

   private bool CheckRightOfWay(RoadUserData roadUserData)
    {
        List<RoadUserData> carRoadUsers = roadUserManager.SelectedRoadUsers;
        Debug.Log(carRoadUsers.ToString());
        // Проверка помехи справа для движения прямо или направо
        if (roadUserData.SidePosition == "south" && roadUserData.MovementDirection == "forward") {
            foreach (RoadUserData otherCar in carRoadUsers)
            {
                if ( (otherCar.SidePosition == "east" && otherCar.MovementDirection=="forward") || 
                     (otherCar.SidePosition == "east" && otherCar.MovementDirection=="left") ) return false;
            }
        }
        if (roadUserData.SidePosition == "south" && roadUserData.MovementDirection == "left") {
            foreach (RoadUserData otherCar in carRoadUsers)
            {
                if ( (otherCar.SidePosition == "east" && otherCar.MovementDirection=="forward") || 
                     (otherCar.SidePosition == "east" && otherCar.MovementDirection=="left") ) return false;

                if ( (otherCar.SidePosition == "north" && otherCar.MovementDirection=="forward") || 
                     (otherCar.SidePosition == "north" && otherCar.MovementDirection=="right") ) return false;
            }
        }
        if (roadUserData.SidePosition == "south" && roadUserData.MovementDirection == "backward") {
            foreach (RoadUserData otherCar in carRoadUsers)
            {
                // if ( (otherCar.SidePosition == "east" && otherCar.MovementDirection=="forward") || 
                //      (otherCar.SidePosition == "east" && otherCar.MovementDirection=="left") ) return false;

                // if ( (otherCar.SidePosition == "north" && otherCar.MovementDirection=="forward") || 
                //      (otherCar.SidePosition == "north" && otherCar.MovementDirection=="right") ) return false;

                //Пропускай всех
            }
        }


        // Проверка помехи справа для движения прямо или направо
        if (roadUserData.SidePosition == "east" && roadUserData.MovementDirection == "forward") {
            foreach (RoadUserData otherCar in carRoadUsers)
            {
                if ( (otherCar.SidePosition == "north" && otherCar.MovementDirection=="forward") || 
                     (otherCar.SidePosition == "north" && otherCar.MovementDirection=="left") ) Debug.Log("Епта блять, не пропустил с востока");
                    //  return false;
            }
        }
        if (roadUserData.SidePosition == "east" && roadUserData.MovementDirection == "left") {
            foreach (RoadUserData otherCar in carRoadUsers)
            {
                if ( (otherCar.SidePosition == "north" && otherCar.MovementDirection=="forward") || 
                     (otherCar.SidePosition == "north" && otherCar.MovementDirection=="left") ) return false;

                if ( (otherCar.SidePosition == "west" && otherCar.MovementDirection=="forward") || 
                     (otherCar.SidePosition == "west" && otherCar.MovementDirection=="right") ) return false;
            }
        }
        if (roadUserData.SidePosition == "south" && roadUserData.MovementDirection == "backward") {
            foreach (RoadUserData otherCar in carRoadUsers)
            {
                // if ( (otherCar.SidePosition == "east" && otherCar.MovementDirection=="forward") || 
                //      (otherCar.SidePosition == "east" && otherCar.MovementDirection=="left") ) return false;

                // if ( (otherCar.SidePosition == "north" && otherCar.MovementDirection=="forward") || 
                //      (otherCar.SidePosition == "north" && otherCar.MovementDirection=="right") ) return false;

                //Пропускай всех
            }
        }

        // Проверка помехи справа для движения прямо или направо
        if (roadUserData.SidePosition == "north" && roadUserData.MovementDirection == "forward") {
            foreach (RoadUserData otherCar in carRoadUsers)
            {
                if ( (otherCar.SidePosition == "west" && otherCar.MovementDirection=="forward") || 
                     (otherCar.SidePosition == "west" && otherCar.MovementDirection=="left") ) return false;
            }
        }
        if (roadUserData.SidePosition == "north" && roadUserData.MovementDirection == "left") {
            foreach (RoadUserData otherCar in carRoadUsers)
            {
                if ( (otherCar.SidePosition == "west" && otherCar.MovementDirection=="forward") || 
                     (otherCar.SidePosition == "west" && otherCar.MovementDirection=="left") ) return false;

                if ( (otherCar.SidePosition == "south" && otherCar.MovementDirection=="forward") || 
                     (otherCar.SidePosition == "south" && otherCar.MovementDirection=="right") ) return false;
            }
        }
        if (roadUserData.SidePosition == "south" && roadUserData.MovementDirection == "backward") {
            foreach (RoadUserData otherCar in carRoadUsers)
            {
                // if ( (otherCar.SidePosition == "east" && otherCar.MovementDirection=="forward") || 
                //      (otherCar.SidePosition == "east" && otherCar.MovementDirection=="left") ) return false;

                // if ( (otherCar.SidePosition == "north" && otherCar.MovementDirection=="forward") || 
                //      (otherCar.SidePosition == "north" && otherCar.MovementDirection=="right") ) return false;

                //Пропускай всех
            }
        }

        // Проверка помехи справа для движения прямо или направо
        if (roadUserData.SidePosition == "west" && roadUserData.MovementDirection == "forward") {
            foreach (RoadUserData otherCar in carRoadUsers)
            {
                if ( (otherCar.SidePosition == "south" && otherCar.MovementDirection=="forward") || 
                     (otherCar.SidePosition == "south" && otherCar.MovementDirection=="left") ) return false;
            }
        }
        if (roadUserData.SidePosition == "west" && roadUserData.MovementDirection == "left") {
            foreach (RoadUserData otherCar in carRoadUsers)
            {
                if ( (otherCar.SidePosition == "south" && otherCar.MovementDirection=="forward") || 
                     (otherCar.SidePosition == "south" && otherCar.MovementDirection=="left") ) return false;

                if ( (otherCar.SidePosition == "east" && otherCar.MovementDirection=="forward") || 
                     (otherCar.SidePosition == "east" && otherCar.MovementDirection=="right") ) return false;
            }
        }
        if (roadUserData.SidePosition == "south" && roadUserData.MovementDirection == "backward") {
            foreach (RoadUserData otherCar in carRoadUsers)
            {
                // if ( (otherCar.SidePosition == "east" && otherCar.MovementDirection=="forward") || 
                //      (otherCar.SidePosition == "east" && otherCar.MovementDirection=="left") ) return false;

                // if ( (otherCar.SidePosition == "north" && otherCar.MovementDirection=="forward") || 
                //      (otherCar.SidePosition == "north" && otherCar.MovementDirection=="right") ) return false;

                //Пропускай всех
            }
        }
        

        // Если машина поворачивает н, то пропускаем всех
        return true;
    }

    private bool CheckFreePath(RoadUserData roadUserData)
    {
        //  TODO: Логика проверки свободного пути 
        //  Пример:
        //  Проверка наличия других машин на той же полосе движения
        //  и в том же направлении, что и текущая машина
        return true;
    }


    public override bool IsViolation(RoadUserData roadUserData)
    {
        // Проверка на нарушение правил движения для машины
        //  Например, проезд на красный свет, выезд на встречную полосу
        return false;
    }
}
