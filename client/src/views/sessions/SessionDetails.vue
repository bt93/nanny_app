<template>
  <div>
      <img src="@/images/loading.gif" alt="Loading" v-if="isLoading">
      <error v-else-if="error"/>
      <div v-else>
          <h2>{{ session.child.firstName }} {{ session.child.lastName }}</h2>
          <p>Gender: {{ getGender }} - DOB: {{ formatDOB }}</p>
          <p>Notes: {{ (session.notes) ? session.notes : 'N/A' }}</p>
          <h3><router-link :to="{name: 'updateSession'}">Update Session</router-link></h3>
          <session-info :session="session" />
      </div>
  </div>
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
    }
}
</script>

<style>

</style>