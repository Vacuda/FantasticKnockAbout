
using UnityEngine;
public static class LevelHouse
{
    public static LevelInfo BuildALevel(int rank)
    {
        LevelInfo info = new LevelInfo();

        Place_Flooring(info);
        Place_Spikes(info, rank);
        Place_Door(info, rank);
        Place_Items(info, rank);

        if(rank == 0)
        {
            info.NeedsAGate = true;
            Remove_TopWallInfo(info);
        }
        else
        {
            info.NeedsAGate = false;
        }

        return info;
    }

    public static void Place_Spikes(LevelInfo info, int rank)
    {
        //early exit
        if(rank == 0) { return; }

        //early edge case correct
        if(rank >= 28) { rank = 28; }
        if(rank < 0) { rank = 0; }

        //initialize spike_spots
        int spike_spots = 1;

        //get spike spots by rank
        if(rank <= 5)
        {
            spike_spots = 1;
        }
        else if(rank <= 10)
        {
            spike_spots = 2;
        }
        else if(rank <= 15)
        {
            spike_spots = 3;
        }
        else if(rank <= 20)
        {
            spike_spots = 4;
        }
        else if(rank <= 25)
        {
            spike_spots = 5;
        }
        else if (rank <= 27)
        {
            spike_spots = 6;
        }
        else
        {
            //full spikes, 1 spike_spot will suffice   
        }

        //initialize
        int runner;
        int spikes_to_place;
        int num_spikes_in_set = ((rank * 4) / spike_spots);

        //loop spike_spots
        while(spike_spots > 0)
        {
            //reset spikes_to_place
            spikes_to_place = num_spikes_in_set;

            //get starting spot
            runner = Random.Range(0, info.spike_array.Length);

            //place spikes to right
            while (spikes_to_place > 0)
            {
                //if blank
                if (info.spike_array[runner] == 0)
                {
                    //set
                    info.spike_array[runner] = 1;

                    //decrement
                    spikes_to_place--;
                }

                //increment
                runner++;

                //if at end of array
                if (runner == info.spike_array.Length)
                {
                    //reset
                    runner = 0;
                }
            }

            //decrement
            spike_spots--;
        }
    }

    public static void Place_Flooring(LevelInfo info)
    {
        for (int i = 0; i < info.floor_array.Length; i++)
        {
            info.floor_array[i] = Random.Range(0, 9); //0-8
        }
    }

    public static void Place_Door(LevelInfo info, int rank)
    {
        int rand;

        //first time, door can't be top
        if(rank == 0)
        {
            //initialize rand
            rand = Random.Range(0, 79); //0-78
        }
        else
        {
            //initialize rand
            rand = Random.Range(0, 110); //0-109
        }

        switch (rand)
        {


            case 24:
                rand = 5;
                break;
            case 25:
                rand = 13;
                break;


            case 54:
                rand = 34;
                break;
            case 55:
                rand = 48;
                break;


            case 80:
                rand = 61;
                break;
            case 81:
                rand = 69;
                break;


            default:
                break;
        }

        //@@@@ 23
        //@@@@ 53
        //@@@@ 79
        //@@@@ 109
        //special cases, just need to be trimmed less than the others
        if (rand == 23 || rand == 53 || rand == 79 || rand == 109)
        {
            //set as doors
            info.wall_array[rand] = 1;
            info.wall_array[rand + 1] = -1;
            info.wall_array[rand + 2] = -1;
            //info.wall_array[rand + 3] = -1; // this can stay out
        }
        else
        {
            //set as doors
            info.wall_array[rand] = 1;
            info.wall_array[rand + 1] = -1;
            info.wall_array[rand + 2] = -1;
            info.wall_array[rand + 3] = -1;
        }


        //remove spikes around door
        info.Remove_Spikes_AroundDoors(rand);
    }

    public static void Place_Items(LevelInfo info, int rank) 
    {
        int num_of_items;

        if(rank <= 5)
        {
            num_of_items = Random.Range(1, 5);
        }
        else if(rank <= 10)
        {
            num_of_items = Random.Range(2, 6);
        }
        else if (rank <= 15)
        {
            num_of_items = Random.Range(3, 7);
        }
        else if (rank <= 20)
        {
            num_of_items = Random.Range(4, 8);
        }
        else if (rank <= 25)
        {
            num_of_items = Random.Range(5, 9);
        }
        else
        {
            num_of_items = Random.Range(6, 10);
        }

        while (num_of_items > 0)
        {
            //get index
            int rand_index = Random.Range(0, info.item_array.Length);

            //if blank
            if (info.item_array[rand_index] == 0)
            {
                info.item_array[rand_index] = Random.Range(1, 5);
                num_of_items--;
            }

        }
    }


    /* UTILITIES*/

    public static void Remove_TopWallInfo(LevelInfo info)
    {
        for(int i = 82; i<info.wall_array.Length; i++)
        {
            info.wall_array[i] = -1;
        }
    }

}
