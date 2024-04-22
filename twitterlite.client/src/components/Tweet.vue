<script setup>
    const props = defineProps({
        authorID: Number,
        content: String,
        tweetID: Number,
        createdAt: String,
        likeNumber: Number
    });
</script>
<template>
    <v-card class="Tweet">
        <div class="User">
            <v-avatar class="Avatar">
                <v-img :src="'https://api.dicebear.com/8.x/pixel-art/svg?seed=' + props.authorID"></v-img>
            </v-avatar>
            <v-card-text class="Username">{{ props.tweetID }}</v-card-text>
            <v-card-text class="TweetedAt">{{ props.createdAt }}</v-card-text>
        </div>
        <v-card-text class="tweetContent">{{ props.content }}</v-card-text>
        <div class="Interaction">
            <v-icon color="grey">mdi-comment</v-icon>
            <v-icon color="red">mdi-heart</v-icon>
            <p class="likeCount">{{ props.likeNumber }}</p>
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
