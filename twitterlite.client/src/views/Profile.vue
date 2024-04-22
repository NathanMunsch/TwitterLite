<script setup>
    import { useStore } from 'vuex';
    import { computed, onMounted } from 'vue';

    const store = useStore();
  
    const user = computed(() => store.state.user.user);
    onMounted(() => {
        store.dispatch('user/getCurrentUser'); 
    });

    function logout() {
        store.dispatch('auth/logout')
        .then(() => {
        })
        .catch((error) => {
            console.error('Logout failed:', error);
        });
    }
</script>

<template>
    <v-tooltip text="Logout">
        <template v-slot:activator="{ props }">
            <v-btn style="position: fixed; top: 20px; right: 20px;" icon="mdi-exit-run" size="large" v-bind="props" @click="logout" color="red"></v-btn>
        </template>
    </v-tooltip>

    <div>
        <v-card class="user">
            <v-avatar class="avatar">
                <v-img :src="`https://api.dicebear.com/8.x/pixel-art/svg?seed=${user.id}`"></v-img>
            </v-avatar>
            <v-card-text class="username">{{ user.username }}</v-card-text>
        </v-card>
    </div>
</template>
  
<style scoped>
.user {
        width: 30%;
        display: flex;
        align-items: center;
        justify-content: space-evenly;
        margin: auto;
        margin-top: 8px;
        background-color: rgb(21,32,43);
        border: 1px solid;
        border-color: rgb(83, 100, 113);
    }
    .username {
        font-weight: bold;
        color: white;
    }
    .avatar{
        margin-left: 4px;
    }
    .title {
        font-size: 20px;
        margin: 5%;
        color: white;
    }
</style>