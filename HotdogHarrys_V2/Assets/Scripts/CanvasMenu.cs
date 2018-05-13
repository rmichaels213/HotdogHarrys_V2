/*----------------------------------------
Filename:		CanvasMenu.cs	
Created By:		Ryan Michaels
Created Date:	04/23/2018
Updated Date:	04/23/2018
----------------------------------------*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CanvasMenu : MonoBehaviour
{
	public static CanvasMenu canvas;

	public Canvas canvasPause;

	public TextMeshProUGUI timerText;

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
				if ( b.name == "ButtonMenu" )
				{
					b.onClick.AddListener( LocalPause );
				}
			}

			TextMeshProUGUI[] text = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
			foreach ( TextMeshProUGUI t in text )
			{
				if ( t.name == "Timer" )
				{
					timerText = t;
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
	/// Toggle the pause menu.
	/// </summary>
	public void LocalPause()
	{
		Debug.Log( "We are pausing" );
		if ( ControllerGameManager.controller.isGameOver )
		{
			return;
		}

		if ( ControllerGameManager.controller.isPaused )
		{
			LocalResume();
			return;
		}
		else
		{
			canvasPause.gameObject.SetActive( true );
			ControllerGameManager.controller.isPaused = true;
			Time.timeScale = 0;
		}
	}

	/// <summary>
	/// Resume the game and hide the menu.
	/// </summary>
	public void LocalResume()
	{
		canvasPause.gameObject.SetActive( false );
		ControllerGameManager.controller.isPaused = false;
		Time.timeScale = 1;
	}

	public void Update()
	{
		float timeLeft = ControllerGameManager.controller.maxTimeAllowed - ControllerGameManager.controller.currentTime;
		timerText.text = timeLeft.ToString("F2");

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
