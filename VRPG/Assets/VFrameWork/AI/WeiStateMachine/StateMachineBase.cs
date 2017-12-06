using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace V
{
    public abstract class StateMachineBase : MonoBehaviour
    {
        [DependencyInjector]
        public EntityCore leBase;
        [DependencyInjector]
        public TransitionControlType transitionManager;
        [DependencyInjector]
        public AnimationSys animatorManager;

        public AiUtility.FieldOfView fieldOfView;
        public LayerMask targetLayer;
        public Transform currentTFTarget;
        [HideInInspector]public Vector3 targetOldPos;

        Rigidbody rgbody;


        public bool Ckeck_FindPlayer()
        {
            return false;
        }

        public Vector3 Get_RandomPos(float from, float to)
        {
            Vector3 pos = Vector3.zero;
            pos = pos.RandomPos(from, to);
            return pos;
        }

        public Vector3 Get_RandomPos_BaseOnCurrentPos(float from, float to)
        {
            Vector3 offset = Vector3.zero;
            offset = offset.RandomPos(from, to);
            return transform.position + offset;
        }

        public Vector3 Get_RandomPosXZ_BasedOnCurrentPos(float from, float to)
        {
            Vector3 offset = Vector3.zero;
            offset = offset.RandomPosXZ(from, to);
            return transform.position + offset;
        }

        public float Get_Distance_To_Target()
        {
            return Vector3.Distance(transform.position, currentTFTarget.position);
        }

        public void Set_Nav_Destination(Vector3 pos)
        {
           // transitionManager.Set_Nav_Destination(pos);
        }

        public void Approach_Target(float offset)
        {
            Vector3 dir = currentTFTarget.position - transform.position;
            dir.Normalize();
            Vector3 newdest = currentTFTarget.position - dir * offset;
            for (int i = 0; i < 10; i++)
            {
                bool walkable = Check_PositionWalkAble(newdest);
                if (walkable)
                {
                    Set_Nav_Destination(newdest);
                    return;
                }
                else
                {
                    Vector2 randOffset = Random.insideUnitCircle;
                    newdest += new Vector3(randOffset.x, newdest.y, randOffset.y);
                }
            }
        }

        public void Look_At_Target()
        {
            transform.LookAt(currentTFTarget.position);
        }

        public void Look_At_Target_XZ() {
            transform.LookAt(new Vector3(currentTFTarget.position.x, transform.position.y, currentTFTarget.position.z));
        }

        public bool Check_PositionWalkAble(Vector3 pos)
        {
            return false;
            //return transitionManager.SamplePosition(pos);
        }

        public bool Check_Target_Get_NewPosition(float sqrOffset)
        {
            if ((currentTFTarget.position - targetOldPos).sqrMagnitude < sqrOffset)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// This Function Usually used for setting a Random Position.
        /// </summary>
        public bool Check_ArriveDestination_NotPathPending()
        {
            return false;
            //return transitionManager.ArriveDestination_NotPathPending();
        }

        public void Anima_Set_Float(string name, float value)
        {
            animatorManager.SetFloat(name, value);
        }

        public void Anima_Set_Float_BasedOnRigdbody(string name)
        {
            if (rgbody == null) { rgbody = GetComponent<Rigidbody>(); if (rgbody == null) { Debug.LogError("No Rigdbody Attack to Game Object"); return; } }
            animatorManager.SetFloat(name, rgbody.velocity.sqrMagnitude);
        }

        public float GetSet___GET_TargetDis___SET_KeepTargetInRange(float range, float desiredRange)
        {
            float dstToTarget = Get_Distance_To_Target();
            if (dstToTarget > range)
            {
                if (Check_Target_Get_NewPosition(0.25f))
                {
                    Approach_Target(desiredRange);
                }
            }
            return dstToTarget;
        }

        /// <summary>
        /// Check if the Target in the max range, and set the position to the desired range
        /// </summary>
        /// <returns> return a bool, which indicate if the target in range </returns>
        public bool CheckSet___CHECK_InRange_SET_KeepTargetInRange(float maxRange, float desiredRange)
        {
            float dstToTarget = Get_Distance_To_Target();
            if (dstToTarget > maxRange)
            {
                if (Check_Target_Get_NewPosition(0.25f))
                {
                    Approach_Target(desiredRange);
                }
                return false;
            }
            return true;
        }

    }
}
