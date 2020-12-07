<template>
  <div class="updateNape">
      <form @submit.prevent="updateNap">
          <label for="startTime">Start Time: </label>
          <div>
              <input type="datetime-local" name="startTime" id="startTime" v-model="nap.startTime">
          </div>
          <label for="endTime">End Time: </label>
          <div>
              <input type="datetime-local" name="endTime" id="endTime" v-model="nap.endTime">
          </div> 
          <label for="notes">Notes: </label>
          <div>
              <textarea name="notes" id="notes" cols="30" rows="10" v-model="nap.notes"></textarea>
          </div>
          <input type="submit" value="Update">        
      </form>
  </div>
</template>

<script>
import sessionService from '@/services/SessionService'

export default {
    data() {
        return {
            endTime: '',
            nap: {}
        }
    },
    created() {
        sessionService.getNapById(this.$route.params.sessionId, this.$route.params.napId)
            .then(res => {
                if (res.status == 200) {
                    this.nap = res.data;
                }
            })
            .catch(err => {
                console.log(err);
            })
    },
    methods: {
        updateNap() {
            sessionService.updateNap(this.nap)
            .then(res => {
                if (res.status == 201) {
                    this.$router.push({name: 'session', params: { id: this.nap.sessionId }})
                }
            })
        }
    }
}
</script>

<style>

</style>