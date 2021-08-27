import '@fortawesome/fontawesome-free/css/all.min.css'
import 'bootstrap-css-only/css/bootstrap.min.css'
import 'mdbvue/lib/css/mdb.min.css'
import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'

import axios from './plugins/axios'
import component from './plugins/component'

Vue.config.productionTip = false
Vue.use(axios)
Vue.use(component)
Vue.prototype.$app = 'Blog'

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
