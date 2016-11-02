using ColossalFramework;
using ICities;
using System;
using UnityEngine;
using DynamicFoliage.OptionsSpace.OptionsFramework;
using DynamicFoliage.OptionsSpace;
using DynamicFoliage.OptionsSpace.Profiles;

namespace DynamicFoliage
{
    public class ChirpBox : IChirperMessage
    {
        private static DateTime lastChrip = DateTime.MinValue;

        public static void SendMessage(string senderName, string msg)
        {
            ChirpPanel cp = ChirpPanel.instance;
            if (cp == null)
                return;

            if (lastChrip == DateTime.MinValue)
                lastChrip = DateTime.Now.AddSeconds(10);

            DateTime now = DateTime.Now;
            //  if ((now - lastChrip).TotalSeconds > 5)
            {
                cp.AddMessage(new ChirpBox() { senderName = senderName, text = msg });
                lastChrip = now;
            }
        }

        public uint senderID
        {
            get
            {
                return 0;
            }
        }

        public string senderName
        {
            get; set;
        }

        public string text
        {
            get; set;
        }
    }

    public class DynamicFoliageEngine
    {
        private static DynamicFoliageEngine instance = null;


        public static DynamicFoliageEngine Instance
        {
            get
            {
                if (instance == null)
                    instance = new DynamicFoliageEngine();
                return instance;
            }
        }

        public static void SetUpdateFrequency(string stub)
        {
            Instance.UpdateFrequency = float.Parse(OptionsWrapper<Options>.Options.updateFrequency);
        }

        public static void SetProfile(string stub)
        {
            Instance.CurrentProfile = ProfileIO.LoadedProfiles[OptionsWrapper<Options>.Options.profile];
        }

        public bool IsInitialized { get; private set; }


        public FoliageProfile CurrentProfile {
            get
            {
                return currentProfile;
            }
            set
            {
                currentProfile = value;
                CurrentAnnualVerdance = value.m_staticAnnualVerdance;
            }
        }
        public VerdanceMeasure[] CurrentAnnualVerdance { get; private set; }

        private FoliageProfile currentProfile;

        private SimulationManager simulationManager;
        private NaturalResourceManager naturalResourcesManager;
        internal IThreading ThreadingManager { get; set; }

        private DateTime lastTimeUpdate;
        public double UpdateFrequency { get; set; }
        
        private int upperIndex = 0;

        private DynamicFoliageEngine()
        {
            this.IsInitialized = false;
            InitializeManagers();
        }

        public void Initialize()
        {
            lastTimeUpdate = DateTime.MinValue;
            SetUpdateFrequency(null);
            SetProfile(null);
            this.IsInitialized = true;
        }

        private bool InitializeManagers()
        {
            SimulationManager sm = Singleton<SimulationManager>.instance;
            if (sm == null)
                return false;

            NaturalResourceManager nm = Singleton<NaturalResourceManager>.instance;
            if (nm == null)
                return false;

            this.simulationManager = sm;
            this.naturalResourcesManager = nm;
            return true;
        }

        public void UpdateFoliage()
        {
            if (!this.IsInitialized)
                return;

            DateTime currentTime = GetDateTime();

            TimeSpan simulationTimeUpdateDelta = currentTime - this.lastTimeUpdate;
            if (simulationTimeUpdateDelta.TotalHours < UpdateFrequency)
                return;

            if (currentTime.Year != this.lastTimeUpdate.Year)
            {
                upperIndex = 0;
            }

            this.lastTimeUpdate = currentTime;

#if DEBUG
            ChirpBox.SendMessage("DebugSimTime + Delta", currentTime.ToString() + ", " + simulationTimeUpdateDelta.ToString());
#endif

            int dayOfYear = currentTime.DayOfYear;

            int numIndices = CurrentAnnualVerdance.Length;

            while (upperIndex < numIndices)
            {
                if (dayOfYear <= CurrentAnnualVerdance[upperIndex].dayOfYear)
                {
                    break;
                }
                upperIndex++;
            }

            float coefficient;

            if (upperIndex == 0)
            {
                coefficient = CurrentAnnualVerdance[upperIndex].verdance;
            } else if (upperIndex == numIndices)
            {
                coefficient = CurrentAnnualVerdance[numIndices - 1].verdance;
            } else
            {
                VerdanceMeasure upper = CurrentAnnualVerdance[upperIndex];
                VerdanceMeasure lower = CurrentAnnualVerdance[upperIndex - 1];
                coefficient = Mathf.Lerp(lower.verdance, upper.verdance, Mathf.InverseLerp(lower.dayOfYear, upper.dayOfYear, dayOfYear));
            }

            if(NaturalResourcesManagerDetour.m_seasonalCoefficient != coefficient)
            {
                NaturalResourcesManagerDetour.m_seasonalCoefficient = coefficient;
                NaturalResourcesManagerDetour.RefreshTexture(true);
            }

#if DEBUG
                ChirpBox.SendMessage("DebugFoliage", "Interpolated Coeff: " + NaturalResourcesManagerDetour.m_seasonalCoefficient.ToString());
#endif
        }

        private DateTime GetDateTime()
        {
            //return this.ThreadingManager.simulationTime;
            return this.simulationManager.FrameToTime(this.ThreadingManager.simulationFrame);
            //int realHour = (int)this.ThreadingManager.simulationDayTimeHour;
            //int realMinute = (int)((this.ThreadingManager.simulationDayTimeHour - realHour) * 60);
            //return new DateTime(this.ThreadingManager.simulationTime.Year, this.ThreadingManager.simulationTime.Month, this.ThreadingManager.simulationTime.Day, realHour, realMinute, 0);
        }
    }
}
