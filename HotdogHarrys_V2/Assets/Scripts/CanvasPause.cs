/*----------------------------------------
Filename:		CanvasPause.cs	
Created By:		Ryan Michaels
Created Date:	04/23/2018
Updated Date:	04/23/2018
----------------------------------------*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasPause : MonoBehaviour
{
	public static CanvasPause canvas;

	/// <summary>
	/// Runs on controller creation.
	/// </summary>
	private void Awake()
	{
		// If the object has not been set yet (this object) 
		if ( canvas == null )
		{
			// Don't EVER destroy this one, and set this object so we have a reference to it later.
			DontDestroyOnLoad( gameObject );
			canvas = this;

			// Reassign the button onClick methods
			Button[] buttons = this.gameObject.GetComponentsInChildren<Button>();
			foreach ( Button b in buttons )
			{
				if ( b.name == "ButtonResume" )
				{
					b.onClick.AddListener( LocalResume );
				}
				if ( b.name == "ButtonMainMenu" )
				{
					b.onClick.AddListener( LocalNavigation_Title );
				}
				if ( b.name == "ButtonQuit" )
				{
					b.onClick.AddListener( LocalNavigation_Quit );
				}
			}
		}
		else if ( canvas != this )
		{
			// If there is an object already stored, and it's not this one, destroy this!
			Destroy( gameObject );
		}
	}

	/// <summary>
	/// Resume the game and hide the menu.
	/// </summary>
	public void LocalResume()
	{
		gameObject.SetActive( false );
		ControllerGameManager.controller.isPaused = false;
		Time.timeScale = 1;
	}

	/// <summary>
	/// Navigate to Title scene
	/// </summary>
	public void LocalNavigation_Title()
	{
		gameObject.SetActive( false );
		CanvasMainMenu.canvas.gameObject.SetActive( true );
		ControllerGameManager.controller.isPaused = false;
		Time.timeScale = 1;

		ControllerGameManager.controller.isGameOver = false;
		ControllerGameManager.controller.isWaitingOnGameOverScreen = false;
		ControllerGameManager.controller.currentTime = 0;
		SceneManager.LoadScene( "Title" );
	}

	/// <summary>
	/// Quit game
	/// </summary>
	public void LocalNavigation_Quit()
	{
		Application.Quit();
	}

	public void Update()
	{
		if ( SceneManager.GetActiveScene().name == "Title" )
		{
			gameObject.SetActive( false );
		}
		else
		{
			gameObject.SetActive( true );
		}
	}
}
