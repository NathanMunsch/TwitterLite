<script setup>
    import { useRouter } from 'vue-router';
    import TweetDialog  from "../components/TweetDialog.vue";

    const router = useRouter();

    function logout() {
        fetch('https://localhost:7078/auth/logout', {
            method: 'GET',
            credentials: 'include'
        }).then(response => {
            if (response.ok) {
                router.push('/login')
            }
        })
    }
</script>

<template>
    <router-link to="/login">login</router-link>
    <br />
    <router-link to="/register">register</router-link>
    <br />
    <router-link to="/admin">admin</router-link>
    <br />
    <button @click="logout">Logout</button>
    <TweetDialog></TweetDialog>
</template>

<style scoped>
</style>
