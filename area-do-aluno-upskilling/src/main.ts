import './assets/main.css'
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

// Vuetify
const vuetify = createVuetify({
    components,
    directives,
  })

const app = createApp(App)
// Vuetify
app.use(vuetify)
app.use(createPinia())
app.use(router)



app.mount('#app')