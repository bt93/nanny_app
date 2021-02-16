<template>
  <div>
      <error v-if="error"/>
      <div v-else>
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
  </div>
</template>

<script>
import sessionService from '@/services/SessionService'
import Error from '@/components/Error.vue'

export default {
  components: { 
      Error
    },
    data() {
        return {
            diaper: {
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
        addDiaper() {
            sessionService.addDiaper(this.diaper, this.$route.params.id)
                .then(res => {
                    if (res.status === 201) {
                        this.$router.push({name: 'session', params: {id: this.$route.params.id}});
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