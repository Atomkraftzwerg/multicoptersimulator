# Multicopter Simulator
Automatically exported from code.google.com/p/multicoptersimulator.

## Introduction
This projects focuses on developing a simulation environment for 
multicopters (multirotors) allowing to experiment with different UAV-types. 
There is also a wind simulation engine provided.
The simulation environment was developed using C# and Microsoft Robotics Developer Studio 4. 
It was developed in the course of a bachelor thesis which you can find 
[here](https://dl.dropboxusercontent.com/u/15145382/bachelorarbeit_martinkuehn.pdf) 
(written in German language).

## What do I need to compile the simulator?
* [.NET framework](http://www.microsoft.com/en-us/download/details.aspx?id=30653)
* The software was developed in Visual-C#, so I recommend using Visual Studio 
  (the [Express version](http://www.microsoft.com/visualstudio/eng/downloads#d-2013-express) 
  is available for free).
* [Microsoft Robotics Developer Studio 4](http://www.microsoft.com/robotics/)
* You will also need the [3D models](https://dl.dropboxusercontent.com/u/15145382/multicopter_3dmodels.rar) 
  of the simulated multicopter types.

## Procedure
1. Install Visual Studio. The .NET framework should be included in the setup.
2. Install Microsoft Robotics Developer Studio 4. The setup wants to install the software 
   in your user folder, I recommend installing it in _C:\Program Files\_. 
   You should keep in mind in which folder you installed MRDS to, because you have to move some 
   files into it. Furthermore you later have to set the path in the project settings.
3. Move the content of the the 3D models archive _multicopter_3dmodels.rar_ into the 
   folder _...\Microsoft Robotics Dev Studio 4\store\media\_.
4. Open the simulator solution (_MulticopterSimulator.sln_) in Visual Studio.
5. Right-click on the _MulticopterSimulation_ project (the *project*, not the *solution*!) 
   and choose the settings. You now need to set manually the paths to the 
   _Microsoft Robotics Dev Studio 4_ directory and also the contained _bin_ directory.
6. The project should now compile.
  
If you have any problems or questions, please don't hesitate to contact me.
  
## Screenshots

### Visual Simulation Environment
![Simulator settings window](https://dl.dropboxusercontent.com/u/15145382/Simulator%20Simulation%20settings.png)

The Visual Simulation Environment provides the graphic output of the simulation. 
In this window the camera views can be controlled, it is also possible to move the camera freely.

### Control window
![Control window](https://dl.dropboxusercontent.com/u/15145382/Simulator%20Control%20Window.png)

The control window allows to control the multicopter entity in the simulator. 
It is also possible to use a Xbox 360 gamepad. Automatic flight control with PID controllers 
can also be activated in this window.

### Simulator settings window
![Simulator settings window](https://dl.dropboxusercontent.com/u/15145382/Simulator%20Simulation%20settings.png)

In this window the options of the wind simulation and the multicopter type can be set.

### PID tuning window
![PID tuning window](https://dl.dropboxusercontent.com/u/15145382/Simulator%20PID-Tuning.png)

The program also provides simulated PID controllers on the multicopter. 
Autotuning wasn't implemented yet, so it is possible to manually tune the P, I and D values 
of the controllers in this window.

### Statistics window
![PID tuning window](https://dl.dropboxusercontent.com/u/15145382/Simulator%20Statistics.png)

This window provides the recording of statistics of a set of values. Also a graphic plot output 
is given for the values. Furthermore some debug data is shown in this window.
