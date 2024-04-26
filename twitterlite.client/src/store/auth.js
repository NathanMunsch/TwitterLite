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
            fetch('https://localhost:7078/auth/logout', {
            method: 'GET',
                credentials: 'include',
            }).then(response => {
                if (response.ok) {
                    commit('setAuthentication', false);
                    router.push('/login');
                }
            });
        },

        login({ commit }, credentials) {
            return new Promise((resolve, reject) => {
                fetch('https://localhost:7078/auth/login', {
                    method: 'POST',
                    credentials: 'include',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(credentials),
                }).then(response => {
                    if (response.ok) {
                        commit('setAuthentication', true);
                        resolve();
                    }
                    else {
                        reject('Failed to login');
                    }
                
                }).catch(error => {
                    reject(error);
                })
            });
        },

        register({ commit }, credentials) {
            commit('setAuthentication', false);
            return fetch('https://localhost:7078/user/create', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                credentials: 'include',
                body: JSON.stringify(credentials),
            }).then(response => {
                if (response.ok) {
                    return 'Account successfully created.';
                } else {
                    return response.json().then(data => {
                        return data.errorMessage || 'Failed to create account';
                    });
                }
            }).catch(error => {
                return 'Failed to create account';
            });
        }

    },
};
