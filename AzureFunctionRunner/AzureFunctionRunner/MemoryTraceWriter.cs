using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Host;

namespace AzureFunctionRunner
{
    /// <summary>
    /// A <see cref="TraceWriter"/> which writes events to an in-memory list.
    /// </summary>
    public class MemoryTraceWriter : TraceWriter
    {
        public List<TraceEvent> Events { get; private set; } = new List<TraceEvent>();

        public MemoryTraceWriter() : base(TraceLevel.Verbose) { }
        public MemoryTraceWriter(TraceLevel level) : base(level) { }

        public override void Trace(TraceEvent traceEvent)
        {
            if (traceEvent.Level <= Level) Events.Add(traceEvent);
        }
    }
}
