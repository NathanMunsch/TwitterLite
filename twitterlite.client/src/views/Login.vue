<script setup>
    import { useField, useForm } from 'vee-validate'
    import { useRouter } from 'vue-router';
    import { ref } from 'vue';
    import FlashMessage from "../components/FlashMessage.vue";

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
    const showSnackbar = ref(false);

    const submit = handleSubmit(values => {
        showSnackbar.value = false;
        fetch('https:localhost:7078/auth/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            body: JSON.stringify({
                'username': values.username,
                'password': values.password
            })
        }).then(response => {
            if (response.ok) {
                router.push('/')
            } else {
                showSnackbar.value = true;
            }
        });
    })
</script>

<template>
    <FlashMessage v-if="showSnackbar" content='Login failed'></FlashMessage>
    <div>
        <div class="mainLogo">
            <img src="/src/images/logo.png" alt="LogoTwitterLite" />
        </div>

        <form @submit.prevent="submit" id="loginForm">
            <v-text-field v-model="username.value.value" :error-messages="username.errorMessage.value" label="Username"></v-text-field>

            <v-text-field v-model="password.value.value" type="password" :error-messages="password.errorMessage.value" label="Password"></v-text-field>

            <v-btn class="me-4" type="submit">
                Login
            </v-btn>
        </form>

        <div id="createAccount">
            <p>Don't have an account ? </p>
            <router-link to="/register">Register</router-link>
        </div>

    </div>
</template>

<style>
    .mainLogo {
        display: flex;
        justify-content: center;
        margin: 5%;
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
