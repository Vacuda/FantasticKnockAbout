
using UnityEngine;

public enum bt_BRICKTYPE
{
    bt_RED, bt_ORANGE, bt_GREEN, bt_YELLOW
}

public enum ot_OBJECTTYPE
{
    ot_WALL, ot_SPIKE, ot_BALL, ot_PADDLE, ot_BRICK, ot_RETROPADDLE, ot_AXE
}

interface ICanCollideWith
{
    ot_OBJECTTYPE object_type { get; set; }
}

public enum w_WAVETYPE
{
    w_SIN, w_COS, w_TAN, w_PINGPONG
}









public class CustomEnums : MonoBehaviour
{

}
