using UnityEngine;
using ICities;
using System;

namespace DynamicFoliage
{
    public class DynamicFoliageThreading : ThreadingExtensionBase
    {
        public override void OnCreated(IThreading threading)
        {
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
                Debug.LogException(ex);
            }
        }

        public override void OnReleased()
        {
        }
    }
}
