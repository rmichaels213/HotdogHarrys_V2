/*----------------------------------------
Filename:		ControllerMenu.cs	
Created By:		Ryan Michaels
Created Date:	04/20/2018
Updated Date:	04/20/2018
----------------------------------------*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerMenu : MonoBehaviour
{
	public static ControllerMenu menu;

	public Canvas canvasPause;
	public Canvas canvasMainMenu;
	public Canvas canvasMenu;
	public Canvas canvasGameOver;

	public void Awake()
	{
		if ( menu == null )
		{
			menu = this;
			DontDestroyOnLoad( menu );
		}
		else if ( this != menu )
		{
			Destroy( this );
		}

		//canvasPause.gameObject.SetActive( false );
		//canvasGameOver.gameObject.SetActive( false );
		//canvasMenu.gameObject.SetActive( true );
	}

	/// <summary>
	/// Quit game
	/// </summary>
	public void Navigation_Quit()
	{
		Application.Quit();
	}

	/// <summary>
	/// Navigate to Start scene
	/// </summary>
	public void Navigation_Start()
	{
		ControllerGameManager.controller.isGameOver = false;
		ControllerGameManager.controller.isWaitingOnGameOverScreen = false;
		ControllerGameManager.controller.currentTime = 0;

		//ControllerRoad.road.Reset();

		//canvasPause.gameObject.SetActive( false );
		//canvasGameOver.gameObject.SetActive( false );

		SceneManager.LoadScene( "MainGame" );

		//canvasMenu.gameObject.SetActive( true );
	}

	/// <summary>
	/// Navigate to Title scene
	/// </summary>
	public void Navigation_Title()
	{
		//canvasPause.gameObject.SetActive( false );
		//canvasGameOver.gameObject.SetActive( false );
		//canvasMenu.gameObject.SetActive( false );

		SceneManager.LoadScene( "Title" );

	}

	/// <summary>
	/// Toggle the pause menu.
	/// </summary>
	public void Pause()
	{
		//canvasGameOver.gameObject.SetActive( false );
		//canvasMenu.gameObject.SetActive( true );

		if ( ControllerGameManager.controller.isGameOver )
		{
			return;
		}

		if ( ControllerGameManager.controller.isPaused )
		{
			Resume();
			return;
		}
		else
		{
			//canvasPause.gameObject.SetActive( true );
			ControllerGameManager.controller.isPaused = true;
			Time.timeScale = 0;
		}
	}

	/// <summary>
	/// Resume the game and hide the menu.
	/// </summary>
	public void Resume()
	{
		//canvasGameOver.gameObject.SetActive( false );
		//canvasMenu.gameObject.SetActive( true );

		//canvasPause.gameObject.SetActive( false );
		ControllerGameManager.controller.isPaused = false;
		Time.timeScale = 1;
	}

	/// <summary>
	/// Runs every frame. Check for typical input.
	/// </summary>
	public void Update()
	{
		if ( Input.GetKeyDown( KeyCode.Escape ) )
		{
			Application.Quit();
		}
	}
}
