using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using GXPEngine.Scenes;

public class MyGame : Game
{
	LevelControl levelControl;

	public MyGame() : base(960, 960, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		levelControl = new LevelControl(960,960);
		levelControl.SetXY(levelControl.width / 2, levelControl.height / 2);
		
		AddChild(levelControl);
	}

	// For every game object, Update is called every frame, by the engine:
	void Update()
	{
		
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}