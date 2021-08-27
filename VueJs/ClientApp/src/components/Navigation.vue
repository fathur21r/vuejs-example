<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
  <div class="container-fluid">
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarTogglerDemo01" aria-controls="navbarTogglerDemo01" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarTogglerDemo01">
      <router-link to="/" class="navbar-brand">{{ $app }}</router-link>
      <ul class="navbar-nav nav nav-pills nav-fill me-auto mb-2 mb-lg-0">
        <li class="nav-item" v-for="m in menu" :key="m.id">
          <router-link :to="m.url" class="nav-link" aria-current="page">{{m.menuName}}</router-link>
        </li>
      </ul>
    </div>
  </div>
</nav>
</template>

<script>

export default {
  components: {
  },
  data: function () {
    return {
      menu: []
    };
  },
  created: function () {
    this.$axios.get("/api/Router/Menu")
      .then((res) => {
          if (res.data.status === "OK") {
              this.menu = res.data.data
          }
      })
      .catch((error) => {
          console.log(error);
      });
  },
  methods: {},
}
</script>
<style>
  .router-link-active{
    background-color: #00B8D4 !important;
  }
</style