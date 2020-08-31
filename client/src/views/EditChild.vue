<template>
  <div class="editChild">
      <img src="../images/loading.gif" alt="loading..." v-if="isLoading">
      <error v-else-if="error"/>
      <div class="childInfo text-center" v-else>
          <h1>Edit Child</h1>
          <img v-if="child.imageUrl !== ''" :src="child.imageUrl" :alt="child.firstName">
          <form @submit.prevent="updateChild">
                <label for="firstName">First Name: </label>
                <input type="text" name="firstName" id="firstName" v-model="child.firstName">
                <label for="lastName">Last Name: </label>
                <input type="text" name="lastName" id="lastName" v-model="child.lastName">
                <label for="gender">Gender: </label>
                <select name="gender" id="gender" v-model="child.gender">
                    <option value="" disabled selected="selected">Choose One</option>
                    <option value="F">Female</option>
                    <option value="M">Male</option>
                    <option value="N">Non-Binary</option>
                    <option value="O">Other</option>
                </select>
                <label for="dateOfBirth">Date of Birth: </label>
                <input type="date" name="dateOfBirth" id="dateOfBirth" v-model="child.dateOfBirth">
                <label for="ratePerHour">Rate Per Hour: </label>
                <input type="number" name="ratePerHour" id="ratePerHour" min="0" step="any" v-model="child.ratePerHour">
                <label for="needsDiapers">Needs Diapers? </label>
                <select name="needsDiapers" id="needsDiapers" v-model="child.needsDiapers">
                    <option :value="true">Yes</option>
                    <option :value="false">No</option>
                </select>
                <input type="submit" value="Update">
          </form>
      </div>
  </div>
</template>

<script>
import childrenService from '../services/ChildrenService'
import Error from '../components/Error'
import moment from 'moment'

export default {
    components: {
        Error
    },
    data() {
        return {
            child: {},
            isLoading: true,
            error: false
        }
    },
    methods: {
        updateChild() {
            this.child.ratePerHour = Number(this.child.ratePerHour);

            childrenService.updateChild(this.child)
                .then(res => {
                    if (res.status === 201) {
                        this.$router.push({name: 'dashboard'});
                    }
                })
                .catch(err => {
                    if (err) {
                        this.error = true;
                    }
                });
        }
    },
    created() {
        childrenService.getChildById(this.$route.params.id)
            .then(res => {
                if (res.status === 200) {
                    this.isLoading = false;
                    this.child = res.data;
                    let day = new Date(this.child.dateOfBirth);
                    this.child.dateOfBirth = moment(day).format("YYYY-MM-DD");
                }
            })
            .catch(err => {
                if (err) {
                    this.isLoading = false;
                    this.error = true;
                } 
            });
    }
}
</script>

<style>

</style>