
using UnityEngine;
using static ot_OBJECTTYPE;

public class DoorWay_Volume : MonoBehaviour
{
    public GameLevel game_level;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICanCollideWith lava = collision.gameObject.GetComponent<ICanCollideWith>();

        if (lava != null)
        {
            if (lava.object_type == ot_BALL)
            {
                //trigger
                game_level.Trigger_SuccessfulExit();

                //shutdown
                Destroy(this);
            }
        }
    }
}
