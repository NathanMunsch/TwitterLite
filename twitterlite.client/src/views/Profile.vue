<template>
    <v-tooltip text="Logout">
        <template v-slot:activator="{ props }">
            <v-btn style="position: fixed; top: 20px; right: 20px;" icon="mdi-exit-run" size="large" v-bind="props" @click="logout"></v-btn>
        </template>
    </v-tooltip>
</template>
<script setup>
    import { useRouter } from 'vue-router';

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
<style scoped>
</style>