<template>
  <div class="endSession">
      <img src="@/images/loading.gif" alt="Loding" v-if="isLoading">
      <error v-else-if="error" />
      <div v-else>
            <h2>{{ this.session.child.firstName }} {{ this.session.child.lastName }}</h2>
            <p>Gender: {{ getGender }} - DOB: {{ formatDOB }}</p>
      </div>
      <h3>Session Details</h3>
      <ul class="options">
            <li><router-link :to="{name: 'addNap'}">Add Nap</router-link></li>
            <li><router-link :to="{name: 'addMeal'}">Add Meal</router-link></li>
            <li><router-link :to="{name: 'addDiaper'}">Add Diaper</router-link></li>
      </ul>
      <form @submit.prevent="endSession">
            <label for="dropOff">Drop Off: </label>
                <div>
                <input 
                required
                type="datetime-local" 
                name="dropOff" 
                id="dropOff" 
                v-model="session.dropOff"
                >
            </div>
            <label for="pickUp">Pick Up: </label>
            <div>
                <input 
                required
                type="datetime-local"
                name="pickUp"
                id="pickUp"
                v-model="session.pickUp"
                >
            </div>
            <label for="notes">Notes: </label>
            <div>
                <textarea name="notes" id="notes" cols="30" rows="10" v-model="session.notes"></textarea>
            </div>
            <input type="submit" value="Update">
      </form>
  </div>
</template>

<script>
import sessionService from '@/services/SessionService'
import Error from '@/components/Error'
import moment from 'moment'

export default {
    components: {
        Error
    },
    data() {
        return {
            session: {},
            isLoading: true,
            error: false
        }
    },
    created() {
        sessionService.getSessionById(this.$route.params.id)
            .then(res => {
                if (res.status === 200) {
                    this.session = res.data;
                    this.isLoading = false;
                }
            })
            .catch(err => {
                if (err) {
                    this.isLoading = false;
                    this.error = true;
                }
            });
    },
    methods: {
        endSession() {
            sessionService.endSession(this.session)
                .then(res => {
                    if (res.status === 201) {
                        this.$router.push('/dashboard');
                    }
                })
                .catch(err => {
                    if (err) {
                        console.log(err);
                    }
                })
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
    }
}
</script>

<style>

</style>