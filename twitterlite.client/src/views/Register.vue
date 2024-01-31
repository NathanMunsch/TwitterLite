<script setup>
    import { reactive } from 'vue';
    import { useRouter } from 'vue-router';

    const data = reactive({
        username: '',
        password: ''
    });

    const router = useRouter();

    const submit = async () => {
        await fetch('https://localhost:7078/auth/register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            body: JSON.stringify(data)
        }).then(response => {
            if (response.ok) {
                alert("User successfully created");
                router.push('/login');
            } else {
                alert("The user was not created");
            }
        });
    }
</script>

<template>
    <form @submit.prevent="submit">
        <div class='container'>
            <div class='div-1'>
                <div class='sub-div-1'>
                    <i class="fa fa-twitter icon"></i>
                </div>
            </div>
            <div class='div-2'>
                <div class='sub-div-2'>
                    <h3>Sign up for TwitterLite</h3>
                </div>
            </div>
            <div class='div-3'>
                <div class='sub-div-3'>
                    <label>Username</label><br>
                    <input v-model="data.username" type='text'>
                </div>
            </div>
            <div class='div-4'>
                <div class='sub-div-4'>
                    <label>Password</label><br>
                    <input v-model="data.password" type='password'>
                </div>
            </div>
            <div class='div-5'>
                <div class='sub-div-5'>
                    <button type="submit">Sign up</button>
                </div>
            </div>
            <div class='div-6'>
                <div class='sub-div-6'>
                    <router-link to="/login">Log in to TwitterLite</router-link>
                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    * {
        margin: 0;
        padding: 0;
        box-sizing: border-box;
    }

    .container {
        height: 100vh;
    }

    .div-1 {
        display: flex;
    }

        .div-1 > .sub-div-1 {
            flex: 1;
            display: flex;
            justify-content: center;
        }

    .sub-div-1 > .icon {
        font-size: 35px;
        color: #1194FF;
        margin: 20px 0 30px 0;
    }

    .div-2 {
        display: flex;
    }

        .div-2 > .sub-div-2 {
            flex: 1;
            display: flex;
            justify-content: center;
        }

            .div-2 > .sub-div-2 > h3 {
                font-family: Verdana;
            }

    .div-3 {
        display: flex;
        justify-content: center;
    }

        .div-3 > .sub-div-3 {
            background: #F8f8f8;
            margin: 20px 0 0 0;
            width: 55%;
            height: 50px;
            position: relative;
            border-bottom: 1.5px solid #555;
        }

    .sub-div-3 > label {
        margin-left: 7px;
        font-size: 14px;
        font-family: helvetica;
        color: #787878;
    }

    .sub-div-3 > input {
        all: unset;
        position: absolute;
        bottom: 0;
        width: 100%;
        height: 30px;
        padding: 9px 0 0 7px;
    }

    .div-4 {
        display: flex;
        justify-content: center;
    }

        .div-4 > .sub-div-4 {
            background: #f8f8f8;
            margin: 20px 0 0 0;
            width: 55%;
            height: 50px;
            position: relative;
            border-bottom: 1.5px solid #555;
        }

    .sub-div-4 > label {
        margin-left: 7px;
        font-size: 14px;
        font-family: helvetica;
        color: #787878;
    }

    .sub-div-4 > input {
        all: unset;
        position: absolute;
        bottom: 0;
        width: 100%;
        height: 30px;
        padding: 9px 0 0 7px;
    }

    .div-5 {
        display: flex;
    }

        .div-5 > .sub-div-5 {
            flex: 1;
            display: flex;
            justify-content: center;
            margin-top: 20px;
        }

            .div-5 > .sub-div-5 > button {
                all: unset;
                height: 50px;
                width: 56%;
                border-radius: 29px;
                text-align: center;
                background: #1194FF;
                opacity: 0.5;
                color: #FFF;
                font-weight: 600;
                font-family: Verdana;
            }

    .div-6 {
        display: flex;
    }

        .div-6 > .sub-div-6 {
            flex: 1;
            display: flex;
            justify-content: center;
        }

            .div-6 > .sub-div-6 > a {
                text-decoration: none;
                color: #1194FF;
                font-size: 14px;
                margin-top: 20px;
                font-family: Verdana;
            }

    @media only screen and (max-width: 480px) and (min-width: 320px) {
        .div-3 > .sub-div-3, .div-4 > .sub-div-4 {
            width: 85%;
        }

        .div-5 > .sub-div-5 > button {
            width: 85.5%;
        }
    }

    @media only screen and (max-width: 800px) and (min-width: 481px) {
        .div-3 > .sub-div-3, .div-4 > .sub-div-4 {
            width: 70%;
        }

        .div-5 > .sub-div-5 > button {
            width: 71%;
        }
    }
</style>
