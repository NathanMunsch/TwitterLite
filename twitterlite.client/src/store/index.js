import { createStore } from 'vuex';
import admin from './admin';
import auth from './auth';
import user from './user';

const store = createStore({
  modules: {
    admin,
    auth,
    user,
  },
});

export default store;