<template>
  <div class="sessionInfo">
      <div class="naps">
          <h3>Naps</h3>
          <ul>
              <li v-for="nap in session.naps" :key="nap.napId">
                 <p>Start Time: {{ formatTime(nap.startTime) }}</p>
                 <p v-if="nap.endTime">End Time: {{ formatTime(nap.endTime) }}</p>
                 <p v-if="nap.notes">Notes: {{ nap.notes }}</p>
                 <router-link :to="{name: 'updateNap', params: {sessionId: session.sessionId, napId: nap.napId}}">Update Nap</router-link>
              </li>
          </ul>
      </div>
      <div class="meals">
          <h3>Meals</h3>
      </div>
      <div class="diapers">
          <h3>Diapers</h3>
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
</style>