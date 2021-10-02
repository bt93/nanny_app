<template>
  <v-app>
    <nav-bar />
    <v-main class="grey ligten-3">
      <router-view />
    </v-main>
  </v-app>
</template>

<script>
import NavBar from './components/NavBar.vue';


export default {
  components: { NavBar },
  name: 'App',
};
</script>

<style>
div {
  margin-bottom: 0 !important;
}
</style>