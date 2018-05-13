/*----------------------------------------
Filename:		CanvasMainMenu.cs	
Created By:		Ryan Michaels
Created Date:	04/23/2018
Updated Date:	04/23/2018
----------------------------------------*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasMainMenu : MonoBehaviour
{
	public static CanvasMainMenu canvas;

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
				if ( b.name == "ButtonStart" )
				{
					b.onClick.AddListener( LocalNavigation_Start );
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
	/// Navigate to Title scene
	/// </summary>
	public void LocalNavigation_Start()
	{
		CanvasMenu.canvas.gameObject.SetActive( true );
		gameObject.SetActive( false );
		SceneManager.LoadScene( "MainGame" );
	}

	/// <summary>
	/// Quit game
	/// </summary>
	public void LocalNavigation_Quit()
	{
		Application.Quit();
	}
}
