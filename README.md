# Rocket-Elevators-Csharp-Controller

Classes are created in seprate cs files. Each file contains the class constructors (instance variables, global variables if there are any and functions if there are any)

`-Controller is going to go through each column (battery class) and pass the requested floor in the chosen columns list`

`-Next step is to find the best elevator to send to the user in the selected column (column class):`

 `-the Lobby is the reference floor in the findElervator function` 
 
  `-a score (number) is assigned to each elevator according to the elevator's position, the floor on which the user requested the elevator from and the gap between the elevators position and the user position`
  
  `-the checkIfElevator is better is going to compare the informations to determine which elevator should be sent to the user`
  
  `-the move function comes in when the elevator gets sent to the user and once the user choses which floor they want to get to (elevator class)`
  

### dotnet test run result

![Screenshot from 2022-10-21][https://github.com/yasmi66/Rocket-Elevators-Csharp-Controller/blob/main/READMEPIC-cs-testruns.png]


