<script setup>
    import { useStore } from "vuex";

    const store = useStore();

    setInterval(function () {
        fetch('https://localhost:7078/auth/user', {
            method: 'GET',
            credentials: 'include'
        }).then(response => {
            if (response.ok) {
                store.dispatch('setAuth', true);
            } else {
                store.dispatch('setAuth', false);
            }
        });
    }, 10000);
</script>

<template>
    <div id="app">
        <router-view></router-view>
    </div>
</template>
