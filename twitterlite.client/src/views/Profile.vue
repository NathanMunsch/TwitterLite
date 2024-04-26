<template>
    <router-link to="/"><v-btn @click="" icon="mdi-arrow-left"></v-btn></router-link>
    
    <div class="user">
        <v-container class="fill-height">
            <v-row>
                <v-col justify="center" align="center">
                    <v-avatar class="avatar" size="70">
                        <v-img :src="`https://api.dicebear.com/8.x/pixel-art/svg?seed=${user.id}`"></v-img>
                    </v-avatar>
                    <h3 class="username">{{ user.username }}</h3>
                    
                    <v-textarea class="biographie" label="Ma bio :"></v-textarea>
                </v-col>
            </v-row>
        </v-container>
    </div>
    <div v-for="tweet in tweets" :key="tweet.id">
        <Tweet :authorID="tweet.authorId" :content="tweet.content" :tweetID="tweet.id" :createdAt="tweet.createdAt" :likeNumber="tweet.numberOfLikes" :isLoggedUserAdmin="user.isAdmin" :loggedUserID="user.id"></Tweet>
    </div>
</template>
  
<script setup>
    import store from '../store';
    import { computed, onMounted, ref, watch } from 'vue';
    import Tweet from "../components/Tweet.vue";
    import TweetDialog from "../components/Dialogs/TweetDialog.vue";

    const user = computed(() => store.state.user.user);
    const tweets = computed(() => store.state.tweet.tweets);

    onMounted(() => {
        store.dispatch('user/getCurrentUser'); 
    });

    watch(user, (newValue) => {
        if (newValue && newValue.id) {
            store.dispatch('tweet/getUserTweets', newValue.id);
        }
    }, { immediate: true });

    function logout() {
        store.dispatch('auth/logout')
        .then(() => {
        })
        .catch((error) => {
            console.error('Logout failed:', error);
        });
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
        font-size: 20px;
        margin: 5%;
        color: white;
    }
    .biographie {
        width: 30%;
        background-color: rgb(21,32,43);
    }
</style>