    import { createStore } from 'vuex';
    import router from '../router'; 

    //import router from './router'; // Assurez-vous que le chemin d'importation est correct

    export default {
        namespaced: true, // Utilisez les espaces de noms pour accéder à ce module sous forme de 'auth/actionName'
    
        state: {
        // Vous pouvez ajouter un état ici, comme le statut de connexion, l'utilisateur actuel, etc.
            isAuthenticated: false,
        },
    
        mutations: {
            // Définissez des mutations pour modifier l'état
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
                  commit('setAuthentication', false); // Met à jour l'état d'authentification
                  router.push('/login'); // Redirige vers la page de login
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