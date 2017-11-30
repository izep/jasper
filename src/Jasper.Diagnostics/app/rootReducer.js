import app from './appReducer'
import capabilities from './Capabilities/capabilitiesReducer'
import live from './Live/liveMessagesReducer'
import sent from './Live/sentMessagesReducer'
import subscriptionInfo from './Subscriptions/subscriptionsReducer'

export default {
  app,
  capabilities,
  live,
  sent,
  subscriptionInfo
}
