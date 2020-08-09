<template>
  <div class="details">
      <h2>Session</h2>
      <img src="../images/loading.gif" alt="Loading" v-if="isLoading">
      <error v-else-if="error"/>
      <div v-else>
          <h3>{{ this.session.child.firstName }} {{ this.session.child.lastName }}</h3>
          <p>Gender: {{ getGender }} - DOB: {{ formatDOB }}</p>
          <h3>Session Details</h3>
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
      </div>
  </div>
</template>

<script>
import sessionService from '../services/SessionService'
import Error from '../components/Error'

export default {
    name: 'session-details',
    components: {
        Error
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
            
            return `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()}`
        },
        getGender() {
            if (this.session.child.gender === 'F') {
                return 'Female';
            } else if (this.child.gender === 'M') {
                return 'Male'
            } else if (this.child.gender === 'N') {
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
                            console.log(res)
                            this.$forceUpdate();
                        } 
                    })
                    .catch(err => {
                        console.log(err);
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
                console.log(err);
                this.isLoading = false;
                this.error = true;
            });
    }
}
</script>

<style>

</style>