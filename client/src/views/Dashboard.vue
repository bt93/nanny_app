<template>
  <div>
    <div class="dashboard" v-if="!error">
      <div class="children">
        <h2>Children</h2>
        <img src="../images/loading.gif" alt="Loading" v-if="childrenLoading">
        <div v-else-if="children.length === 0">
          <p>You do not have any children in your care.</p>
          <router-link :to="{name: 'newChild'}">Add Child</router-link>
        </div>
        <div v-else>
          <router-link :to="{name: 'newChild'}">Add Child</router-link>
          <child-container v-for="child in children" :key="child.childId" :child="child" />
        </div>
      </div>
      <div class="sessions">
        <h2>Todays Sessions</h2>
        <img src="../images/loading.gif" alt="Loading" v-if="sessionsLoading">
        <div v-else-if="sessions.length === 0">
          <p>Currently no sessions.</p>
          <router-link class="main-btn" :to="{name: 'newSession'}">New Session</router-link>
        </div>
        <div v-else>
          <router-link class="main-btn" :to="{name: 'newSession'}">New Session</router-link>
          <session-container v-for="session in sessions" :key="session.sessionId" :session="session" />
        </div>
      </div>
    </div>
    <div v-else>
      <error />
    </div>
  </div>
</template>

<script>
import SessionContainer from '../components/SessionContainer'
import ChildContainer from '../components/ChildContainer'
import sessionService from '../services/SessionService'
import childrenService from '../services/ChildrenService'
import Error from '../components/Error'

export default {
  components: {
    SessionContainer,
    ChildContainer,
    Error
  },
  data() {
    return {
      sessions: [],
      children: [],
      sessionsLoading: true,
      childrenLoading: true,
      error: false
    }
  },
  created() {
    sessionService.getCurrentSessions()
      .then(res => {
        if (res.status === 200) {
          this.sessionsLoading = false;
          res.data.forEach(s => this.sessions.push(s));
        }
      }).catch(err => {
        if (err) {
          this.error = true;
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