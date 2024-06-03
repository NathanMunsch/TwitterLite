<template>
    <router-link to="/"><v-btn icon="mdi-arrow-left"></v-btn></router-link>
    
    <div class="user">
        <v-container class="fill-height">
            <v-row>
                <v-col justify="center" align="center">
                    <v-avatar class="avatar" size="70">
                        <v-img :src="`https://api.dicebear.com/8.x/pixel-art/svg?seed=${user.id}`"></v-img>
                    </v-avatar>
                    <h3 class="username">{{ user.username }}</h3>
                </v-col>
            </v-row>
        </v-container>
    </div>
    
    <div class="biographie">
        <v-container class="fill-height">
            <v-row class="biographie-row">
                <v-col cols="12" sm="4">
                    <v-text-field 
                    v-model="newUsername"
                    type="input" 
                    label="Username" 
                    bg-color="#313B45" 
                    color="primary"
                    hint="Enter your new username"
                    >
                    </v-text-field>
                </v-col>
                <v-col cols="12" sm="4" class="grey-background">
                    <v-text-field
                        v-model="newPassword"
                        type="password"
                        label="Password" 
                        bg-color="#313B45" 
                        color="primary"
                        hide-details="auto"
                        hint="Enter your new password"
                    >
                    </v-text-field>
                </v-col>
                <v-col cols="12" sm="4">
                    <v-btn variant="tonal" color="white" @click="openPasswordPrompt">
                        Edit Profile
                    </v-btn>
                </v-col>
            </v-row>
        </v-container>
        <h3 class="title">My tweets :</h3>
    </div>

    <div v-for="tweet in tweets" :key="tweet.id">
        <Tweet :authorID="tweet.authorId" :content="tweet.content" :tweetID="tweet.id" :createdAt="tweet.createdAt" :likeNumber="tweet.numberOfLikes" :isLoggedUserAdmin="user.isAdmin" :loggedUserID="user.id"></Tweet>
    </div>

    <PasswordPromptDialog
        v-model="passwordPromptDialogVisible"
        :userId="String(user.id)"
        :username="user.username"
        @close="passwordPromptDialogVisible = false"
        @confirm="updateProfile"
    />
</template>
  
<script setup>
    import store from '../store';
    import { computed, onMounted, ref, watch } from 'vue';
    import Tweet from "../components/Tweet.vue";
    import PasswordPromptDialog from "../components/Dialogs/PasswordPromptDialog.vue";

    const user = computed(() => store.state.user.user);
    const tweets = computed(() => store.state.tweet.tweets);

    const newUsername = ref('');
    const newPassword = ref('');
    const passwordPromptDialogVisible = ref(false);

    onMounted(() => {
        store.dispatch('user/getCurrentUser'); 
    });

    watch(user, (newValue) => {
        if (newValue && newValue.id) {
            store.dispatch('tweet/getUserTweets', newValue.id);
        }
    }, { immediate: true });

    function openPasswordPrompt() {
        passwordPromptDialogVisible.value = true;
    }

    async function updateProfile(oldPassword) {
        const updateData = {
            newUsername: newUsername.value || null,
            newPassword: newPassword.value || null,
            oldPassword: oldPassword
        };

        try {
            const response = await store.dispatch('auth/update', updateData);
            newUsername.value = '';
            newPassword.value = '';
        } catch (error) {
            console.error(error);
        }
    }
</script>

<style scoped>
    .user {
        display: flex;
        align-items: center;
    }
    .username {
        font-weight: bold;
        color: white;
    }
    .avatar{
        display: flex;
        justify-content: center;
        border: 1px solid;
    }
    .title {
        font-size: 17px;
        display: flex;
        justify-content: left;
        color: white;
        margin-left: 18px;
    }
    .biographie {
        width: 40%;
        display: flex;
        flex-direction: column;
        margin: auto;
    }
    .biographie-row .v-col {
        padding: 0 10px;
    }
    .grey-background .v-input__control .v-field__input {
        background-color: #757575; 
        color: white;
    }
    @media screen and (max-width: 920px) {
        .biographie {
            width: 100%;
        }
        .biographie-row .v-col {
            flex: 1;
            padding: 0 5px;
        }
    }

</style>