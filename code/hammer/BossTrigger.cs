﻿using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hammer;
using System.Linq;


[Library( "xoxoxo_boss_trigger" )]
[Display( Name = "Boss Trigger", GroupName = "xoxoxo", Description = "If the boss is inside this trigger he will be able to see you kiss." )]
public partial class BossTrigger : BaseTrigger
{

	public override void Spawn()
	{

		base.Spawn();

	}

	/// Only works with dynamic entities, and those suck!
	/*public override void Touch( Entity other )
	{

		if ( !other.IsServer ) return;
		if ( other is not Boss boss ) return;

		boss.IsInsideTrigger = true;

		base.Touch( other );


	}

	public override void EndTouch( Entity other )
	{

		if ( !other.IsServer ) return;
		if ( other is not Boss boss ) return;

		boss.IsInsideTrigger = false;


	}*/

	[Event.Tick]
	public void SearchBosses()
	{

		var bosses = Entity.All.OfType<Boss>().ToList();

		foreach ( var boss in bosses )
		{

			if ( WorldSpaceBounds.Overlaps( boss.WorldSpaceBounds ) )
			{

				boss.IsInsideTrigger = true;

			}
			else
			{

				boss.IsInsideTrigger = false;

			}

		}


	}

}
