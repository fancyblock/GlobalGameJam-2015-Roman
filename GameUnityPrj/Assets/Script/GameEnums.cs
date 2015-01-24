using UnityEngine;
using System.Collections;

public class GameEnums  
{
    public const int GAME_STATUS_READY = 0;
    public const int GAME_STATUS_RUNNING = 1;
    public const int GAME_STATUS_PAUSE = 3;

    public const int EVT_TYPE_REBELLION = 1;
    public const int EVT_TYPE_BLACKMAIL = 2;
    public const int EVT_TYPE_TRIBUTE = 3;
    public const int EVT_TYPE_GOLD_ORE = 4;
    public const int EVT_TYPE_INVADE = 5;

    public const float REBEL_FACTOR = 0.12f;
    public const float GOLD_FACTOR = 0.00001f;
    public const float INVADE_FACTOR = 0.000012f;
}
