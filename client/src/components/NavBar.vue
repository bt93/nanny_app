<template>
  <div>
      <v-app-bar
      app
      color="rgb(72, 223, 185)"
    >
      <v-app-bar-nav-icon class="d-xl-none d-lg-none d-md-none" @click="drawer = !drawer"/>
      <v-spacer />
      <v-toolbar-items v-if="$store.state.token != ''" class="d-none d-md-flex">
        <v-btn to="/dashboard" text  active-class="teal accent-4 elevation-7">Dashboard</v-btn>
        <v-btn to="/session" text active-class="teal accent-4 elevation-7">Sessions</v-btn>
        <v-btn to="/settings" text active-class="teal accent-4 elevation-7">Settings</v-btn>
        <v-btn to="/logout" text>Logout</v-btn>
      </v-toolbar-items> 
      <v-toolbar-items v-else class="d-none d-md-flex">
        <v-btn to="/login" text  active-class="teal accent-4 elevation-7">Login</v-btn>
        <v-btn to="/register" text active-class="teal accent-4 elevation-7">Create Account</v-btn>
      </v-toolbar-items>
      <v-spacer />
    </v-app-bar>
    <v-navigation-drawer
        v-model="drawer"
        absolute
        left
        temporary>
        <v-list v-if="$store.state.token != ''">
          <v-list-item to="/dashboard">Dashboard</v-list-item>
          <v-list-item to="/session">Sessions</v-list-item>
          <v-list-item to="settings">Settings</v-list-item>
          <v-list-item to="/logout">Logout</v-list-item>
        </v-list>
        <v-list v-else>
          <v-list-item to="/login">Login</v-list-item>
          <v-list-item to="/register">Create Account</v-list-item>
        </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script>
export default {
  data() {
    return {
      drawer: false
    }
  }
}
</script>
