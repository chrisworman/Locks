using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Locks.iOS.Views
{
	public class ParticleSystem : View
	{

		public Vector2 ParticleVelocity { get; set; }

		public bool ShouldRandomizeParticleVelocity { get; set; }

		public bool ShouldRandomizeParticleVelocityDirectionX { get; set; }

		public bool ShouldRandomizeParticleVelocityDirectionY { get; set; }

		public double ParticleLifeSpanMilliseconds { get; set; }

		public bool ShouldRandomizeParticleLifeSpan { get; set; }

		public Vector2 ParticleAcceleration { get; set; }

		public double ParticleBirthDelayMilliseconds { get; set; }

		public bool ShouldRandomizeParticleBirthRate { get; set; }

		public bool ShouldShrink { get; set; }

		public bool ShouldTrail { get; set; }

		public delegate void OnStoppedDelegate (ParticleSystem particleSystemThatIsComplete);

		public OnStoppedDelegate OnStopped;

		private bool StopRequested { get; set; }

		private ViewLayers Particles { get; set; }

		private Texture2D[] ParticleTextures { get; set; }

		private ViewLayers Trails { get; set; }

		private double NextParticleBirthTimeMilliseconds { get; set; }

		private static Random RandomGenerator = new Random ();

		public ParticleSystem (Vector2 position, Constants.ViewLayer layer, params Texture2D[] particleTextures) :
			base (position, layer)
		{
			ParticleTextures = particleTextures;
			InitializeAndSetDefaults ();
		}

		private void InitializeAndSetDefaults()
		{
			Particles = new ViewLayers ();
			Trails = new ViewLayers ();
			ParticleBirthDelayMilliseconds = 50d;
			ParticleVelocity = new Vector2 (2.5f, 2.5f);
			ShouldRandomizeParticleVelocity = true;
			ShouldRandomizeParticleVelocityDirectionX = true;
			ShouldRandomizeParticleVelocityDirectionY = true;
			ParticleLifeSpanMilliseconds = 3000d;
			ShouldRandomizeParticleLifeSpan = true;
			ParticleAcceleration = new Vector2 (0, 0.05f);
			NextParticleBirthTimeMilliseconds = -1.0d;
			ShouldRandomizeParticleBirthRate = true;
			StopRequested = false;
			ShouldShrink = true;
			ShouldTrail = false;
		}

		public override void Update (GameTime gameTime)
		{

			base.Update (gameTime);

			Particles.Update (gameTime);
			Particles.RemoveNonVisibleViews ();

			Trails.Update (gameTime);
			Trails.RemoveNonVisibleViews ();

			if (StopRequested) {
				if (Particles.Count == 0 && OnStopped != null) {
					OnStopped (this);
				}
			} else {
				SpawnParticleIfNecessary (gameTime);
				if (ShouldTrail) {
					UpdateTrails ();
				}
			}

		}

		public override void Draw (GameTime gameTime, GraphicsDeviceManager graphics)
		{
			Trails.Draw (gameTime, graphics);
			Particles.Draw (gameTime, graphics);
		}

		public void Stop()
		{
			StopRequested = true;
		}

		private void SpawnParticleIfNecessary(GameTime gameTime)
		{

			// Initialize NextParticleBirthTimeMilliseconds if necessary
			if (NextParticleBirthTimeMilliseconds == -1.0d) {
				NextParticleBirthTimeMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
			}

			// Time to spawn a new particle?
			double currentTimeMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
			if (currentTimeMilliseconds >= NextParticleBirthTimeMilliseconds) {
				Particles.Add (CreateNewParticle());
				NextParticleBirthTimeMilliseconds = GetNextBirthTime (currentTimeMilliseconds);
			}

		}

		private Sprite CreateNewParticle()
		{
			Sprite newParticle = new Sprite (GetNextParticleTexture(), Position, Layer);
			newParticle.CenterOrigin ();
			double particleLifeSpan = GetNextParticleLifeSpan ();
			newParticle.StartEffect (new Effects.Disappear (particleLifeSpan));
			newParticle.StartEffect (new Effects.Motion (GetNextParticleVelocity(), ParticleAcceleration, particleLifeSpan));
			if (ShouldShrink) {
				newParticle.StartEffect (new Effects.Scale (1f, 0f, particleLifeSpan));
			}
			return newParticle;
		}

		private double GetNextBirthTime(double currentTimeMilliseconds)
		{
			if (ShouldRandomizeParticleBirthRate) {
				return currentTimeMilliseconds + ParticleBirthDelayMilliseconds * RandomGenerator.NextDouble ();
			} else {
				return currentTimeMilliseconds + ParticleBirthDelayMilliseconds;
			}
		}

		private double GetNextParticleLifeSpan()
		{
			if (ShouldRandomizeParticleLifeSpan) {
				return ParticleLifeSpanMilliseconds * RandomGenerator.NextDouble ();
			} else {
				return ParticleLifeSpanMilliseconds;
			}
		}

		private Texture2D GetNextParticleTexture()
		{
			if (ParticleTextures.Length == 1) {
				return ParticleTextures [0];
			} else {
				return ParticleTextures [RandomGenerator.Next (0, ParticleTextures.Length)];
			}
		}

		private Vector2 GetNextParticleVelocity()
		{
			if (ShouldRandomizeParticleVelocity) {
				float randomX = ParticleVelocity.X * (float)RandomGenerator.NextDouble ();
				float randomY = ParticleVelocity.Y * (float)RandomGenerator.NextDouble ();
				if (ShouldRandomizeParticleVelocityDirectionX &&  RandomGenerator.NextDouble () > 0.5d) {
					randomX = -randomX;
				}
				if (ShouldRandomizeParticleVelocityDirectionY && RandomGenerator.NextDouble () > 0.5d) {
					randomY = -randomY;
				}
				return new Vector2 (randomX, randomY);
			} else {
				return ParticleVelocity;
			}
		}

		private void UpdateTrails() {
			for (int layer = 0; layer < (int) Constants.ViewLayer.Modal+1; layer++) {
				List<View> viewLayer = Particles.GetLayer ((Constants.ViewLayer)layer);
				foreach (View view in viewLayer) {
					Sprite particle = (Sprite) view;
					Sprite trail = new Sprite (particle.Texture, particle.Position, particle.Layer);
					trail.Scale = particle.Scale;
					trail.Origin = particle.Origin;
					trail.RotationRadians = particle.RotationRadians;
					trail.OverlayColor = particle.OverlayColor;
					trail.StartEffect (new Effects.Disappear (200d));
					Trails.Add (trail);
				}
			}
		}

	}
}

