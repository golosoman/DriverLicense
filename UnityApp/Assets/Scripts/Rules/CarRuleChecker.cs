// using UnityEngine;
// using System.Collections.Generic;

// public class CarRuleChecker : RuleChecker
// {
//     private RoadUserManager roadUserManager;
//     private CarMovement carMovement;
//     private Dictionary<string, bool> seeTransport = new Dictionary<string, bool>(){
//         { "north", false},
//         { "south", false},
//         { "east", false},
//         {"west", false}
//     };
//     public override void Initialize(TicketData ticketData)
//     {
//         this.ticketData = ticketData;
//         // Получаем RoadUserManager
//         roadUserManager = FindObjectOfType<RoadUserManager>();
//         carMovement = GetComponent<CarMovement>();
//     }

//     public override bool IsMovementAllowed(RoadUserData roadUserData)
//     {
//         // Проверка разрешения движения для машины
//         // 1. Проверка светофора
//         // if (CheckTrafficLight(roadUserData)) {
//         //     return true;
//         // }
//         // // 2. Проверка знаков
//         // else if (CheckSigns(roadUserData)) {
//         //     return true;
//         // }
//         // 3. Проверка помехи справа
//         if (CheckRightOfWay(roadUserData)) {
//             return true;
//         }

//         // 4. Проверка свободного пути (простое условие, можно расширить)
//         // if (CheckFreePath(roadUserData)) {
//         //     return true;
//         // }

//         // Если ни одно условие не выполнено, движение запрещено
//         return false;
//     }

//     private bool CheckTrafficLight(RoadUserData roadUserData)
//     {
//         List<RoadUserData> carRoadUsers = roadUserManager.SelectedRoadUsers;

//         //   Ищем светофор на той стороне перекрестка, где находится машина
//         foreach (TrafficLightData trafficLight in ticketData.TrafficLightsArr)
//         {
//             if (trafficLight.SidePosition == roadUserData.SidePosition)
//             {
//                 if(trafficLight.State == "green" && roadUserData.SidePosition == "south" && roadUserData.MovementDirection == "left"){
//                     foreach (RoadUserData otherCar in carRoadUsers)
//                     {
//                         if ( (otherCar.SidePosition == "north" && otherCar.MovementDirection=="forward") || 
//                             (otherCar.SidePosition == "north" && otherCar.MovementDirection=="right") ) return false;

//                         if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="left" && !carMovement.PBPR) {carMovement.PBPR = true; continue;}
//                         if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="left" && carMovement.PBPR) return false;
//                     }
//                 }
//                 if (trafficLight.State == "green") {
//                     return true; // Движение разрешено
//                 }
//             }
//         }

//         return false; // Светофор есть, но движение запрещено
//     }

//     private bool CheckSigns(RoadUserData roadUserData)
//     {
//         // 1. Проверка наличия знаков
//         if (ticketData.SignsArr.Length == 0) {
//             return true; // Знаков нет
//         }

//         // 2. Проверка знаков на той стороне перекрестка, где находится машина
//         foreach (SignData sign in ticketData.SignsArr)
//         {
//             if (sign.SidePosition == roadUserData.SidePosition)
//             {
//                 // TODO: Логика проверки знаков (например, "Уступи дорогу")
//                 //  Пример:
//                 if (sign.ModelName == "YieldSign") {
//                     return false; // Движение запрещено из-за знака
//                 }
//             }
//         }

//         return true; // Знаки есть, но движение разрешено
//     }

//    private bool CheckRightOfWay(RoadUserData roadUserData)
//     {
//         // carMovement.PBPR = false;
//         List<RoadUserData> carRoadUsers = roadUserManager.SelectedRoadUsers;
//         Debug.Log(carRoadUsers.ToString());
//         // Приверка движения с Юга
//         if (roadUserData.SidePosition == "south" && roadUserData.MovementDirection == "forward") {
//             foreach (RoadUserData otherCar in carRoadUsers)
//             {
//                 if ( (otherCar.SidePosition == "east" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "east" && otherCar.MovementDirection=="left") ||
//                      (otherCar.SidePosition == "east" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "east" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["east"] = true; continue;}
//                 if (otherCar.SidePosition == "east" && otherCar.MovementDirection=="backward" && seeTransport["east"]) return false;
//             }
//         }
//         if (roadUserData.SidePosition == "south" && roadUserData.MovementDirection == "left") {
//             foreach (RoadUserData otherCar in carRoadUsers)
//             {
//                 if ( (otherCar.SidePosition == "east" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "east" && otherCar.MovementDirection=="left") ||
//                      (otherCar.SidePosition == "east" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "east" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["east"] = true; continue;}
//                 if (otherCar.SidePosition == "east" && otherCar.MovementDirection=="backward" && seeTransport["east"]) return false;

