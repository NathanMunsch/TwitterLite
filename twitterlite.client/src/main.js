import { createApp } from 'vue';
import App from './App.vue';
import { createRouter, createWebHistory } from 'vue-router';

import Home from './views/Home.vue';
import Login from './views/Login.vue';
import Register from './views/Register.vue';

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

const vuetify = createVuetify({
  components,
  directives,
})

const authMiddleware = (to, from, next) => {
    fetch('https://localhost:7078/auth/user', {
        method: 'GET',
        credentials: 'include'
    }).then(response => {
        if (response.ok) {
            return next();
        } else {
            router.push('/login');
        }
    });
};

const routes = [
    { path: '/', component: Home, meta: { middleware: authMiddleware } },
    { path: '/login', component: Login },
    { path: '/register', component: Register }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, from, next) => {
    if (to.meta.middleware) {
        to.meta.middleware(to, from, next);
    } else {
        next();
    }
});

const app = createApp(App);

app.use(router);

app.use(vuetify);

app.mount('#app');
