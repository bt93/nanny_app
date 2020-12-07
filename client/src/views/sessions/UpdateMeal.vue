<template>
  <div class="updateMeal">
        <form @submit.prevent="updateMeal">
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

export default {
    data() {
        return {
            meal: {}
        }
    },
    created() {
        sessionService.getMealById(this.$route.params.sessionId, this.$route.params.mealId)
        .then(res => {
            if (res.status == 200) {
                this.meal = res.data;
            }
        })
        .catch(err => {
            if (err) {
                console.log(err);
            }
        });
    },
    methods: {
        updateMeal() {
            sessionService.updateMeal(this.meal)
                .then(res => {
                    if (res.status == 201) {
                        this.$router.push({name: 'session', params: {id: this.meal.sessionId}});
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