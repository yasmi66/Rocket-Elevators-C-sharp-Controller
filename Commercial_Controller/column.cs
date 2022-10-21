using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Column
    {
       
        public string status;
        public int amountOfElevators, amountOfFloors, ID;
        public List<Elevator> elevatorsList;
        public List<CallButton> callButtonsList;
        public List<ServedFloors> servedFloors;

        private int elevatorID = 1;

        public Column(int _ID, int _amountOfFloors, int _amountOfElevators, List<int> _servedFloors, bool _isBasement)
        {
            this.ID = _ID;
            this.status = "online";
            this.amountOfFloors = _amountOfFloors;
            this.amountOfElevators = _amountOfElevators;
            this.elevatorsList = new List<Elevator>();
            this.callButtonsList = new List<CallButton>();
            this.servedFloors = new List<ServedFloors>();

            createElevators(_amountOfFloors, _amountOfElevators);
            createCallButtons(_amountOfFloors, _isBasement);
        }

        public void createCallButtons(int _amountOfFloors, bool _isBasement)
        {
            if (_isBasement)
            {  
                int buttonFloor = -1;
                for (int i = 0; i < _amountOfFloors; i++)
                {
                CallButton callButton = new CallButton(Global.callButtonID, "OFF", buttonFloor, "up");
                this.callButtonsList.Add(callButton);
                buttonFloor--;
                Global.callButtonID++;
                }
            }
            else 
            {
                int buttonFloor = 1;
                for (int i = 0; i < _amountOfFloors; i++)
                {
                    CallButton callButton = new CallButton(Global.callButtonID, "OFF", buttonFloor, "down");
                    this.callButtonsList.Add(callButton);
                    buttonFloor++;
                    Global.callButtonID++;
                }
            }
        }

        public void createElevators(int _amountOfFloors, int _amountOfElevators)
        {
            for (int i = 0; i < _amountOfElevators; i++){
                Elevator elevator = new Elevator(Global.elevatorID);
                this.elevatorsList.Add(elevator);
                Global.elevatorID++;
            }
        }

        //Simulate when a user press a button on a floor to go back to the first floor
        public Elevator requestElevator(int userPosition, string direction)
        {
            Elevator elevator = this.findElevator(userPosition, direction);
            elevator.addNewRequest(userPosition);
            elevator.move();

            elevator.addNewRequest(1);
            elevator.move();
            return elevator;

        }

        public Elevator findElevator(int requestedFloor, string requestedDirection)
        {  
            BestElevatorInformations bestElevatorInformations = new BestElevatorInformations();
            bestElevatorInformations.bestElevator = null;
            bestElevatorInformations.bestScore = 6;
            bestElevatorInformations.referenceGap = 10000000;
            

            if (requestedFloor == 1) {
                foreach (Elevator elevator in this.elevatorsList)
                {
                    if(1 == elevator.currentFloor && elevator.status == "stopped"){
                        bestElevatorInformations = this.checkIfElevatorIsBetter(1, elevator, bestElevatorInformations, requestedFloor);
                    }
                    else if (1 == elevator.currentFloor && elevator.status == "idle"){
                        bestElevatorInformations = this.checkIfElevatorIsBetter(2, elevator, bestElevatorInformations, requestedFloor);
                    }
                    else if (1 > elevator.currentFloor && elevator.direction == "up"){
                        bestElevatorInformations = this.checkIfElevatorIsBetter(3, elevator, bestElevatorInformations, requestedFloor);
                    }
                    else if (1 < elevator.currentFloor && elevator.direction == "down"){
                        bestElevatorInformations = this.checkIfElevatorIsBetter(4, elevator, bestElevatorInformations, requestedFloor);
                    }
                    else {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(5, elevator, bestElevatorInformations, requestedFloor);
                    }

                }
            }
            else 
            {
                foreach (Elevator elevator in this.elevatorsList)
                {
                    if (requestedFloor == elevator.currentFloor && elevator.status == "stopped" && requestedDirection == elevator.direction){
                        bestElevatorInformations = this.checkIfElevatorIsBetter(1, elevator, bestElevatorInformations, requestedFloor);
                    }
                    else if (requestedFloor > elevator.currentFloor && elevator.direction == "up" && requestedDirection == "up"){
                        bestElevatorInformations = this.checkIfElevatorIsBetter(2, elevator, bestElevatorInformations, requestedFloor);
                    }
                    else if (requestedFloor < elevator.currentFloor && elevator.direction == "down" && requestedDirection == "down"){
                        bestElevatorInformations = this.checkIfElevatorIsBetter(3, elevator, bestElevatorInformations, requestedFloor);
                    }
                    else if (elevator.status == "idle"){
                        bestElevatorInformations = this.checkIfElevatorIsBetter(4, elevator, bestElevatorInformations, requestedFloor);
                    }
                    else{
                        bestElevatorInformations = this.checkIfElevatorIsBetter(5, elevator, bestElevatorInformations, requestedFloor);
                    }

                }
            }

            return bestElevatorInformations.bestElevator;
        }

        public BestElevatorInformations checkIfElevatorIsBetter(int scoreToCheck, Elevator newElevator, BestElevatorInformations bestElevatorInformations, int floor)
        {
            if (scoreToCheck < bestElevatorInformations.bestScore){
                bestElevatorInformations.bestScore = scoreToCheck;
                bestElevatorInformations.bestElevator = newElevator;
                bestElevatorInformations.referenceGap = Math.Abs(newElevator.currentFloor - floor);
            }
            else if (bestElevatorInformations.bestScore == scoreToCheck)
            {
               int gap = Math.Abs(newElevator.currentFloor - floor);
            if (bestElevatorInformations.referenceGap > gap)
            {
                bestElevatorInformations.bestElevator = newElevator;
                bestElevatorInformations.referenceGap = gap;
                }
            }
            return
            bestElevatorInformations;
            
        }
    }
}