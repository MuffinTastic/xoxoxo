﻿using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

public class HeartParticle : Panel
{

	TimeSince lifeTime = 0f;
	float deathTime;
	float transitions;
	float particleSize;
	float rotation;
	float particleSpeed;
	Vector2 velocity;

	public HeartParticle( float duration = 1f, float size = 1f, float speed = 1f, Vector2? direction = null )
	{

		deathTime = duration;
		transitions = duration / 2;
		particleSpeed = speed;
		particleSize = Rand.Float( 20, 50 ) * size;

		Style.Height = 0;
		Style.BackgroundAngle = Length.Percent( rotation );
		Style.ZIndex = (int)( Time.Now * 100 );

		velocity = direction.Value * Rand.Float( 5f, 10f );

	}

	public override void Tick()
	{
		
		float velocityStrength = 10f * particleSpeed;
		float gravityStrength = 1f;
		velocity = new Vector2( velocity.x, velocity.y + (float)Math.Pow( lifeTime * gravityStrength, 2f ) );
		Style.Left = Length.Pixels( Style.Left.Value.GetPixels( Screen.Width ) + velocity.x * Time.Delta * velocityStrength );
		Style.Top = Length.Pixels( Style.Top.Value.GetPixels( Screen.Height ) + velocity.y * Time.Delta * velocityStrength );


		Style.Height = Length.Pixels( Math.Min( lifeTime, transitions ) / transitions * particleSize );
		Style.Opacity = Math.Min( deathTime - lifeTime , transitions ) / transitions;

		if ( lifeTime > deathTime )
		{

			Delete();

		}

	}

}
