import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import Vuex from 'vuex'

import store from './store'
//import env from './environments/environment';

Vue.config.productionTip = false

Vue.use(Vuex)
new Vue({
  vuetify,
  store,
  render: h => h(App)
}).$mount('#app')
