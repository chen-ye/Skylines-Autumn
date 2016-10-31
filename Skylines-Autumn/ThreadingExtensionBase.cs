using UnityEngine;
using ICities;
using System;
using FoliageChirpLogger;

namespace DynamicFoliage
{
    public class DynamicFoliageThreading : ThreadingExtensionBase
    {
        public override void OnCreated(IThreading threading)
        {
            ChirpLog.Debug("hi");

            DynamicFoliageEngine.Instance.ThreadingManager = threading;
        }

        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            try
            {
                DynamicFoliageEngine.Instance.UpdateFoliage();
            }
            catch (Exception ex)
            {
                //TODO Log this 
                ChirpLog.Error(ex.Message);
            }
        }

        public override void OnReleased()
        {
        }
    }
}
