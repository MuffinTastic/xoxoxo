﻿using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hammer;


[Library( "xoxoxo_boss_trigger" )]
[Display( Name = "Boss Trigger", GroupName = "xoxoxo", Description = "If the boss is inside this trigger he will be able to see you kiss." )]
public partial class BossTrigger : BaseTrigger
{

	public override void Spawn()
	{

		base.Spawn();

	}

	public override void Touch( Entity other )
	{

		if ( !other.IsServer ) return;
		if ( other is not Boss boss ) return; // Cool code CrayZ! Thanks

		boss.IsInsideTrigger = true;

		base.Touch( other );


	}

	public override void EndTouch( Entity other )
	{

		if ( !other.IsServer ) return;
		if ( other is not Boss boss ) return; // Cool code CrayZ! Thanks

		boss.IsInsideTrigger = false;


	}

}
