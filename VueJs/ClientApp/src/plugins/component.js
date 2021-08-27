import Vue from 'vue'
import * as mdbvue from 'mdbvue'


export default {
    // called by Vue.use(FirstPlugin)
    install() {
        for (const component in mdbvue) {
            Vue.component(component, mdbvue[component])
          }
    }
}