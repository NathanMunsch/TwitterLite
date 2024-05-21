<script setup>
import { ref, watchEffect } from 'vue';
import store from '../../store';

const dialogIsOpen = ref(false);
const props = defineProps({
    action: Number,
    // 1 = deleteUser
    userID: Number,
    confirmationDialogVisible: Boolean
});

    console.log(props.userID);

const closeDialog = () => {
    dialogIsOpen.value = false;
    emit('close');
}

const emit = defineEmits(['close']);

const deleteUser = (userId) => {
    store.dispatch('admin/deleteUser', userId);
    closeDialog();
};

watchEffect(() => {
    dialogIsOpen.value = props.confirmationDialogVisible;
});
</script>
<template>
    <v-dialog v-model="dialogIsOpen" class="tweetDialog" persistent>
        <v-card style="background-color: rgb(21,32,43);">
            <v-card-text style="color: white;" v-if="props.action == 1">Are you sure you want to delete this user ?</v-card-text>
            <v-card-actions>
                <v-btn text="Yes" @click="deleteUser(props.userID)" color="success" style="margin-right: 400px; font-size: 15px;"></v-btn>
                <v-btn text="No" @click="closeDialog" color="error" style="font-size: 15px;"></v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<style scoped>
    .tweetDialog {
        width: 600px;
    }
</style>