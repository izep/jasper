using System.Collections;
using Jasper.Bus.Runtime.Subscriptions;

namespace Jasper.Diagnostics.Messages
{
    public class BusSubscriptions : ClientMessage
    {
        public Subscription[] Subscriptions { get; }

        public BusSubscriptions(Subscription[] subscriptions)
            : base("bus-subscriptions")
        {
            Subscriptions = subscriptions;
        }
    }
}
