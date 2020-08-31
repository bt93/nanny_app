<template>
  <div class="editChild">
      <img src="../images/loading.gif" alt="loading..." v-if="isLoading">
      <error v-else-if="error"/>
      <div class="childInfo text-center" v-else>
          <h1>Edit Child</h1>
          <form>
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
          </form>
      </div>
  </div>
</template>

<script>
import childrenService from '../services/ChildrenService'
import Error from '../components/Error'

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
    created() {
        childrenService.getChildById(this.$route.params.id)
            .then(res => {
                if (res.status === 200) {
                    this.isLoading = false;
                    this.child = res.data
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