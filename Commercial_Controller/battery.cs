using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Battery
    {
        public string status;
        public int ID, amountOfFloors, columnID, floorRequestButtonID;
        public List<Column> columnsList;
        public List<FloorRequestButton> floorRequestButtonsList;
        
        private int colummnID = 1;

        public Battery(int _ID, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            this.status = "online";
            this.ID = _ID;
            this.columnsList = new List<Column>();
            this.floorRequestButtonsList = new List<FloorRequestButton>();


            if (_amountOfBasements > 0){
                this.createBasementFloorRequestButtons(_amountOfBasements);
                this.createBasementColumn(_amountOfBasements, _amountOfElevatorPerColumn);
                _amountOfColumns--;
            }
            this.createFloorRequestButtons(_amountOfFloors);
            this.createColumns(_amountOfColumns, _amountOfFloors, _amountOfElevatorPerColumn);
        }

        public void createBasementColumn(int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            List <int> servedFloors = new List <int>();
            int floor = -1;

            for (int i = 0; i < _amountOfBasements; i++){
                servedFloors.Add(floor);
                floor--;
            };
            Column column = new Column(Global.colummnID, _amountOfBasements, _amountOfElevatorPerColumn, servedFloors, true);
            this.columnsList.Add(column);
            Global.colummnID++;
        }

        public void createColumns(int _amountOfColumns, int _amountOfFloors, int _amountOfElevatorPerColumn)
        {
            int amountOfFloorsPerColumn = (int)Math.Round((double)_amountOfFloors /_amountOfColumns);
            int floor = 1;
            List <int> servedFloors = new List <int>();

            for (int i = 0 ; i <_amountOfColumns; i++){
                    for (i = 0; i < amountOfFloorsPerColumn; i++){
                        if (floor <= _amountOfFloors){
                        servedFloors.Add(floor);
                        floor++;
                        }
                    }
            Column column = new Column(Global.colummnID, _amountOfFloors, _amountOfElevatorPerColumn, servedFloors, false);
            this.columnsList.Add(column);
            Global.colummnID++;
            }
        }  

        public void createFloorRequestButtons(int _amountOfFloors)
        {
            int buttonFloor = 1;

            for (int i = 0; i < _amountOfFloors; i++){
                FloorRequestButton floorRequestButton = new FloorRequestButton(Global.floorRequestButtonID, "OFF", buttonFloor, "up");
                this.floorRequestButtonsList.Add(floorRequestButton);
                buttonFloor++;
                Global.floorRequestButtonID++;
            }
        }

        public void createBasementFloorRequestButtons(int _amountOfBasements)
        {
            int buttonFloor = -1;

            for (int i = 0; i < _amountOfBasements; i++){
                FloorRequestButton floorRequestButton = new FloorRequestButton(Global.floorRequestButtonID, "OFF", buttonFloor, "down");
                this.floorRequestButtonsList.Add(floorRequestButton);
                buttonFloor--;
                Global.floorRequestButtonID++;
            }
        }

        public Column findBestColumn(int _requestedFloor)
        {
            foreach (Column column in this.columnsList){
            
                if(column.servedFloorsList.Contains(_requestedFloor)){
                    return column;
                }
            }
            return null;
        }

        //Simulate when a user press a button at the lobby
        public (Column, Elevator) assignElevator(int _requestedFloor, string _direction)
        {
            Column column = this.findBestColumn(_requestedFloor);
            Elevator elevator = column.findElevator(1, _direction);
            elevator.addNewRequest(1);
            elevator.move();

            elevator.addNewRequest(_requestedFloor);
            elevator.move();
        }
    }
}

