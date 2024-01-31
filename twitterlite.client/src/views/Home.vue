<script setup>
    import { computed } from "vue";
    import { useStore } from "vuex";
    import TweetDialog  from "../components/TweetDialog.vue";

    const store = useStore()
    const auth = computed(() => store.state.authenticated)

    function logout() {
        fetch('https://localhost:7078/auth/logout', {
            method: 'GET',
            credentials: 'include'
        }).then(response => {
            if (response.ok) {
                store.dispatch('setAuth', false)
            }
        })
    }
</script>

<template>
    <router-link v-if="!auth" to="/login">login</router-link>
    <br />
    <router-link v-if="!auth" to="/register">register</router-link>
    <br />
    <button v-if="auth" @click="logout">Logout</button>
    <TweetDialog></TweetDialog>
</template>

<style scoped>
</style>
