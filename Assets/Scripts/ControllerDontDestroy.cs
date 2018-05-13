/*----------------------------------------
Filename:		ControllerDontDestroy.cs	
Created By:		Ryan Michaels
Created Date:	04/23/2018
Updated Date:	04/23/2018
----------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDontDestroy : MonoBehaviour
{
	public static ControllerDontDestroy controller;

	public void Awake()
	{
		if ( controller == null )
		{
			controller = this;
			DontDestroyOnLoad( controller );
		}
		else if ( this != controller )
		{
			Destroy( this );
		}
	}
}
