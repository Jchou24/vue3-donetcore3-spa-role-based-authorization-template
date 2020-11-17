import { createStore } from 'vuex'

import createMultiTabState from 'vuex-multi-tab-state'

import authentication from './authentication'
import pageIdle from './pageIdle'

export default createStore({
  state: {
  },
  mutations: {
  },
  actions: {
  },
  modules: {
    authentication,
    pageIdle,
  },
  plugins: [
    createMultiTabState(),
  ],
})
