import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Register from '../views/Register.vue'
import Profile from '../views/Profile.vue'
import Admin from '../views/Admin.vue'


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
        { 
            path: '/home', 
            name: 'home',
            component: Home,
            meta: { middleware: authMiddleware }  
        },
        
        { 
            path: '/', 
            redirect: {name: 'home'} 
        },
    
        { 
            path: '/login', 
            name: 'login',
            component: Login
        },
    
        { 
            path: '/register', 
            name: 'register', 
            component: Register 
        },
    
        { 
            path: '/profile', 
            name: 'profile',
            component: Profile
        },

        {
            path: '/admin',
            name: 'admin',
            component: Admin
        }
    ];

const router = createRouter({
    history: createWebHistory(),
    routes,
  });

  router.beforeEach((to, from, next) => {
    if (to.meta.middleware) {
        to.meta.middleware(to, from, next);
    } else {
        next();
    }
});

  export default router;