//                 if ( (otherCar.SidePosition == "north" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "north" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="left" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["north"] = true; continue;}
//                 if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="left" && seeTransport["north"]) return false;
//                 if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["north"] = true; continue;}
//                 if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="backward" && seeTransport["north"]) return false;
//             }
//         }
//         if (roadUserData.SidePosition == "south" && roadUserData.MovementDirection == "backward") {
//             foreach (RoadUserData otherCar in carRoadUsers)
//             {
//                 if ((otherCar.SidePosition == "east" && otherCar.MovementDirection=="forward") || 
//                     (otherCar.SidePosition == "east" && otherCar.MovementDirection=="left") || 
//                     (otherCar.SidePosition == "east" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "east" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["east"] = true; continue;}
//                 if (otherCar.SidePosition == "east" && otherCar.MovementDirection=="backward" && seeTransport["east"]) return false;

//                 if ( (otherCar.SidePosition == "north" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "north" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="left" && !carMovement.PBPR) {seeTransport["north"] = true;carMovement.PBPR = true; continue;}
//                 if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="left" && seeTransport["north"]) return false;
//                 if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["north"] = true; continue;}
//                 if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="backward" && seeTransport["north"]) return false;

//                 if(((otherCar.SidePosition == "west" && otherCar.MovementDirection=="forward") || 
//                     (otherCar.SidePosition == "west" && otherCar.MovementDirection=="left") || 
//                     (otherCar.SidePosition == "west" && otherCar.MovementDirection=="right")) && !carMovement.PBPR) {seeTransport["west"] = true;carMovement.PBPR = true; continue;}
//                 if(((otherCar.SidePosition == "west" && otherCar.MovementDirection=="forward") || 
//                     (otherCar.SidePosition == "west" && otherCar.MovementDirection=="left") || 
//                     (otherCar.SidePosition == "west" && otherCar.MovementDirection=="right")) && seeTransport["west"]) return false;
//                 if(otherCar.SidePosition == "west" && otherCar.MovementDirection=="backward" && !carMovement.PBPR ) {carMovement.PBPR = true; seeTransport["west"] = true; continue;}
//                 if (otherCar.SidePosition == "west" && otherCar.MovementDirection=="backward" && seeTransport["west"]) return false;
//             }
//         }


//         // Проверка движения с востока
//         if (roadUserData.SidePosition == "east" && roadUserData.MovementDirection == "forward") {
//             foreach (RoadUserData otherCar in carRoadUsers)
//             {
//                 if ( (otherCar.SidePosition == "north" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "north" && otherCar.MovementDirection=="left") ||
//                      (otherCar.SidePosition == "north" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["north"] = true; continue;}
//                 if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="backward" && seeTransport["north"]) return false;
//             }
//         }
//         if (roadUserData.SidePosition == "east" && roadUserData.MovementDirection == "left") {
//             foreach (RoadUserData otherCar in carRoadUsers)
//             {
//                 if ( (otherCar.SidePosition == "north" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "north" && otherCar.MovementDirection=="left") ||
//                      (otherCar.SidePosition == "north" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["north"] = true; continue;}
//                 if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="backward" && seeTransport["north"]) return false;

//                 if ( (otherCar.SidePosition == "west" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "west" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "west" && otherCar.MovementDirection=="left" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["west"] = true; continue;}
//                 if (otherCar.SidePosition == "west" && otherCar.MovementDirection=="left" && seeTransport["west"]) return false;
//                 if (otherCar.SidePosition == "west" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["west"] = true; continue;}
//                 if (otherCar.SidePosition == "west" && otherCar.MovementDirection=="backward" && seeTransport["west"]) return false;
//             }
//         }
//         if (roadUserData.SidePosition == "east" && roadUserData.MovementDirection == "backward") {
//             foreach (RoadUserData otherCar in carRoadUsers)
//             {
//                 if ((otherCar.SidePosition == "north" && otherCar.MovementDirection=="forward") || 
//                     (otherCar.SidePosition == "north" && otherCar.MovementDirection=="left") || 
//                     (otherCar.SidePosition == "north" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["north"] = true; continue;}
//                 if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="backward" && seeTransport["north"]) return false;

//                 if ( (otherCar.SidePosition == "west" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "west" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "west" && otherCar.MovementDirection=="left" && !carMovement.PBPR) {seeTransport["west"] = true;carMovement.PBPR = true; continue;}
//                 if (otherCar.SidePosition == "west" && otherCar.MovementDirection=="left" && seeTransport["west"]) return false;
//                 if (otherCar.SidePosition == "west" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["west"] = true; continue;}
//                 if (otherCar.SidePosition == "west" && otherCar.MovementDirection=="backward" && seeTransport["west"]) return false;

