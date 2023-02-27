using UnityEngine;
using static ot_OBJECTTYPE;

public class EndRetro_Volume : MonoBehaviour
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
                game_level.Entered_EndRetro_Volume();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ICanCollideWith lava = collision.gameObject.GetComponent<ICanCollideWith>();

        if (lava != null)
        {
            if (lava.object_type == ot_BALL)
            {
                //trigger
                game_level.Close_Gate();

                //shutdown
                Destroy(this);
            }
        }
    }

    public void Destroy_ThisVolume()
    {
        Destroy(gameObject);
    }
}
