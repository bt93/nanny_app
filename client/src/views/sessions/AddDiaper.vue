<template>
  <div>
      <form @submit.prevent="addDiaper">
          <label for="time">Time: </label>
          <div>
              <input type="datetime-local" name="time" id="time" v-model="diaper.time" required>
          </div>
          <label for="notes">Notes: </label>
          <div>
              <textarea name="notes" id="notes" cols="30" rows="10" v-model="diaper.notes"></textarea>
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
            diaper: {
                notes: ''
            }
        }
    },
    methods: {
        addDiaper() {
            sessionService.addDiaper(this.diaper, this.$route.params.id)
                .then(res => {
                    if (res.status === 201) {
                        this.$router.push({name: 'session', params: {id: this.$route.params.id}});
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