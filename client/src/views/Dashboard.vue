<template>
  <div class="dashboard">
    <div class="children">
      <h2>Children</h2>
    </div>
    <div class="sessions">
      <router-link :to="{name: ''}">New Session</router-link>
      <h2>Todays Sessions</h2>
      <img src="../images/loading.gif" alt="Loading" v-if="isLoading">
      <session-container v-else v-for="session in sessions" :key="session.sessionId" :session="session"/>
    </div>
  </div>
</template>

<script>
import SessionContainer from '../components/SessionContainer'
import sessionService from '../services/SessionService'

export default {
  components: {
    SessionContainer
  },
  data() {
    return {
      sessions: [],
      isLoading: true
    }
  },
  created() {
    sessionService.getCurrentSessions()
      .then(res => {
        if (res.status === 200) {
          this.isLoading = false;
          res.data.forEach(s => this.sessions.push(s));
        }
      });
  }
}
</script>

<style>
@media screen and (min-width: 500px) {
  .dashboard {
    margin: 0 -5vh;
    display: grid;
    grid-template-columns: 20vh 1fr;
  }

  .dashboard a {
    color: rgb(58, 81, 102);
    text-decoration: none;
    font-size: 20px;
  }

  .sessions {
    border: 20px black;
  }
}
</style>