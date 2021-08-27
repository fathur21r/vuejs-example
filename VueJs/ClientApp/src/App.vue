<template>
  <div id="app">
    <Navigation/>
    <router-view/>
  </div>
</template>
<script>
// @ is an alias to /src
import Navigation from '@/components/Navigation.vue'

export default {
  name: "Home",
  components: {
    Navigation,
  },
  data: function () {
    return {
      msg: 'Welcome to Your ' + this.$app,
    };
  },
  created: function () {},
  methods: {},
}
</script>
<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: justify;
  color: #2c3e50;
}
</style>
