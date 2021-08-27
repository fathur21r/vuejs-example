<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <HelloWorld :msg="msg"/>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'

export default {
  name: "Home",
  components: {
    HelloWorld,
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
