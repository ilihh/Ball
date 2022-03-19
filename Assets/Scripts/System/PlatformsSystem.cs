namespace BallGame
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	class PlatformsSystem : MoveSystem
	{
		class Instances
		{
			Stack<Transform> Cache = new Stack<Transform>();

			List<Transform> Active = new List<Transform>();

			GameObject Prefab;

			public int ActiveCount
			{
				get => Active.Count;
			}

			public int CacheCount
			{
				get => Cache.Count;
			}

			public float FullLength
			{
				get
				{
					var n = Active.Count;
					if (n == 0)
					{
						return 0;
					}

					var first = Active[0];
					if (n == 1)
					{
						return first.localScale.x;
					}

					var last = Active[n - 1];
					return last.position.x + (last.transform.localScale.x / 2f) - first.position.x;
				}
			}

			public Instances(GameObject prefab)
			{
				Prefab = prefab;
			}

			void Disable(Transform instance)
			{
				instance.gameObject.SetActive(false);
				Cache.Push(instance);
			}

			public void Reset()
			{
				foreach (var instance in Active)
				{
					if (instance != null)
					{
						Disable(instance);
					}
				}

				Active.Clear();
			}

			public void Destroy()
			{
				Reset();

				foreach (var x in Cache)
				{
					if (x != null)
					{
						UnityEngine.Object.Destroy(x.gameObject);
					}
				}

				Cache.Clear();
			}

			public List<Transform>.Enumerator GetEnumerator()
			{
				return Active.GetEnumerator();
			}

			Transform Get()
			{
				if (Cache.Count > 0)
				{
					var instance = Cache.Pop();
					instance.gameObject.SetActive(true);
					return instance;
				}

				return UnityEngine.Object.Instantiate(Prefab).transform;
			}

			void Add(float position, float width)
			{
				var instance = Get();

				var pos = instance.position;
				pos.x = position + (width / 2f);
				instance.position = pos;

				var scale = instance.localScale;
				scale.x = width;
				instance.localScale = scale;

				Active.Add(instance);
			}

			public void Generate(PlacementConfig placement)
			{
				var length = FullLength;

				float pos;
				float platform_width;

				if (Active.Count == 0)
				{
					pos = placement.StartPosition;
					platform_width = placement.GenerateFirstPlatform();
					Add(pos, platform_width);

					pos += platform_width;
					length += platform_width;
				}
				else
				{
					var last = Active[Active.Count - 1];
					pos = last.transform.position.x + (last.transform.localScale.x / 2f);
				}

				while (length < placement.ActiveLength)
				{
					var pit_width = placement.GeneratePit();
					pos += pit_width;
					length += pit_width;

					platform_width = placement.GeneratePlatform();
					Add(pos, platform_width);

					pos += platform_width;
					length += platform_width;
				}
			}

			public void Reduce(PlacementConfig placement)
			{
				if (Active.Count == 0)
				{
					return;
				}

				var instance = Active[0];
				if (instance.position.x < placement.DisableAtPoint)
				{
					Disable(instance);
					Active.RemoveAt(0);
				}
			}
		}

		Instances Ceiling;

		Instances Floor;

		PlatformsConfig platforms;

		PlacementConfig placement;

		public PlatformsSystem(PlatformsConfig platforms, PlacementConfig placement, float speed)
		{
			this.platforms = platforms;
			this.placement = placement;

			Ceiling = new Instances(platforms.Ceiling);
			Floor = new Instances(platforms.Floor);

			Speed = speed;
		}

		public override void Update(float deltaTime)
		{
			Ceiling.Reduce(placement);
			Floor.Reduce(placement);

			Ceiling.Generate(placement);
			Floor.Generate(placement);
		}

		public override void FixedUpdate(float deltaTime)
		{
			if (Pause)
			{
				return;
			}

			foreach (var p in Ceiling)
			{
				Move(p, deltaTime);
			}

			foreach (var p in Floor)
			{
				Move(p, deltaTime);
			}
		}

		public override void Reset()
		{
			Ceiling.Reset();
			Floor.Reset();
		}

		public override void Start()
		{
			base.Start();

			Ceiling.Generate(placement);
			Floor.Generate(placement);
		}

		public override void Destroy()
		{
			Reset();

			Ceiling.Destroy();
			Floor.Destroy();
		}
	}
}