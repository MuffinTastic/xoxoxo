﻿using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hammer;

[Library( "xoxoxo_clock" )]
[Model( Model = "models/clock/clock.vmdl" )]
[Display( Name = "Game Clock", GroupName = "xoxoxo", Description = "Shows you the time of the day, usual shift is from 9am to 5pm" )]

public partial class Clock : AnimEntity
{

	public override void Spawn()
	{

		base.Spawn();

		SetModel( "models/clock/clock.vmdl" );

		EnableDrawing = true;
		UseAnimGraph = false;
		PlaybackRate = 0;

	}

	const float startingTime = 9.07058f; // 9am
	const float turnDuration = 7.79628f; // 8 hours ( It really isn't that precise :-/ )
	const float clockHours = 12f;
	float hourFraction => 1f / clockHours;

	[Event.Tick]
	public void Tick()
	{

		float time = xoxoxo.Game.RoundTimeNormal * turnDuration;

		CurrentSequence.TimeNormalized = ( (time / clockHours ) + hourFraction * startingTime ) % 1f;

		DebugOverlay.Text( Position - Vector3.Up * CollisionBounds.Size.z / 2f, $"RoundTime: {xoxoxo.Game.RoundTime}\nNormalTime: {xoxoxo.Game.RoundTimeNormal}" );

	}

}
