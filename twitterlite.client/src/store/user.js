export default {
  namespaced: true,
  state: {
    user: {},
    selectedUser: {},
  },
  mutations: {
    setCurrentUser(state, user) {
      state.user = user;
    },
    setSelectedUser(state, user) { 
      state.selectedUser = user;
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
    async getUserById({ commit }, userId) {
        try {
            const response = await fetch(`https://localhost:7078/user/get/${userId}`, {
                method: 'GET',
                credentials: 'include'
            });
            if (!response.ok) {
                throw new Error(response.statusText);
            }
            const data = await response.json();
            commit('setSelectedUser', data.user);
        } catch (error) {
            console.error("Erreur lors de la récupération de l'utilisateur:", error);
        }
    },
  },
};
