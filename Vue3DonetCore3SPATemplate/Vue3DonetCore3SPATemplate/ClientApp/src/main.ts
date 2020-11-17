import { createApp } from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'

import Toast, { PluginOptions } from "vue-toastification";
import "vue-toastification/dist/index.css";

// for icon
import 'material-design-icons/iconfont/material-icons.css'

createApp(App)
    .use(store)
    .use(router)
    .use(Toast)
    .mount('#app')
