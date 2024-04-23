import { createStore } from 'vuex';

export default {
  namespaced: true,
  state: {
    users: [],
    showFlashMessage: false,
  },
  mutations: {
    setUsers(state, users) {
      state.users = users;
    },
    removeUser(state, userId) {
      state.users = state.users.filter(user => user.id !== userId);
    },
    setShowFlashMessage(state, value) {
      state.showFlashMessage = value;
    },
  },
  actions: {
    async getUsers({ commit }) {
      try {
        const response = await fetch('https://localhost:7078/admin/all-user', {
          method: 'GET',
          credentials: 'include',
        });
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const data = await response.json();
        commit('setUsers', data.users);
      } catch (error) {
        console.error('Error fetching users:', error);
      }
    },

    async deleteUser({ commit }, userId) {
      try {
        const response = await fetch(`https://localhost:7078/admin/delete-user/${userId}`, {
          method: 'DELETE',
          credentials: 'include',
        });
        if (!response.ok) {
          throw new Error('Network response was not ok');
        } else {
          commit('removeUser', userId);
          commit('setShowFlashMessage', true);

          setTimeout(() => {
            commit('setShowFlashMessage', false);
          }, 3000);
        }
      } catch (error) {
        console.error('Error deleting user:', error);
      }
    },
  },
};