/*----------------------------------------
Filename:		ControllerRoadBlock.cs	
Created By:		Ryan Michaels
Created Date:	04/22/2018
Updated Date:	04/22/2018
----------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRoadBlock : MonoBehaviour
{
	/// <summary>
	/// When start colliding
	/// </summary>
	/// <param name="collider"></param>
	public void OnTriggerEnter2D( Collider2D collider )
	{
		if ( collider.gameObject.tag.Contains( "Player" ) )
		{
			Debug.Log( "Stopping a player" );
			collider.transform.position = new Vector2( collider.transform.position.x, collider.transform.position.y + .5f );
		}
	}
}
