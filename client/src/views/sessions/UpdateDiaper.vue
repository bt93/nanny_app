<template>
  <div class="updateDiaper">
        <form @submit.prevent="addDiaper">
          <label for="time">Time: </label>
          <div>
              <input type="datetime-local" name="time" id="time" v-model="diaper.time" required>
          </div>
          <label for="notes">Notes: </label>
          <div>
              <textarea name="notes" id="notes" cols="30" rows="10" v-model="diaper.notes"></textarea>
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
            diaper: {}
        }
    },
    created() {
        sessionService.getDiaperById(this.$route.params.sessionId, this.$route.params.diaperId)
            .then(res => {
                if (res.status == 200) {
                    this.diaper = res.data;
                }
            })
            .catch(err => {
                if (err) {
                    console.log(err);
                }
            })
    }
}
</script>

<style>

</style>