//                 if(((otherCar.SidePosition == "south" && otherCar.MovementDirection=="forward") || 
//                     (otherCar.SidePosition == "south" && otherCar.MovementDirection=="left") || 
//                     (otherCar.SidePosition == "south" && otherCar.MovementDirection=="right")) && !carMovement.PBPR) {seeTransport["south"] = true;carMovement.PBPR = true; continue;}
//                 if(((otherCar.SidePosition == "south" && otherCar.MovementDirection=="forward") || 
//                     (otherCar.SidePosition == "south" && otherCar.MovementDirection=="left") || 
//                     (otherCar.SidePosition == "south" && otherCar.MovementDirection=="right")) && seeTransport["south"]) return false;
//                 if(otherCar.SidePosition == "south" && otherCar.MovementDirection=="backward" && !carMovement.PBPR ) {carMovement.PBPR = true; seeTransport["south"] = true; continue;}
//                 if (otherCar.SidePosition == "south" && otherCar.MovementDirection=="backward" && seeTransport["south"]) return false;
//             }
//         }


//         // Проверка движения с севера
//         if (roadUserData.SidePosition == "north" && roadUserData.MovementDirection == "forward") {
//             foreach (RoadUserData otherCar in carRoadUsers)
//             {
//                 if ( (otherCar.SidePosition == "west" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "west" && otherCar.MovementDirection=="left") ||
//                      (otherCar.SidePosition == "west" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "west" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["west"] = true; continue;}
//                 if (otherCar.SidePosition == "west" && otherCar.MovementDirection=="backward" && seeTransport["west"]) return false;
//             }
//         }
//         if (roadUserData.SidePosition == "north" && roadUserData.MovementDirection == "left") {
//             foreach (RoadUserData otherCar in carRoadUsers)
//             {
//                 if ( (otherCar.SidePosition == "west" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "west" && otherCar.MovementDirection=="left") ||
//                      (otherCar.SidePosition == "west" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "west" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["west"] = true; continue;}
//                 if (otherCar.SidePosition == "west" && otherCar.MovementDirection=="backward" && seeTransport["west"]) return false;

//                 if ( (otherCar.SidePosition == "south" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "south" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "south" && otherCar.MovementDirection=="left" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["south"] = true; continue;}
//                 if (otherCar.SidePosition == "south" && otherCar.MovementDirection=="left" && seeTransport["south"]) return false;
//                 if (otherCar.SidePosition == "south" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["south"] = true; continue;}
//                 if (otherCar.SidePosition == "south" && otherCar.MovementDirection=="backward" && seeTransport["south"]) return false;
//             }
//         }
//         if (roadUserData.SidePosition == "north" && roadUserData.MovementDirection == "backward") {
//             foreach (RoadUserData otherCar in carRoadUsers)
//             {
//                 if ((otherCar.SidePosition == "west" && otherCar.MovementDirection=="forward") || 
//                     (otherCar.SidePosition == "west" && otherCar.MovementDirection=="left") || 
//                     (otherCar.SidePosition == "west" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "west" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["west"] = true; continue;}
//                 if (otherCar.SidePosition == "west" && otherCar.MovementDirection=="backward" && seeTransport["west"]) return false;

//                 if ( (otherCar.SidePosition == "south" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "south" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "south" && otherCar.MovementDirection=="left" && !carMovement.PBPR) {seeTransport["south"] = true;carMovement.PBPR = true; continue;}
//                 if (otherCar.SidePosition == "south" && otherCar.MovementDirection=="left" && seeTransport["south"]) return false;
//                 if (otherCar.SidePosition == "south" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["south"] = true; continue;}
//                 if (otherCar.SidePosition == "south" && otherCar.MovementDirection=="backward" && seeTransport["south"]) return false;

//                 if(((otherCar.SidePosition == "eath" && otherCar.MovementDirection=="forward") || 
//                     (otherCar.SidePosition == "eath" && otherCar.MovementDirection=="left") || 
//                     (otherCar.SidePosition == "eath" && otherCar.MovementDirection=="right")) && !carMovement.PBPR) {seeTransport["eath"] = true;carMovement.PBPR = true; continue;}
//                 if(((otherCar.SidePosition == "eath" && otherCar.MovementDirection=="forward") || 
//                     (otherCar.SidePosition == "eath" && otherCar.MovementDirection=="left") || 
//                     (otherCar.SidePosition == "eath" && otherCar.MovementDirection=="right")) && seeTransport["eath"]) return false;
//                 if(otherCar.SidePosition == "eath" && otherCar.MovementDirection=="backward" && !carMovement.PBPR ) {carMovement.PBPR = true; seeTransport["eath"] = true; continue;}
//                 if (otherCar.SidePosition == "eath" && otherCar.MovementDirection=="backward" && seeTransport["eath"]) return false;
//             }
//         }

