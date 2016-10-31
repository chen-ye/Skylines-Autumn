using ColossalFramework;
using ICities;
using System;
using DynamicFoliage.OptionsFramework;
using DynamicFoliage.Redirection;
using System.Collections.Generic;
using UnityEngine;

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

        public static void SetUpdateFrequency(bool stub)
        {
            Instance.UpdateFrequency = OptionsWrapper<Options>.Options.updateFrequency;
        }

        public bool IsInitialized { get; private set; }

        private SimulationManager simulationManager;
        private NaturalResourceManager naturalResourcesManager;
        internal IThreading ThreadingManager { get; set; }

        private DateTime lastTimeUpdate;
        public double UpdateFrequency { get; set; }

        public List<Tuple<int, float>> SeasonalVerdance { get; private set; }
        private int upperIndex = 0;

        private DynamicFoliageEngine()
        {
            this.IsInitialized = false;
            this.UpdateFrequency = OptionsWrapper<Options>.Options.updateFrequency;
            InitializeManagers();
        }

        public void Initialize()
        {
            lastTimeUpdate = DateTime.MinValue;
            SeasonalVerdance = new List<Tuple<int, float>>()
            {
                new Tuple<int, float>(new DateTime(2016,3,1).DayOfYear,.2F),
                new Tuple<int, float>(new DateTime(2016,4,1).DayOfYear,1F),
                new Tuple<int, float>(new DateTime(2016,7,1).DayOfYear,1F),
                new Tuple<int, float>(new DateTime(2016,9,9).DayOfYear,.6F),
                new Tuple<int, float>(new DateTime(2016,9,16).DayOfYear,.5F),
                new Tuple<int, float>(new DateTime(2016,10,1).DayOfYear,.4F),
                new Tuple<int, float>(new DateTime(2016,11,1).DayOfYear,.2F)
            };
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

            int numIndices = SeasonalVerdance.Count;

            while (upperIndex < numIndices)
            {
                if (dayOfYear <= SeasonalVerdance[upperIndex].First)
                {
                    break;
                }
                upperIndex++;
            }

            float coefficient;

            if (upperIndex == 0)
            {
                coefficient = SeasonalVerdance[upperIndex].Second;
            } else if (upperIndex == numIndices)
            {
                coefficient = SeasonalVerdance[numIndices - 1].Second;
            } else
            {
                Tuple<int, float> upper = SeasonalVerdance[upperIndex];
                Tuple<int, float> lower = SeasonalVerdance[upperIndex - 1];
                coefficient = Mathf.Lerp(lower.Second, upper.Second, Mathf.InverseLerp(lower.First, upper.First, dayOfYear));
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
            return this.ThreadingManager.simulationTime;
            //int realHour = (int)this.ThreadingManager.simulationDayTimeHour;
            //int realMinute = (int)((this.ThreadingManager.simulationDayTimeHour - realHour) * 60);
            //return new DateTime(this.ThreadingManager.simulationTime.Year, this.ThreadingManager.simulationTime.Month, this.ThreadingManager.simulationTime.Day, realHour, realMinute, 0);
        }
    }
}
