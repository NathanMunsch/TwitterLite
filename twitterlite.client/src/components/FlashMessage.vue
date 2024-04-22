<script setup>
    import { ref } from 'vue';

    const props = defineProps({
        content: String,
        timeout: Number
    });

    const snackbar = ref(true);
</script>

<template>
    <div class="text-center">
        <v-snackbar v-model="snackbar"
                    :timeout="5000">
            {{ props.content }}

            <template v-slot:actions>
                <v-btn color="blue"
                       variant="text"
                       @click="snackbar = false">
                    Close
                </v-btn>
            </template>
        </v-snackbar>
    </div>
</template>

<style scoped></style>
