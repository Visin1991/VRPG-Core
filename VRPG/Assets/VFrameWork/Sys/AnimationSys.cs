using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace V
{
	public class AnimationSys : SystemCore {
		
		public Animator animator;

		public override System.Type Type{get{ return typeof(AnimationSys);}}

		public Animator Animator{get{ return animator;}}

        System.Random rand = new System.Random();

        AnimationCallBack callback;

        float currentVelocity;
        bool enableMotionInput = true;
        float nextInputTime;

        CameraSys cameramanager;
        Transform mainBody;

        //LEAnimationCallBack callback;

        public void PlayerUpdate()
        {
            if (SystemInput.Instance.CurrentVH == Vector2.zero)
            {
                animator.SetFloat("Speed X", 0.0f);
                animator.SetFloat("Speed Z", 0.0f);
            }
            else
            {

                float yaw = cameramanager.CurrentCamera.Yaw;
                float bodyY = mainBody.eulerAngles.y;
                Vector2 vh = SystemInput.Instance.CurrentVH.Rotate(bodyY - yaw);
                animator.SetFloat("Speed X", vh.x);
                animator.SetFloat("Speed Z", vh.y);
            }
        }

        public virtual void SetKeyStatue(InputIndex index, bool state) { animator.SetBool(index.ToString(), state); }

        public virtual void SetMovementForward(float forward) { animator.SetFloat("Forward", forward); }

        public void SetMovementStrafe(float strafe) { animator.SetFloat("Strafe", strafe); }

        public void SetMovementForwardSmoothDamp(float forward)
        {
            float current = animator.GetFloat("Forward");
            float newCurrent = Mathf.SmoothDamp(current, forward, ref currentVelocity, 0.15f);
            animator.SetFloat("Forward", newCurrent);
        }

        public void SetMotionType(AnimationMotionType type) { if (enableMotionInput) animator.SetInteger("MotionType", (int)type); }

        public void Force_SetMotionType(AnimationMotionType type)
        {
            animator.SetInteger("MotionType", (int)type);
        }

        public void SetMotionIndex(int motionIndex) { if (enableMotionInput) animator.SetInteger("MotionIndex", motionIndex); }

        public void Force_SetMotionIndex(int motionIndex) { animator.SetInteger("MotionIndex", motionIndex); }

        public void SetMotionIndex_Random_From_To(int from, int to)
        {
            nextInputTime += Time.deltaTime;
            if (nextInputTime > 3.0f)
            {
                int index = rand.Next(from, to);
                animator.SetInteger("MotionIndex", index);
                nextInputTime = 0.0f;
            }
        }

        public void SetBool(string name, bool value)
        {
            if (enableMotionInput)
                animator.SetBool(name, value);
        }

        public void Force_SetBool(string name, bool value)
        {
            animator.SetBool(name, value);
        }

        public void SetTrigger(string name)
        {
            if (enableMotionInput)
                animator.SetTrigger(name);
        }

        public void Force_SetTrigger(string name) { animator.SetTrigger(name); }

        public void SetFloat(string name, float value) { if (enableMotionInput) animator.SetFloat(name, value); }

        public void Force_SetFloat(string name, float value) { animator.SetFloat(name, value); }

        public Animator GetAnimator()
        {
            return animator;
        }

        public void InvokeMethod(MethodInfo m, object[] obj) { if (callback != null) { callback.InvokeMethod(m, obj); } else { Debug.Log("Animation CallBack Component is Null"); } }

        public enum AnimationMotionType
        {
            IWR_0,
            MELEE_1,
            HoldGun_2,
            STUFF_3
        }

    }
}
