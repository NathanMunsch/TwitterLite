    import { createStore } from 'vuex';
    import router from '../router'; 

    export default {
        namespaced: true,
    
        state: {
            isAuthenticated: false,
        },
    
        mutations: {
            setAuthentication(state, status) {
                state.isAuthenticated = status;
            },
        },
    
        actions: {
          logout({ commit }) {
            return new Promise((resolve, reject) => {
              fetch('https://localhost:7078/auth/logout', {
                method: 'GET',
                credentials: 'include'
              }).then(response => {
                if (response.ok) {
                  commit('setAuthentication', false);
                  router.push('/login');
                  resolve();
                } else {
                  reject('Failed to logout');
                }
              }).catch(error => {
                reject(error);
              });
            });
          },
        },
    };