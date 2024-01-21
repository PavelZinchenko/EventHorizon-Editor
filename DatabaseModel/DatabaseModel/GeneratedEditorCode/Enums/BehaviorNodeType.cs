//-------------------------------------------------------------------------------
//                                                                               
//    This code was automatically generated.                                     
//    Changes to this file may cause incorrect behavior and will be lost if      
//    the code is regenerated.                                                   
//                                                                               
//-------------------------------------------------------------------------------

namespace EditorDatabase.Enums
{
	public enum BehaviorNodeType
	{
		Undefined = 0,
		SubTree = 1,
		[TooltipText("Executes nodes sequentially, stopping when the first node succeeds")]
		Selector = 2,
		[TooltipText("Executes nodes sequentially, stopping when the first node fails")]
		Sequence = 3,
		Parallel = 4,
		[TooltipText("Executes random node, selects another after 'Cooldown' sec")]
		RandomSelector = 5,
		Invertor = 6,
		ConstantResult = 7,
		CompleteOnce = 8,
		HaveEnoughEnergy = 50,
		IsLowOnHp = 51,
		[TooltipText("true if player did any action this frame")]
		IsControledByPlayer = 52,
		FindEnemy = 100,
		[TooltipText("Requires target, uses range of selected weapons")]
		MoveToAttackRange = 101,
		[TooltipText("Requires target, uses selected weapons")]
		Attack = 102,
		SelectWeapon = 103,
		SpawnDrones = 104,
		Ram = 105,
		DetonateShip = 106,
		Vanish = 107,
		[TooltipText("Requires target, uses range of selected weapons")]
		MaintainAttackRange = 108,
		Wait = 109,
		LookAtTarget = 110,
		LookForSecondaryTargets = 111,
		LookForThreats = 112,
		AttackSecondaryTargets = 113,
		ActivateDevice = 114,
		RechargeEnergy = 115,
		SustainAim = 116,
		ChargeWeapons = 117,
		Chase = 118,
		IsWithinAttackRange = 150,
		MotherShipRetreated = 200,
		MotherShipDestroyed = 201,
		FlyAroundMothership = 202,
		GoBerserk = 203,
		TargetMothership = 204,
		MothershipLowHp = 205,
		ShowMessage = 300,
		DebugLog = 301,
	}
}
