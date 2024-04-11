<script setup>
    import { ref } from 'vue';
</script>
<template>
    <v-card class="Tweet">
        <div class="User">
            <v-avatar class="Avatar">
                <v-img :src="'https://api.dicebear.com/8.x/pixel-art/svg?seed=test'"></v-img>
            </v-avatar>
            <v-card-text class="Username">Nickname</v-card-text>
            <v-card-text class="TweetedAt">27/03/2024 16:35:08</v-card-text>
        </div>
        <v-card-text class="tweetContent">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</v-card-text>
        <div class="Interaction">
            <v-icon color="grey">mdi-comment</v-icon>
            <v-icon color="red">mdi-heart</v-icon>
            <p class="likeCount">28</p>
            <v-icon color="grey">mdi-heart-outline</v-icon>
            <v-icon color="blue">mdi-delete</v-icon>
        </div>
    </v-card>
</template>
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
        justify-content: space-evenly;
        margin-bottom: 8px;
    }
    .likeCount{
        color: white;
    }
</style>
