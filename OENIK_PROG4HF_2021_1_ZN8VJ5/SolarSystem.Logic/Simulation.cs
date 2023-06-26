namespace SolarSystem.Logic
{
    using System;
    using System.Linq;
    using SolarSystem.Data.Models;
    using SolarSystem.Repository;

    /// <summary>
    /// This class is responsible for evolving stars over time.
    /// A star has 3 phases:
    /// - main-sequence
    /// - red giant
    /// - stellar remnant
    /// Each star has a change to switch phases depending on the star's properties
    /// and the simulation's direction (we can simulate both forward and backward in time).
    /// </summary>
    public class Simulation
    {
        private static Random rnd = new Random();
        private IStarRepository repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="Simulation"/> class.
        /// </summary>
        /// <param name="repo">Connection to Data layer.</param>
        public Simulation(IStarRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Runs the simulation forward.
        /// </summary>
        /// <param name="dt">Time step size.</param>
        public void RunForward(int dt)
        {
            foreach (Star star in this.repo.GetAll().ToList())
            {
                // If star is a main sequence star
                if (this.repo.IsMainSequence(star))
                {
                    // Then try to push it into a transition
                    if (CheckRedGiantTrainsition(star.Mass, dt))
                    {
                        this.RedGiantTrasition(star);
                    }

                    // Since stars are sable untill their transition
                    // we don't need to do anything in the 'else' branch
                }

                // If star is in red giant phase
                if (this.repo.InRedGiantPhase(star))
                {
                    // Try to push it into a transition
                    if (CheckTransitionToStellarRemnant(dt))
                    {
                        this.StellarRemnantTransition(star);
                    }
                }

                // Stars in the stellar remnant phase slowly radiate away their temperature
                else
                {
                    switch (star.Type)
                    {
                        case StellarType.D: this.repo.ChangeTemperature(star.Id, 0.001f * dt); break;
                        case StellarType.NeutronStar: this.repo.ChangeTemperature(star.Id, 0.000001f * dt); break;
                        case StellarType.BlackHole: this.repo.ChangeTemperature(star.Id, float.Epsilon * dt); break;
                    }

                    // Update habiatable zone after changing the temperature
                    this.repo.UpdateHabitableZone(star.Id);
                }
            }
        }

        /// <summary>
        /// Runs the simulation backward.
        /// </summary>
        /// <param name="dt">Time step size.</param>
        public void RunBackward(int dt)
        {
            foreach (Star star in this.repo.GetAll().ToList())
            {
                if (this.repo.IsStellarRemnant(star))
                {
                    if (CheckRedGiantTrainsitionBackward(dt))
                    {
                        this.RedGiantTransitionBackward(star);
                    }
                    else
                    {
                        switch (star.Type)
                        {
                            case StellarType.D: this.repo.ChangeTemperature(star.Id, 1.001f * dt); break;
                            case StellarType.NeutronStar: this.repo.ChangeTemperature(star.Id, 1.000001f * dt); break;
                            case StellarType.BlackHole: this.repo.ChangeTemperature(star.Id, (1 + float.Epsilon) * dt); break;
                        }
                    }
                }

                if (this.repo.InRedGiantPhase(star))
                {
                    if (CheckMainSequenceTransition(dt))
                    {
                        this.MainSequenceTransition(star);
                    }
                }
            }
        }

        // Trainsition checking methods used in forward simulation
        private static bool CheckRedGiantTrainsition(float mass, int dt)
        {
            // Transition propbability is proportional to star's mass
            // If we use bigger intervals then there is more 'time' for the trainsition to happen
            return (rnd.NextDouble() + 0.01) * dt * 100 > mass;
        }

        private static bool CheckTransitionToStellarRemnant(int dt)
        {
            return rnd.Next(1, 10) * dt > 10;
        }

        // Trainsition checking methods used in backward simulation
        private static bool CheckRedGiantTrainsitionBackward(int dt)
        {
            return rnd.Next(1, 15) * dt / 5 > 10;
        }

        private static bool CheckMainSequenceTransition(int dt)
        {
            return (rnd.Next(0, 3) + 0.1) * dt >= 2;
        }

        // Transition method from main-sequence to red giant
        private void RedGiantTrasition(Star star)
        {
            this.repo.ChangeType(star.Id, StellarType.RedGiant);
            this.repo.ChangeTemperature(star.Id, 0.5f);
            this.repo.ChangeMass(star.Id, star.Mass * 0.05f);
            this.repo.UpdateHabitableZone(star.Id);
        }

        // Transition method red giant to stellar remnant
        private void StellarRemnantTransition(Star star)
        {
            if (star.Mass < 1.4f)
            {
                this.repo.ChangeType(star.Id, StellarType.D);
            }
            else if (star.Mass < 3f)
            {
                this.repo.ChangeType(star.Id, StellarType.NeutronStar);
            }
            else
            {
                this.repo.ChangeType(star.Id, StellarType.BlackHole);
            }
        }

        // Backward transition method from stellar remnant to red giant
        private void RedGiantTransitionBackward(Star star)
        {
            this.repo.ChangeType(star.Id, StellarType.RedGiant);
        }

        // Backward transition method from red giant to main-sequence
        private void MainSequenceTransition(Star star)
        {
            this.repo.ChangeTemperature(star.Id, 2f);
            this.repo.ChangeMass(star.Id, star.Mass * 20f);
            this.repo.UpdateHabitableZone(star.Id);

            if (star.Temperature > 30)
            {
                this.repo.ChangeType(star.Id, StellarType.O);
            }
            else if (star.Temperature > 10)
            {
                this.repo.ChangeType(star.Id, StellarType.B);
            }
            else if (star.Temperature > 7.5)
            {
                this.repo.ChangeType(star.Id, StellarType.A);
            }
            else if (star.Temperature > 6)
            {
                this.repo.ChangeType(star.Id, StellarType.F);
            }
            else if (star.Temperature > 5)
            {
                this.repo.ChangeType(star.Id, StellarType.G);
            }
            else if (star.Temperature > 3.5)
            {
                this.repo.ChangeType(star.Id, StellarType.K);
            }
            else
            {
                this.repo.ChangeType(star.Id, StellarType.M);
            }
        }
    }
}
