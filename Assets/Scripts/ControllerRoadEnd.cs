/*----------------------------------------
Filename:		ControllerRoadEnd.cs	
Created By:		Ryan Michaels
Created Date:	04/23/2018
Updated Date:	04/23/2018
----------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRoadEnd : MonoBehaviour
{
	/// <summary>
	/// When start colliding
	/// </summary>
	/// <param name="collider"></param>
	public void OnTriggerEnter2D( Collider2D collider )
	{
		if ( collider.gameObject.tag.Contains( "Player" ) )
		{
			Debug.Log( "Game over!" );
			ControllerGameManager.controller.isGameOver = true;
		}
	}
}
