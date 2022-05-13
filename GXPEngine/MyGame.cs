using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{
	LevelControl levelControl;
	//SceneManager sceneManager;

	public MyGame() : base(1920, 1080, false)		// Create a window that's 800x600 and NOT fullscreen
	{


		//sceneManager = new SceneManager();
		//AddChild(sceneManager);

		//sceneManager.LoadLevel("MainMenu");

		levelControl = new LevelControl(1920, 1080);
		levelControl.SetXY(0, 0);
		
		AddChild(levelControl);

/*		Sprite background = new Sprite("Backgrounds/BackgroundwoutShip.png");
		game.AddChild(background); */
	}

	// For every game object, Update is called every frame, by the engine:
	void Update()
	{
		
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		Game game = new MyGame();
		game.Start();
		//game.RenderMain = false;
		// Create a "MyGame" and start it
	}
}