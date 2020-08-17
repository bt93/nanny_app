<template>
  <div class="dashboard">
    <div class="children">
      <h2>Children</h2>
      <img src="../images/loading.gif" alt="Loading" v-if="childrenLoading">
      <child-container v-else v-for="child in children" :key="child.childId" :child="child" />
    </div>
    <div class="sessions">
      <router-link :to="{name: 'newSession'}">New Session</router-link>
      <h2>Todays Sessions</h2>
      <img src="../images/loading.gif" alt="Loading" v-if="sessionsLoading">
      <session-container v-else v-for="session in sessions" :key="session.sessionId" :session="session" />
    </div>
  </div>
</template>

<script>
import SessionContainer from '../components/SessionContainer'
import ChildContainer from '../components/ChildContainer'
import sessionService from '../services/SessionService'
import childrenService from '../services/ChildrenService'

export default {
  components: {
    SessionContainer,
    ChildContainer
  },
  data() {
    return {
      sessions: [],
      children: [],
      sessionsLoading: true,
      childrenLoading: true
    }
  },
  created() {
    sessionService.getCurrentSessions()
      .then(res => {
        if (res.status === 200) {
          this.sessionsLoading = false;
          res.data.forEach(s => this.sessions.push(s));
        }
      });

    childrenService.getAllChildren()
      .then(res => {
        if (res.status === 200) {
          this.childrenLoading = false;
          res.data.forEach(c => this.children.push(c));
        }
      })
  }
}
</script>

<style>
@media screen and (min-width: 500px) {
  .dashboard {
    margin: 0 -8vh;
    display: grid;
    grid-template-columns: 20vh 1fr;
  }

  .dashboard a {
    color: rgb(58, 81, 102);
    text-decoration: none;
    font-size: 20px;
  }

  .sessions {
    border-left: 2px rgb(58, 81, 102) solid;
    padding-left: 20px;
  }

  .children {
    padding-right: 2px;
  }
}
</style>