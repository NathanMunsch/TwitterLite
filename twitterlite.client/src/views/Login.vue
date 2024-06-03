<script setup>
    import { useField, useForm } from 'vee-validate'
    import { useRouter } from 'vue-router';
    import { ref } from 'vue';
    import FlashMessage from '../components/FlashMessage.vue';
    import { useStore } from 'vuex';

    const store = useStore();

    const { handleSubmit } = useForm({
        validationSchema: {
            username(value) {
                if (value?.length == null || value?.length < 3 || value?.length > 32) {
                    return 'Must be between 3 and 32 characters in length.';
                }

                if (!/^[a-zA-Z0-9]+$/.test(value)) {
                    return 'Must contain only alphanumeric characters.';
                }

                return true;
            },
            password(value) {
                if (!/.{12,32}/.test(value)) {
                    return 'Must be between 12 and 32 characters in length.';
                }

                if (!/(?=.*[a-z])/.test(value)) {
                    return 'Must contain at least one lowercase letter.';
                }

                if (!/(?=.*[A-Z])/.test(value)) {
                    return 'Must contain at least one uppercase letter.';
                }

                if (!/(?=.*[0-9])/.test(value)) {
                    return 'Must contain at least one digit.';
                }

                if (!/[*.!@$%^&(){}[\]:;<>,.?/~_+\-=|\\]/.test(value)) {
                    return 'Must contain at least one special character among *.!@$%^&(){}[]:;<>,.?/~_+-=|\.';
                }

                return true;
            },
        },
    })

    const username = useField('username')
    const password = useField('password')
    const router = useRouter();

    const showFlashMessage = ref(false);

    const submit = handleSubmit(values => {
        showFlashMessage.value = false;
        store.dispatch('auth/login', { username: values.username, password: values.password }).then((response) => {
            router.push('/');
        }).catch(error => {
            showFlashMessage.value = true;
        });        
    })
</script>

<template>
    <FlashMessage v-if="showFlashMessage" content="Login failed."></FlashMessage>
    <div>
        <v-img class="centered-image" :width="500" aspect-ratio="16/9" cover src="/src/images/logo.png" />

        <form @submit.prevent="submit" id="loginForm">
            <v-text-field style="color:lightgrey;" v-model="username.value.value" :error-messages="username.errorMessage.value" label="Username"></v-text-field>

            <v-text-field style="color:lightgrey;" v-model="password.value.value" type="password" :error-messages="password.errorMessage.value" label="Password"></v-text-field>

            <v-btn class="me-4" type="submit">
                Login
            </v-btn>
        </form>

        <div id="createAccount">
            <p style="color:lightgrey;">Don't have an account ? </p>
            <router-link to="/register">Register</router-link>
        </div>

    </div>
</template>

<style>
    .centered-image {
        display: block;
        margin: auto;
        margin-top: 5%;
        margin-bottom: 5%;
    }

    #loginForm {
        margin: auto;
        width: 50%;
    }

        #loginForm button {
            width: 100%;
            background-color: #3780FF;
            font-weight: bold;
            margin-bottom: 10px
        }

    #createAccount {
        display: flex;
        justify-content: center;
        flex-direction: row;
    }

        #createAccount p {
            margin-right: 5px;
            font-weight: bold;
        }
</style>
