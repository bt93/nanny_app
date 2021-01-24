<template>
  <v-container>
    <v-row >
      <v-col
        cols="12"
        lg="3"
      >
        <v-sheet
          rounded="lg"
          min-height="600"
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
          <v-row v-else-if="children.length == 0">
            <v-col>
              <p>No children found. Add one by clicking the button above.</p>
            </v-col>
          </v-row>
          <v-virtual-scroll
            v-else
            :items="children"
            :item-height="460"
            max-height="600"
            bench="1"
          >
            <template v-slot:default="{ item }">
              <v-list-item :key="item.childId" class="mb-12">
                <v-row>
                  <child-container :child="item" class="ma-auto" />
                </v-row>
                
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
              :disabled="children.length == 0"
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
          <v-row v-else-if="children.length == 0">
            <v-col>
              <p>A child must be added in order to create a session.</p>
            </v-col>
          </v-row>
          <v-row v-else-if="sessions.length == 0">
            <v-col>
              <p>No sessions found. Add one by clicking the button above.</p>
            </v-col>
          </v-row>
          <v-virtual-scroll 
            v-else
            :items="sessions"
            :item-height="340"
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
          min-height="300"
        >
          <v-card-title class="white--text primary darken-1">
            <span v-if="caretakerLoading">Welcome!</span>
            <span v-else>Welcome, {{ caretaker.firstName }} {{ caretaker.lastName }}!</span>
            <v-spacer></v-spacer>
            <v-btn
              color="white"
              class="text--primary"
              fab
              :to="{name: 'settings'}"
            >
               <v-icon
                color="black"
                large
               >mdi-plus</v-icon>
            </v-btn>
          </v-card-title>
          <v-row
            justify="center"
            v-if="caretakerLoading"
          >
            <v-progress-circular
              indeterminate
              color="primary"
              class="mx-auto"
            />
          </v-row>
          <caretaker-container :caretaker="caretaker" v-else/>
        </v-sheet>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import caretakerService from '@/services/CaretakerService'
import sessionService from '../services/SessionService'
import childrenService from '../services/ChildrenService'
import SessionContainer from '../components/SessionContainer'
import ChildContainer from '../components/ChildContainer'
import CaretakerContainer from '../components/CaretakerContainer'
//import Error from '../components/Error'

export default {
  components: {
    SessionContainer,
    ChildContainer,
    CaretakerContainer,
    //Error
  },
  data() {
    return {
      sessions: [],
      children: [],
      caretaker: {},
      sessionsLoading: true,
      childrenLoading: true,
      caretakerLoading: true,
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
    
    caretakerService.getCaretaker()
      .then(res => {
        if (res.status === 200) {
          this.caretakerLoading = false;
          this.caretaker = res.data;
        }
      })
  }
}
</script>