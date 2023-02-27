
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelBuilder : MonoBehaviour
{


    public Tilemap _tilemap;
    public GameObject gate;
    private GameLevel game_level;
    public Sprite[] sprite_array;

    [Header("Prefabs")]
    public GameObject PF_Wall;
    public GameObject PF_Spike;
    public GameObject PF_DoorWay;
    public GameObject PF_FloorTile;
    public GameObject PF_Axe;
    public GameObject PF_GolfClub;
    public GameObject PF_Pizza;
    public GameObject PF_NailPolish;

    /* TEMP MEMBERS */
    Vector3Int grid_pos;
    Vector3 pos;
    Quaternion rot;

    private void Awake()
    {
        grid_pos = new Vector3Int(0, 0, 0);
        pos = new Vector3(0.0f, 0.0f, 0.0f);
        rot = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

    }

    void Start()
    {
        game_level = gameObject.GetComponent<GameLevel>();

    }

    public void BuiltOut_ThisLevel(LevelInfo info)
    {
        Build_Floorings(info);

        Build_CornerStones();
        Build_Gate(info);
        Build_Spikes(info);
        Build_Walls(info);
        Build_Items(info);






    }

    /* BUILDS */

    void Build_Gate(LevelInfo info)
    {
        if (info.NeedsAGate)
        {
            //full rows
            for (int x = -18; x <= 11; x++)
            {
                //top row, attach to gate
                MakeBlock(x, 14, true);

                //top row, attach to gate
                MakeBlock(x, 13, true);
            }
        }
    }

    void Build_Floorings(LevelInfo info)
    {
        int runner = 0;

        //loop cols
        for(int y = 14; y>=-14; y -= 2)
        {
            //loop rows
            for (int x = -20; x <= 12; x+=2)
            {
                //make tile
                MakeFloorTile(x, y, info.floor_array[runner]);

                //increment runner
                runner++;
            }
        }
    }

    void Build_CornerStones()
    {

        MakeBlock(-20, 14);
        MakeBlock(-20, 13);
        MakeBlock(-19, 14);
        MakeBlock(-19, 13);

        MakeBlock(12, 14);
        MakeBlock(12, 13);
        MakeBlock(13, 14);
        MakeBlock(13, 13);

        MakeBlock(-20, -14);
        MakeBlock(-20, -15);
        MakeBlock(-19, -14);
        MakeBlock(-19, -15);

        MakeBlock(12, -14);
        MakeBlock(12,-15);
        MakeBlock(13, -14);
        MakeBlock(13, -15);


    }

    void Build_Walls(LevelInfo info)
    {
        int runner = 0;
        int object_code = 0;

        //left side
        for (int y = 12; y >= -13; y--)
        {
            //get object_code
            object_code = info.wall_array[runner];

            //if wall = 0
            if (object_code == 0)
            {
                //left col
                MakeBlock(-20, y);

                //right col
                MakeBlock(-19, y);
            }

            //if doorway = 1
            if (object_code == 1)
            {
                MakeDoorWay(-19, y, 270);
            }

            //if skipping = -1
            if (object_code == -1)
            {
                //do nothing
            }

            //increment runner
            runner++;
        }

        //bottom
        for (int x = -18; x <= 11; x++)
        {
            //get object_code
            object_code = info.wall_array[runner];

            //if wall = 0
            if (object_code == 0)
            {
                //top row
                MakeBlock(x, -14);

                //bottom row
                MakeBlock(x, -15);
            }

            //if doorway = 1
            if (object_code == 1)
            {
                MakeDoorWay(x, -14, 0);
            }

            //if skipping = -1
            if (object_code == -1)
            {
                //do nothing
            }

            //increment runner
            runner++;
        }

        //right side
        for (int y = -13; y <= 12; y++)
        {
            //get object_code
            object_code = info.wall_array[runner];

            //if wall = 0
            if (object_code == 0)
            {
                //left col
                MakeBlock(12, y);

                //right col
                MakeBlock(13, y);
            }

            //if doorway = 1
            if (object_code == 1)
            {
                MakeDoorWay(12, y, 90);
            }

            //if skipping = -1
            if (object_code == -1)
            {
                //do nothing
            }

            //increment runner
            runner++;
        }

        //top
        for (int x = 11; x >= -18; x--)
        {
            //get object_code
            object_code = info.wall_array[runner];

            //if wall = 0
            if (object_code == 0)
            {
                //top row
                MakeBlock(x, 13);

                //bottom row
                MakeBlock(x, 14);
            }

            //if doorway = 1
            if (object_code == 1)
            {
                MakeDoorWay(x, 13, 180);
            }

            //if skipping = -1
            if (object_code == -1)
            {
                //do nothing
            }

            //increment runner
            runner++;
        }
    }

    void Build_Spikes(LevelInfo info)
    {
        int runner = 0;

        //loop left side
        for (int y = 12; y >= -13; y--)
        {
            //if spike
            if (info.spike_array[runner] == 1)
            {
                MakeSpike(-18, y, 270);
            }

            //increment runner
            runner++;
        }

        //loop bot row
        for (int x = -18; x <= 11; x++)
        {
            //if spike
            if (info.spike_array[runner] == 1)
            {
                MakeSpike(x, -13, 0);
            }

            //increment runner
            runner++;
        }

        //loop right side
        for (int y = -13; y <= 12; y++)
        {
            //if spike
            if (info.spike_array[runner] == 1)
            {
                MakeSpike(11, y, 90);
            }

            //increment runner
            runner++;
        }

        //loop top row
        for(int x = 11; x >= -18; x--)
        {
            //if spike
            if (info.spike_array[runner] == 1)
            {
                MakeSpike(x, 12, 180);
            }

            //increment runner
            runner++;
        }
    }

    void Build_Items(LevelInfo info)
    {
        int runner = 0;

        //loop cols
        for (int y = 8; y >= -8; y -= 2)
        {
            //loop rows
            for (int x = -14; x <= 6; x += 2)
            {
                //make tile
                MakeItem(x, y, info.item_array[runner]);

                //increment runner
                runner++;
            }
        }
    }

    /* MAKES */

    void MakeItem(int x, int y, int style)
    {
        GameObject item = null;

        switch (style)
        {
            case 1:
                item = Instantiate(PF_Axe);
                break;
            case 2:
                item = Instantiate(PF_GolfClub);
                break;
            case 3:
                item = Instantiate(PF_Pizza);
                break;
            case 4:
                item = Instantiate(PF_NailPolish);
                break;
            default:
                break;
        }

        //change grid values
        grid_pos.x = x;
        grid_pos.y = y;

        //get world pos
        pos = _tilemap.GetCellCenterWorld(grid_pos);

        //safety
        if (item != null)
        {
            //set position
            item.transform.position = pos;
        }
    }
    void MakeFloorTile(int x, int y, int style)
    {
        //instantiate block
        GameObject floor_tile = Instantiate(PF_FloorTile);

        //change grid values
        grid_pos.x = x;
        grid_pos.y = y;

        //get world pos
        pos = _tilemap.GetCellCenterWorld(grid_pos);

        //set position
        floor_tile.transform.position = pos;

        //change tile sprite
        floor_tile.transform.Find("FloorTile").GetComponent<SpriteRenderer>().sprite = sprite_array[style];
    }

    void MakeDoorWay(int x, int y, int rotation)
    {
        //instantiate block
        GameObject doorway = Instantiate(PF_DoorWay);

        //change grid values
        grid_pos.x = x;
        grid_pos.y = y;

        //get world pos
        pos = _tilemap.GetCellCenterWorld(grid_pos);

        //set position
        doorway.transform.position = pos;

        //set rotation
        doorway.transform.Rotate(0, 0, rotation);

        //set ref
        doorway.transform.Find("DoorWay_Volume").GetComponent<DoorWay_Volume>().game_level = game_level;
    }

    void MakeSpike(int x, int y, int rotation, bool IsAttachedToGate = false)
    {
        //instantiate block
        GameObject spike = Instantiate(PF_Spike);

        //change grid values
        grid_pos.x = x;
        grid_pos.y = y;

        //get world pos
        pos = _tilemap.GetCellCenterWorld(grid_pos);

        //set position
        spike.transform.position = pos;

        //set rotation
        spike.transform.Rotate(0, 0, rotation);

        //if need to attach to gate
        if (y==12 && rotation == 180)
        {
            spike.transform.parent = gate.transform;
        }

        //if need to attach to gate
        if(IsAttachedToGate)
        {
            spike.transform.parent = gate.transform;
        }
    }

    void MakeBlock(int x, int y, bool attachToGate = false)
    {
        //instantiate block
        GameObject block = Instantiate(PF_Wall);

        //change grid values
        grid_pos.x = x;
        grid_pos.y = y;

        //get world pos
        pos = _tilemap.GetCellCenterWorld(grid_pos);

        //set position
        block.transform.position = pos;

        //if need to attach to gate
        if (attachToGate)
        {
            block.transform.parent = gate.transform;
        }
    }
}
