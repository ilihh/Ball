namespace BallGame
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class BaseSystem
	{
		public bool Pause
		{
			get;
			protected set;
		} = true;

		public virtual void Start()
		{
			SetPause(false);
		}

		public virtual void Destroy()
		{
		}


		public virtual void Reset()
		{
		}

		public virtual void Update(float deltaTime)
		{
		}

		public virtual void FixedUpdate(float deltaTime)
		{
		}

		public virtual void SetPause(bool pause)
		{
			Pause = pause;
		}
	}
}
