// src/store/index.js
import { createStore } from 'vuex';
import admin from './admin';
import auth from './auth';

// Create the Vuex store with modules
const store = createStore({
  modules: {
    admin,
    auth,
    // any other modules you might have
  },
  // You can also include any global state, mutations, actions, getters, etc. here
});

export default store;