using System.Linq;
using System.Threading.Tasks;
using Jasper.Bus.Model;
using Jasper.Bus.Runtime.Subscriptions;

namespace Jasper.Diagnostics.Messages
{
    public static class DiagnosticsHandler
    {
        public static InitialData Receive(RequestInitialData message, HandlerGraph graph)
        {
            var chains = graph.Chains.OrderBy(c => c.TypeName).Select(ChainModel.For);
            return new InitialData(chains);
        }

        public static async Task<BusSubscriptions> Receive(RequestBusSubscriptions message, ISubscriptionsRepository subscriptionsRepository)
        {
            var subs = await subscriptionsRepository.GetSubscriptions();
            return new BusSubscriptions(subs);
        }
    }
}
