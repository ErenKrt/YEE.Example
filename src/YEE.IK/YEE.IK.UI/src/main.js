import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import { createPinia } from 'pinia';
import API from './utils/api'
import router from './router';


createApp(App)
.use(createPinia())
.use(router)
.use(API, { baseURL: "/api/v1/" })
.mount('#app')
