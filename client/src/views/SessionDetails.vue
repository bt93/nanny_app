<template>
  <div class="details">
      <img src="../images/loading.gif" alt="Loading" v-if="isLoading">
      <error v-else-if="error"/>
      <div v-else>
          <h2>{{ this.session.child.firstName }} {{ this.session.child.lastName }}</h2>
          <p>Gender: {{ getGender }} - DOB: {{ formatDOB }}</p>
          <h3>Session Details</h3>
          <ul class="options">
              <li><router-link :to="{name: 'endSession'}">End Session</router-link></li>
              <li><router-link :to="{name: 'addNap'}">Add Nap</router-link></li>
              <li><router-link :to="{name: 'addMeal'}">Add Meal</router-link></li>
              <li><router-link :to="{name: 'addDiaper'}">Add Diaper</router-link></li>
          </ul>
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
.options {
    list-style: none;
    margin: 20px;
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
}

.options > li {
    margin: 20px;
}

.options > li > a {
    text-decoration: none;
    color: rgb(58, 81, 102);
    border: rgb(58, 81, 102) 2px solid;
    padding: 5px;
    border-radius: 20px;
}

.options > li > a:hover {
    background-color: rgb(58, 81, 102);
    color: white;
}
</style>