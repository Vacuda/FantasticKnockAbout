using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static bt_BRICKTYPE;
using static ot_OBJECTTYPE;


public class Brick : MonoBehaviour, ICanCollideWith
{
    public bt_BRICKTYPE brick_type;
    public ot_OBJECTTYPE object_type { get; set; } = ot_BRICK;
    private SpriteRenderer rend;

    private Color32 red;
    private Color32 orange;
    private Color32 green;
    private Color32 yellow ;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();

    }

    void Start()
    {
        red = new Color32(165, 30, 10, 255);
        orange = new Color32(198, 136, 10, 255);
        green = new Color32(10, 132, 50, 255);
        yellow = new Color32(199, 199, 43, 255);


    Change_Color();
    }

    void Change_Color()
    {
        switch (brick_type){

            case bt_RED:
                rend.color = red;
                break;
            case bt_ORANGE:
                rend.color = orange;
                break;
            case bt_GREEN:
                rend.color = green;
                break;
            case bt_YELLOW:
                rend.color = yellow;
                break;
            default:
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
