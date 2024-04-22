<script setup>
    import { useStore } from 'vuex';
  
    const store = useStore();
  
    function logout() {
        store.dispatch('auth/logout') // Remplacez 'yourModuleName' par le nom de votre module Vuex si nécessaire
        .then(() => {
            // Traitement en cas de succès, si nécessaire
        })
        .catch((error) => {
            console.error('Logout failed:', error); // Gestion des erreurs
        });
    }
</script>

<template>
    <v-tooltip text="Logout">
        <template v-slot:activator="{ props }">
            <v-btn style="position: fixed; top: 20px; right: 20px;" icon="mdi-exit-run" size="large" v-bind="props" @click="logout" color="red"></v-btn>
        </template>
    </v-tooltip>
</template>
  
<style scoped>
</style>