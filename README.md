# Rocket-Elevators-C#-Controller

## Requirements & Scenarios

The program to develop is an elevator controller that can be set up in a building with any number of batteries, columns, elevators or floors.
In summary, the controller must be capable of supporting two main events:

1. A person presses a call button on a floor to request an elevator. The controller selects an available cage and it is routed to that person based on two parameters provided by pressing the button:
- The floor where the person is
- The direction in which he will go (up or down)

2. A person at the Lobby requests a floor and is sent to the correct column. The parameters provided are :
- The floor where the user want to go
- The direction in which he will go (up or down)

## Architecture

Classes are created in seprate cs files. Each file contains the class constructors (instance variables, global variables if there are any and functions if there are any)

1. Controller is going to go through each column (battery class) and pass the requested floor in the chosen columns list 

2. Next step is to find the best elevator to send to the user in the selected column (column class):

 - the Lobby is the reference floor in the findElervator function
 
 - a score (number) is assigned to each elevator according to the elevator's position, the floor on which the user requested the elevator from and the gap between the elevators position and the user position
  
 - the checkIfElevator is better is going to compare the informations to determine which elevator should be sent to the user
  
 - the move function comes in when the elevator gets sent to the user and once the user choses which floor they want to get to (elevator class)

## Installation & Testing

As long as you have .NET 6.0 installed on your computer, nothing more needs to be installed:

The code to run the scenarios is included in the Commercial_Controller folder, and can be executed there with:

` dotnet run <SCENARIO-NUMBER> `

Running the tests
To launch the tests, make sure to be at the root of the repository and run:

` dotnet test `

You can also get more details about each test by adding the -v n flag:

` dotnet test -v n `

## dotnet test run result

![Screenshot from 2022-10-21][https://github.com/yasmi66/Rocket-Elevators-Csharp-Controller/blob/main/READMEPIC-cs-testruns.png]


