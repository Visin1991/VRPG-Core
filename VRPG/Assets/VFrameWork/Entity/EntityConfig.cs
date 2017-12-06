using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fraction = System.Int32;

namespace V
{
	
[System.Serializable]
public class EntityConfig {
		
		public bool connectSystemInput = false;
		public Fraction fraction = DefaultFaction;
		public LayerMask layer = 1;
		public TransitionDriverType transitionDriver = TransitionDriverType.CharacterController;
		public TransitionInputType transitionInptType = TransitionInputType.KeyJoystic;
		public RotationType rotationType = RotationType.Look_At_MousePos;

		readonly static int DefaultFaction = 0;
	}
}


public enum TransitionDriverType
{
	RiggidBody,
	CharacterController,
	Direct
}

public enum TransitionInputType
{
	Non,
	KeyJoystic,
	Nav_PositionCommand,
	Direct_Teleport
}

public enum RotationType
{
	Look_At_MousePos,
	Look_To_CameraDir,
	Non_Strafe_And_Back
}
	
