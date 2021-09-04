
namespace Aruco
{
    class Spot
    {
        int id;
        bool occupied = false;
        int counterNewState = 0;
        public Spot(int id)
        {
            this.id = id;
        }

        public bool IsOccupied()
        {
            return occupied;
        }

        public int getId()
        {
            return id;
        }

        public void Occupied(bool recivedState)
        {
            if(recivedState !=  occupied)
            {
                counterNewState++;
                if (counterNewState > 5)
                {
                    occupied = recivedState;
                    counterNewState = 0;
                }
            }
            else
            {
                counterNewState = 0;
            }
            
        }
        
    }
}
