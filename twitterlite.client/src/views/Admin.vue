<script setup>
    import { onMounted, ref } from "vue";

    var users = ref([]);
    const showFlashMessage = ref(false);

    async function getUsers() {
        try {
            const response = await fetch('https://localhost:7078/user/all', {
                method: 'GET',
                credentials: 'include'
            });
            if (!response.ok) {
                throw new Error('Réponse réseau non réussie');
            }
            const data = await response.json();
            users.value = data.users;
        } catch (error) {
            console.error("Erreur lors de la récupération des utilisateurs:", error);
        }
    }

    async function deleteUser(userId) {
        try {
            const response = await fetch('https://localhost:7078/admin/delete-user/' + userId, {
                method: 'DELETE',
                credentials: 'include'
            });
            if (!response.ok) {
                throw new Error('Réponse réseau non réussie');
            }
            else {
                users.value = users.value.filter(user => user.id !== userId);
                showFlashMessage.value = true;
            }
        } catch (error) {
            console.error("Erreur lors de la récupération des utilisateurs:", error);
        }
    }

    onMounted(() => {
        getUsers();
    })
</script>

<template>
    <v-title class="title">Manage Users</v-title>

    <div v-for="user in users" :key="user.id">
        <v-card class="user">
            <v-avatar>
                <v-img class="avatar" :src="'https://api.dicebear.com/8.x/pixel-art/svg?seed=' + user.id"></v-img>
            </v-avatar>
            <v-card-text>{{ user.username }}</v-card-text>
            <v-btn @click="deleteUser(user.id)" icon="mdi-delete-outline" size="small" color="red"></v-btn>
        </v-card>
        <FlashMessage v-if="showFlashMessage" content="User Deleted Successfully."></FlashMessage>
    </div>
</template>

<style scoped>
    .user {
        width: 20%;
        display: flex;
        align-items: center;
        justify-content: space-evenly;
        margin: auto;
        margin-top: 8px;
    }

    .title {
        margin-left: 20%;
        font-size: 20px;
    }
</style>