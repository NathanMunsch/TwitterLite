import './assets/main.css'

import { createApp } from 'vue';
import App from './App.vue';
import { createRouter, createWebHistory } from 'vue-router';
import router from './router';
import store from './store'; // Importez votre store Vuex ici

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import { aliases, mdi } from 'vuetify/iconsets/mdi'

const vuetify = createVuetify({
    components,
    directives,
    icons: {
        defaultSet: 'mdi',
        aliases,
        sets: {
            mdi,
        },
    },
})

const app = createApp(App);

app.use(router);
app.use(store); // Ajoutez le store à votre application
app.use(vuetify);

app.mount('#app');