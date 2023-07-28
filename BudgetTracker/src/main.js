import '@/assets/css/main.css';

import { createApp } from 'vue';
import { createPinia } from 'pinia';
import Vue3Toastify from 'vue3-toastify';

import App from './App.vue';
import router from './router';

const app = createApp(App);

app.use(Vue3Toastify, {
    theme: 'dark',
});
app.us
app.use(createPinia());
app.use(router);

app.mount('#app');
