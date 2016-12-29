using System.Collections.Generic;
using System.Reflection;
using ICities;
using DynamicFoliage.Redirection;
using UnityEngine;

namespace DynamicFoliage
{
    public class LoadingExtension : LoadingExtensionBase
    {

        private static Dictionary<MethodInfo, RedirectCallsState> _redirects;

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
            _redirects = RedirectionUtil.RedirectAssembly();
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            new GameObject("DynamicFoliage").AddComponent<InfoViewMonitor>();

            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
                return;

            if (!DynamicFoliageEngine.Instance.IsInitialized)
                DynamicFoliageEngine.Instance.Initialize();
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            NaturalResourcesManagerDetour.m_modifiedX1 = null;
            NaturalResourcesManagerDetour.m_modifiedX2 = null;
            NaturalResourcesManagerDetour.m_modifiedBX1 = null;
            NaturalResourcesManagerDetour.m_modifiedBX2 = null;
            if (NaturalResourcesManagerDetour.infoViewTexture != null)
            {
                Object.Destroy(NaturalResourcesManagerDetour.infoViewTexture);
            }
            NaturalResourcesManagerDetour.infoViewTexture = null;
            if (NaturalResourcesManagerDetour.infoViewTextureB != null)
            {
                Object.Destroy(NaturalResourcesManagerDetour.infoViewTextureB);
            }
            NaturalResourcesManagerDetour.infoViewTextureB = null;
            var go = GameObject.Find("DynamicFoliage");
            if (go != null)
            {
                Object.Destroy(go);
            }
        }

        public override void OnReleased()
        {
            base.OnReleased();
            RedirectionUtil.RevertRedirects(_redirects);
        }
    }
}