<template>
    <v-dialog v-model="dialogIsOpen" class="passwordDialog" persistent max-width="600px">
        <v-card style="background-color: rgb(21,32,43);">
            <v-text-field
                v-model="password"
                type="password"
                label="Current Password"
                :rules="[v => !!v || 'Password is required']"
                style="margin: auto; margin-top: 20px; width: 80%; color:lightgrey;"
                clearable
                counter
            ></v-text-field>
            <v-card-actions>
                <v-btn text="Cancel" @click="closeDialog" color="success"></v-btn>
                <v-btn text="Confirm" @click="confirmPassword" color="success" class="ml-auto"></v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
    <FlashMessage v-if="showFlashMessageSuccess" content="Profile successfully modified."></FlashMessage>
    <FlashMessage v-if="showFlashMessageError" content="An error occurred."></FlashMessage>
</template>

<script setup>
import { ref, watch } from 'vue';
import FlashMessage from '../FlashMessage.vue';

const dialogIsOpen = ref(false);
const password = ref('');
const showFlashMessageSuccess = ref(false);
const showFlashMessageError = ref(false);

const emit = defineEmits(['close', 'confirm']);
const props = defineProps({
    userId: {
        type: String,
        required: true,
    },
    username: {
        type: String,
        required: true,
    },
    modelValue: {
        type: Boolean,
        required: true,
    },
});

watch(() => props.modelValue, (newValue) => {
    dialogIsOpen.value = newValue;
});

watch(dialogIsOpen, (newValue) => {
    if (!newValue) emit('close');
});

const closeDialog = () => {
    dialogIsOpen.value = false;
};

const confirmPassword = () => {
    if (password.value) {
        showFlashMessageSuccess.value = true;
        emit('confirm', password.value);
        closeDialog();
    } else {
        showFlashMessageError.value = true;
    }
};
</script>

<style scoped>
.passwordDialog {
    width: 100%;
    max-width: 600px;
}
.tweet-text-field /deep/ .v-counter {
    color: white !important;
}
</style>