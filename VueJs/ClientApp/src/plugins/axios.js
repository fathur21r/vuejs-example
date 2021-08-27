import vue from 'vue'
import axios from 'axios'

const api_url = 'https://localhost:44387/'

const instance = axios.create({
    baseURL: api_url,
    timeout: 1000,
    headers: { 'X-Custom-Header': 'foobar' }
});


export default {
    // called by Vue.use(FirstPlugin)
    install() {
        vue.prototype.$axios = instance
    }
}