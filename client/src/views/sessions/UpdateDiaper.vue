<template>
  <div class="updateDiaper">
        <error v-if="error"/>
        <form @submit.prevent="updateDiaper" v-else>
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
import Error from '../../components/Error.vue'

export default {
    components: {
        Error
    },
    data() {
        return {
            diaper: {},
            error: false
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
                    console.error(err);
                    this.error = true;
                }
            })
    },
    methods: {
        updateDiaper() {
            sessionService.updateDiaper(this.diaper)
                .then(res => {
                    if (res.status == 201) {
                        this.$router.push({name: 'session', params: {id: this.diaper.sessionId}});
                    }
                })
                .catch(err => {
                    if (err) {
                        console.error(err);
                        this.error = true;
                    }
                })
        }
    }
}
</script>

<style>

</style>