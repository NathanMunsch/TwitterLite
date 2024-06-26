<template>
    <!-- verif si le user est admin ou si le tweet appartient au user -->
    <v-menu :location="bottom" rounded>
        <template v-slot:activator="{ props }">
            <v-avatar class="avatar">
                <v-img :src="'https://api.dicebear.com/8.x/pixel-art/svg?seed=' + id" v-bind="props"></v-img>
            </v-avatar>
        </template>
        <v-list>
            <v-list-item>
                <router-link to="/profile" class="router-link-custom">
                    <v-list-item-title>Profile</v-list-item-title>
                </router-link>
                <v-divider class="my-3" v-if="isAdmin"></v-divider>
                <router-link to="/admin" v-if="isAdmin" class="router-link-custom">
                    <v-list-item-title>Admin Page</v-list-item-title>
                </router-link>
                <v-divider class="my-3"></v-divider>
                <v-list-item-title style="color: red; cursor: pointer;" @click="logout">Logout</v-list-item-title>
            </v-list-item>
        </v-list>
    </v-menu>
    <v-tooltip text="Filter">
        <template v-slot:activator="{ props }">
            <v-btn style="position: fixed; bottom: 120px; right: 30px;" icon="mdi-filter-menu" size="large" v-bind="props" @click="openFilterDialog"></v-btn>
        </template>
    </v-tooltip>
    <v-tooltip text="Tweet">
        <template v-slot:activator="{ props }">
            <v-btn style="position: fixed; bottom: 50px; right: 30px;" icon="mdi-bird" size="large" v-bind="props" @click="openTweetDialog"></v-btn>
        </template>
    </v-tooltip>
    <TweetDialog :tweetDialogVisible="tweetDialogIsOpen" @update:tweetDialogVisible="tweetDialogIsOpen = $event"></TweetDialog>
    <FilterDialog :filterDialogVisible="filterDialogIsOpen" @update:filterDialogVisible="filterDialogIsOpen = $event" @update:filterMode="filterMode = $event"></FilterDialog>
    <div v-for="tweet in tweets" :key="tweet.id">
        <Tweet :authorID="tweet.authorId" :content="tweet.content" :tweetID="tweet.id" :createdAt="tweet.createdAt" :likeNumber="tweet.numberOfLikes" :isLoggedUserAdmin="isAdmin" :loggedUserID="id"></Tweet>
    </div>
    <p style="color: lightgrey; text-align:center; font-size:20px; margin-top: 250px;" v-if="tweets.length == 0">Nothing to see here...</p>
</template>

<script setup>
    import { onMounted, ref } from 'vue';
    import { useStore } from 'vuex';
    import Tweet from "../components/Tweet.vue";
    import TweetDialog from "../components/Dialogs/TweetDialog.vue";
    import FilterDialog from "../components/Dialogs/FilterDialog.vue";

    const id = ref('');
    const tweetDialogIsOpen = ref(false);
    const filterDialogIsOpen = ref(false);
    const isAdmin = ref(false);
    const store = useStore();
    var tweets = ref([]);
    var filterMode = ref('newest');

    function logout() {
        store.dispatch('auth/logout')
            .then(() => {
            })
            .catch((error) => {
                console.error('Logout failed:', error);
            });
    }

    async function getUserID() {
        try {
            const response = await fetch('https://localhost:7078/auth/user', {
                method: 'GET',
                credentials: 'include'
            });
            if (!response.ok) {
                throw new Error('R�ponse r�seau non r�ussie');
            }
            const data = await response.json();
            id.value = data.user.id;
            isAdmin.value = data.user.isAdmin;
            console.log(isAdmin.value)
        } catch (error) {
            console.error("Erreur lors de la r�cup�ration des utilisateurs:", error);
        }
    }

    function openTweetDialog() {
        tweetDialogIsOpen.value = true;
    }

    function openFilterDialog() {
        filterDialogIsOpen.value = true;
    }

    async function getTweetsNewest() {
        try {
            const response = await fetch('https://localhost:7078/tweet/newest', {
                method: 'GET',
                credentials: 'include'
            });
            if (!response.ok) {
                throw new Error('R�ponse r�seau non r�ussie');
            }
            const data = await response.json();
            tweets.value = data.tweets;
        } catch (error) {
            console.error("Erreur lors de la r�cup�ration des utilisateurs:", error);
        }
    }

    async function getTweetsOldest() {
        try {
            const response = await fetch('https://localhost:7078/tweet/oldest', {
                method: 'GET',
                credentials: 'include'
            });
            if (!response.ok) {
                throw new Error('R�ponse r�seau non r�ussie');
            }
            const data = await response.json();
            tweets.value = data.tweets;
        } catch (error) {
            console.error("Erreur lors de la r�cup�ration des utilisateurs:", error);
        }
    }

    async function getTweetsMostLiked() {
        try {
            const response = await fetch('https://localhost:7078/tweet/most-liked', {
                method: 'GET',
                credentials: 'include'
            });
            if (!response.ok) {
                throw new Error('R�ponse r�seau non r�ussie');
            }
            const data = await response.json();
            tweets.value = data.tweets;
        } catch (error) {
            console.error("Erreur lors de la r�cup�ration des utilisateurs:", error);
        }
    }

    async function getAllTweets(filterMode) {
        if (filterMode == "newest") {
            await getTweetsNewest();
        } else if (filterMode == "oldest") {
            await getTweetsOldest();
        } else if (filterMode == "mostLiked") {
            await getTweetsMostLiked();
        }
    }

    onMounted(() => {
        getUserID();
        getAllTweets(filterMode.value);
    });

    // retrieve tweets every 5 seconds
    setInterval(() => {
        getUserID();
        getAllTweets(filterMode.value);
    }, 5000)
</script>

<style scoped>
    .avatar {
        position: fixed;
        top: 10px;
        right: 10px;
        margin: 10px;
        cursor: pointer;
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

    .router-link-custom {
        text-decoration: none;
        color: inherit;
    }
</style>
