<template>
  <router-link :to="{name: 'session', params: {id: this.session.sessionId}}">
    <div class="sessionContainer">
        <h3>{{ session.child.firstName }} {{ session.child.lastName }}</h3>
        <p>Rate: ${{ session.child.ratePerHour }}</p>
        <p>Drop off: {{ formatTime(session.dropOff) }}</p>
        <p>Pick up: {{ formatTime(session.pickUp) }}</p>
    </div>
  </router-link>
</template>

<script>
import moment from 'moment'

export default {
  name: 'session-container',
  props: {
    session: Object
  },
  methods: {
    formatTime(time) {
      if (time === null) {
        return 'N/A'
      }

      const date = new Date(time)
      return moment(date).format("llll");
    },
  }
}
</script>

<style>
.sessionContainer {
  border: rgb(58, 81, 102) 2px solid;
  border-radius: 20px;
  margin-bottom: 20px;
}

.sessionContainer:hover {
  background-color: rgb(37, 57, 73);
  color: rgb(72, 223, 185);
}
</style>