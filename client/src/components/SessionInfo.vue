<template>
  <div class="sessionInfo">
      <div class="naps">
          <h3 v-if="session.naps.length > 0">Naps</h3>
          <ul>
              <li v-for="nap in session.naps" :key="nap.napId" class="info">
                 <p>Start Time: {{ formatTime(nap.startTime) }}</p>
                 <p v-if="nap.endTime">End Time: {{ formatTime(nap.endTime) }}</p>
                 <p v-if="nap.notes">Notes: {{ nap.notes }}</p>
                 <router-link :to="{name: 'updateNap', params: {sessionId: session.sessionId, napId: nap.napId}}">Update Nap</router-link>
              </li>
          </ul>
      </div>
      <div class="meals">
          <h3 v-if="session.meals.length > 0">Meals</h3>
          <ul>
              <li v-for="meal in session.meals" :key="meal.mealId" class="info">
                  <p>Time: {{ formatTime(meal.time) }}</p>
                  <p>Type: {{ meal.type }}</p>
                  <p v-if="meal.notes">Notes: {{ meal.notes }}</p>
                  <router-link :to="{name: 'updateMeal', params: {sessionId: session.sessionId, mealId: meal.mealId}}">Update Meal</router-link>
              </li>
          </ul>
      </div>
      <div class="diapers" v-if="session.child.needsDiapers">
          <h3 v-if="session.diapers.length > 0">Diapers</h3>
          <ul>
              <li v-for="diaper in session.diapers" :key="diaper.diaperId" class="info">
                  <p>Time: {{ formatTime(diaper.time) }}</p>
                  <p v-if="diaper.notes">Notes: {{ diaper.notes }}</p>
                  <router-link :to="{name: 'updateDiaper', params: {sessionId: session.sessionId, diaperId: diaper.diaperId}}">Update Diaper</router-link>
              </li> 
          </ul>
      </div>
  </div>
</template>

<script>
import moment from 'moment'

export default {
    props: {
        session: Object
    },
    methods: {
        formatTime(time) {
            let date = new Date(time);

            return moment(date).format('LT');
        }
    }
}
</script>

<style>
.sessionInfo {
    display: flex;
    justify-content: space-between;
}

.info {
    padding-bottom: 20px;
    border-top: 2px solid black;
}
</style>