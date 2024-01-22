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
		[TooltipText("Always returs SUCCESS")]
		Undefined = 0,
		SubTree = 1,
		[TooltipText("Executes nodes sequentially, stopping when the first node succeeds")]
		Selector = 2,
		[TooltipText("Executes nodes sequentially, stopping when the first node fails")]
		Sequence = 3,
		[TooltipText("Executes nodes simultaneously. Returns SUCCESS if all succeed, FAILURE if all fail, RUNNING otherwise")]
		Parallel = 4,
		[TooltipText("Executes random node, selects another after 'Cooldown' sec")]
		RandomSelector = 5,
		[TooltipText("Executes node, swaps SUCCESS and FAILURE")]
		Invertor = 6,
		[TooltipText("Executes node, returns 'Result'")]
		ConstantResult = 7,
		[TooltipText("Executes node until it succeeds. Returns 'Result' after that")]
		CompleteOnce = 8,
		[TooltipText("Executes random node, stops after it succeeds, selects another after 'Cooldown' sec")]
		RandomExecutor = 9,
		HaveEnoughEnergy = 50,
		IsLowOnHp = 51,
		[TooltipText("true if player did any action this frame")]
		IsControledByPlayer = 52,
		[TooltipText("Requires threat list")]
		HaveIncomingThreat = 53,
		[TooltipText("Looks for nearest enemy. Changes target every MaxCooldown sec if it's defined. SUCCESS if found, FAILURE otherwise")]
		FindEnemy = 100,
		[TooltipText("Moves towards target until inside attack radius. Requires main target. Uses attack range of selected weapons. SUCCESS if inside attack range, FAILURE if any error, RUNNING otherwise")]
		MoveToAttackRange = 101,
		[TooltipText("Requires main target. Uses selected weapons. Return SUCCESS when activates any weapon, FAILURE otherwise")]
		Attack = 102,
		[TooltipText("Returns SUCCESS if any weapon found, FAILURE otherwise. If no weapon selected all weapons will be used")]
		SelectWeapon = 103,
		[TooltipText("Spawns drones and clones. Returns RUNNING until there are no more drones left to spawn, then returns SUCCESS if any drones active, FAILURE otherwise")]
		SpawnDrones = 104,
		[TooltipText("Tries to ram the target, can use Afterburner and Fortification. Returns FAILURE if there is no target or it's moving too fast, RUNNING otherwise")]
		Ram = 105,
		DetonateShip = 106,
		[TooltipText("Makes ship disappear completely, without a trace")]
		Vanish = 107,
		[TooltipText("Keeps ship at required distance. Requires main target. Uses attack range of selected weapons. SUCCESS if inside valid range, FAILURE if any error, RUNNING otherwise")]
		MaintainAttackRange = 108,
		Wait = 109,
		LookAtTarget = 110,
		[TooltipText("Updates the list of enemies other than the main target. Returns SUCCESS if any found, FAILURE otherwise")]
		LookForSecondaryTargets = 111,
		[TooltipText("Updates threat list. Returns SUCCESS if any found, FAILURE otherwise")]
		LookForThreats = 112,
		[TooltipText("Attacks any secondary target with selected weapons if possible. Return SUCCESS when activates any weapon, FAILURE otherwise")]
		AttackSecondaryTargets = 113,
		[TooltipText("Activates device. Returns SUCCESS if activated, FAILURE otherwise")]
		ActivateDevice = 114,
		[TooltipText("If energy level drops below FailIfLess, enters recharging state and returns FAILURE until energy level reaches RestoreUntil, otherwise returns SUCCESS")]
		RechargeEnergy = 115,
		[TooltipText("If any directional weapon active, maintain focus on target and returns RUNNING. Returns FAILURE otherwise")]
		SustainAim = 116,
		[TooltipText("Charges all selected weapons that requires charging. Returns FAILURE if no such weapons, SUCCESS if fully charged, RUNNING otherwise")]
		ChargeWeapons = 117,
		[TooltipText("Follows the target. Returns FAILURE if there is no target, RUNNING otherwise")]
		Chase = 118,
		[TooltipText("Changes the ship's trajectory to avoid collision. Requires threat list. Returns SUCCESS if there are no threats, RUNNING otherwise")]
		AvoidThreats = 119,
		[TooltipText("Makes ship stop moving. Returns SUCCESS if standing still, RUNNING otherwise")]
		Stop = 120,
		[TooltipText("Activates weapons with high recoil to get impulse. Returns FAILURE if doesn't have such weapons, RUNNING otherwise")]
		UseRecoil = 121,
		[TooltipText("Turns ship towards threats and activates shield. Returns FAILURE if there are no shields or threats, RUNNING otherwise")]
		DefendWithFronalShield = 122,
		IsWithinAttackRange = 150,
		MotherShipRetreated = 200,
		MotherShipDestroyed = 201,
		FlyAroundMothership = 202,
		[TooltipText("Detaches from the mothership. Starts treating all ships as enemies. Returns SUCCESS once activated, FAILURE after that")]
		GoBerserk = 203,
		[TooltipText("Sets the mothership as a main target. Returns FAILURE if it's absent, otherwise SUCCESS")]
		TargetMothership = 204,
		MothershipLowHp = 205,
		[TooltipText("Spawns text over the ship sprite that disappears shortly. Retruns SUCCESS")]
		ShowMessage = 300,
		[TooltipText("Writes message to the log file (https://docs.unity3d.com/Manual/LogFiles.html). Retruns SUCCESS")]
		DebugLog = 301,
	}
}
