<template>
  <v-container class="details">
      <v-row v-if="isLoading" justify="center">
          <v-progress-circular 
            indeterminate
            color="primary"
          />
      </v-row>
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
          <v-row justify="center" class="my-12">
             <session-links :sessionId="session.sessionId"/> 
          </v-row>
          <v-row justify="center">
             <v-form @submit.prevent="updateSession">
                <v-row>
                   <v-text-field 
                    type="datetime-local"
                    label="Drop Off Time"
                    v-model="session.dropOff"
                    />  
                </v-row>
                <v-row>
                   <v-textarea 
                    label="notes"
                    v-model="session.notes"
                    />  
                </v-row>
                <v-row justify="center" class="mt-4 mb-12">
                    <v-btn type="submit">Update</v-btn>
                </v-row>
            </v-form> 
          </v-row>
          
      </v-card>
      <session-info v-if="!isLoading" :session="session" />
  </v-container>
</template>

<script>
import sessionService from '@/services/SessionService'
import Error from '@/components/Error'
import moment from 'moment'
import SessionInfo from '../../components/SessionInfo.vue'
import SessionLinks from '@/components/SessionLinks'

export default {
    name: 'update-session',
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