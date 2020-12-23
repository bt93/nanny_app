<template >
  <v-app class="mb-0">
    <nav-bar />
    <v-main class="grey ligten-3 mb-0">
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