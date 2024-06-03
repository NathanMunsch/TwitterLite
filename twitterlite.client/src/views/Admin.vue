<script setup>
import { computed, ref, onMounted } from 'vue';
import FlashMessage from '@/components/FlashMessage.vue'; 
import ConfirmationDialog from '@/components/dialogs/ConfirmationDialog.vue';
import store from '../store';


onMounted(() => {
  store.dispatch('admin/getUsers');
});

const users = computed(() => store.state.admin.users);
const showFlashMessage = computed(() => store.state.admin.showFlashMessage);
const confirmationDialogIsOpen = ref(false);
const confirmationUserId = ref(0);

const deleteUser = (userId) => {
  store.dispatch('admin/deleteUser', userId); 
};

function openConfirmationDialog(userId) {
    confirmationDialogIsOpen.value = true;
    confirmationUserId.value = userId
}
</script>

<template>
    <router-link to="/"><v-btn @click="" icon="mdi-arrow-left" style="position: fixed; top: 10px; left: 10px;"></v-btn></router-link>
    <p style="color: white; font-size: 20px; text-align:center;">Admin Page - Manage Users</p>
    <div v-for="user in users" :key="user.id">
        <v-card class="user">
            <v-avatar class="avatar">
                <v-img :src="'https://api.dicebear.com/8.x/pixel-art/svg?seed=' + user.id"></v-img>
            </v-avatar>
            <v-card-text class="username">{{ user.username }}</v-card-text>
            <v-tooltip text="Admin User">
                <template v-slot:activator="{ props }">
                    <v-icon class="admin" v-if="user.isAdmin" color="white" v-bind="props">mdi-crown-outline</v-icon>
                </template>
            </v-tooltip>
            <v-tooltip text="Delete User">
                <template v-slot:activator="{ props }">
                    <v-btn class="delete" @click="openConfirmationDialog(user.id)" icon="mdi-delete-outline" size="small" color="red" v-bind="props"></v-btn>
                </template>
            </v-tooltip>
        </v-card>
        <FlashMessage v-if="showFlashMessage" content="User Deleted Successfully."></FlashMessage>
    </div>
    <ConfirmationDialog :action="1" :userID="confirmationUserId" :confirmationDialogVisible="confirmationDialogIsOpen" @close="confirmationDialogIsOpen = false"></ConfirmationDialog>
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
    .admin {
        margin: 16px;
    }
    .delete{
        margin-right: 8px;
    }
</style>