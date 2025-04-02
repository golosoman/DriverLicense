using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public static class GlobalState
{
    public static string userToken = "eyJhbGciOiJIUzI1NiJ9.eyJyb2xlIjoiUk9MRV9VU0VSIiwiaWQiOjIsInN1YiI6ImdvbG9zb21hbiIsImlhdCI6MTc0MzUyOTU1MywiZXhwIjoxNzQzNjczNTUzfQ.apRzOlqpAQfOCEIdVtgi9dDmWV3Qsgq8ysdbQ3Unas4";
    public static int questionId = 28;
    public static int ticketId = -1;

    public static void ClearData()
    {
        questionId = 28;
        ticketId = -1;
    }

    public static void ClearAllData()
    {
        ClearData();
        userToken = null;
    }
}
