<template>
  <v-container class="details">
      <img src="@/images/loading.gif" alt="Loading" v-if="isLoading">
      <error v-else-if="error"/>
      <v-card v-else>
          <v-row justify="center" class="py-2">
             <v-btn :to="{name: 'session', params: {id: this.$route.params.id}}">Go back</v-btn> 
          </v-row>
          <v-row justify="center">
              <h2>{{ session.child.firstName }} {{ session.child.lastName }}</h2>
          </v-row>
          <v-row justify="center">
              <p>Gender: {{ getGender }} - DOB: {{ formatDOB }}</p>
          </v-row>
          <v-row justify="center">
            <h3>Session Details</h3>  
          </v-row>
          <session-links :sessionId="session.sessionId"/>
          <form @submit.prevent="updateSession">
              <div>
                  <label for="dropOff">Drop Off: </label>
                    <input 
                    type="datetime-local" 
                    name="dropOff" 
                    id="dropOff" 
                    v-model="session.dropOff"
                    >
              </div>
              <div>
                <label for="notes">Notes: </label>
                <textarea name="notes" id="notes" cols="30" rows="10" v-model="session.notes"></textarea>
              </div>
              <input type="submit" value="Update">
          </form>
      </v-card>
      <session-info :session="session" />
  </v-container>
</template>

<script>
import sessionService from '@/services/SessionService'
import Error from '@/components/Error'
import moment from 'moment'
import SessionInfo from '../../components/SessionInfo.vue'
import SessionLinks from '@/components/SessionLinks'

export default {
    name: 'session-details',
    components: {
        Error,
        SessionInfo,
        SessionLinks,
    },
    data() {
        return {
            session: {},
            error: false,
            isLoading: true
        }
    },
    computed: {
        formatDOB() {
            const date = new Date(this.session.child.dateOfBirth);
            
            return moment(date).format('LL');
        },
        getGender() {
            if (this.session.child.gender === 'F') {
                return 'Female';
            } else if (this.session.child.gender === 'M') {
                return 'Male'
            } else if (this.session.child.gender === 'N') {
                return 'Non-Binary'
            }
            
            return 'Other'
        }
    },
    methods: {
        updateSession() {
            if (this.session.pickUp === null) {
                sessionService.updateCurrentSession(this.session, this.session.sessionId)
                    .then(res => {
                        if (res.status === 201) {
                            this.$router.push({name: 'session', params: {id: this.$route.params.id}})
                        } 
                    })
                    .catch(err => {
                        console.error(err);
                        this.error = true;
                    });
            }
        }
    },
    created() {
        sessionService.getSessionById(this.$route.params.id)
            .then(res => {
                if (res.status === 200) {
                    this.isLoading = false;
                    this.session = res.data;
                }
            })
            .catch(err => {
                console.error(err);
                this.isLoading = false;
                this.error = true;
            });
    }
}
</script>

<style>

</style>