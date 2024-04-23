<script setup>
import { computed, onMounted } from 'vue';
import { useStore } from 'vuex';
import FlashMessage from '@/components/FlashMessage.vue'; 

const store = useStore();

onMounted(() => {
  store.dispatch('admin/getUsers');
});

const users = computed(() => store.state.admin.users);
const showFlashMessage = computed(() => store.state.admin.showFlashMessage);

const deleteUser = (userId) => {
  store.dispatch('admin/deleteUser', userId); 
};
</script>

<template>
    <router-link to="/"><v-btn @click="" icon="mdi-arrow-left"></v-btn></router-link>
    <v-title class="title">Manage Users</v-title>
    <div v-for="user in users" :key="user.id">
        <v-card class="user">
            <v-avatar class="avatar">
                <v-img :src="'https://api.dicebear.com/8.x/pixel-art/svg?seed=' + user.id"></v-img>
            </v-avatar>
            <v-card-text class="username">{{ user.username }}</v-card-text>
            <v-icon class="admin" v-if="user.isAdmin" color="white">mdi-crown-outline</v-icon>
            <v-btn class="delete" @click="deleteUser(user.id)" icon="mdi-delete-outline" size="small" color="red"></v-btn>
        </v-card>
        <FlashMessage v-if="showFlashMessage" content="User Deleted Successfully."></FlashMessage>
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
    .admin {
        margin: 16px;
    }
    .delete{
        margin-right: 8px;
    }
</style>