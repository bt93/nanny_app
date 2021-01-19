<template>
  <v-container>
      <v-row v-if="isLoading" justify="center">
          <v-progress-circular 
            indeterminate
            color="primary"
          />
      </v-row>
      <error v-else-if="error"/>
      <v-container v-else>
          <v-card class="mx-12 pa-12">
            <v-row>
                <v-col>
                    <h2>{{ session.child.firstName }} {{ session.child.lastName }}</h2>
                    <p>Gender: {{ getGender }} - DOB: {{ formatDOB }}</p>
                    <h3>Drop Off</h3>
                    <p>Notes: {{ (session.notes) ? session.notes : 'N/A' }}</p>
                </v-col>
                <v-col>
                <v-img
                        :src="session.child.imageUrl"
                        :alt="session.child.firstName"
                        max-width="20vh"
                        v-if="session.child.imageUrl"
                    />
                    <v-avatar
                        size="90"
                        color="primary"
                        v-else
                    >
                    <v-icon dark large>
                        mdi-account-circle
                    </v-icon>
                    </v-avatar> 
                </v-col>  
            </v-row>

            
            <v-spacer />
            <v-btn :to="{name: 'updateSession'}">Update Session</v-btn>
          </v-card>
          
          <session-info :session="session" />
      </v-container>
  </v-container>
</template>

<script>
import sessionService from '@/services/SessionService'
import Error from '@/components/Error'
import moment from 'moment'
import SessionInfo from '../../components/SessionInfo.vue'

export default {
    components: {
        Error,
        SessionInfo
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
    created() {
    sessionService.getSessionById(this.$route.params.id)
        .then(res => {
            if (res.status === 200) {
                this.isLoading = false;
                this.session = res.data;
            }
        })
        .catch(err => {
            console.log(err);
            this.isLoading = false;
            this.error = true;
        });
    },
    methods: {
        formatTime(time) {
            let date = new Date(time);

            return moment(date).format('LLL');
        }
    }
}
</script>

<style>

</style>