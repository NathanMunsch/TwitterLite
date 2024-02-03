<script setup>
    import { useField, useForm } from 'vee-validate'
    import { ref } from 'vue';
    import FlashMessage from '../components/FlashMessage.vue';

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

    const username = useField('username');
    const password = useField('password');

    const showFlashMessage = ref(false);
    const flashMessageContent = ref('');

    const submit = handleSubmit(values => {
        showFlashMessage.value = false;
        fetch('https://localhost:7078/auth/register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            body: JSON.stringify({
                'username': values.username,
                'password': values.password
            })
        }).then(response => {
            if (response.ok) {
                flashMessageContent.value = 'Account successfully created.';
                showFlashMessage.value = true;
            } else {
                flashMessageContent.value = 'Error, your account has not been created.';
                response.json().then(data => {
                    if (data.errorMessage != null) {
                        flashMessageContent.value += " " + data.errorMessage + ".";
                    }
                });
                showFlashMessage.value = true;
            }
        });
    })
</script>

<template>
    <FlashMessage v-if="showFlashMessage" :content="flashMessageContent"></FlashMessage>
    <div>
        <div class="mainLogo">
            <img src="/src/images/logo.png" alt="LogoTwitterLite" />
        </div>

        <form @submit.prevent="submit" id="loginForm">
            <v-text-field v-model="username.value.value" :error-messages="username.errorMessage.value" label="Username"></v-text-field>

            <v-text-field v-model="password.value.value" type="password" :error-messages="password.errorMessage.value" label="Password"></v-text-field>

            <v-btn class="me-4" type="submit">
                Register
            </v-btn>
        </form>

        <div id="createAccount">
            <p>Already have an account ? </p>
            <router-link to="/login">Login</router-link>
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
