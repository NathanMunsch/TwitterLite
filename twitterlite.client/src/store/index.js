import { createStore } from 'vuex';
import admin from './admin';
import auth from './auth';
import user from './user';
import tweet from './tweet';

const store = createStore({
  modules: {
    admin,
    auth,
    user,
    tweet,
  },
});

export default store;