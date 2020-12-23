<template>
  <v-container>
    <v-row >
      <v-col
        cols="12"
        lg="3"
      >
        <v-sheet
          rounded="lg"
          min-height="668"
        >
          <v-card-title class="white--text primary darken-1">
            Children
            <v-spacer></v-spacer>
            <v-btn
              color="white"
              class="text--primary"
              fab
              :to="{name: 'newChild'}"
            >
               <v-icon
                color="black"
                large
               >mdi-plus</v-icon>
            </v-btn>
          </v-card-title>
          <v-row
            justify="center"
            v-if="childrenLoading"
          >
            <v-progress-circular
              indeterminate
              color="primary"
              class="mx-auto"
            />
          </v-row>
          <v-virtual-scroll
            v-else
            :items="children"
            :item-height="140"
            min-height="668"
          >
            <template v-slot:default="{ item }">
              <v-list-item :key="item.childId">
                <child-container :child="item" />
              </v-list-item>
            </template>
          </v-virtual-scroll>
        </v-sheet>
      </v-col>
      <v-col
        cols="12"
        lg="6"
      >
        <v-sheet
          min-height="75vh"
          rounded="lg"
        >
          <v-card-title class="white--text primary darken-1">
            Current Sessions
            <v-spacer></v-spacer>
            <v-btn
              color="white"
              class="text--primary"
              fab
              :to="{name: 'newSession'}"
            >
               <v-icon
                color="black"
                large
               >mdi-plus</v-icon>
            </v-btn>
          </v-card-title>
          <v-row 
            v-if="sessionsLoading"
            justify="center"
          >
            <v-progress-circular 
              indeterminate
              color="primary"
              class="mx-auto"
            />
          </v-row>
          <v-virtual-scroll 
            v-else
            :items="sessions"
            :item-height="100"
            min-height="75vh"
          >
            <template v-slot:default="{item}">
              <v-list-item :key="item.sessionId">
                <session-container :session="item" />
              </v-list-item>
            </template>
          </v-virtual-scroll>
        </v-sheet>
      </v-col>
      <v-col
        cols="12"

        lg="3"
      >
        <v-sheet
          rounded="lg"
          min-height="668"

        >
        </v-sheet>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import SessionContainer from '../components/SessionContainer'
import ChildContainer from '../components/ChildContainer'
import sessionService from '../services/SessionService'
import childrenService from '../services/ChildrenService'
//import Error from '../components/Error'

export default {
  components: {
    SessionContainer,
    ChildContainer,
    //Error
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