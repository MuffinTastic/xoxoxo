﻿using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hammer;

public enum KisserState
{

	Working,
	Kissing,
	Busted,
	Running

}

[Library( "xoxoxo_kisser" )]
[Model( Model = "models/citizen/citizen.vmdl" )]
[Display( Name = "Kisser", GroupName = "xoxoxo", Description = "The player or their partner" )]

public partial class Kisser : Human
{
	[Property, FGDType( "target_destination" )]
	public string SeatName { get; internal set; }
	public Prop Seat => FindByName( SeatName ) as Prop;
	[Property, FGDType( "target_destination" )]
	public string DeskName { get; internal set; }
	public Prop Desk => FindByName( DeskName ) as Prop;
	[Property, FGDType( "target_destination" )]
	public string MonitorName { get; internal set; }
	public Prop Monitor => FindByName( MonitorName ) as Prop;

	[Net] public KisserState CurrentState { get; internal set; } = KisserState.Working;
	public bool IsKissing => CurrentState == KisserState.Kissing;
	public bool IsLeft => this == Entities.KisserLeft;

	[Event.Tick.Server]
	public void HandleVisuals()
	{

		SetAnimParameter( "Sitting", CurrentState != KisserState.Running );
		SetAnimParameter( IsLeft ? "Kissing" : "Kissing2", IsKissing );

		var distance = Entities.KisserLeft.OriginalPosition.Distance( Entities.KisserRight.OriginalPosition ) / 2 - 24.7f;

		var wishRotation = Rotation.FromYaw( IsLeft ? (IsKissing ? 270f : 180f) : ( IsKissing ? 90f : 0f ) );
		var wishPosition = OriginalPosition + OriginalRotation.Backward * ( IsKissing ? distance : 0f );

		Rotation = Rotation.Lerp( Rotation, wishRotation, 0.1f );
		Position = Vector3.Lerp( Position, wishPosition, 0.1f );

		if ( !IsDressed )
		{

			SetAttire( IsLeft ? "terrence" : "theresa" );

		}

		Seat.Rotation = Rotation.RotateAroundAxis( Vector3.Up, -90f );
		Seat.Position = Position.WithZ( Seat.Position.z ) + Rotation.Backward * 4f;
		
		Monitor.SetMaterialGroup( IsKissing ? 3 : ( IsLeft ? 4 : 0 ) );

	}

}
