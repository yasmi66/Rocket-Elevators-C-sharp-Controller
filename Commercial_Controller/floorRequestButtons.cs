namespace Commercial_Controller
{
    //Button on a floor or basement to go back to lobby
    public class FloorRequestButton
    {
        public string direction, status;
        public int floor, ID; 

        public FloorRequestButton(int _ID, string _status, int _floor, string _direction)
        {
            this.ID = _ID;
            this.status = _status;
            this.floor = _floor;
            this.direction = _direction;
        }
    }
}