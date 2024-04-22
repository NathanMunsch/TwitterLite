<template>
    <router-link to="/profile">
        <v-avatar class="avatar">
            <v-img :src="'https://api.dicebear.com/8.x/pixel-art/svg?seed=' + id"></v-img>
        </v-avatar>
    </router-link>
    <router-link to="/admin">admin</router-link>
    <v-tooltip text="Filter">
        <template v-slot:activator="{ props }">
            <v-btn style="position: fixed; bottom: 120px; right: 30px;" icon="mdi-filter-menu" size="large" v-bind="props"></v-btn>
        </template>
    </v-tooltip>
    <v-tooltip text="Tweet">
        <template v-slot:activator="{ props }">
            <v-btn style="position: fixed; bottom: 50px; right: 30px;" icon="mdi-bird" size="large" v-bind="props" @click="openTweetDialog"></v-btn>
        </template>
    </v-tooltip>
    <TweetDialog :tweetDialogVisible="tweetDialogIsOpen" @update:tweetDialogVisible="tweetDialogIsOpen = $event"></TweetDialog>
    <div v-for="tweet in tweets" :key="tweet.id">
        <Tweet :authorID="tweet.authorId" :content="tweet.content" :tweetID="tweet.id" :createdAt="tweet.createdAt" :likeNumber="tweet.numberOfLikes"></Tweet>
    </div>
</template>

<script setup>
    import { onMounted, ref } from 'vue';
    import Tweet from "../components/Tweet.vue";
    import TweetDialog from "../components/Dialogs/TweetDialog.vue";

    const id = ref('');
    const tweetDialogIsOpen = ref(false);
    var tweets = ref([]);

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
        } catch (error) {
            console.error("Erreur lors de la récupération des utilisateurs:", error);
        }
    }

    function openTweetDialog() {
        tweetDialogIsOpen.value = true;
    }

    async function getAllTweets() {
        try {
            const response = await fetch('https://localhost:7078/tweet/newest', {
                method: 'GET',
                credentials: 'include'
            });
            if (!response.ok) {
                throw new Error('Réponse réseau non réussie');
            }
            const data = await response.json();
            tweets.value = data.tweets;
        } catch (error) {
            console.error("Erreur lors de la récupération des utilisateurs:", error);
        }
    }

   onMounted(() => {
        getUserID();
        getAllTweets();
    });

    // retrieve tweets every 5 seconds
    setInterval(() => {
        getUserID();
        getAllTweets();
    }, 5000)
</script>

<style scoped>
    .avatar {
        position: absolute;
        top: 0;
        right: 0;
        margin: 10px;
    }
    .tweet {
        width: 40%;
        margin: auto;
        margin-top: 8px;
    }
    .filters {
        color: lightgrey;
        display: flex;
        align-items: center;
        margin: auto;
        width: 50px;
    }
</style>
