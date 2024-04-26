export default {
  namespaced: true,
  state: {
    user: [],
  },
  mutations: {
    setCurrentUser(state, user) {
      state.user = user;
    },
  },
  actions: {
    async getCurrentUser({ commit }) {
        try {
            const response = await fetch(`https://localhost:7078/auth/user`, {
                method: 'GET',
                credentials: 'include',
            });
            
            if (!response.ok) {
                throw new Error(response.statusText);
            }
            const data = await response.json();
            commit('setCurrentUser', data.user);
        } catch (error) {
            console.error('Error fetching user:', error);
        }
    },
  },
};