//         // Проверка движения с запада
//         if (roadUserData.SidePosition == "west" && roadUserData.MovementDirection == "forward") {
//             foreach (RoadUserData otherCar in carRoadUsers)
//             {
//                 if ( (otherCar.SidePosition == "south" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "south" && otherCar.MovementDirection=="left") ||
//                      (otherCar.SidePosition == "south" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "south" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["south"] = true; continue;}
//                 if (otherCar.SidePosition == "south" && otherCar.MovementDirection=="backward" && seeTransport["south"]) return false;
//             }
//         }
//         if (roadUserData.SidePosition == "west" && roadUserData.MovementDirection == "left") {
//             foreach (RoadUserData otherCar in carRoadUsers)
//             {
//                 if ( (otherCar.SidePosition == "south" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "south" && otherCar.MovementDirection=="left") ||
//                      (otherCar.SidePosition == "south" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "south" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["south"] = true; continue;}
//                 if (otherCar.SidePosition == "south" && otherCar.MovementDirection=="backward" && seeTransport["south"]) return false;

//                 if ( (otherCar.SidePosition == "east" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "east" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "east" && otherCar.MovementDirection=="left" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["east"] = true; continue;}
//                 if (otherCar.SidePosition == "east" && otherCar.MovementDirection=="left" && seeTransport["east"]) return false;
//                 if (otherCar.SidePosition == "east" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["east"] = true; continue;}
//                 if (otherCar.SidePosition == "east" && otherCar.MovementDirection=="backward" && seeTransport["east"]) return false;
//             }
//         }
//         if (roadUserData.SidePosition == "west" && roadUserData.MovementDirection == "backward") {
//             foreach (RoadUserData otherCar in carRoadUsers)
//             {
//                 if ((otherCar.SidePosition == "south" && otherCar.MovementDirection=="forward") || 
//                     (otherCar.SidePosition == "south" && otherCar.MovementDirection=="left") || 
//                     (otherCar.SidePosition == "south" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "south" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["south"] = true; continue;}
//                 if (otherCar.SidePosition == "south" && otherCar.MovementDirection=="backward" && seeTransport["south"]) return false;

//                 if ( (otherCar.SidePosition == "east" && otherCar.MovementDirection=="forward") || 
//                      (otherCar.SidePosition == "east" && otherCar.MovementDirection=="right") ) return false;
//                 if (otherCar.SidePosition == "east" && otherCar.MovementDirection=="left" && !carMovement.PBPR) {seeTransport["east"] = true;carMovement.PBPR = true; continue;}
//                 if (otherCar.SidePosition == "east" && otherCar.MovementDirection=="left" && seeTransport["east"]) return false;
//                 if (otherCar.SidePosition == "east" && otherCar.MovementDirection=="backward" && !carMovement.PBPR) {carMovement.PBPR = true; seeTransport["east"] = true; continue;}
//                 if (otherCar.SidePosition == "east" && otherCar.MovementDirection=="backward" && seeTransport["east"]) return false;

//                 if(((otherCar.SidePosition == "north" && otherCar.MovementDirection=="forward") || 
//                     (otherCar.SidePosition == "north" && otherCar.MovementDirection=="left") || 
//                     (otherCar.SidePosition == "north" && otherCar.MovementDirection=="right")) && !carMovement.PBPR) {seeTransport["north"] = true;carMovement.PBPR = true; continue;}
//                 if(((otherCar.SidePosition == "north" && otherCar.MovementDirection=="forward") || 
//                     (otherCar.SidePosition == "north" && otherCar.MovementDirection=="left") || 
//                     (otherCar.SidePosition == "north" && otherCar.MovementDirection=="right")) && seeTransport["north"]) return false;
//                 if(otherCar.SidePosition == "north" && otherCar.MovementDirection=="backward" && !carMovement.PBPR ) {carMovement.PBPR = true; seeTransport["north"] = true; continue;}
//                 if (otherCar.SidePosition == "north" && otherCar.MovementDirection=="backward" && seeTransport["north"]) return false;
//             }
//         }
//         return true;
//     }

//     private bool CheckFreePath(RoadUserData roadUserData)
//     {
//         //  TODO: Логика проверки свободного пути 
//         //  Пример:
//         //  Проверка наличия других машин на той же полосе движения
//         //  и в том же направлении, что и текущая машина
//         return true;
//     }


//     public override bool IsViolation(RoadUserData roadUserData)
//     {
//         // Проверка на нарушение правил движения для машины
//         //  Например, проезд на красный свет, выезд на встречную полосу
//         return false;
//     }
// }
