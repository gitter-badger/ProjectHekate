﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectHekate.Core.Interfaces;

namespace ProjectHekate.Core
{
    public delegate IEnumerator<WaitInFrames> EmitterUpdateDelegate(Emitter emitter, IEngine engine);

    /// <summary>
    /// Emitters are objects that fire off bullets. They are attached to a controller (you should not have a dangling emitter) and their positions are offset from the controller's position.
    /// </summary>
    public interface IEmitter : IPositionable
    {
        float OffsetX { get; set; }
        float OffsetY { get; set; }
        bool IsEnabled { get; set; }
        int FramesAlive { get; }

        // orbit-specific stuff
        bool Orbiting { get; }
        float OrbitDistance { get; set; }
    }

    public class Emitter : AbstractScriptedObject<EmitterUpdateDelegate>, IEmitter
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float OffsetX { get; set; }
        public float OffsetY { get; set; }
        public float Angle { get; set; }
        public bool IsEnabled { get; set; }
        public int FramesAlive { get; internal set; }

        // orbit-specific stuff
        public bool Orbiting { get; internal set; }
        public float OrbitDistance { get; set; }


        internal readonly List<Emitter> Emitters = new List<Emitter>();

        internal Emitter()
        {}

        internal IEnumerator<WaitInFrames> Update(IEngine engine)
        {
            return UpdateFunc != null ? UpdateFunc(this, engine) : null;
        }
    }
}
