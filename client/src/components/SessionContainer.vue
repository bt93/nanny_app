<template>
  <v-card class="mx-auto px-12 py-3" min-width="30vw">
    <v-card-title>{{ session.child.firstName }} {{ session.child.lastName }}</v-card-title>
    <v-card-text>Rate: ${{ session.child.ratePerHour }}</v-card-text>
    <v-card-text>Drop Off: {{ formatTime(session.dropOff) }}</v-card-text>
    <v-card-text>Drop Off: {{ formatTime(session.pickUp) }}</v-card-text>
    <v-btn
      :to="{name: 'session', params: {id: session.sessionId}}"
    >Session Details For {{ session.child.firstName }}</v-btn>
  </v-card>
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