<template>
  <div>
    <v-sheet
      class="dashboard d-flex" 
    v-if="!error">
      <v-sheet elevation="2" class="children">
        <h2>Children</h2>
        <v-progress-circular 
          color="primary"
          indeterminate
          v-if="childrenLoading" />
        <v-container v-else-if="children.length === 0">
          <p>You do not have any children in your care.</p>
          <router-link :to="{name: 'newChild'}">Add Child</router-link>
        </v-container>
        <v-container v-else class="d-flex flex-column">
          <router-link :to="{name: 'newChild'}">Add Child</router-link>
          <child-container v-for="child in children" :key="child.childId" :child="child" />
        </v-container>
      </v-sheet>
      <v-container class="sessions">
        <h2>Todays Sessions</h2>
        <v-progress-circular 
          color="primary"
          indeterminate
          v-if="sessionsLoading" />
        <v-container v-else-if="children.length === 0">
          
        </v-container>
        <v-container v-else-if="sessions.length === 0">
          <p>Currently no sessions.</p>
          <router-link class="main-btn" :to="{name: 'newSession'}">New Session</router-link>
        </v-container>
        <v-container v-else>
          <router-link class="main-btn" :to="{name: 'newSession'}">New Session</router-link>
          <session-container v-for="session in sessions" :key="session.sessionId" :session="session" />
        </v-container>
      </v-container>
    </v-sheet>
    <v-container v-else>
      <error />
    </v-container>
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
          this.sessions = res.data;
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
          this.children = res.data;
        }
      })
  }
}
</script>