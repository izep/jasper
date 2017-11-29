const initialState = {
  subscriptions: []
}

export default (state = initialState, action = {}) => {
  switch(action.type) {
    case 'bus-subscriptions':
      return {
        subscriptions: action.subscriptions
      }
    default: return state
  }
}
