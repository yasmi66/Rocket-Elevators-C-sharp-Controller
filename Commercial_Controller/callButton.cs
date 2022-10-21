namespace Commercial_Controller
{
    //Button on a floor or basement to go back to lobby
    public class CallButton
    {
        public string status, direction;
        public int ID, floor;
        public CallButton(int _ID, string _status, int _floor, string _direction)
        {
            this.ID = _ID;
            this.status = _status;
            this.floor =_floor;
            this.direction = _direction;
        }
    }
}