# Introduction #
This projects focuses on developing a simulation environment for multicopters (multirotors) allowing to experiment with different UAV-types. The simulation environment was developed using C# and Microsoft Robotics Developer Studio 4. It was developed in the course of a bachelor thesis which you can find [here](https://dl.dropboxusercontent.com/u/15145382/bachelorarbeit_martinkuehn.pdf) (written in German language).

# What do I need to compile the simulator? #
  * [.NET framework](http://www.microsoft.com/en-us/download/details.aspx?id=30653)
  * The software was developed in Visual-C# so I recommend using Visual Studio (the [express version](http://www.microsoft.com/visualstudio/eng/downloads#d-2013-express) is available for free)
  * [Microsoft Robotics Developer Studio 4](http://www.microsoft.com/robotics/)
  * You will also need the [3D models](https://dl.dropboxusercontent.com/u/15145382/multicopter_3dmodels.rar) of the simulated multicopter types

# Procedure #
  1. Install Visual Studio. The .NET framework should be included in the setup.
  1. Install Microsoft Robotics Developer Studio 4. The setup wants to install the software in your user folder, I recommend installing it in _C:\Program Files\_. You should keep in mind in which folder you installed MRDS to, because you have to move some files into it. Furthermore you later have to set the path in the project settings.
  1. Move the content of the the 3D models archive _multicopter\_3dmodels.rar_ into the folder _...\Microsoft Robotics Dev Studio 4\store\media\_.
  1. Open the simulator solution (_MulticopterSimulator.sln_) in Visual Studio.
  1. Right-click on the _MulticopterSimulation_ project (the **project**, not the **solution**!) and choose the settings. You now need to set manually the paths to the _Microsoft Robotics Dev Studio 4_ directory and also the contained _bin_ directory.
  1. The project should now compile.