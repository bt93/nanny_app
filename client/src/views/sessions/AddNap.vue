<template>
  <div>
      <error  v-if="error"/>
      <div v-else>
        <session-return :prevRoute="prevRoute" />
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
  </div>
</template>

<script>
import sessionService from '@/services/SessionService'
import SessionReturn from '../../components/SessionReturn.vue'
import Error from '@/components/Error'

export default {
  components: { 
      SessionReturn,
      Error
   },
    data() {
        return {
            nap: {
                notes: ''
            },
            prevRoute: '',
            error: false
        }
    },
    // checks to see which route is coming from and changes SessionReturn link acordingly
    beforeRouteEnter(to, from, next) {
        next(vm => {
            vm.prevRoute = from.name;
        })
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