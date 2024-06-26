<template>
    <v-card class="Tweet">
        <div class="User">
            <v-avatar class="Avatar">
                <v-img :src="'https://api.dicebear.com/8.x/pixel-art/svg?seed=' + props.authorID"></v-img>
            </v-avatar>
            <v-card-text class="Username">{{ username }}</v-card-text>
            <v-card-text class="TweetedAt">{{ formattedDate }}</v-card-text>
            <v-menu :location="bottom" rounded v-if="props.isLoggedUserAdmin || props.loggedUserID == props.authorID">
                <template v-slot:activator="{ props }">
                    <v-icon v-bind="props" color="grey" style="margin-right: 10px;">mdi-dots-vertical</v-icon>
                </template>
                <v-list>
                    <v-list-item>
                        <v-list-item-title v-if="props.isLoggedUserAdmin" style="cursor: pointer;" @click="deleteTweet(props.tweetID)">Delete(Admin)</v-list-item-title>
                        <v-list-item-title v-else-if="props.loggedUserID == props.authorID" style="cursor: pointer;" @click="deleteOwnTweet(props.tweetID)">Delete</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-menu>
        </div>
        <v-card-text class="tweetContent">{{ props.content }}</v-card-text>
            <div class="like">
                <v-icon v-if="liked" @click="unlikeTweet(props.tweetID)" color="red">mdi-heart</v-icon>
                <v-icon v-else color="grey" @click="likeTweet(props.tweetID)">mdi-heart-outline</v-icon>
                <p v-if="props.likeNumber > 0" style="color: white;">{{ props.likeNumber }}</p>
            </div>
    </v-card>
    <FlashMessage v-if="showFlashMessageSuccess" content="Tweet deleted successfully."></FlashMessage> 
    <FlashMessage v-if="showFlashMessageError" content="An error occurred."></FlashMessage>
</template>
<script setup>
    import { ref, onMounted } from 'vue';
    import FlashMessage from './FlashMessage.vue';

    const props = defineProps({
        authorID: Number,
        content: String,
        tweetID: Number,
        createdAt: String,
        likeNumber: Number,
        author: Object,
        isLoggedUserAdmin: Boolean,
        loggedUserID: Number
    });

    var date = props.createdAt.substring(0, 19);  
    var formattedDate = date.replace("T", " ");
    const username = ref('');
    const isAdmin = ref(false);
    const liked = ref(false);
    const showFlashMessageSuccess = ref(false);
    const showFlashMessageError = ref(false);

    function deleteTweet(tweetID) {
        showFlashMessageSuccess.value = false;
        showFlashMessageError.value = false;
        fetch('https://localhost:7078/admin/delete-tweet/' + tweetID, {
            method: 'DELETE',
            credentials: 'include',
        }).then(response => {
            if (response.ok) {
                showFlashMessageSuccess.value = true;
            }
            else {
                showFlashMessageError.value = true;
            }
        });
    }

    function deleteOwnTweet(tweetID) {
        showFlashMessageSuccess.value = false;
        showFlashMessageError.value = false;
        fetch('https://localhost:7078/tweet/delete/' + tweetID, {
            method: 'DELETE',
            credentials: 'include',
        }).then(response => {
            if (response.ok) {
                showFlashMessageSuccess.value = true;
            }
            else {
                showFlashMessageError.value = true;
            }
        });
    }

    async function getUser() {
        try {
            const response = await fetch('https://localhost:7078/user/get/' + props.authorID, {
                method: 'GET',
                credentials: 'include'
            });
            if (!response.ok) {
                throw new Error('R�ponse r�seau non r�ussie');
            }
            const data = await response.json();
            username.value = data.user.username;
        } catch (error) {
            console.error("Erreur lors de la r�cup�ration des utilisateurs:", error);
        }
    }

    function likeTweet(tweetID) {
        fetch('https://localhost:7078/tweet/like/create/' + tweetID, {
            method: 'GET',
            credentials: 'include',
        }).then(response => {
            if (response.ok) {
                liked.value = true;
            }
            else {
                console.log('Erreur lors de la tentative de like du tweet')
            }
        });
    }

    function unlikeTweet(tweetID) {
        console.log("u2")
        fetch('https://localhost:7078/tweet/like/delete/' + tweetID, {
            method: 'DELETE',
            credentials: 'include',
        }).then(response => {
            if (response.ok) {
                liked.value = false;
            } else {
                console.log('Erreur lors de la tentative de suppression de like du tweet')
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
        width: 90%;
        max-width: 600px;
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
    .like {
        display: flex;
        justify-content: center;
        margin-bottom: 4px;
    }
</style>
