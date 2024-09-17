using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadUserData
{
    public int Id { get; set; }
    public string TypeParticipant { get; set; } // Тип участника движения Car, Human, Train, SpecCar
    public string ModelName { get; set; } // Название модели для поиска префаба
    public string SidePosition { get; set; } // Положение на сцене North, South, East, West
    public string NumberPosition { get; set; } // Позиция после положения 1, 2, 3 (если несколько полос движения)
    public string MovementDirection { get; set; } // Направление движения Farward, Backward, Left, Right
}
