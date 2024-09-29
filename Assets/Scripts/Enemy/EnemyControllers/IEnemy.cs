using Enemy.States;
using UnityEngine;

public interface IEnemy
{
   float Speed { get; set; }
   float MaxSpeed { get; set; }
   float AngularSpeed { get; set; }
   Vector3 TargetPosition { get; }
	EnemyAnimation Animation { get; set; }
	Rigidbody GetRigidBody();
	Transform EnemyTransform { get; }

}
