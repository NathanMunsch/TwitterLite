<template>
    <v-dialog v-model="dialogIsOpen" class="tweetDialog" persistent>
        <div class="user">
            <router-link to="/profile">
                <v-avatar class="avatar">
                    <v-img :src="'https://api.dicebear.com/8.x/pixel-art/svg?seed=' + id"></v-img>
                </v-avatar>
            </router-link>
            <p style="color:lightgrey; font-weight:bold;">{{ username }}</p>
        </div>
        <v-card style="background-color: rgb(21,32,43);">
            <v-text-field v-model="tweetText" class="tweet-text-field" style="margin: auto; margin-top: 20px; width: 80%; color:lightgrey;" placeholder="What's Up?!" label="Tweet" clearable counter></v-text-field>
            <v-card-actions>
                <v-btn text="Close" @click="closeDialog" color="success"></v-btn>
                <v-btn text="Send" @click="addTweet" color="success" class="ml-auto"></v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
    <FlashMessage v-if="showFlashMessageSuccess" content="Tweet sent successfully."></FlashMessage>
    <FlashMessage v-if="showFlashMessageError" content="An error occurred."></FlashMessage>
</template>
<script setup>
    import { ref, watchEffect, onMounted } from 'vue';
    import { useStore } from 'vuex';
    import FlashMessage from '../FlashMessage.vue';

    const id = ref('');
    const username = ref('');
    const store = useStore();
    const emit = defineEmits(['update:tweetDialogVisible']);
    const props = defineProps({
        tweetDialogVisible: Boolean
    });
    const dialogIsOpen = ref(false);
    const tweetText = ref('');
    const showFlashMessageSuccess = ref(false);
    const showFlashMessageError = ref(false);

    const closeDialog = () => {
        dialogIsOpen.value = false;
        emit('update:tweetDialogVisible', false);
    }

    async function getUserID() {
        try {
            const response = await fetch('https://localhost:7078/auth/user', {
                method: 'GET',
                credentials: 'include'
            });
            if (!response.ok) {
                throw new Error('Réponse réseau non réussie');
            }
            const data = await response.json();
            id.value = data.user.id;
            username.value = data.user.username;
        } catch (error) {
            console.error("Erreur lors de la récupération des utilisateurs:", error);
        }
    }

    function addTweet() {
        showFlashMessageSuccess.value = false;
        showFlashMessageError.value = false;
        fetch('https://localhost:7078/Tweet/Create', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            body: JSON.stringify({
                'content': tweetText.value
            })
        }).then(response => {
            if (response.ok) {
                showFlashMessageSuccess.value = true;
                closeDialog();
            } else {
                showFlashMessageError.value = true;
            }
        });
    }

    onMounted(() => {
        getUserID();
    });

    watchEffect(() => {
        dialogIsOpen.value = props.tweetDialogVisible;
    });
</script>
<style scoped>
    .tweetDialog {
        width: 600px;
    }
    .tweet-text-field /deep/ .v-counter {
        color: white !important;
    }
    .user {
        display: flex; 
        align-items: center; 
    }
    .avatar {
        margin-right: 10px;
        margin-bottom: 10px;
    }
</style>