<script setup>
    import { ref } from 'vue';

    var tweetContent = ref('');

    function sendTweet() {

    }
</script>

<template>
    <v-card style="width: 50%;" class="mx-auto"     theme="dark" color="grey">
        <v-card-text>Ecrivez un tweet...</v-card-text>
        <v-textarea v-model="tweetContent" counter auto-grow bg-color="white" variant="solo" style="width: 75%; margin: auto;"></v-textarea>
        <v-btn @click="sendTweet" color="blue" style="margin: 10px;">Envoyer</v-btn>
    </v-card>
</template>

<style scoped></style>