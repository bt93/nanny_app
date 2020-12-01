<template>
  <div>
      <router-link :to="{name: 'session', params: {id: $route.params.id}}">Back to Session</router-link>
      <form @submit.prevent="submitNap">
          <label for="startTime">Start Time: </label>
          <div>
              <input type="datetime-local" name="startTime" id="startTime" v-model="nap.startTime" required>
          </div>
          <label for="endTime">End Time: </label>
          <div>
              <input type="datetime-local" name="endTime" id="endTime" v-model="nap.endTime">
          </div>
          <label for="notes">Notes: </label>
          <div>
              <textarea name="notes" id="notes" cols="30" rows="10" v-model="nap.notes"></textarea>
          </div>
          <input type="submit">
      </form>
  </div>
</template>

<script>
import sessionService from '@/services/SessionService'

export default {
    data() {
        return {
            nap: {
                notes: ''
            }
        }
    },
    methods: {
        submitNap() {
            sessionService.addNap(this.nap, this.$route.params.id)
                .then(res => {
                    if (res.status === 201) {
                        this.$router.push({name: 'session', params: {id: this.$route.params.id}})
                    }
                })
                .catch(err => {
                    if (err) {
                        console.log(err);
                    }
                })
        }
    }
}
</script>

<style>

</style>