<template>
    <v-dialog v-model="dialogIsOpen" class="filterDialog" persistent>
        <v-card style="background-color: rgb(21,32,43);">
            <v-card-actions>
                <div style="display: flex; justify-content: center; gap: 100px;">
                    <v-btn text="Newest" @click="filterNewest" style="color: lightgrey;" size="large"></v-btn>
                    <v-btn text="Oldest" @click="filterOldest" style="color: lightgrey;" size="large"></v-btn>
                    <v-btn text="Most-Liked" @click="filterMostLiked" style="color: lightgrey;" size="large"></v-btn>
                </div>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<script setup>
    import { ref, watchEffect } from 'vue';

    const dialogIsOpen = ref(false);
    const props = defineProps({
        filterDialogVisible: Boolean
    });

    const emit = defineEmits(['update:filterDialogVisible', 'update:filterMode']);
    const closeDialog = () => {
        dialogIsOpen.value = false;
        emit('update:filterDialogVisible', false);
    }

    function filterNewest() {
        closeDialog();
        emit('update:filterMode', 'newest');
    }

    function filterOldest() {
        closeDialog();
        emit('update:filterMode', 'oldest');
    }

    function filterMostLiked() {
        closeDialog();
        emit('update:filterMode', 'mostLiked');
    }

    watchEffect(() => {
        dialogIsOpen.value = props.filterDialogVisible;
    });
</script>
<style scoped>
    .filterDialog {
        width: 600px;
    }
</style>