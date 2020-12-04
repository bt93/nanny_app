<template>
  <div>
      <session-return :prevRoute="prevRoute" />
      <form @submit.prevent="addMeal">
          <label for="time">Time: </label>
          <div>
              <input type="datetime-local" name="time" id="time" v-model="meal.time" required>
          </div>
          <label for="type">Type: </label>
          <div>
              <select name="type" id="type" v-model="meal.type" required>
                  <option disabled selected="selected" value="">Choose One</option>
                  <option value="breakfast">Breakfast</option>
                  <option value="lunch">Lunch</option>
                  <option value="dinner">Dinner</option>
                  <option value="snack">Snack</option>
                  <option value="other">Other</option>
              </select>
          </div>
          <label for="notes">Notes: </label>
          <div>
              <textarea name="notes" id="notes" cols="30" rows="10" v-model="meal.notes"></textarea>
          </div>
          <input type="submit">
      </form>
  </div>
</template>

<script>
import sessionService from '@/services/SessionService'
import SessionReturn from '../../components/SessionReturn.vue'

export default {
  components: { SessionReturn },
    data() {
        return {
            meal: {
                notes: ''
            },
            prevRoute: ''
        }
    },
    // checks to see which route is coming from and changes SessionReturn link acordingly
    beforeRouteEnter(to, from, next) {
        next(vm => {
            vm.prevRoute = from.name;
        })
    },
    methods: {
        addMeal() {
            sessionService.addMeal(this.meal, this.$route.params.id)
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