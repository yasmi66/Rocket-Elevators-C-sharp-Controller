using System.Threading;
using System.Collections.Generic;
using System;

namespace Commercial_Controller
{
    public class Elevator
    {
        public string status, direction;
        public int currentFloor, amountOfFloors, ID;
        public List<int> floorRequestsList, completedRequestsList;
        public bool overweight, obstruction;
        public Door Door;
        


        public Elevator(int _elevatorID, string _status, int _amountOfFloors, int _currentFloor)
        {
            this.ID = _elevatorID;
            this.status = _status;
            this.amountOfFloors = _amountOfFloors;
            this.currentFloor = _currentFloor;
            this.Door = new Door(ID, "closed");
            this.floorRequestsList = new List<int>();
            this.completedRequestsList = new List<int>();
            this.direction = "none";
            this.overweight = false;
            this.obstruction = false;

            
        }
        public void move()
        {
            while(this.floorRequestsList.Count != 0)
            {
                int destination = floorRequestsList[0];
                this.status = "moving";
                if(this.direction == "up")
                {
                    while(this.currentFloor < destination)
                    {
                        this.currentFloor++;
                    }
                }
                else if(this.direction == "down")
                {
                    while(this.currentFloor > destination)
                    {
                        this.currentFloor--;
                    }
                }
                this.status = "stopped";
                // this.operateDoors();
                this.floorRequestsList.RemoveAt(0);
                this.completedRequestsList.Add(destination);
            }
            this.status = "idle";
        }  

        public void sortFloorList()
        {
            if (this.direction == "up"){

            }
        }

        // public void operateDoors()
        // {
        //     this.Door.status = "opened";
        //     if (this.overweight) {
        //         this.Door.status = "closing";
        //     }
        //     if (this.obstruction){
        //         this.Door.status = "closed";
        //     }else{
        //         this.operateDoors();
        //     }
            
        //         while (this.overweight){
        //             Console.WriteLine("OVERWEIGHT");
        //         }
        //     this.operateDoors();
        // }

        public void addNewRequest(int requestedFloor)
        {
            if (!this.floorRequestsList.Contains(requestedFloor)){
                this.floorRequestsList.Add(requestedFloor);
            }
            if (this.currentFloor < requestedFloor){
                this.direction = "up";
            }
            if (this.currentFloor > requestedFloor){
                this.direction = "down";
            }
        }
    
    }

}
