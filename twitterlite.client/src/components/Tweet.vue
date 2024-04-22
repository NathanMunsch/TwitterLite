<template>
    <v-card class="Tweet">
        <div class="User">
            <v-avatar class="Avatar">
                <v-img :src="'https://api.dicebear.com/8.x/pixel-art/svg?seed=' + props.authorID"></v-img>
            </v-avatar>
            <v-card-text class="Username">{{ username }}</v-card-text>
            <v-card-text class="TweetedAt">{{ formattedDate }}</v-card-text>
        </div>
        <v-card-text class="tweetContent">{{ props.content }}</v-card-text>
        <div class="Interaction">
            <v-icon color="grey" class="mr-auto" style="margin: 8px;">mdi-comment</v-icon>
            <v-icon v-if="liked" color="red">mdi-heart</v-icon>
            <v-icon v-else color="grey" @click="likeTweet(props.tweetID)">mdi-heart-outline</v-icon>
            <p class="likeCount">{{ props.likeNumber }}</p>
            <v-icon v-if="isAdmin" color="blue" class="ml-auto" style="margin: 8px;">mdi-delete</v-icon>
        </div>
    </v-card>
</template>
<script setup>
    import { ref, onMounted } from 'vue';

    const props = defineProps({
        authorID: Number,
        content: String,
        tweetID: Number,
        createdAt: String,
        likeNumber: Number,
        author: Object
    });

    var date = props.createdAt.substring(0, 19);
    var formattedDate = date.replace("T", " ");
    const username = ref('');
    const isAdmin = ref(false);
    const liked = ref(false);

    async function getUser() {
        try {
            const response = await fetch('https://localhost:7078/user/get/' + props.authorID, {
                method: 'GET',
                credentials: 'include'
            });
            if (!response.ok) {
                throw new Error('Réponse réseau non réussie');
            }
            const data = await response.json();
            username.value = data.user.username;
            isAdmin.value = data.user.isAdmin;
            console.log(isAdmin.value)
        } catch (error) {
            console.error("Erreur lors de la récupération des utilisateurs:", error);
        }
    }

    function likeTweet(tweetID) {
        console.log("u2")
        fetch('https://localhost:7078/tweet/like/create/' + tweetID, {
            method: 'GET',
            credentials: 'include',
        }).then(response => {
            if (!response.ok) {
                console.log('Erreur lors de la tentative de like du tweet')
            }
        });
    }

    async function hasUserLiked(tweetID) {
        fetch('https://localhost:7078/user/has-liked/' + tweetID, {
            method: 'GET',
            credentials: 'include'
        }).then(response => {
            if (response.ok) {
                liked.value = true;
            } else {
                liked.value = false;
            }
        });
    }

    onMounted(() => {
        getUser();
        hasUserLiked(props.tweetID);
        console.log(liked.value)
    });
</script>
<style scoped>
    .User {
        display: flex;
        align-items: center;
    }
    .Avatar {
        margin: 8px 0 0 8px;
    }
    .Username{
        font-weight: bold;
        color: white;
    }
    .Tweet {
        width: 40%;
        display: flex;
        flex-direction: column;
        margin: auto;
        margin-top: 30px;
        background-color: rgb(21,32,43);
        border: 1px solid;
        border-color: rgb(83, 100, 113);
    }
    .TweetedAt {
        color: lightgrey;
        text-align: end;
    }
    .tweetContent{
        color: white;
    }
    .Interaction {
        display: flex;
        align-items: center;
        margin-bottom: 8px;
    }
    .likeCount{
        color: white;
    }
</style>
