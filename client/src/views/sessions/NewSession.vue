<template>
  <div class="newSession">
      <error v-if="error"/>
      <form @submit.prevent="submit" v-else>
        <label for="child">Child: </label>
        <div>
          <select name="child" id="child" v-model="session.childId" required>
            <option disabled selected="selected" value="">Choose One</option>
            <option v-for="child in children" :key="child.childId" :value="child.childId">{{ child.firstName }} {{ child.lastName }}</option>
          </select>
        </div>
        <label for="dropOff">Drop Off: </label>
        <div>
          <input 
          type="datetime-local" 
          name="dropOff" 
          id="dropOff" 
          v-model="session.dropOff"
          required
          >
        </div>
        <label for="notes">Notes: </label>
        <div>
          <textarea name="notes" id="notes" cols="30" rows="10" v-model="session.notes"></textarea>
        </div>
        <input type="submit">
      </form>
  </div>
</template>

<script>
import childrenService from '@/services/ChildrenService'
import sessionService from '@/services/SessionService'
import Error from '../../components/Error.vue'

export default {
  components :{ 
    Error
  },
  data() {
    return {
      session: {
        notes: ''
      },
      children: [],
      error: false
    }
  },
  methods: {
    submit() {
      sessionService.createSession(this.session)
        .then(res => {
          if (res.status === 201) {
            this.$router.push('/dashboard');
          }
        })
        .catch(err => {
          console.error(err);
          this.error = true;
        })
    }
  },
  created() {
    childrenService.getAllChildren()
      .then(res => {
        if (res.status === 200) {
          this.children = res.data
        }
      })
      .catch(err => {
        if (err) {
          this.error = true;
        }
      });
  }
}
</script>

<style>

</style>