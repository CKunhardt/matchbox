using System.Collections.Generic;
using Urho;

namespace Matchbox.Components
{
    public class MBDot : Node
    {
        public List<MBLine> edges { get; private set; }

        public void AddEdge(MBLine edge)
        {
            edges.Add(edge);
        }

    }
}
