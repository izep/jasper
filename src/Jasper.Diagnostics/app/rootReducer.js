import app from './appReducer'
import handlerChains from './HandlerChains/handlerChainsReducer'
import live from './Live/liveMessagesReducer'
import sent from './Live/sentMessagesReducer'
import subscriptionInfo from './Subscriptions/subscriptionsReducer'

export default {
  app,
  handlerChains,
  live,
  sent,
  subscriptionInfo
}
