import { createStore } from 'vuex';
import admin from './admin';
import auth from './auth';

const store = createStore({
  modules: {
    admin,
    auth,
  },
});

export default store;